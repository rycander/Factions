using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public int faction;
	private int spawnrate;
	public Unit prototype;

	// Use this for initialization
	void Start ()
	{
		SpawnUnit ();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	public void SpawnUnit () {
		Unit clone = (Unit)Instantiate(prototype, transform.position, transform.rotation);
	}
}