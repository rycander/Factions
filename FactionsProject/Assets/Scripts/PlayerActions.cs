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
    
    //needed to trigger animations
    Animator anim;
    public float direction;
	
	// Use this for initialization
	void Start() {
		targetPos = transform.position; 
		health = maxHealth; 
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
        anim.SetFloat("speed", 0); //sets animation to idle
        transform.eulerAngles = new Vector3(270, 180, 0); //resets the rotation of the sprite
		
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
        
        direction = targetPos.x - transform.position.x; //defines if object is moving left or right           
        if (direction >= 0)
            transform.Rotate(0, 180, 0); //sets movement animation to right
        else 
            transform.Rotate(0, 0, 0); //sets movement animation to left
    
		if (targetPos != transform.position) {
			if (attackDude) {
				targetPos = unit.position; 
				float dist = Vector3.Distance(transform.position, targetPos);
				if (dist <= attackDistance) {
					targetPos = transform.position;           
					StartCoroutine("Attack"); 
                    anim.SetTrigger("attackDude"); //trigger the attack animation
                    
				}
				else {
					StopCoroutine("Attack");
				}
			}
			transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * clickSpeed);
            anim.SetFloat("speed", 1); //activates movement animation
           
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
