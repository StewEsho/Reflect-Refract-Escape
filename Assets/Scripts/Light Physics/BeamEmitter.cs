using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEmitter : MonoBehaviour {

  public static Vector2 beamAStart, beamAEnd, beamBStart, beamBEnd;
  Vector2 beamDirection;

	// Use this for initialization
	void Start () {
    beamDirection = new Vector2(4, 0);
	}

	// Update is called once per frame
	void Update () {
    beamAStart = new Vector2(transform.position.x + 0, transform.position.y + 0);
    beamAEnd = new Vector2(transform.position.x + 4, transform.position.y + 0);
    beamBStart = new Vector2(transform.position.x + 0, transform.position.y - 1);
    beamBEnd = new Vector2(transform.position.x + 4, transform.position.y - 1);

    Debug.DrawRay(beamAStart, beamDirection, Color.white);
    Debug.DrawRay(beamBStart, beamDirection, Color.white);
	}
}
