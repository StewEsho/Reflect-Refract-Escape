using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lens : BeamEmitter
{
    Vector2 centralAxis;
    float angle;
    Vector2 refractionDir;
    float zRotation;
    int alternateAngleModifer = 1; //alternates between 1 and -1 each hit.

    public void Start()
    {
        centralAxis = new Vector2(0, 0);
        angle = 0;
        zRotation = 0;
    }

    public void Update()
    {
        zRotation = transform.eulerAngles.z;
        centralAxis = new Vector2(Mathf.Cos(Mathf.Deg2Rad * zRotation), Mathf.Sin(Mathf.Deg2Rad * zRotation));
    }

    public void RefractLight(Vector2 hit, Vector2 incidenceDir, Vector2 normal)
    {
        float crossSign = Mathf.Sign(Vector3.Cross(normal, incidenceDir).z);
        angle = (Mathf.PI) -
                Mathf.Acos(Vector2.Dot(normal, incidenceDir) / (normal.magnitude * incidenceDir.magnitude));
        angle *= crossSign * alternateAngleModifer;
        // alternateAngleModifer *= -1;
        float refraction_x = normal.x * Mathf.Cos(angle) - normal.y * Mathf.Sin(angle);
        float refraction_y = normal.x * Mathf.Sin(angle) + normal.y * Mathf.Cos(angle);
        refractionDir = -1 * new Vector2(refraction_x, refraction_y);

        EmitLight(hit - 0.1f * normal, refractionDir);
        //Cast ray across lens for output light
        // RaycastHit2D crossLensRaycast = Physics2D.Raycast(hit, incidenceDir, 1);
        // if (crossLensRaycast.collider != null){
        //   EmitLight(crossLensRaycast.point.addX(-0.1f), refractionDir);
        // } else {
        //   EmitLight(hit + incidenceDir, incidenceDir);
        // }
        // }
        // float crossSign = 0;
        // if (Vector2.Dot(centralAxis, incidenceDir) < 0){
        //   crossSign = Mathf.Sign(Vector3.Cross(centralAxis, incidenceDir).z);
        // angle = (Mathf.PI) - Mathf.Acos(Vector2.Dot(centralAxis, incidenceDir) / (centralAxis.magnitude * incidenceDir.magnitude));
        // angle *= crossSign;
        // float reflection_x = centralAxis.x * Mathf.Cos(angle) - centralAxis.y * Mathf.Sin(angle);
        // float reflection_y = centralAxis.x * Mathf.Sin(angle) + centralAxis.y * Mathf.Cos(angle);
        // refractionDir = -1 * new Vector2 (reflection_x, reflection_y);
        //   EmitLight(hit + (0.01f * centralAxis), refractionDir);
        // }
    }
}