using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int one_star;
    public int two_star;
    public int three_star;
    private Animator controller;
    private bool gameWon;
    public GameObject UIpanel;
    private bool toggle;
    private GameObject[] exitObjects;
    
    // Use this for initialization
    void Start()
    {
        toggle = false;
        controller = GameObject.Find("/Canvas/Rating").GetComponent<Animator>();
        exitObjects = GameObject.FindGameObjectsWithTag("exit");
    }

    // Update is called once per frame
    void Update()
    {
        gameWon = true;
        GameObject[] mirrors = GameObject.FindGameObjectsWithTag("mirror");
        int mirrorAmount = mirrors.Length;
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
//            int star_amount = 0;
//            controller.SetBool("is_win", true);
//            if (one_star <= mirrorAmount && mirrorAmount < two_star)
//            {
//                star_amount = 1;
//            }
//            if (two_star <= mirrorAmount && mirrorAmount < three_star)
//            {
//                star_amount = 2;
//            }
//            if (three_star <= mirrorAmount )
//            {
//                star_amount = 3;
//            }
//            controller.SetInteger("star", star_amount);
            if (Input.GetButtonDown("A_P1"))
            {
                controller.SetBool("is_win", true);
                SceneManager.LoadScene((int)Mathf.Repeat(SceneManager.GetActiveScene().buildIndex + 1, SceneManager.sceneCountInBuildSettings));
                gameWon = false;
            }

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
