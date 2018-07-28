using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBobbing : MonoBehaviour
{
    public float bobMagnitude = 0.18f;
    public float bobSpeed = 1;
    public float angularMagnitude = 8;
    public float angularSpeed = 1.3f;

    private float bias; //alters the time start so multiple bobbing items are not synced.
    private Vector3 initialPosition;

    // Use this for initialization
    private void Start()
    {
        bias = Random.Range(0f, 4.0f);
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        var time = Time.time + bias;
        transform.localPosition = transform.localPosition.SetY(initialPosition.y + bobMagnitude * Mathf.Sin(bobSpeed * time));
        transform.localEulerAngles = transform.localEulerAngles.SetZ(angularMagnitude * Mathf.Sin(angularSpeed * time));
    }
}