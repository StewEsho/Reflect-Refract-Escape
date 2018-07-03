using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.5f;
    Rigidbody2D rb;
    Vector3 delta;
    [SerializeField] private string ID;
    private SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        delta = new Vector3(0, 0, 0);
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        delta = new Vector3(Input.GetAxis("LeftJoystickX_" + ID), -Input.GetAxis("LeftJoystickY_" + ID), 0);
        delta *= speed;
        rb.velocity = delta;
        if (delta.x > 0)
            sprite.flipX = true;
        else if (delta.x < 0)
            sprite.flipX = false;
    }
}