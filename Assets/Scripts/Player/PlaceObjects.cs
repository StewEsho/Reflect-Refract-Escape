﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceObjects : MonoBehaviour
{

    enum State
    {
        Standard,
        PlaceMode,
        Carrying
    };
    
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
    private List<GameObject> placementStack; //TODO: check to see if placement stack is even needed.
    private State state = State.Standard; //State for whether in place mode or carrying optic or neither, etc. //TODO: needed???
    private bool inPlaceMode = false; //true when inside of place mode
    private bool isCarrying = false; //true when carrying an optic
    private GameObject carryInRange, carry; //object in range to be carried, and object that is actually being carried.
    private Vector2 ghostPlacementDir;

    //UI for selecting and placing items
    private SpriteRenderer selector; //TODO: move this ui and tooltips to a seperate PlayerUI Script
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

        if (state == State.PlaceMode || state == State.Carrying)
        {
            //Controls for when in place mode
            if (state == State.PlaceMode)
            {
                // Exit placement mode with B as well as X
                if (Input.GetButtonDown("Delete_" + id))
                {
                    TogglePlaceMode();
                }
                
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
                
                //move positioner with right stick
                ghostPlacementDir = new Vector2(Input.GetAxis("RightJoystickX_" + id), -Input.GetAxis("RightJoystickY_" + id));
                if(ghostPlacementDir.sqrMagnitude > controllerDeadzone)
                    ghostPositioner.localPosition = ghostPlacementDir.normalized * ghostDistance;
            
                //rotate positioner with triggers
                float zRotation = Input.GetAxis("Triggers_" + id) * rotationSpeed * Time.deltaTime;
                ghostPositioner.transform.Rotate(0, 0, zRotation);
                
                //Ensure ghost's transform matches the positioner's transform
                ghosts[objectIndex].transform.position = ghostPositioner.position;
                ghosts[objectIndex].transform.rotation = ghostPositioner.rotation;
            } 
            else if (state == State.Carrying)
            {
                try
                {
                    if (carry != null)
                    {
                        //move object with right stick
                        Vector2 dir = new Vector2(Input.GetAxis("RightJoystickX_" + id), -Input.GetAxis("RightJoystickY_" + id));
                        if(dir.sqrMagnitude > controllerDeadzone)
                            carry.transform.localPosition = dir.normalized * ghostDistance;
            
                        //rotate object with triggers
                        float zRotation = Input.GetAxis("Triggers_" + id) * rotationSpeed * Time.deltaTime;
                        carry.transform.Rotate(0, 0, zRotation);
                        
                        //Press B to delete the object being carried.
                        if (Input.GetButtonDown("Delete_" + id))
                        {
                            Destroy(carry);
                            carry = null;
                            state = State.Standard;
                            //TODO: reduce mirror placed count (or not to make things more challenging maybe). Just take care of it.
                        }
                    
                        //Press A to place down object being carried.
                        if (Input.GetButtonDown("Place_" + id))
                        {
                            carry.transform.SetParent(null);
                            carry = null; //By setting the reference to null, the object will no longer move around and thus be "placed".
                            isCarrying = false;
                            state = State.Standard;
                            //TODO: enable carryable on object
                        }
                    }
                    
                    
                    //Ensure ghost's transform matches the positioner's transform
//                    carry.transform.position = ghostPositioner.position;
//                    carry.transform.rotation = ghostPositioner.rotation;
                }
                catch (NullReferenceException e)
                {
                    Debug.LogException(e, this);
                }
            }
        }
        else if (state == State.Standard) //Not placing nor carrying an object
        {
            if (carryInRange != null && Input.GetButtonDown("Place_" + id))
            {
                carry = carryInRange;
                state = State.Carrying;
                carryInRange = null;
                carry.transform.SetParent(transform, true); //parent the object;
                //TODO: disable carryable from object
            }
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
            GameObject ghost = Instantiate(obj.gameObject, ghostPositioner.position, ghostPositioner.rotation, transform);
            //Add GhostOptic script, which will continue the process of instansiating the ghost
            ghost.AddComponent<GhostOptic>();
            //Add ghost to list of ghosts;
            ghosts.Add(ghost);
        }
    }

    public void ClearGhosts()
    {
        //TODO: Implement PlaceObjects.ClearGhosts()
    }

    public void TogglePlaceMode()
    {
        if (state != State.Carrying)
        {
            inPlaceMode = !inPlaceMode;
            state = inPlaceMode ? State.PlaceMode : State.Standard;
            ghosts[objectIndex].SetActive(inPlaceMode);
            selector.transform.parent.gameObject.SetActive(inPlaceMode);
        }
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

    public void AddCarryInRange(GameObject go)
    {
        carryInRange = carryInRange == null ? go : carryInRange;
    }

    public void RemoveCarryInRange()
    {
        carryInRange = null;
    }

    public string GetState()
    {
        return state.ToString();
    }

}
