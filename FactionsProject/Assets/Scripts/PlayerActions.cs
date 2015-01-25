using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	private Transform enemy;
	private Vector3 enemyPos;
	private Vector3 targetPos;
	private Transform unit; 
	private bool attackDude = false;
	public float controlSpeed = 0.2f;
	public float clickSpeed = 1f; 
	public float attackTime = 1f; 
	public int attackStrength = 10; 
	public float attackDistance = 0.5f;
	public int health{get; set;}
	public int maxHealth = 100; 
	
	// Use this for initialization
	void Start() {
		targetPos = transform.position; 
		health = maxHealth; 
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100000)) { 
				if (hit.transform.tag == "Ground") {
					targetPos = ray.GetPoint(hit.distance - 0.5f);  
					attackDude = false;
					StopCoroutine("Attack");
					enemy = null;
				}
				if (hit.transform.tag == "Populus") {
					unit = hit.transform; 
					targetPos = unit.position; 
					attackDude = true; 
					enemy = hit.transform; 
					enemyPos = enemy.position;
				}
			}
		}
		
		if (attackDude && enemyPos != enemy.position) {
			targetPos = enemy.position;
			enemyPos = enemy.position;
        }
		if (targetPos != transform.position) {
			if (attackDude) {
				targetPos = unit.position; 
				float dist = Vector3.Distance(transform.position, targetPos);
				if (dist <= attackDistance) {
					targetPos = transform.position; 
					StartCoroutine("Attack");
				}
				else {
					StopCoroutine("Attack");
				}
			}
			transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * clickSpeed); 
		}
	}

	public void Hurt(int damage) {
		health -= damage; 
	}

	IEnumerator Attack() {
		for (;unit.GetComponent<Unit>().health > 0;) {
			unit.GetComponent<Unit>().Hurt(attackStrength, transform); 
			yield return new WaitForSeconds(attackTime); 
		}
	}

	void onTriggerEnter (Collider col) {

	}

	void OnCollisionEnter (Collision col)
	{

	}
}
