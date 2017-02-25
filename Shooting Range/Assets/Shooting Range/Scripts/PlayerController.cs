using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : SingletonMonoBehaviour<PlayerController> {	
	public bool IsPressed = false;

	public Text ReloadText;

	public GameObject ReloadPlane;

	public Text WarningText;

	public int MagSize;

	public int CurrentMag;

	public float speed = 50;

	public Color startColor;
	public Color highlightColor;

	public GameObject RestartButton;
	public GameObject StartButton;

	public int startLives;

	public int currentLives;

	public bool PlayerDead;

	public Text GameOverText;

	public bool Restart;
	public Text lifeText;

	public Text pointsText;
	public float currentPoints;

	public AudioSource reloadSound;

	public GameObject objHitByRay;
	public string objHitTag;


	public SpriteRenderer HitmarkerRenderer;
	public float HitAnimationDuration;
	private float mHitEndTime = 0.0f;
	public AudioSource hitSound;


	public Text newScore;
	public float newScoreAnimationDuration;
	private float mNewScoreEndTime = 0.0f;
	public float newPoints;

	// Use this for initialization
	void Start () {
		newScore.text = "";
		currentLives = startLives;

		RestartButton.gameObject.GetComponent<Renderer> ().material.color = startColor;
		StartButton.gameObject.GetComponent<Renderer> ().material.color = startColor;

		CurrentMag = MagSize;
		SetMag ();

		WarningText.text = "";
		GameOverText.text = "";
		pointsText.text = "0";
		currentPoints = 0;
	}

	// Update is called once per frame
	void Update () {

		GameObject Reticle = GameObject.Find ("Reticle");
		Reticle reticleScript = Reticle.GetComponent<Reticle>();
		//----------------
		// INPUT MANAGER
		//----------------

		IsPressed = false;
		detectInputEvents();

		if (Input.GetKey("right")){
			transform.Rotate(0, speed*Time.deltaTime, 0);
		}
		if (Input.GetKey("left")){ 
			transform.Rotate(0, -speed*Time.deltaTime, 0);
		}

		//-------------------
		// PLAYER CONTROLLER
		//-------------------
		if (IsPressed == true) {
			mHitEndTime = Time.unscaledTime + HitAnimationDuration;
			mNewScoreEndTime = Time.unscaledTime + newScoreAnimationDuration;
		}

		objHitByRay = Raycaster.getInstance ().getObjectHitByRay ();
		objHitTag = objHitByRay.tag;

		// Reload
		if ((objHitByRay != null) && (objHitByRay == ReloadPlane)) {
			if (CurrentMag != MagSize){
				reloadSound.Play ();
				CurrentMag = MagSize;
				SetMag ();
				reticleScript.lastShot = true;
			}
		} 


		// Highlight Button
		if ((objHitByRay != null) && (objHitByRay == RestartButton)) {
			RestartButton.gameObject.GetComponent<Renderer> ().material.color = highlightColor;
		} else {
			RestartButton.gameObject.GetComponent<Renderer> ().material.color = startColor;
		}

		if ((objHitByRay != null) && (objHitByRay == StartButton)) {
			StartButton.gameObject.GetComponent<Renderer> ().material.color = highlightColor;
		} else {
			StartButton.gameObject.GetComponent<Renderer> ().material.color = startColor;
		}

		// Send Reload Warning
		if (CurrentMag == 0) {
			WarningText.text = "Look Down to Reload!";
		} else {
			WarningText.text = "";
		}

		// Change Mag amount to red when low
		if (CurrentMag < 4) {
			ReloadText.color = new Color (255, 0, 0);
		} else {
			ReloadText.color = new Color (255, 255, 255);
		}
			
		Restart = false;



		// Hit Targets
		if (Raycaster.getInstance ().anythingHitByRay ()) {
			if (checkForClick ()) {
				if (CurrentMag != 0) {
					if (PlayerDead == false) {
						CurrentMag--;
						SetMag ();
						if (objHitByRay != null) {
							if (objHitTag == "Target") { 
								newPoints = (Mathf.Round(((objHitByRay.transform.position.z) / 35) * 100) * 10);
								currentPoints += newPoints;
								pointsText.text = currentPoints.ToString (); 

								objHitByRay.GetComponent<Renderer> ().enabled = false;


								hitSound.Play ();
								if (Time.unscaledTime < mHitEndTime) {
									HitmarkerRenderer.enabled = true;
								} 
									

								if (Time.unscaledTime < mNewScoreEndTime) {
									newScore.text = "+ " + newPoints.ToString ();
								} 
							}
						}
					}
				}
				if (objHitByRay != null) {
					if (objHitByRay == RestartButton) {
						RestartGame();
					}
					if (objHitByRay == StartButton) {
						RestartGame();
					}
				}
			}
		}

		if (Time.unscaledTime > mHitEndTime) {
			HitmarkerRenderer.enabled = false;
		} 

		if (Time.unscaledTime > mNewScoreEndTime) {
			newScore.text = "";
		} 

		if (currentLives < 1) {
			PlayerDead = true;
			GameOverText.text = "GAME OVER!";
			WarningText.text = "";
			RestartButton.gameObject.SetActive (true);
		}
	}

	// Called every frame from update to check for input events.
	void detectInputEvents() {
		if(Input.GetKeyDown (KeyCode.Mouse0) || Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown (KeyCode.JoystickButton0) || Input.GetKeyDown("space")) {
			IsPressed = true;
		}
	}

	// Returns true if a main left click, or main joystick event happened. Otherwise, it returns false if no
	// input events were detected.
	public bool checkForClick() {
		return IsPressed;
	}

	void SetMag(){
		ReloadText.text = CurrentMag.ToString();
	}

	void RestartGame(){
		Restart = true;
		PlayerDead = false;
		CurrentMag = MagSize;
		currentLives = startLives;
		lifeText.text = "Lives Left: " + currentLives.ToString ();
		SetMag ();
		RestartButton.gameObject.SetActive (false);
		StartButton.gameObject.SetActive (false);
		GameOverText.text = "";

		currentPoints = 0;
		pointsText.text = currentPoints.ToString ();
	}
}