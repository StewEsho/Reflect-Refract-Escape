using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)) ]
public class Dragabble : MonoBehaviour {

  Vector2 screenPos;
  Vector2 mousePos;
  SpriteRenderer rend;
  Camera cam;
  bool isDragging;

	// Use this for initialization
	void Start () {
    mousePos = new Vector2(0, 0);
    rend = GetComponent<SpriteRenderer>();
    cam = Camera.main;
    isDragging = false;
	}

	// Update is called once per frame
	void Update () {
    mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    if (Input.GetMouseButtonDown(0) && rend.bounds.Contains(mousePos))
      isDragging = true;

    if (isDragging){
      transform.position = mousePos;
      if (!Input.GetMouseButton(0))
        isDragging = false;
    }
	}
  //
  // void OnDrawGizmosSelected() {
  //       Vector2 center = rend.bounds.center;
  //       float radius = rend.bounds.extents.magnitude;
  //       Gizmos.color = Color.white;
  //       Gizmos.DrawWireSphere(center, radius);
  //   }
}
