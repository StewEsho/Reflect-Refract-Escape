using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusSwitch : Switch
{

    private int numberOfHits;
    public int threshold = 5;

    void Update()
    {
        lightsource.intensity = numberOfHits * 5;
        isActive = numberOfHits > threshold;
    }

    void LateUpdate()
    {
        numberOfHits = 0;
    }

    // Use this for initialization
    /** Overrides Switch.Activate */
    public override void Activate()
    {
        numberOfHits++;
    }
}
