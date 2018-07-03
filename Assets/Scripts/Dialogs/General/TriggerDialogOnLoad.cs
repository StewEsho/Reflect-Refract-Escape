using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<DialogTrigger>().triggerDialogue();
	}
	
}
