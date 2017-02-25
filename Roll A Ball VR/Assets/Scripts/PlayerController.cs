using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText, winText;
	public GameObject Pickups;
	public AudioClip ringGet;
	AudioSource audio;

	private Rigidbody rb;
	private int count, numPickups;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		winText.text = "";
		Transform[] children;
		children = Pickups.GetComponentsInChildren<Transform>();
		numPickups = children.Length;
		audio = GetComponent<AudioSource>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	 {
			 if (other.gameObject.CompareTag ("Pickup"))
			 {
					 other.gameObject.SetActive (false);
					 count++;
					 SetCountText();
					 audio.PlayOneShot(ringGet, 1f);

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
