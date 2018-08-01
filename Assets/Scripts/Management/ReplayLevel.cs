using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayLevel : MonoBehaviour {

    // Use this for initialization
    public string level_to_load;
	void Start () {
		
	}
	
	// Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(level_to_load);
    }
}
