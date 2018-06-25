using System;

using UnityEngine;

[Serializable]
public class Gun
{
    public int PoolId;
    public bool Active;
    public float IntervalInS;
    public float Speed;
    public Vector2 Direction;
    float _lastShootTime;
    public void SetLastShootTime(float lastShootTime)
    {
        _lastShootTime = lastShootTime;
    }

    public float GetLastShootTime()
    {
        return _lastShootTime;
    }
}