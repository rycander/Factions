using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int maxHealth = 100; 
	public int health {get; set;}
	public int faction; 

	public int attackStrength = 1;
	public float attackTime = 1f;
	public float attackDistance = 1f; 
	public bool attackDude = false; 

	public float speed = 4f; 

	private Transform enemy; 
	private Vector3 enemyPos; 
	private Vector3 targetPos;

	// Use this for initialization
	void Start () {
		health = maxHealth; 
//		enemy =  GameObject.FindGameObjectWithTag("Player").transform; 
		targetPos = transform.position; 
//		attackDude = true; 
	}
	
	// Update is called once per frame
	void Update () {
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

		if (targetPos != transform.position) {
			if (attackDude) {
				float dist = Vector3.Distance(transform.position, enemy.position);
				if (dist <= attackDistance) {
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
		}
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
