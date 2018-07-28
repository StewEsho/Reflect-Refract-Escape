using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 V2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    public static Vector3 V3(this Vector2 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }

    public static Vector2 addY(this Vector2 vector, float offset)
    {
        return new Vector2(vector.x, vector.y + offset);
    }

    public static Vector3 addY(this Vector3 vector, float offset)
    {
        return new Vector3(vector.x, vector.y + offset, vector.z);
    }

    public static Vector2 addX(this Vector2 vector, float offset)
    {
        return new Vector2(vector.x + offset, vector.y);
    }

    public static Vector3 addX(this Vector3 vector, float offset)
    {
        return new Vector3(vector.x + offset, vector.y, vector.z);
    }
    
    public static Vector3 addZ(this Vector3 vector, float offset)
    {
        return new Vector3(vector.x, vector.y, vector.z + offset);
    }

    public static Vector2 SetY(this Vector2 vector, float newValue)
    {
        return new Vector2(vector.x, newValue);
    }

    public static Vector3 SetY(this Vector3 vector, float newValue)
    {
        return new Vector3(vector.x, newValue, vector.z);
    }

    public static Vector2 SetX(this Vector2 vector, float newValue)
    {
        return new Vector2(newValue, vector.y);
    }

    public static Vector3 SetX(this Vector3 vector, float newValue)
    {
        return new Vector3(newValue, vector.y, vector.z);
    }
    
    public static Vector3 SetZ(this Vector3 vector, float newValue)
    {
        return new Vector3(vector.x, vector.y, newValue);
    }

    public static T Pop<T>(this List<T> list)
    {
        T item = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        return item;
    }
}