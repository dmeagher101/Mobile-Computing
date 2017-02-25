using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {

	public Vector3 speed;

	// Use this for initialization
	void Start () {
		Vector3 forward = Player.getInstance().transform.TransformDirection (Vector3.forward);

		Vector3 movement = forward;

		rb.AddForce (forward);
		Vector3 movement = new Vector3 (0.0f, 0.0f, 1f);
	}
	
	// Update is called once per frame
	void FixedUUpdate () {
		
	}
}
