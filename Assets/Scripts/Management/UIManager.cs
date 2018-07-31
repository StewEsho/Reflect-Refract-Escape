using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UIManager : MonoBehaviour
{
    private Canvas canvas;
    private CanvasScaler canvasScaler;
    private Animator levelCompleteAnimator;
    private GameObject controlsPanel;
    private GameObject opticCountPanel;
    private Text opticCountText;

    void Start()
    {
        //Properly configure the Canvas' render mode as a Camera Screen Space.
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 10;
        canvas.sortingOrder = 300;

        //Configuire the canvas scaler to fit a 16:9 aspect ratio.
        canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.referenceResolution = new Vector2(1280, 720);
        canvasScaler.matchWidthOrHeight = 1.0f;
        canvasScaler.referencePixelsPerUnit = 64;

        levelCompleteAnimator = transform.Find("Level Complete").gameObject.GetComponent<Animator>();
        controlsPanel = transform.Find("Controls").gameObject;
        opticCountPanel = transform.Find("OpticCount").gameObject;
        opticCountText = opticCountPanel.transform.Find("Number").gameObject.GetComponent<Text>();
        
        controlsPanel.SetActive(false);
        opticCountPanel.SetActive(false);
    }

    public void ShowLevelCompletePanel()
    {
        if (levelCompleteAnimator != null)
        {
            levelCompleteAnimator.SetBool("is_win", true);
        }
        else
        {
            Debug.LogError("No LevelComplete panel could be found in the canvas!");
        }
    }

    public void ToggleControls()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(!controlsPanel.activeSelf);
        }
        else
        {
            Debug.LogError("No Controls panel could be found in the canvas!");
        }
    }

    public void EnableOpticCount(int limit)
    {
        //Activates the Optic Count panel
        if (opticCountPanel != null && opticCountText != null)
        {
            opticCountPanel.SetActive(true);
            opticCountText.text = limit.ToString();
        }
        else
        {
            Debug.LogError("No OpticCount panel could be found in the canvas!");
        }
    }

    public void SetOpticCount(int count)
    {
        if (opticCountPanel != null && opticCountText != null)
        {
            opticCountText.text = count.ToString();
        }
    }
}