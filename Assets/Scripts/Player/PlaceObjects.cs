using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour {

  public GameObject obj;
  public float rotationSpeed = 5f;
  GameObject placed_object;

	// Use this for initialization
	void Start () {
	}

//new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")){
      placed_object = Instantiate(obj,
          Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
          Quaternion.identity) as GameObject;
    }
    if (Input.GetButton("Fire1")){
      if (placed_object != null){
        float zRotation = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        // placed_object.transform.eulerAngles = new Vector3 (0, 0, placed_object.transform.eulerAngles.z + zRotation);
        placed_object.transform.Rotate(0, 0, zRotation);
      }
    }
    if (Input.GetButtonUp("Fire1")){
      placed_object = null;
    }
	}
}
