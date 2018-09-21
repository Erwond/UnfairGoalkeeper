using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceController : MonoBehaviour
{

	private bool moving;
	private Vector3 mousePos;
	private Vector2 roundedMousePos;
	private bool editMode;

	private Rigidbody2D rb;
	private float initialGravity;
	public bool alternateGravity;

	public bool airPlacementAllowed;
	public LayerMask allTilesLayer;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent <Rigidbody2D> ();
		if (!alternateGravity)
			initialGravity = rb.gravityScale;
	}
	
	// Update is called once per frame
	void Update ()
	{
		editMode = GameObject.FindGameObjectWithTag ("PlacementManager").GetComponent <ObjectPlacement> ().editMode;
		if (Input.GetMouseButtonUp (0))
			moving = false;

		if (moving) {
			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (airPlacementAllowed)
				roundedMousePos = new Vector2 (Mathf.Round (mousePos.x), Mathf.Round (mousePos.y));
			else
				roundedMousePos = new Vector2 (Mathf.Round (mousePos.x), Mathf.Round (transform.position.y));
			RaycastHit2D rayHit = Physics2D.Raycast (roundedMousePos, Vector2.zero, Mathf.Infinity, allTilesLayer);
			if (!rayHit)
				transform.position = roundedMousePos;
		}

		if (!alternateGravity) {
			if (editMode) {
				rb.gravityScale = 0;
			} else
				rb.gravityScale = initialGravity;
		}
	}

	void OnMouseDown ()
	{
		Debug.Log (editMode);
		if (editMode) {
			Debug.Log ("true");
			moving = true;
		}
	}

}
