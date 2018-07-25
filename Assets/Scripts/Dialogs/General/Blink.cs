using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{

    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        StartBlinking();
    }


    IEnumerator Blinking()
    {
        while (true)
        {
            switch (image.color.a.ToString())
            {
                case "0":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine("Blinking");
    }

    void StopBlinking()
    {
        StopAllCoroutines();
    }

}

