using UnityEngine;

using Entitas;

public interface IView
{
    void Link(IEntity entity, IContext context);
    GameObject GetGameObject();
    Vector3 GetPosition();
    Vector3 GetRotation();
    Vector3 GetScale();
}
