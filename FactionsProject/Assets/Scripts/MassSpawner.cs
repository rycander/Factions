using UnityEngine;
using System.Collections;

public class MassSpawner : MonoBehaviour {
	public Unit redUnit;
	public Unit bluUnit;
	public Unit ylwUnit;
	public int spawnNum;
	
	// Use this for initialization
	void Start ()
	{
		SpawnUnits ();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	void SpawnUnits () {
		for (int i = 0; i < spawnNum; ++i){
			Instantiate(redUnit, randomVec3(50), transform.rotation);
			Instantiate(bluUnit, randomVec3(50), transform.rotation);
			Instantiate(ylwUnit, randomVec3(50), transform.rotation);
		}
	}

	Vector3 randomVec3 (int bounds) {
		return new Vector3(Random.Range(-bounds, bounds), 0, Random.Range(-bounds, bounds));
	}
}
