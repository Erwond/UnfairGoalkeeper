using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject ball;
	private BallController ballScript;
	private Vector3 target;
	private Vector3 camPos;

	// Use this for initialization
	void Start ()
	{
		ballScript = ball.GetComponent <BallController> ();
		camPos = transform.position;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Return)) {
			CameraBack ();
		}
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (ballScript.ballShot && ball.transform.position.x > transform.position.x) {
			target = ball.transform.position;
			target.z = -10;
			target.y = transform.position.y;
			transform.position = Vector3.Slerp (transform.position, target, 3 * Time.deltaTime);
		}

	}

	public void SwitchPosition ()
	{
		if (transform.position == camPos) {
			Debug.Log ("CamPos");
			transform.position = new Vector3 (0, camPos.y, camPos.z);
		} else {
			Debug.Log (camPos);
			transform.position = camPos;
		}
	}

	public void CameraBack ()
	{
		transform.position = camPos;
	}
}
