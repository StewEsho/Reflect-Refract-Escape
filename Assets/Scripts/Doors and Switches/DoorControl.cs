using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorControl : MonoBehaviour {
    private TilemapCollider2D tc2d;
    private TilemapRenderer tr;
    private bool open;
	// Use this for initialization
	void Start () {
        tc2d = GetComponent<TilemapCollider2D>();
        tr = GetComponent<TilemapRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        tc2d.enabled = true;
        tr.enabled = true;
        if (open == true)
        {
            tc2d.enabled = false;
            tr.enabled = false;
        }
        open = false;
	}

    public void disable()
    {
        open = true;
    }
}
