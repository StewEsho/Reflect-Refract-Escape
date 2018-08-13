using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
    public bool win;
    private List<GameObject> players = new List<GameObject>();

	// Use this for initialization
	void Start () {
        win = false;
	}

    // Update is called once per frame
    private void LateUpdate()
    {
        if (players.Count == GameObject.FindGameObjectsWithTag("Player").Length)
        {
            win = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            if (players.Contains(collision.transform.gameObject) == false)
            {
                players.Add(collision.transform.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            if (players.Contains(collision.transform.gameObject) == true)
            {
                players.Remove(collision.transform.gameObject);
            }
        }
    }
}
