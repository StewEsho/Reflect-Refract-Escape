using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressureSwitch : MonoBehaviour
{

	public BeamEmitter beam;
	public bool isEnabled = false;

	void Start()
	{
		beam.IsActive = isEnabled;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			isEnabled = !isEnabled;
		}
		
		beam.IsActive = isEnabled;
	}
}
