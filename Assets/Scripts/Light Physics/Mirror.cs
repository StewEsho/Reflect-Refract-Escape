using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mirror : MonoBehaviour, IOptic
{
    Vector2 centralAxis;
    float angle;
    Vector2 reflectionDir;
    float zRotation;

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
     * Returns the direction to cast a reflected light ray
     */
    public Vector2 Refract(Vector2 hit, Vector2 incidenceDir, Vector2 normal)
    {
        float crossSign = Mathf.Sign(Vector3.Cross(normal, incidenceDir).z);
        angle = (Mathf.PI) -
                Mathf.Acos(Vector2.Dot(normal, incidenceDir) / (normal.magnitude * incidenceDir.magnitude));
        angle *= crossSign;
        float reflection_x = normal.x * Mathf.Cos(angle) - normal.y * Mathf.Sin(angle);
        float reflection_y = normal.x * Mathf.Sin(angle) + normal.y * Mathf.Cos(angle);
        reflectionDir = new Vector2(reflection_x, reflection_y);

        return reflectionDir;
    }
}