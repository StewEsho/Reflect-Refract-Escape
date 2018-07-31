using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UIManager : MonoBehaviour
{
	private Canvas canvas;
	private CanvasScaler canvasScaler;

	void Start ()
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
	}
}
