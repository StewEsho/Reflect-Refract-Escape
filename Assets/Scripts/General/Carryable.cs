using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(LineRenderer))]
/**
 * To be added to an empty game object as a CHILD of the carryable item.
 * This object will contain the trigger that acts as the range for picking objects up.
 */
public class Carryable : MonoBehaviour
{
	private LineRenderer lr;
	// Use this for initialization
	void Start ()
	{
		lr = GetComponent<LineRenderer>();
		transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent.parent == null)
		{
			lr.enabled = false;
		}
		else
		{
			lr.enabled = true;
			lr.SetPositions(new Vector3[] {transform.position, transform.parent.parent.position});
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			PlaceObjects po = other.gameObject.GetComponent<PlaceObjects>();
			if (po.GetState() == "Standard")
				po.AddCarryInRange(transform.parent.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			PlaceObjects po = other.gameObject.GetComponent<PlaceObjects>();
			if (po.GetState() == "Standard")
				po.RemoveCarryInRange();
		}
	}
}
