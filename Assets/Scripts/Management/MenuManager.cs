using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	private Button playButton;

	// Use this for initialization
	void Start ()
	{
		playButton = transform.Find("Button").GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("A_P1") || Input.GetButtonDown("A_P2"))
		{
			playButton.onClick.Invoke();
		}
	}
}
