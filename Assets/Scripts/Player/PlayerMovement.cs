using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

  public float speed = 2.5f;
  Rigidbody2D rb;
  Vector3 delta;

	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody2D>();
    rb.freezeRotation = true;
    delta = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update () {
    delta = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    delta *= speed;
		rb.velocity = delta;
	}
}
