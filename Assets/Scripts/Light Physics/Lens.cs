using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lens : MonoBehaviour
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

    /**
     * Returns the direction to cast a refracted light ray
     */
    public Vector2 Refract(Vector2 hit, Vector2 incidenceDir, Vector2 normal)
    {
        float crossSign = Mathf.Sign(Vector3.Cross(normal, incidenceDir).z);
        angle = (Mathf.PI) -
                Mathf.Acos(Vector2.Dot(normal, incidenceDir) / (normal.magnitude * incidenceDir.magnitude));
        angle *= crossSign * alternateAngleModifer;
        // alternateAngleModifer *= -1;
        float refraction_x = normal.x * Mathf.Cos(angle) - normal.y * Mathf.Sin(angle);
        float refraction_y = normal.x * Mathf.Sin(angle) + normal.y * Mathf.Cos(angle);
        refractionDir = -1 * new Vector2(refraction_x, refraction_y);
        return refractionDir;
    }
}