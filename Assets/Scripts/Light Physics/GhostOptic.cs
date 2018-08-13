using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GhostOptic : MonoBehaviour
{
	private LineRenderer liner;

	void Awake()
	{
		//Set ghost's collider to trigger
		GetComponent<Collider2D>().isTrigger = true;
		//Set ghost's sprite color to grey + transparent
		transform.Find("Sprite").GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
		//Disable carryable on ghost
		transform.Find("Carryable").gameObject.SetActive(false);
		//Set ghost's tag to "Ghost"
		transform.tag = "Ghost";
//		//Add LineRenderer to ghost
//		LineRenderer liner = gameObject.GetComponent<LineRenderer>();
//		if (gameObject.GetComponent<LineRenderer>() == null)
//			liner = gameObject.AddComponent<LineRenderer>();
//		//Properly configure the LineRenderer
//		liner.startColor = Color.white;
//		liner.startWidth = 0.05f;
//		liner.endColor = Color.white;
//		liner.endWidth = 0.05f;
		//Disable ghost
		gameObject.SetActive(false);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
