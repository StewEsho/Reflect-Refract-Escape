using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
/**
 * To be added to an empty game object as a CHILD of the carryable item.
 * This object will contain the trigger that acts as the range for picking objects up.
 */
public class Carryable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
