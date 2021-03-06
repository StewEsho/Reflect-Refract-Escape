﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class BeamEmitter : MonoBehaviour
{
    public static Vector2 beamAStart, beamAEnd, beamBStart, beamBEnd;
    public GameObject lightray;
    public bool IsDelayed = false;
    public bool IsActive = true;
    [Range(1, 12)] public int numOfBeams = 7;
    private List<GameObject> castLightrays = new List<GameObject>();
    protected bool isWon;
    private int num_hit;

    private Vector2 beamDirection;
    private Vector2 hitpoint, hitpointA, hitpointB;
    private MeshFilter mf;
    private Vector2[] beamOrigins;

    // Use this for initialization
    private void Start()
    {
        num_hit = 0;
        beamDirection = -transform.right;
        mf = GetComponent<MeshFilter>();
        isWon = false;
        beamOrigins = new Vector2[numOfBeams];
        for (var i = 0; i < numOfBeams; i++)
        {
            var spacing = (float) (i - numOfBeams / 2) / 10;
            var xSpace = spacing * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
            var ySpace = spacing * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            beamOrigins[i] = transform.position.V2().addX(xSpace).addY(ySpace);
            var newLight = Instantiate(lightray, beamOrigins[i], Quaternion.identity);
            newLight.GetComponent<LineRenderer>().SetPosition(0, beamOrigins[i]);
            castLightrays.Add(newLight);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsActive)
        {
            foreach (var ray in castLightrays)
            {
                ray.SetActive(true);
            }
        }
        else if (!IsActive)
        {
            foreach (var ray in castLightrays)
            {
                ray.SetActive(false);
            }
        }

        if (IsActive)
        {
            if (!IsDelayed)
            {
                EmitLight(beamDirection);
                // RenderLight(new Vector2[] {transform.position, transform.position.addX(-4), transform.position.addX(-4).addY(-4), transform.position.addY(-4)});
                num_hit = 0;
            }
            else
            {
                StartCoroutine(Pause());
            }
        }

        //TODO: Optimize the code after finishing up the entire system.
    }

    public List<Vector2> CalculateLightPoints(Vector2 origin, Vector2 dir)
    {
        var hit = Physics2D.Raycast(origin, dir, 30);
        var hitpoints = new List<Vector2>();
        hitpoints.Add(hit.point);

        if (hit.collider == null) return hitpoints;


        IOptic optic = hit.collider.gameObject.GetComponent<IOptic>();
        if (optic != null)
            if (hit.collider.transform.CompareTag("Ghost"))
            {
                //this object was a ghost, so continue to fire the light while rendering a ghost light
                hitpoints.AddRange(CalculateLightPoints(hit.point - 0.01f * hit.normal, dir));
                var ghostHit = Physics2D.Raycast(hit.point, optic.Refract(hit.point, dir, hit.normal), 30);
//                hit.collider.gameObject.GetComponent<LineRenderer>().SetPositions(new Vector3[] {hit.point, ghostHit.point});
                Debug.DrawLine(hit.point, ghostHit.point, Color.yellow);
            }
            else
            {
                if (optic is Lens)
                {
                    var newDir = optic.Refract(hit.point, dir, hit.normal);
                    var newHit = Physics2D.Raycast(hitpoint - 0.01f * hit.normal,
                        newDir, 1);
                    if (newHit.collider == null)
                    {
                        hitpoints.AddRange(CalculateLightPoints(newHit.point - 0.01f * hit.normal, newDir));
                    }
                    else
                    {
                        hitpoints.AddRange(CalculateLightPoints(hit.point - 0.01f * hit.normal,
                            optic.Refract(hit.point, dir, hit.normal)));
                    }
                }
                else if (optic is Mirror)
                {
                    hitpoints.AddRange(CalculateLightPoints(hit.point + 0.01f * hit.normal,
                        optic.Refract(hit.point, dir, hit.normal)));
                }
            }

        var switchBttn = hit.collider.gameObject.GetComponent<Switch>();
        if (switchBttn != null && gameObject.name != "Ghost") switchBttn.Activate();

        return hitpoints;
    }

    public void EmitLight(Vector2 dir)
    {
        foreach (var ray in castLightrays)
        {
            var points = CalculateLightPoints(ray.transform.position, dir);
            RenderLight(points, ray.GetComponent<LineRenderer>());
        }
    }

    //Obsolete (don't delete tho in case we ever need the code again ;) )
    public Mesh createMeshFromPoints(Vector2[] vertices2D)
    {
        var vertices = new Vector3[vertices2D.Length];
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices2D[i] = transform.InverseTransformPoint(vertices2D[i]);
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, -0.5f);
        }

        var tr = new Triangulator(vertices2D);
        var newTriangles = tr.Triangulate();
        var mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    public void RenderLight(List<Vector2> points, LineRenderer lr)
    {
        lr.positionCount = points.Count + 1;
        for (var i = 0; i < points.Count; i++) lr.SetPosition(i + 1, points[i]);
    }

    IEnumerator Pause()
    {
        foreach (var ray in castLightrays)
        {
            ray.SetActive(false);
        }

        yield return new WaitForSeconds(5);
        foreach (var ray in castLightrays)
        {
            ray.SetActive(true);
        }

        IsDelayed = false;
    }
}