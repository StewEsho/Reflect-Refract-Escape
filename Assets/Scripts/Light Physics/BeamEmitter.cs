using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class BeamEmitter : MonoBehaviour
{
    public static Vector2 beamAStart, beamAEnd, beamBStart, beamBEnd;
    public GameObject lightray;
    [Range(1, 12)] public int numOfBeams = 7;
    private List<GameObject> current_light = new List<GameObject>();
    protected bool isWon;
    private int num_hit;

    Vector2 beamDirection;
    Vector2 hitpoint, hitpointA, hitpointB;
    MeshFilter mf;
    Vector2[] beamOrigins;

    // Use this for initialization
    void Start()
    {
        num_hit = 0;
        beamDirection = -transform.right;
        mf = GetComponent<MeshFilter>();
        isWon = false;
        beamOrigins = new Vector2[numOfBeams];
        for (int i = 0; i < numOfBeams; i++)
        {
            float spacing = (float) (i - (numOfBeams / 2)) / 10;
            float xSpace = spacing * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
            float ySpace = spacing * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            beamOrigins[i] = transform.position.V2().addX(xSpace).addY(ySpace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy the old lightrays

        EmitLight(beamOrigins, beamDirection);
        // RenderLight(new Vector2[] {transform.position, transform.position.addX(-4), transform.position.addX(-4).addY(-4), transform.position.addY(-4)});
        num_hit = 0;
    }

    public Vector2 EmitLight(Vector2 origin, Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, 30);
        hitpoint = hit.point;
        GameObject light_to_cast = Instantiate(lightray, transform.position, Quaternion.identity);
        light_to_cast.GetComponent<LineRenderer>().SetPosition(0, origin);
        light_to_cast.GetComponent<LineRenderer>().SetPosition(1, hitpoint);

        if (hit.collider != null)
        {
            Mirror mirror = hit.collider.gameObject.GetComponent<Mirror>();
            if (mirror != null)
            {
                mirror.ReflectLight(hitpoint, dir, hit.normal);
            }

            Lens lens = hit.collider.gameObject.GetComponent<Lens>();
            if (lens != null)
            {
                lens.RefractLight(hitpoint, dir, hit.normal);
            }

            Switch switchBttn = hit.collider.gameObject.GetComponent<Switch>();
            if (switchBttn != null)
            {
                if (gameObject.name != "Ghost")
                {
                    switchBttn.Lightup();
                }

            }
        }

        return hitpoint;
    }

    public void EmitLight(Vector2[] origins, Vector2 dir)
    {
        int counter = 0;
        List<Vector2> pointsToRender = new List<Vector2>();
        foreach (Vector2 origin in origins)
        {
            pointsToRender.Add(origin);
            pointsToRender.Add(EmitLight(origin, dir));
            counter += 1;
        }

        RenderLight(pointsToRender.ToArray());
    }

    public Mesh createMeshFromPoints(Vector2[] vertices2D)
    {
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices2D[i] = transform.InverseTransformPoint(vertices2D[i]);
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, -0.5f);
        }

        Triangulator tr = new Triangulator(vertices2D);
        int[] newTriangles = tr.Triangulate();
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    public void RenderLight(Vector2[] points)
    {
        Mesh light = createMeshFromPoints(points);
        light.name = "Light";
        mf.mesh = light;
    }
}