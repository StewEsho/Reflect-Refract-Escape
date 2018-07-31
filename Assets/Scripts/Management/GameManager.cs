using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
//    public int one_star;
//    public int two_star;
//    public int three_star;
    private UIManager ui;
    private bool gameWon;
    private GameObject[] exitObjects;
    [SerializeField]
    private int mirrorLimit = -1;

    private bool areOpticsLimited = false;
    private int mirrorAmount = 0;

    // Use this for initialization
    void Start()
    {
        ui = GameObject.Find("/Canvas").GetComponent<UIManager>();
        exitObjects = GameObject.FindGameObjectsWithTag("exit");
        if (mirrorLimit >= 1)
        {
            ui.EnableOpticCount(mirrorLimit);
            areOpticsLimited = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameWon = true;
        if (areOpticsLimited)
            StartCoroutine(CountMirrors());
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
            ui.ShowLevelCompletePanel();
            if (Input.GetButtonDown("A_P1"))
            {
                //Load next level
                SceneManager.LoadScene((int) Mathf.Repeat(SceneManager.GetActiveScene().buildIndex + 1,
                    SceneManager.sceneCountInBuildSettings));
                gameWon = false;
            }
        }

        if (Input.GetButtonDown("X_P1") || Input.GetButtonDown("X_P2"))
        {
            ui.ToggleControls();
        }
    }

    public bool MoreMirrorsAvailible()
    {
        return !areOpticsLimited || (mirrorLimit > mirrorAmount);
    }

    IEnumerator CountMirrors()
    {
        GameObject[] mirrors = GameObject.FindGameObjectsWithTag("mirror"); 
        mirrorAmount = mirrors.Length;
        ui.SetOpticCount(mirrorLimit - mirrorAmount);
        yield return new WaitForSeconds(.1f);
    }
}