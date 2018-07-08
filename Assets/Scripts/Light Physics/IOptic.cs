using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOptic {
	/**
	 * Interface from which Lens and Mirror implement.
	 * This is only to reduce repeated code primarily in BeamEmitter and elsewhere.
	 * NOTE: While a mirror technically "reflects" not "refracts", by implementing IOptic the mirror class's primary method is called Refract.
	 */
	Vector2 Refract(Vector2 hit, Vector2 incidenceDir, Vector2 normal);
}
