using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Switch : MonoBehaviour {
    protected bool isWon;
    public bool isActive;
    protected Light lightsource;
    [SerializeField]
    protected List<GameObject> controlled_items;

	// Use this for initialization
	void Start () {
    isWon = false;
    isActive = false;
    lightsource = transform.GetChild(0).GetComponent<Light>(); //maybe use seperate sprite based animation?
	}

  void LateUpdate(){
    lightsource.intensity = isActive ? 20 : 0;
    foreach (GameObject controlled_item in controlled_items)
    {
        // NOTE: create and use Interactable interface instead
        // item.GetComponent<Interactable>().activate();
        if (controlled_item.tag == "Door")
        {
            controlled_item.SetActive(!isActive);
        }
    }
    isActive = false;

  }

    public virtual void Lightup()
    {
      isActive = true;
    }

}
