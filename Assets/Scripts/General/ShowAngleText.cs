using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAngleText : MonoBehaviour {
    private string display;
    private TextMesh textMesh;
    private float degree;

	// Use this for initialization
	void Start () {
        textMesh = gameObject.GetComponent<TextMesh>();
	}

	// Update is called once per frame
	void Update () {
        degree = gameObject.transform.rotation.eulerAngles.z;
        if (degree > 180f)
        {
            degree = 360 - degree;
        }
        display = degree.ToString("0");
        textMesh.text = display;
	}
}
