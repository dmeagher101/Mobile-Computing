using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float speed = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey("up")){
			transform.Rotate(-speed*Time.deltaTime, 0, 0);
		}
		if (Input.GetKey ("down")){
			transform.Rotate(speed*Time.deltaTime, 0, 0);
		}
	}
}
