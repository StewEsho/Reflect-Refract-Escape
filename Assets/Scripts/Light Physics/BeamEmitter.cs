﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeamEmitter : MonoBehaviour
{
    public GameObject Lightray;
    public bool IsDelayed = false;
    public bool IsActive = true;
    protected bool WasActive = true;
    [Range(1, 12)] public int NumberOfBeams = 3;
    protected List<GameObject> castLightrays = new List<GameObject>();
    protected bool isWon;
    protected int num_hit;

    protected Vector2 beamDirection;
    protected Vector2 hitpoint, hitpointA, hitpointB;
    protected MeshFilter mf;
    protected Vector2[] beamOrigins;

    // Use this for initialization
    protected void Start()
    {
        num_hit = 0;
        beamDirection = -transform.right;
        mf = GetComponent<MeshFilter>();
        isWon = false;
        beamOrigins = new Vector2[NumberOfBeams];
        for (var i = 0; i < NumberOfBeams; i++)
        {
            var spacing = (float) (i - NumberOfBeams / 2) / 10;
            var xSpace = spacing * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
            var ySpace = spacing * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            beamOrigins[i] = transform.position.V2().addX(xSpace).addY(ySpace);
            var newLight = Instantiate(Lightray, beamOrigins[i], Quaternion.identity, transform);
            newLight.GetComponent<LineRenderer>().SetPosition(0, beamOrigins[i]);
            castLightrays.Add(newLight);
        }

        IsActive = !IsDelayed;
        WasActive = IsActive;
    }

    // Update is called once per frame
    protected void Update()
    {
        //basically copy the same code from start(), 
        //if we want the light source to be carryable we have to recalculate the starting position as well as the light direction for every frame.
        beamDirection = -transform.right;
        for (var i = 0; i < NumberOfBeams; i++)
        {
            var spacing = (float)(i - NumberOfBeams / 2) / 10;
            var xSpace = spacing * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
            var ySpace = spacing * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            beamOrigins[i] = transform.position.V2().addX(xSpace).addY(ySpace);
            castLightrays[i].GetComponent<LineRenderer>().SetPosition(0, beamOrigins[i]);
            castLightrays[i].transform.position = beamOrigins[i];
        }
        if (IsActive != WasActive)
        {
            if (IsActive)
            {
                foreach (var ray in castLightrays)
                {
                    ray.SetActive(true);
                }
            }
            else
            {
                foreach (var ray in castLightrays)
                {
                    ray.SetActive(false);
                }
            }

            WasActive = IsActive;
        }

        if (IsActive)
        {
            if (!IsDelayed)
            {
                EmitLight(beamDirection);
                num_hit = 0;
            }
            else
            {
                StartCoroutine(Pause());
            }
        }
    }

    protected List<Vector2> CalculateLightPoints(Vector2 origin, Vector2 dir)
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

    protected void EmitLight(Vector2 dir)
    {
        foreach (var ray in castLightrays)
        {
            var points = CalculateLightPoints(ray.transform.position, dir);
            RenderLight(points, ray.GetComponent<LineRenderer>());
        }
    }

    protected void RenderLight(List<Vector2> points, LineRenderer lr)
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