using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceObjects : MonoBehaviour
{

    [SerializeField]
    private string ID;
    [SerializeField]
    private List<PlaceableItem> objects; // list of all placable objects
    [SerializeField]
    public float rotationSpeed;
    [SerializeField]
    public float placeDistance = 1.25f;

    public readonly int OBJECT_LIMIT = 5;
    public readonly float DEADZONE = 0.19f;

    private int objectIndex = 0; // which object in objects to select
    private List<GameObject> placementStack;
    private GameObject placed_object;
    private bool inPlaceMode = false; // when true, player can place objects
    private Vector2 placeDir; //direction player will place their objects
    private List<GameObject> ghosts; //list of all ghost object instances

    //"UI" for selecting and placing items
    private SpriteRenderer selector;
    private GameObject ghost;
    private SpriteRenderer ghostSprite;

    // Use this for initialization
    void Start()
    {
        placementStack = new List<GameObject>();
        selector = transform.Find("Selector").Find("Item").GetComponent<SpriteRenderer>();
        selector.sprite = objects[objectIndex].thumbnail;
        if (transform.Find("Ghost").gameObject != null)
        {
            ghost = transform.Find("Ghost").gameObject;
        }
        ghostSprite = ghost.transform.GetChild(0).GetComponent<SpriteRenderer>();
//        ghostSprite.sprite = objects[objectIndex].ghostSprite;
        ghost.SetActive(inPlaceMode);
        selector.transform.parent.gameObject.SetActive(inPlaceMode);
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for placement toggle
        if (Input.GetButtonDown("TogglePlacement_" + ID))
        {
            TogglePlaceMode();
        }

        // objects can be deleted even if not in place mode
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
                placed_object = Instantiate(objects[objectIndex].gameObject,
                    ghost.transform.position,
                    ghost.transform.rotation) as GameObject;
                placementStack.Add(placed_object);
                placed_object = null;
            }

            //Scroll through items with bumpers
            if (Input.GetButtonDown("SelectNext_" + ID))
            {
                ghosts[objectIndex].SetActive(false);
                objectIndex = (int)Mathf.Repeat(objectIndex + 1, objects.Count);
                selector.sprite = objects[objectIndex].thumbnail;
//                ghostSprite.sprite = objects[objectIndex].ghostSprite;
            }
            if (Input.GetButtonDown("SelectPrev_" + ID))
            {
                objectIndex = (int)Mathf.Repeat(objectIndex - 1, objects.Count);
                selector.sprite = objects[objectIndex].thumbnail;
//                ghostSprite.sprite = objects[objectIndex].ghostSprite;
            }

            //rotate objects with triggers
            float zRotation = Input.GetAxis("Triggers_" + ID) * rotationSpeed * Time.deltaTime;
            ghost.transform.Rotate(0, 0, zRotation);

            placeDir = new Vector2(Input.GetAxisRaw("DPadX_" + ID), -Input.GetAxisRaw("DPadY_" + ID));

            //move ghost with left stick
            if (Mathf.Abs(Input.GetAxisRaw("DPadX_" + ID)) > DEADZONE)
            {
                Vector3 eulerangle = ghost.transform.eulerAngles;
                ghost.transform.RotateAround(transform.position, new Vector3(0, 0, 1), -Input.GetAxisRaw("DPadX_" + ID) * rotationSpeed / 5);
                ghost.transform.eulerAngles = eulerangle;

            }
        }
    }

    void TogglePlaceMode()
    {
        inPlaceMode = !inPlaceMode;
        ghost.SetActive(inPlaceMode);
        selector.transform.parent.gameObject.SetActive(inPlaceMode);
    }

    public void SetGhostList(List<GameObject> ghosts)
    {
        this.ghosts = ghosts;
    }
   
}
