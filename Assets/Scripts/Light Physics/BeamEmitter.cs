using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BeamRenderer))]
public class BeamEmitter : MonoBehaviour {

  public static Vector2 beamAStart, beamAEnd, beamBStart, beamBEnd;
  Vector2 beamDirection;
  Vector2 hitpoint, hitpointA, hitpointB;
  BeamRenderer bm;

	// Use this for initialization
	void Start () {
    beamDirection = new Vector2(4, 0);
    bm = GetComponent<BeamRenderer>();
	}

	// Update is called once per frame
	void Update () {
    // beamAStart = new Vector2(transform.position.x + 0, transform.position.y + 0);
    // beamAEnd = new Vector2(transform.position.x + 4, transform.position.y + 0);
    // beamBStart = new Vector2(transform.position.x + 0, transform.position.y - 1);
    // beamBEnd = new Vector2(transform.position.x + 4, transform.position.y - 1);
    // Vector2[] vertices = {beamAStart, beamAEnd, beamBEnd, beamBStart};
    //
    EmitLight(new Vector2[] {transform.position, new Vector2(transform.position.x, transform.position.y - 0.1f)}, - Vector2.right);
	}

  void OnDrawGizmosSelected() {
    float radius = 0.5f;
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(hitpoint, radius);
  }

  public void EmitLight(Vector2 origin, Vector2 dir){


    // Debug.DrawRay(beamAStart, beamDirection, Color.white);
    // Debug.DrawRay(beamBStart, beamDirection, Color.white);
    RaycastHit2D hit = Physics2D.Raycast(origin, dir, 30);
    if (hit.collider != null) {
      hitpoint = hit.point;
      bm.RenderLight(new Vector2[] {origin, hitpoint, new Vector2(0, 0), new Vector2(0.1f, 0.1f)});
      // Debug.Log("Noice");
      FlatMirror mirror = hit.collider.gameObject.GetComponent<FlatMirror>();
      if(mirror != null){
        mirror.ReflectLight(hitpoint, dir);
      }
    }
  }

  public void EmitLight(Vector2 [] origins, Vector2 dir){
    foreach (Vector2 origin in origins){
      RaycastHit2D hit = Physics2D.Raycast(origin, dir, 30);
      bool hasHit = false;

      if (hit.collider != null ){
        hasHit = true;
        hitpoint = hit.point;
        Debug.DrawLine(origin, hitpoint);
        Mirror mirror = hit.collider.gameObject.GetComponent<Mirror>();
        if(mirror != null){
          mirror.ReflectLight(hitpoint, dir);
        }
      }
    }
    // RaycastHit2D hitA = Physics2D.Raycast(originA, dir, 30);
    // RaycastHit2D hitB = Physics2D.Raycast(originB, dir, 30);
    // bool hasAHit, hasBHit = false;
    //
    // if (hitA.collider != null ){
    //   hasAHit = true;
    //   hitpointA = hitA.point;
    //   Debug.DrawLine(originA, hitpointA);
    //   Mirror mirror = hitA.collider.gameObject.GetComponent<Mirror>();
    //   if(mirror != null){
    //     mirror.ReflectLight(hitpointA, dir);
    //   }
    // }
    // if (hitB.collider != null ){
    //   hasBHit = true;
    //   hitpointB = hitB.point;
    //   Debug.DrawLine(originB, hitpointB);
    //   Mirror mirror = hitB.collider.gameObject.GetComponent<Mirror>();
    //   if(mirror != null){
    //     mirror.ReflectLight(hitpointB, dir);
    //   }
    // }
    // if (hitA.collider != null && hitB.collider != null) {
    //
    //   bm.RenderLight(new Vector2[] {originA, originB, hitpointB, hitpointA});
    // }
  }
}
