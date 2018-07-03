using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogWhenLightup : MonoBehaviour {
    private Switch sw;
	// Use this for initialization
	void Start () {
        sw = GetComponent<Switch>();
	}
	
	// Update is called once per frame
	void Update () {
		if (sw.transform.GetChild(0).GetComponent<Light>().intensity == 20)
        {
            GetComponent<DialogTrigger>().triggerDialogue();
//            Destroy(this);
        }
	}
}
