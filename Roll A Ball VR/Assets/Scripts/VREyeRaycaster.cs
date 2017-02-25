using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]


public class VREyeRaycaster : MonoBehaviour {

	public Text countText, winText;
	public GameObject Pickups, button, InputManager;
	public AudioClip ringGet;
	AudioSource sound;

	private int count, numPickups;


	void Start() {
		count = 0;
		SetCountText();
		winText.text = "";
		Transform[] children;
		children = Pickups.GetComponentsInChildren<Transform>();
		numPickups = children.Length;
		button.GetComponent<Renderer>().material.color = Color.red;
	}

	// Use this for initialization
	void FixedUpdate()
	{
		Vector3 fwd = transform.TransformDirection(Vector3.forward);

		RaycastHit hit;

		if(Physics.Raycast(transform.position, fwd, out hit, 50f)) {
			GameObject other = hit.collider.gameObject;
			if(other.CompareTag("Pickup")) {
				other.gameObject.SetActive (false);
				count++;
				SetCountText();
				GetComponent<AudioSource>().PlayOneShot(ringGet, 1f);
			}

			if(other.CompareTag("Button")) {
				if(InputManager.GetComponent<InputManager>().IsPressed == true) {
					if(button.GetComponent<Renderer>().material.color == Color.red) {
						button.GetComponent<Renderer>().material.color = Color.blue;
					}

					else {
						button.GetComponent<Renderer>().material.color = Color.red;
					}
				}
			}
		}
	}

	void SetCountText ()
	 {
			 countText.text = "Count: " + count.ToString ();
			 if (count >= numPickups - 1)
			 {
					 winText.text = "You Win!";
			 }
	 }
}
