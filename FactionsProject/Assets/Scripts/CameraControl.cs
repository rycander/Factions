﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Transform player;
	public float offsetZ = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(player.position.x, transform.position.y, player.position.z - offsetZ); 
	}
}
