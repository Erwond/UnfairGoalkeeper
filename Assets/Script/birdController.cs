using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{

	private Rigidbody2D rb;
	private Vector3 initialPos;
	private Animator birdAnim;
	//private float flyingSpeed = 100f;

	private bool editMode;

	// Use this for initialization
	void Start ()
	{

	}

	void Awake ()
	{
		rb = gameObject.GetComponent <Rigidbody2D> ();
		birdAnim = gameObject.GetComponent <Animator> ();
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*editMode = GameObject.FindGameObjectWithTag ("PlacementManager").GetComponent <ObjectPlacement> ().editMode;
		if (editMode && rb.velocity.x != 0) {
			rb.velocity = Vector2.zero;
		} else if (!editMode && rb.velocity.x == 0 && rb.gravityScale != 1) {
			rb.AddForce (Vector2.left * flyingSpeed);
		}*/
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		
		rb.gravityScale = 1;
		birdAnim.speed = 0;
	}

	public void resetBird ()
	{
		transform.position = new Vector2 (3, initialPos.y);
	}
}
