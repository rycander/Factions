using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	private Transform player;
	public string state;
	private Collider[] colliders;
	private bool isFirst = true;
	private Spawn spawn;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		spawn = (Spawn)GameObject.FindGameObjectWithTag("Spawn").transform.GetComponent<Spawn>();
		state = "ground";
	}
	
	// Update is called once per frame
	void Update () {
		if ("ground" == state) {
			float distance = Vector3.Distance(transform.position, player.position);
			if (distance <= 1) {
				state = "pickup";
				Debug.Log ("Food.update: pickup");
			}
			else {

				AttractUnit ();
			}
		}
		else if ("pickup" == state) {
			if (isFirst) {
				isFirst = false;
				Debug.Log ("Food.update: is picked up");
			}
			transform.position = new Vector3(player.position.x, transform.position.y, player.position.z); 
			float distance = Vector3.Distance(transform.position, spawn.transform.position);
			if (distance <= 1) {
				spawn.SpawnUnit();
				gameObject.SetActive (false);
			}
		}
	}

	void AttractUnit() {
		float radius = 10.0f;
		colliders = Physics.OverlapSphere(transform.position, radius);		
		for (var i = 0; i < colliders.Length; i++) {
			Collider col = colliders[i];
			if ("Populus" == col.transform.tag) {
				float speed = 1f;  // TODO use unit speed
				float step = speed * Time.deltaTime;
				col.transform.position = Vector3.MoveTowards(col.transform.position, transform.position, step);
			}
		}
	}
}
