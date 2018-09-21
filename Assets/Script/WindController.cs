using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{

	private bool addWindForce;
	private float windForce = 8;

	private GameObject ball;
	public string windDirection;
	private Vector2 forceDirection;

	// Use this for initialization
	void Start ()
	{
		ball = GameObject.FindGameObjectWithTag ("ball");

		if (windDirection == "left") {
			Debug.Log ("Links");
			forceDirection = -transform.right;
		} else if (windDirection == "up") {
			Debug.Log ("Up");
			forceDirection = transform.up;
			windForce = windForce * 1.5f;
		}
			
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (addWindForce) {
			ball.GetComponent <Rigidbody2D> ().AddForce (forceDirection * windForce);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "ball") {
			addWindForce = true;
		}
			
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "ball")
			addWindForce = false;
	}
}
