using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods{

	public static Vector2 V2(this Vector3 vector){
    return new Vector2(vector.x, vector.y);
  }

  public static Vector3 V3(this Vector2 vector){
    return new Vector3(vector.x, vector.y, 0);
  }

  public static T Pop<T>(this List<T> list) {
    T r = list[list.Count - 1];
    list.RemoveAt(list.Count - 1);
    return r;
  }
}
