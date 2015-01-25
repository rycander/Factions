using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	private Transform player;
	public string state;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
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
		}
		else if ("pickup" == state) {
			// TODO
		}
	}
}
