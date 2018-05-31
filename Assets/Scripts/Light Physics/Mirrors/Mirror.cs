using UnityEngine;

public interface  Mirror {
  void ReflectLight(Vector2 hit, Vector2 incidenceDir);
  void ReflectLight(Vector2 hitA, Vector2 hitB, Vector2 incidenceDir);
}
