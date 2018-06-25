using UnityEngine;

public class InputService : CachedMonoBehaviour, IInputListener
{
    [SerializeField]
    Vector3 _moveDrawOffset = Vector3.zero;

    [SerializeField]
    float _moveDrawMultiply = 1f;

    [SerializeField]
    Color _moveDrawColor = Color.green;

    [SerializeField]
    Vector3 _rotationDrawOffset = Vector3.zero;

    [SerializeField]
    float _rotationDrawMultiply = 1f;

    [SerializeField]
    Color _rotationDrawColor = Color.red;

    float _moveHorizontal, _moveVertical, _rotationHorizontal, _rotationVertical;

    bool _fire;

    void Awake()
    {
        Contexts.sharedInstance.input.CreateEntity().AddInputListener(this);
    }

    void Update()
    {
        var moveOffset = cachedTransform.position + _moveDrawOffset;
        Debug.DrawLine(moveOffset, moveOffset + (Vector3.right * _moveHorizontal * _moveDrawMultiply), _moveDrawColor);
        Debug.DrawLine(moveOffset, moveOffset + (Vector3.forward * _moveVertical * _moveDrawMultiply), _moveDrawColor);

        var rotationOffset = cachedTransform.position + _rotationDrawOffset;
        Debug.DrawLine(rotationOffset, rotationOffset + (Vector3.right * _rotationHorizontal * _rotationDrawMultiply), _rotationDrawColor);
        Debug.DrawLine(rotationOffset, rotationOffset + (Vector3.forward * _rotationVertical * _rotationDrawMultiply), _rotationDrawColor);
    }

    public void OnInput(InputEntity entity, float MoveHorizontal, float MoveVertical, float RotationHorizontal, float RotationVertical, bool Fire)
    {
        _moveHorizontal = MoveHorizontal;
        _moveVertical = MoveVertical;
        _rotationHorizontal = RotationHorizontal;
        _rotationVertical = RotationVertical;
        _fire = Fire;
    }
}
