using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallController : MonoBehaviour
{

	private Rigidbody2D rb;

	private Vector3 ballPos;
	private Vector3 ballDir;
	private Vector3 mousePos;

	private bool ballClicked;
	public bool ballShot;

	private Vector3 lookAtMouseAngle;
	private float shootingAngel;
	private float distanceToBall;

	private float ballForce = 400;
	private float maxDistance = 3;

	public GameObject editPlayButton;

	public GameObject dirIndicator;
	private GameObject thisIndicator;
	private float indicatorCounter;
	private float initialSeconds = 0.1f;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent <Rigidbody2D> ();
		indicatorCounter = initialSeconds;
		ballPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonUp (0) && ballClicked) {
			Time.timeScale = 1f;
			ballClicked = false;
			ballShot = true;
			rb.gravityScale = 1;
			rb.AddForce (transform.right * ballForce * distanceToBall);
			distanceToBall = 0;
		}
		if (ballClicked && !ballShot) {
			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos.z += 10;
			lookAtMouseAngle = (mousePos - ballPos).normalized;
			transform.right = -lookAtMouseAngle;
			distanceToBall = Vector3.Distance (mousePos, ballPos);
			if (distanceToBall > maxDistance)
				distanceToBall = maxDistance;
			indicatorCounter -= Time.deltaTime;
			Time.timeScale = 0.5f;
			if (indicatorCounter < 0) {
				thisIndicator = (GameObject)Instantiate (dirIndicator, ballPos, Quaternion.identity);
				thisIndicator.GetComponent <Rigidbody2D> ().AddForce (transform.right * ballForce * distanceToBall);
				indicatorCounter = initialSeconds;
			}

		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			TryAgain();
		}
	}

	void OnMouseDown ()
	{
		ballClicked = true;
		if (!ballShot) {
			editPlayButton.SetActive (false);
			ballPos = transform.position;
		}
	}

	public void TryAgain(){
		transform.position = ballPos;
		editPlayButton.SetActive (true);
		ballShot = false;
		ballClicked = false;
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0f; 
		rb.gravityScale = 0;
	}
}

