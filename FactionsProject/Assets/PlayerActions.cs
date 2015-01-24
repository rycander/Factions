using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {
	
	public Vector3 targetPos;
	public float speed;
	
	// Use this for initialization
	void start() {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100000)) { 
				targetPos = ray.GetPoint(0); 
				targetPos.z = 0;
				transform.position = targetPos; 
			}
		}

		if (targetPos.z < 0) {
			transform.Translate(speed * Input.GetAxis ("Horizontal"),
			                    speed * Input.GetAxis ("Vertical"),
			                    0);
		} else {

		}
	}
}

