using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int maxHealth = 100; 
	public int health {get; set;}
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
	
	// Update is called once per frame
	void Update () {
    
        anim.SetFloat("speed", 0); //sets animation to idle
        transform.eulerAngles = new Vector3(270, 180, 0); //resets the rotation of the sprite
        
		if (health <= 0) {
			//TODO add to inactive list 
			gameObject.SetActive(false); 
		}
		if (enemy != null) {
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
            //trigger the attack animation
            anim.SetTrigger("attackDude");
		}
	}

	void stopAttacking () {
		attackDude = false;
		targetPos = transform.position;
		enemy = null; 
	}

	IEnumerator AttackPlayer () {
		for (;enemy.GetComponent<PlayerActions>().health > 0;) {
			enemy.GetComponent<PlayerActions>().Hurt(attackStrength); 
			yield return new WaitForSeconds(attackTime); 
        }
    }
	IEnumerator AttackUnit () {
		for (;enemy.GetComponent<Unit>().health > 0;) {
			enemy.GetComponent<Unit>().Hurt(attackStrength, transform); 
			yield return new WaitForSeconds(attackTime); 
		}
	}
}
