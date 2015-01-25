using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class UI : MonoBehaviour {

	PlayerActions player; 
	public GameObject healthTxt;
	public GameObject messageTxt;
	Text health; 
	Text message; 

	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.Find("Canvas");
		Text[] textValue = canvas.GetComponentsInChildren<Text>();

		health = healthTxt.GetComponent<Text>(); 
		messageTxt.SetActive(false); 
		player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerActions>();

		health.text = "Health: " + player.health;
	}
	
	// Update is called once per frame
	void Update () {
		health.text = "Health: " + player.health;
		if (player.health <= 0) {
			messageTxt.SetActive(true);
			Time.timeScale = 0f; 
		}
	}
}
