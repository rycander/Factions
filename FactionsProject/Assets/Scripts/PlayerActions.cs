using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	private GameObject enemy;
	private GameObject target;
	private Vector3 targetPos;
	private Vector3 startPos;
	private float lerpTime = 0;
	private float startTime;
	private float t;
	private Transform unit; 
	private bool attackDude = false; 
	public float controlSpeed = 0.2f;
	public float clickSpeed = 1f; 
	public float attackTime = 1f; 
	public int attackStrength = 10; 
	public float attackDistance = 0.5f;
	
	// Use this for initialization
	void start() {
		targetPos = new Vector3 (0, -0.1f, 0); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			startPos = transform.position; 
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100000)) { 
				if (hit.transform.tag == "Ground") {
					targetPos = ray.GetPoint(0);  
					attackDude = false;
					StopCoroutine("Attack"); 
				}
				if (hit.transform.tag == "Populus") {
					//TODO get move position to populi 
					unit = hit.transform; 
					targetPos = unit.position; 
					attackDude = true; 
				}
				targetPos.y = 0;
				lerpTime = Vector3.Distance(startPos, targetPos) / clickSpeed; 
				startTime = Time.time;
			}
		}

		if (targetPos.y < 0) {
			transform.Translate(controlSpeed * Input.GetAxis ("Horizontal"),
			                    controlSpeed * Input.GetAxis ("Vertical"),
			                    0);
		}
		else if (lerpTime > 0) {
			t = (Time.time - startTime) / lerpTime; 
			transform.position = Vector3.Lerp(startPos, targetPos, t); 
			if (attackDude) {
				float dist = Vector3.Distance(transform.position, targetPos);
				if (dist <= attackDistance) {
					t = 1; 
					StartCoroutine("Attack");
				}
			}
			if (t >= 1) {
				targetPos = new Vector3 (0, -0.1f, 0); 
				lerpTime = 0; 
			}
		}
	}

	void LateUpdate () {
		if (attackDude) {

		}
	}

	IEnumerator Attack() {
		for (;unit.GetComponent<Unit>().health > 0;) {
			unit.GetComponent<Unit>().health -= attackStrength; 
			yield return new WaitForSeconds(attackTime); 
		}
	}

	void MayAttack (GameObject target) {
		if ("Square" == target.name) {
			enemy = target;
			Debug.Log("MayAttack " + enemy.ToString ());
		}
	}

	void onTriggerEnter (Collider col) {

	}

	void OnCollisionEnter (Collision col)
	{
		Debug.Log("onCollisionEnter " + col.gameObject.ToString ());
		if(null != enemy && enemy == col.gameObject)
		{
			enemy = null;
			Debug.Log("   attack " + col.gameObject.ToString ());
			Destroy(col.gameObject);
		}
	}
}
