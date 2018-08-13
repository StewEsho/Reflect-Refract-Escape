using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceObjects : MonoBehaviour
{
    enum State
    {
        Standard,
        Carrying
    };

    //Controller Constants
    private string ROTATION_AXIS;
    private string POSITIONING_AXIS;
    private string MAIN_BUTTON;
    private string DELETE_BUTTON;
    private string ALT_BUTTON;

    [SerializeField] private string id; //"P1" or "P2"
    [SerializeField] private float rotationSpeed = 50;
    [SerializeField] private float ghostDistance = 1.25f;
    [SerializeField] private float controllerDeadzone = 0.19f;


    private List<GameObject> placementStack; //TODO: check to see if placement stack is even needed.

    private State
        state = State.Standard; //State for whether in place mode or carrying optic or neither, etc. //TODO: needed???

    private GameObject preparedFactoryObject; //object that will be spawned from a factory.
    private Vector3 preparedFactoryPosition;
    private bool preparedToSpawn;

    private GameObject carryInRange, carry; //object in range to be carried, and object that is actually being carried.
    private Vector2 ghostPlacementDir;
    private Flashlight flashlight;
    private bool canRotateObject = true; //used to time rotation
    private bool canPositionObject = true; //used to time positioning
    private bool toggle = false;

    void Start()
    {
        ROTATION_AXIS = "DPadX_" + id;
        POSITIONING_AXIS = "DPadY_" + id;
        MAIN_BUTTON = "A_" + id;
        DELETE_BUTTON = "B_" + id;
        ALT_BUTTON = "X_" + id;
        Debug.Log(MAIN_BUTTON);
        placementStack = new List<GameObject>();
    }

    void Update()
    {
        if (state == State.Carrying)
        {
            try
            {
                if (carry != null)
                {
                    //position object around player
                    if (canPositionObject && Mathf.Abs(Input.GetAxis(POSITIONING_AXIS)) > 0.1f)
                    {
                        StartCoroutine(PositionObject(carry.transform));
                    }

                    //rotate object
                    if (canRotateObject && Mathf.Abs(Input.GetAxis(ROTATION_AXIS)) > 0.1f)
                    {
                        StartCoroutine(RotateObjects(carry.transform));
                    }

                    //Press A to place down object being carried.
                    if (Input.GetButtonDown(MAIN_BUTTON))
                    {
                        if (flashlight != null)
                        {
                            flashlight.ToggleFlashlight();
                        }
                        else
                        {
                            carry.transform.SetParent(null);
                            carry = null; //By setting the reference to null, the object will no longer move around and thus be "placed".
                            state = State.Standard;
                        }
                    }

                    //Press B to delete the object being carried.
                    if (Input.GetButtonDown(DELETE_BUTTON))
                    {
                        if (flashlight != null)
                        {
                            Destroy(flashlight);
                            flashlight = null;
                        }
                        Destroy(carry);
                        carry = null;
                        state = State.Standard;
                        //TODO: reduce mirror placed count (or not to make things more challenging maybe). Just take care of it.
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Debug.LogException(e, this);
            }
        }
        else if (state == State.Standard) //Not placing nor carrying an object
        {
            if (Input.GetButtonDown(MAIN_BUTTON))
            {
                if (carryInRange != null)
                {
                    PickUpObject();
                } 
                else if (preparedToSpawn)
                {
                    SpawnNewOptic();
                }
            }
        }
    }

    public void PickUpObject()
    {
        carry = carryInRange;
        state = State.Carrying;
        carry.transform.SetParent(transform, true); //parent the object;
        carryInRange = null;
        flashlight = carry.GetComponent<Flashlight>();
        if (flashlight != null)
        {
            carry.transform.localPosition = carry.transform.localPosition.normalized * 0.6f;
        }
    }

    public void AddCarryInRange(GameObject go)
    {
        carryInRange = carryInRange == null ? go : carryInRange;
    }

    public void RemoveCarryInRange()
    {
        carryInRange = null;
    }

    public void PrepareForSpawning(GameObject optic, Vector3 position) //called when nearing an optic factory
    {
        this.preparedToSpawn = true;
        this.preparedFactoryObject = optic;
        this.preparedFactoryPosition = position;
    }
    
    public void UnprepareForSpawning() //called when exiting an optic factory
    {
        this.preparedToSpawn = false;
        this.preparedFactoryObject = null;
    }

    public void SpawnNewOptic()
    {
        this.preparedToSpawn = false;
        GameObject spawn = Instantiate(this.preparedFactoryObject, this.preparedFactoryPosition, this.preparedFactoryObject.transform.rotation,
            transform);
        this.carryInRange = spawn;
        PickUpObject();
    }

    public string GetState()
    {
        return state.ToString();
    }

    IEnumerator RotateObjects(Transform t)
    {
        float zRotation = Mathf.Sign(-Input.GetAxis(ROTATION_AXIS)) * 15;
        t.transform.Rotate(0, 0, zRotation);
        canRotateObject = false;
        yield return new WaitForSeconds(0.2f);
        canRotateObject = true;
    }

    IEnumerator PositionObject(Transform t)
    {
        float deltaAngle = Mathf.Sign(-Input.GetAxis(POSITIONING_AXIS)) * 5;
        t.transform.localPosition = Quaternion.AngleAxis(deltaAngle, Vector3.forward) * t.transform.localPosition;
        canPositionObject = false;
        yield return new WaitForSeconds(0.02f);
        canPositionObject = true;
    }
}