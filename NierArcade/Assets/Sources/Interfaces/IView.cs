using UnityEngine;

using Entitas;

public interface IView
{
    void Link(IEntity entity, IContext context);
    GameObject GetGameObject();
    void SetPosition(Vector3 position);
    Vector3 GetPosition();
    void SetRotation(Quaternion rotation);
    Quaternion GetRotation();
    Rigidbody2D GetRigidbody2D();
}
