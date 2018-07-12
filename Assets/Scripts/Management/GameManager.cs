using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameWon;
    public GameObject UIpanel;
    private bool toggle;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        toggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameWon = true;
        GameObject[] exitObjects = GameObject.FindGameObjectsWithTag("exit");
        foreach (GameObject e in exitObjects)
        {
            if (!e.GetComponent<Exit>().win)
            {
                gameWon = false;
                break;
            }
        }
        if (gameWon)
        {
            SceneManager.LoadScene((int)Mathf.Repeat(SceneManager.GetActiveScene().buildIndex + 1, SceneManager.sceneCountInBuildSettings));
            gameWon = false;
        }
        if (Input.GetButtonDown("StartButton_P1"))
        {
            if (toggle == false)
            {
                UIpanel.SetActive(true);
                toggle = true;
            }
            else
            {
                UIpanel.SetActive(false);
                toggle = false;
            }
        }
    }
}
