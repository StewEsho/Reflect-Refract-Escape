using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBobbing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localPosition = transform.localPosition.SetY(0.18f * Mathf.Sin(Time.time));
		transform.localEulerAngles = transform.localEulerAngles.SetZ(8 * Mathf.Sin(1.3f * Time.time));
	}
}
