using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{

	private float counter;

	// Use this for initialization
	void Start ()
	{
		counter = 0.3f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (counter > 0)
			counter -= Time.deltaTime;
		else
			Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name == "Ground") {
			Destroy (gameObject);
		}
	}
}
