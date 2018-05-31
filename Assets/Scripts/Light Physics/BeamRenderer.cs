using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class BeamRenderer : MonoBehaviour {

  public static Vector2 beamAStart, beamAEnd, beamBStart, beamBEnd;
  MeshFilter mf;

  void Start(){
    mf = GetComponent<MeshFilter>();
  }

	public void RenderLight (Vector2[] points) {
    Mesh light = createMeshFromPoints(points);
    light.name = "Light";
    mf.mesh = light;
	}

  void OnDrawGizmosSelected() {
    float radius = 0.1f;
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(beamAStart, radius);
    Gizmos.DrawWireSphere(beamBStart, radius);
    Gizmos.DrawWireSphere(beamAEnd, radius);
    Gizmos.DrawWireSphere(beamBEnd, radius);
  }

  public Mesh createMeshFromPoints(Vector2[] vertices2D){
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
