using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : SingletonMonoBehaviour<ButtonManager> {

	public Color startColor;
	public Color highlightColor;

	public GameObject StartButton;

	// Use this for initialization
	void Start () {
		StartButton.gameObject.GetComponent<Renderer> ().material.color = startColor;
	}

	// Update is called once per frame
	void Update () {

		//if (Raycaster.getInstance ().anythingHitByRay ()) {

		GameObject objHitByRay = Raycaster.getInstance ().getObjectHitByRay ();
		string objHitTag = objHitByRay.tag;

		if ((objHitByRay != null) && (objHitByRay == StartButton)) {
			StartButton.gameObject.GetComponent<Renderer> ().material.color = highlightColor;
		} else {
			StartButton.gameObject.GetComponent<Renderer> ().material.color = startColor;
		}
	}
}
