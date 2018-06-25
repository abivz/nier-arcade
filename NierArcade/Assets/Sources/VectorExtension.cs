using UnityEngine;

public static class Vector2Extension
{
    public static float GetAngleDeg(this Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}