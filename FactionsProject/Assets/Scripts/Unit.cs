using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int maxHealth = 5; 
	public int health;
	public int faction; 

	public int attackStrength = 1;
	public float attackTime = 1f;
	public float attackDistance = 1f; 
	public bool attackDude{get; set;}  
	public float loseDistance = 10f; 

	public float speed = 4f; 

	private Transform enemy; 
	private Vector3 enemyPos; 
	private Vector3 targetPos;
    
    //needed to trigger animations
    Animator anim;
    public float direction; 

	// Use this for initialization
	void Start () {
		health = maxHealth; 
//		enemy =  GameObject.FindGameObjectWithTag("Player").transform; 
		targetPos = transform.position; 
		attackDude = false; 
        anim = GetComponent<Animator>();
	}

	void FixedUpdate () {
		rigidbody.velocity = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
    
        anim.SetFloat("speed", 0); //sets animation to idle
        transform.eulerAngles = new Vector3(270, 180, 0); //resets the rotation of the sprite
        
		if (health <= 0) {
			//TODO add to inactive list 
			gameObject.SetActive(false); 
		}
		enemy = NearestForeigner(enemy);
		if (enemy != null) {
			attackDude = true;
			if (enemyPos != enemy.position) {
				targetPos = enemy.position;
				enemyPos = enemy.position;
			}
		}

        direction = targetPos.x - transform.position.x; //defines if object is moving left or right    
        if (direction >= 0)
            transform.Rotate(0, 180, 0); //sets movement animation to right
        else 
            transform.Rotate(0, 0, 0); //sets movement animation to left

		if (targetPos != transform.position) {
			if (attackDude) {
				float dist = Vector3.Distance(transform.position, enemy.position);
				
				if (dist > loseDistance) 
					stopAttacking(); 
				else if (dist <= attackDistance) {
					targetPos = transform.position;
					if (enemy.gameObject.tag == "Player") {
						StopCoroutine("AttackUnit");
						StartCoroutine("AttackPlayer");
					} else {
						StopCoroutine("AttackPlayer"); 
                        StartCoroutine("AttackUnit");
					}
                }
				else {
					StopCoroutine("AttackUnit");
                    StopCoroutine("AttackPlayer"); 
                    targetPos = enemy.position; 
				}
			}
			transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            anim.SetFloat("speed", 1); //activates movement animation           
        }
	}

	public void Hurt (int damage, Transform attacker) {
		anim.SetTrigger("Hit");
        health -= damage; 
		targetPos = transform.position; 
		if (enemy == null) {
			enemy = attacker;
			attackDude = true; 
			if (enemy.gameObject.tag == "Player") {
				StopCoroutine("AttackUnit");
				StartCoroutine("AttackPlayer");
			} else {
				StopCoroutine("AttackPlayer"); 
				StartCoroutine("AttackUnit");
			}
            
            anim.SetTrigger("attackDude");//trigger the attack animation
		}
	}

	void stopAttacking () {
		attackDude = false;
		targetPos = transform.position;
		enemy = null; 
	}

	IEnumerator AttackPlayer () {
		for (;gameObject.activeSelf && enemy != null && enemy.GetComponent<Unit>().health > 0;) {
			anim.SetTrigger("attackDude");//trigger the attack animation
			enemy.GetComponent<PlayerActions>().Hurt(attackStrength); 
			if (gameObject.activeSelf)
				yield return new WaitForSeconds(attackTime); 
        }
    }

	IEnumerator AttackUnit () {
		for (;gameObject.activeSelf && enemy != null && enemy.GetComponent<Unit>().health > 0;) {
            anim.SetTrigger("attackDude");//trigger the attack animation
			enemy.GetComponent<Unit>().Hurt(attackStrength, transform); 
			if (gameObject.activeSelf)
				yield return new WaitForSeconds(attackTime); 
		}
	}

	/**
	 * Unit attacks nearest active enemy.
	 * Ignore inactive.
	 * Filter to units of another faction.
	 * 
	 * TODO If player attacked units more, attack player as if a member of another faction.
	 */
	Transform NearestForeigner(Transform enemy) {
		if (null == enemy || !enemy.gameObject.activeSelf) {
			enemy = null;
			GameObject[] units = GameObject.FindGameObjectsWithTag("Populus");
			float nearestDistance = float.PositiveInfinity;
			for (int u = 0; u < units.Length; u++) {
				Unit unit = units[u].GetComponent<Unit>();
				if (faction != unit.faction) {
					Transform other = units[u].transform;
					float distance = Vector3.Distance(transform.position, other.position);
					if (distance < nearestDistance) {
						enemy = other;
					}
				}
			}
		}
		return enemy;
	}
}
