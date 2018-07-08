using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceObjectsOLD : MonoBehaviour
{

    [SerializeField]
    private string ID;
    [SerializeField]
    private List<PlaceableItem> placableObjects; // list of all placable placableObjects
    [SerializeField]
    public float rotationSpeed;
    [SerializeField]
    public float placeDistance = 1.25f;

    public readonly int OBJECT_LIMIT = 5;
    public readonly float DEADZONE = 0.19f;

    private int objectIndex = 0; // which object in placableObjects to select
    private List<GameObject> placementStack;
    private GameObject placed_object;
    private bool inPlaceMode = true; // when true, player can place placableObjects
    private Vector2 placeDir; //direction player will place their placableObjects
    private List<GameObject> ghosts; //list of all ghost object instances

    //"UI" for selecting and placing items
    private SpriteRenderer selector;
    private GameObject ghost;
    private GameObject ghostPositioner;
    private SpriteRenderer ghostSprite;

    // Use this for initialization
    void Start()
    {
        placementStack = new List<GameObject>();
        selector = transform.Find("Selector").Find("Item").GetComponent<SpriteRenderer>();
        selector.sprite = placableObjects[objectIndex].thumbnail;
        if (transform.Find("Ghost").gameObject != null)
        {
            ghostPositioner = transform.Find("Ghost").gameObject;
            Debug.Log(ghostPositioner);
        }
        ghost = ghosts[objectIndex];
        ghostSprite = ghost.transform.GetChild(0).GetComponent<SpriteRenderer>();
//        ghostSprite.sprite = placableObjects[objectIndex].ghostSprite;
//        ghost.SetActive(inPlaceMode);
        ghost.SetActive(inPlaceMode);
        Debug.Log(ghost);
        selector.transform.parent.gameObject.SetActive(inPlaceMode);
//        TogglePlaceMode();
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for placement toggle
        if (Input.GetButtonDown("TogglePlacement_" + ID))
        {
            TogglePlaceMode();
        }

        // placableObjects can be deleted even if not in place mode
        // if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.Z) && placementStack.Count > 0){
        if (Input.GetButtonDown("Delete_" + ID) && placementStack.Count > 0)
        {
            GameObject objectToRemove = placementStack.Pop();
            Destroy(objectToRemove);
        }

        if (inPlaceMode)
        {
            //Press A to place selected object
            if (Input.GetButtonDown("Place_" + ID) && placementStack.Count < OBJECT_LIMIT)
            {
                placed_object = Instantiate(placableObjects[objectIndex].gameObject,
                    ghost.transform.position,
                    ghost.transform.rotation) as GameObject;
                placementStack.Add(placed_object);
                placed_object = null;
            }

            //Scroll through items with bumpers
            if (Input.GetButtonDown("SelectNext_" + ID))
            {
                ghosts[objectIndex].SetActive(false);
                objectIndex = (int)Mathf.Repeat(objectIndex + 1, ghosts.Count);
                ghosts[objectIndex].SetActive(true);
                ghost = ghosts[objectIndex];
                selector.sprite = placableObjects[objectIndex].thumbnail;
//                ghostSprite.sprite = placableObjects[objectIndex].ghostSprite;
            }
            if (Input.GetButtonDown("SelectPrev_" + ID))
            {
                ghosts[objectIndex].SetActive(false);
                objectIndex = (int)Mathf.Repeat(objectIndex - 1, ghosts.Count);
                ghosts[objectIndex].SetActive(true);
                ghost = ghosts[objectIndex];
                selector.sprite = placableObjects[objectIndex].thumbnail;
//                ghostSprite.sprite = placableObjects[objectIndex].ghostSprite;
            }
            
            //rotate positioner with triggers
            float zRotation = Input.GetAxis("Triggers_" + ID) * rotationSpeed * Time.deltaTime;
            ghostPositioner.transform.Rotate(0, 0, zRotation);

            placeDir = new Vector2(Input.GetAxisRaw("DPadX_" + ID), -Input.GetAxisRaw("DPadY_" + ID));

            //move positioner with left stick
            if (Mathf.Abs(Input.GetAxisRaw("DPadX_" + ID)) > DEADZONE)
            {
                Vector3 eulerangle = ghost.transform.eulerAngles;
                ghostPositioner.transform.RotateAround(transform.position, new Vector3(0, 0, 1), -Input.GetAxisRaw("DPadX_" + ID) * rotationSpeed / 5);
                ghostPositioner.transform.eulerAngles = eulerangle;

            }
            
            //move ghost onto positioner
            ghost.transform.position = ghostPositioner.transform.position;
            ghost.transform.rotation = ghostPositioner.transform.rotation;
        }
    }

    void TogglePlaceMode()
    {
        inPlaceMode = !inPlaceMode;
//        ghost.SetActive(inPlaceMode);
        ghosts[objectIndex].SetActive(inPlaceMode);
        selector.transform.parent.gameObject.SetActive(inPlaceMode);
    }

    public void SetGhostList(List<GameObject> ghosts)
    {
        this.ghosts = new List<GameObject>(ghosts);
    }
   
}
