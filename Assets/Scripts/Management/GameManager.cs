using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameWon;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        
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
            SceneManager.LoadScene((int) Mathf.Repeat(SceneManager.GetActiveScene().buildIndex + 1, SceneManager.sceneCountInBuildSettings));
            gameWon = false;
        }
        GameObject[] lights = GameObject.FindGameObjectsWithTag("Lightray");
        foreach (GameObject lightray in lights)
        {
            Destroy(lightray);
        }
    }
}
