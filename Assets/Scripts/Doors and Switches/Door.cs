using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
	private bool isOpen = false;
	private Animator animator;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
	}
    // Close the door if open, and open the door if closed
    public void Toggle()
	{
		isOpen = !isOpen;
		animator.SetBool("isOpen", isOpen);
	}
	
	//Specify whether to open or close the door
	public void SetDoorState(bool open)
	{
		if (isOpen != open )
		{
			isOpen = open;
			animator.SetBool("isOpen", isOpen);
		}
	}
}
