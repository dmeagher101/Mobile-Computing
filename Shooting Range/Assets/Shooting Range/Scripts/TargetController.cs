using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : SingletonMonoBehaviour<TargetController> 
{
	public Vector3 StartPosition;

	public float startSpeed;
	public float speedIncrement;

	public Text lifeText;

	private float currentSpeed;

	void Start ()
	{
		currentSpeed = startSpeed;
		GameObject Player = GameObject.Find ("Player");
		PlayerController playerScript = Player.GetComponent<PlayerController>();
		lifeText.text = "Lives Left: " + playerScript.currentLives.ToString ();

		this.transform.position = StartPosition;
	}


	// Update is called once per frame
	void Update () 
	{
		GameObject Player = GameObject.Find ("Player");
		PlayerController playerScript = Player.GetComponent<PlayerController>();

		if (playerScript.PlayerDead == false) {
			this.transform.position += (new Vector3 (0, 0, -currentSpeed) * Time.deltaTime);
		}

		if (this.transform.position.z < 3) {
			this.GetComponent<Renderer> ().enabled = false;
			if (playerScript.currentLives > 0) {
				playerScript.currentLives--;
			}
			lifeText.text = "Lives Left: " + playerScript.currentLives.ToString ();
		}

		if (this.GetComponent<Renderer> ().enabled == false) {
			this.transform.position = StartPosition;
			this.GetComponent<Renderer> ().enabled = true;
			currentSpeed += speedIncrement;
		}

		if (playerScript.Restart == true)
		{
			currentSpeed = startSpeed;
			this.transform.position = StartPosition;
		}
	}
}