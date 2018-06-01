using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour {

  public GameObject obj;
  public float rotationSpeed;
  GameObject placed_object;
  List<GameObject> placementStack;

	// Use this for initialization
	void Start () {
    placementStack = new List<GameObject>();
	}

//new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
      placed_object = Instantiate(obj,
          Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
          Quaternion.identity) as GameObject;
      placementStack.Add(placed_object);
    }
    if (Input.GetMouseButton(0)){
      if (placed_object != null){
        float zRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        // placed_object.transform.eulerAngles = new Vector3 (0, 0, placed_object.transform.eulerAngles.z + zRotation);
        placed_object.transform.Rotate(0, 0, zRotation);
      }
    }
    if (Input.GetMouseButtonUp(0)){
      placed_object = null;
    }

    if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.Z) && placementStack.Count > 0){
      Debug.Log(placementStack);
      GameObject objectToRemove = placementStack.Pop();
      Destroy(objectToRemove);
    }
	}
}
