using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : BeamEmitter {

    public void ToggleFlashlight()
    {
        this.IsActive = !this.IsActive;
    }
}
