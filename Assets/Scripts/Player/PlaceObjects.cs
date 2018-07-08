using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceObjects : MonoBehaviour
{

    [SerializeField]
    private string id; //"P1" or "P2"
    [SerializeField]
    private List<PlaceableItem> objects; // list of all placable placableObjects
    private List<GameObject> ghosts; //list of ghosts (NOT serializable)
    [SerializeField]
    private float rotationSpeed = 50;
    [SerializeField]
    private float ghostDistance = 1.25f;
    [SerializeField] 
    private float controllerDeadzone = 0.19f;

    private int objectIndex = 0;
    private List<GameObject> placementStack;
    private bool inPlaceMode = false;
    private Vector2 ghostPlacementDir;

    //UI for selecting and placing items
    private SpriteRenderer selector;
    private Transform ghostPositioner; //a positioner which is moved and rotated, while the selected ghost matches its transform.

    void Start()
    {
        placementStack = new List<GameObject>();
        ghosts = new List<GameObject>();
        selector = transform.Find("Selector").Find("Item").GetComponent<SpriteRenderer>();
        selector.sprite = objects[objectIndex].thumbnail;
        ghostPositioner = transform.Find("Ghost Positioner");
        CreateGhosts(); //instansiates ghosts for this level
    }

    void Update()
    {
        // Wait for placement mode toggle
        if (Input.GetButtonDown("TogglePlacement_" + id))
        {
            TogglePlaceMode();
        }

        //Controls for when in place mode
        if (inPlaceMode)
        {
            //Press A to place selected object
            if (Input.GetButtonDown("Place_" + id))
            {
                placementStack.Add(Instantiate(objects[objectIndex].gameObject, ghostPositioner.position, ghostPositioner.rotation));
            }
            
            //Use bumpers to scroll through items
            if (Input.GetButtonDown("SelectNext_" + id))
            {
                SelectNewObject(objectIndex + 1);
            }
            if (Input.GetButtonDown("SelectPrev_" + id))
            {
                SelectNewObject(objectIndex - 1);
            }
            
            //rotate positioner with triggers
            float zRotation = Input.GetAxis("Triggers_" + id) * rotationSpeed * Time.deltaTime;
            ghostPositioner.transform.Rotate(0, 0, zRotation);
            
            //move positioner with right stick
            ghostPlacementDir = new Vector2(Input.GetAxis("RightJoystickX_" + id), -Input.GetAxis("RightJoystickY_" + id));
            if(ghostPlacementDir.sqrMagnitude > controllerDeadzone)
                ghostPositioner.localPosition = ghostPlacementDir.normalized * ghostDistance;
            
            //Ensure ghost's transform matches the positioner's transform
            ghosts[objectIndex].transform.position = ghostPositioner.position;
            ghosts[objectIndex].transform.rotation = ghostPositioner.rotation;
        }
    }

    public void CreateGhosts()
    {
        //first, align the ghost positioner properly;
        ghostPositioner.localPosition = Vector2.left * ghostDistance;
        ghostPositioner.rotation = Quaternion.identity;
        foreach (PlaceableItem obj in objects)
        {
            //Instansiate ghost as child
            GameObject ghost = Instantiate(obj.gameObject, ghostPositioner.position, ghostPositioner.rotation, this.transform);
            //Add GhostOptic script, which will continue the process of instansiating the ghost
            ghost.AddComponent<GhostOptic>();
            //Add ghost to list of ghosts;
            ghosts.Add(ghost);
        }
    }

    public void ClearGhosts()
    {
        
    }

    public void TogglePlaceMode()
    {
        inPlaceMode = !inPlaceMode;
        ghosts[objectIndex].SetActive(inPlaceMode);
        selector.transform.parent.gameObject.SetActive(inPlaceMode);
    }

    public void SelectNewObject(int index)
    {
        //disable the old ghost
        ghosts[objectIndex].SetActive(false);
        //set index for new object, enable new ghost, change thumbnail accordingly.
        objectIndex = (int) Mathf.Repeat(index, ghosts.Count);
        ghosts[objectIndex].SetActive(true);
        selector.sprite = objects[objectIndex].thumbnail;
    }

}
