using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnabler : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start () {
        ReplayLevel[] portals = Resources.FindObjectsOfTypeAll<ReplayLevel>();
        Debug.Log(portals.Length);
        foreach(ReplayLevel portal in portals)
        {
            portal.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
}
