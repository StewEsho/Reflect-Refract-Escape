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
        Debug.Log("Mirror Limit: " + mirrorLimit);
        if (mirrorLimit >= 1)
        {
            ui.EnableOpticCount(mirrorLimit);
            areOpticsLimited = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("StartButton_P1") || Input.GetButtonUp("StartButton_P2"))
        {
            if (GameObject.Find("PauseMenu").GetComponent<Animator>().GetBool("IsTrigger"))
            {
                CancelRestart();
            }
            else
            {
                GameObject.Find("PauseMenu").GetComponent<Animator>().SetBool("IsTrigger", true);
            }
           
        }
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
            ui.ShowLevelCompletePanel();
            if (Input.GetButtonDown("A_P1") || Input.GetButtonDown("A_P2"))
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
        GameObject[] lenses = GameObject.FindGameObjectsWithTag("Lens");
        mirrorAmount = mirrors.Length + lenses.Length;
        ui.SetOpticCount(mirrorLimit - mirrorAmount);
        yield return new WaitForSeconds(.1f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CancelRestart()
    {
        GameObject.Find("PauseMenu").GetComponent<Animator>().SetBool("IsTrigger", false);
    }
}