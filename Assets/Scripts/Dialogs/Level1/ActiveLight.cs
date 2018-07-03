using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLight : MonoBehaviour {
    public bool enable;
	// Use this for initialization
	void Start () {
        enable = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(enable);
        if (enable == true)
        {
            GetComponent<BeamEmitter>().enabled = true;
        }
        else
        {
            GetComponent<BeamEmitter>().enabled = false;
        }
	}
}
