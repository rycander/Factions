using UnityEngine;
using System.Collections;

public class FoodSpawner : MonoBehaviour {
	
	public int faction;
	private int spawnrate;
	public GameObject prototype;

	// Use this for initialization
	void Start () {
		SpawnUnit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnUnit() {
		Instantiate(prototype, transform.position, transform.rotation);
	}
}
