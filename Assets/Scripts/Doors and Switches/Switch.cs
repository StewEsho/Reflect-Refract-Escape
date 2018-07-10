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
    protected List<GameObject> ControlledItems;

	// Use this for initialization
    private void Start () {
    isWon = false;
    isActive = false;
    lightsource = transform.GetChild(0).GetComponent<Light>(); //maybe use seperate sprite based animation?
	}

    private void LateUpdate(){
    lightsource.intensity = isActive ? 20 : 0;
      foreach (var item in ControlledItems)
      {
          // NOTE: create and use Interactable interface instead
          // item.GetComponent<Interactable>().activate();
          var door = item.GetComponent<Door>();
          if (door != null) door.SetDoorState(isActive);
      }
    isActive = false;

  }

    public virtual void Activate()
    {
      isActive = true;
    }

}
