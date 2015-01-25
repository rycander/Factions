using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int maxHealth = 100; 
	public int health {get; set;}
	public int faction; 

	// Use this for initialization
	void Start () {
		health = maxHealth; 
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			//TODO add to inactive list 
			gameObject.SetActive(false); 
		}
	}
}
