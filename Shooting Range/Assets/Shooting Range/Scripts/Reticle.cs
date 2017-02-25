using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



public class Reticle : SingletonMonoBehaviour<Reticle> {

	public GameObject Laser;

	public float LaserAnimationDuration = 0.05f;
	public float BlowAnimationDuration = 0.8f;

	public Text ReloadText;

	private float mLaserEndTime = 0.0f;
	private float mBlowEndTime = 0.0f;

	private PlayerController mPlayerController = null;

	public AudioSource gunSound;

	public bool lastShot = true;

	// Use this for initialization
	void Start () {
		mPlayerController = PlayerController.getInstance(); 
	}

	// Update is called once per frame
	void Update () {
		GameObject Player = GameObject.Find ("Player");
		PlayerController playerScript = Player.GetComponent<PlayerController>();

		GameObject Blowback = GameObject.Find ("PM-40_Top");
		// Make Sure Reticle always faces camera
		//this.transform.LookAt(Camera.main.transform.position);

		// While the user has clicked we signal the flag to lerp the reticle color.
		if(mPlayerController != null) {
			if (mPlayerController.IsPressed == true) {
				mLaserEndTime = Time.unscaledTime + LaserAnimationDuration;
				mBlowEndTime = Time.unscaledTime + BlowAnimationDuration;
			}
		}
			

		if ((playerScript.CurrentMag != 0) || (lastShot == true)) {
			if (Time.unscaledTime < mBlowEndTime) {
				Blowback.transform.localPosition = new Vector3 (0.0894165f, -50f , 46.60154f);
			} else {
				Blowback.transform.localPosition = new Vector3 (0.0894165f, 3.147339f , 46.60154f);
			}
		} else {
			Blowback.transform.localPosition = new Vector3 (0.0894165f, -50f , 46.60154f);
		}

		// True if the animation hasn't reached the end yet.
		if ((playerScript.CurrentMag != 0) || (lastShot == true)) {
			if (Time.unscaledTime < mLaserEndTime) {
				gunSound.Play ();
				Laser.gameObject.SetActive (true);
			} else {
				Laser.gameObject.SetActive (false);
			}

			if (playerScript.CurrentMag == 0){
				lastShot = false;
			}
		} else {
			Laser.gameObject.SetActive (false);
		}
	}
}