using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpticFactory : MonoBehaviour
{

	public GameObject Optic;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			PlaceObjects po = other.gameObject.GetComponent<PlaceObjects>(); //access PlaceObjects script
			if (po.GetState() == "Standard")
			{
				po.PrepareForSpawning(Optic, transform.parent.position);
			}
		}
	}
}
