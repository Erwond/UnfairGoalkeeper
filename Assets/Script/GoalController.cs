using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{

	private int goalCounter;

	// Use this for initialization
	void Start ()
	{
		goalCounter = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name == "Ball") {
			goalCounter++;
		}
	}
}
