using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class BeamRenderer : MonoBehaviour {

  public static Vector2 beamAStart, beamAEnd, beamBStart, beamBEnd;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void LateUpdate () {
    beamAStart = BeamEmitter.beamAStart;
    beamAEnd = BeamEmitter.beamAEnd;
    beamBStart = BeamEmitter.beamBStart;
    beamBEnd = BeamEmitter.beamBEnd;
    GetComponent<MeshFilter>().mesh = createLightMesh(new Vector2[] {beamAStart, beamAEnd, beamBStart});
	}

  void OnDrawGizmosSelected() {
    float radius = 0.1f;
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(beamAStart, radius);
    Gizmos.DrawWireSphere(beamBStart, radius);
    Gizmos.DrawWireSphere(beamAEnd, radius);
    Gizmos.DrawWireSphere(beamBEnd, radius);
  }

  public Mesh createLightMesh(Vector2[] vertices2D){
    Vector3[] vertices =  new Vector3[vertices2D.Length];;
    for(int i = 0; i < vertices.Length; i++){
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
}
