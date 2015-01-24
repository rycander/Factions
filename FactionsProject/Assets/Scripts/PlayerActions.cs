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
	public float controlSpeed = 0.2f;
	public float clickSpeed = 1f; 
	public float attackTime = 1f; 
	
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
<<<<<<< HEAD
				if (hit.transform.tag == "Ground") {
					targetPos = ray.GetPoint(0); 
					targetPos.y = 0;
					lerpTime = Vector3.Distance(startPos, targetPos) / clickSpeed; 
					startTime = Time.time; 
				}
				if (hit.transform.tag == "Populus") {
					//TODO get move position to populi 
				}
=======
				targetPos = ray.GetPoint(0); 
				targetPos.y = 0;
//				transform.position = targetPos; 
				lerpTime = Vector3.Distance(startPos, targetPos) / clickSpeed; 
				startTime = Time.time;
				// Debug.Log("hit " + hit.collider.gameObject.name.ToString ());
				target = hit.collider.gameObject;
				MayAttack (target);
>>>>>>> 75bfbc9b2e59ecc243ef97a2ddd223f8169a8740
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
			if (t >= 1) {
				targetPos = new Vector3 (0, -0.1f, 0); 
				lerpTime = 0; 
			}
		}
	}
<<<<<<< HEAD

	IEnumerator Attack() {
	//TODO Attack stuff
		yield return new WaitForSeconds(attackTime); 
	}
}
=======
>>>>>>> 75bfbc9b2e59ecc243ef97a2ddd223f8169a8740

	void MayAttack (GameObject target) {
		if ("Square" == target.name) {
			enemy = target;
			Debug.Log("MayAttack " + enemy.ToString ());
		}
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
