using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpticFactory : MonoBehaviour
{
	private GameManager gameManager;
	private bool isEnabled;
	public GameObject Optic;
	
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		isEnabled = !gameManager.MoreMirrorsAvailible();
	}

	void Update()
	{
		StartCoroutine(CheckIfEnabled());
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (isEnabled && other.transform.CompareTag("Player"))
		{
			PlaceObjects po = other.gameObject.GetComponent<PlaceObjects>(); //access PlaceObjects script
			po.PrepareForSpawning(Optic, transform.parent.position);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			PlaceObjects po = other.gameObject.GetComponent<PlaceObjects>(); //access PlaceObjects script
			po.UnprepareForSpawning();
		}
	}

	IEnumerator CheckIfEnabled()
	{
		isEnabled = gameManager.MoreMirrorsAvailible() || Optic.transform.CompareTag("BeamEmitter");
		yield return new WaitForSeconds(0.1f);
	}
		
}
