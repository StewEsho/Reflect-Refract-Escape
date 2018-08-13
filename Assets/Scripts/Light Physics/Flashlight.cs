using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : BeamEmitter {

    public void ToggleFlashlight()
    {
        this.IsActive = !this.IsActive;
    }

    public void Update()
    {
        base.Update();
        
        Vector2 orthogonalBeamDirection = Vector2.Perpendicular(beamDirection).normalized;
        for (var i = 0; i < NumberOfBeams; i++)
        {
            var spacing = (float) (i - NumberOfBeams / 2) / 10;
            
            beamOrigins[i] = transform.position.V2() + (spacing * orthogonalBeamDirection);
            castLightrays[i].GetComponent<LineRenderer>().SetPosition(0, beamOrigins[i]);
            castLightrays[i].transform.position = beamOrigins[i];
        }
    }
}
