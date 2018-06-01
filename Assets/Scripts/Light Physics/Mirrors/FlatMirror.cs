﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BeamRenderer))]
public class FlatMirror : BeamEmitter, Mirror {

  Vector2 normal;
  float angle;
  Vector2 reflectionDir;
  float zRotation;

  public void Start(){
    normal = new Vector2 (0, 0);
    angle = 0;
    zRotation = 0;
  }

  public void Update(){
    zRotation = transform.eulerAngles.z;
    normal = new Vector2 (Mathf.Cos(Mathf.Deg2Rad * zRotation), Mathf.Sin(Mathf.Deg2Rad * zRotation));
    if (isWon){
      Debug.Log(SceneManager.sceneCount);
      if (Input.GetKeyDown(KeyCode.K)){
        SceneManager.LoadScene(Random.Range(0, SceneManager.sceneCountInBuildSettings));
      }
    }
  }

  public void ReflectLight(Vector2 hit, Vector2 incidenceDir){
    float crossSign = 0;
    if (Vector2.Dot(normal, incidenceDir) < 0){
      crossSign = Mathf.Sign(Vector3.Cross(normal, incidenceDir).z);
      angle = (Mathf.PI) - Mathf.Acos(Vector2.Dot(normal, incidenceDir) / (normal.magnitude * incidenceDir.magnitude));
      angle *= crossSign;
      float reflection_x = normal.x * Mathf.Cos(angle) - normal.y * Mathf.Sin(angle);
      float reflection_y = normal.x * Mathf.Sin(angle) + normal.y * Mathf.Cos(angle);
      reflectionDir = new Vector2 (reflection_x, reflection_y);
      EmitLight(hit + (0.01f * normal), reflectionDir);
    }
  }
}