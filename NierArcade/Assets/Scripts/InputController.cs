using UnityEngine;

using Entitas;

public class InputController : MonoBehaviour
{
    [SerializeField]
    string _axisMoveHorizontal;

    [SerializeField]
    string _axisMoveVertical;

    [SerializeField]
    string _axisRotationHorizontal;

    [SerializeField]
    string _axisRotationVertical;

    [SerializeField]
    string _axisFire;

    [SerializeField]
    float _rotationLerpMultiplier = 1f;

    Contexts _contexts;

    float _moveHorizontal, _moveVertical, _rotationHorizontal, _rotationVertical;

    void Awake()
    {
        _contexts = Contexts.sharedInstance;
    }

    void Update()
    {
        _moveHorizontal  = Input.GetAxis(_axisMoveHorizontal);
        _moveVertical    = Input.GetAxis(_axisMoveVertical);
        _rotationHorizontal = Mathf.Lerp(_rotationHorizontal, Input.GetAxis(_axisRotationHorizontal), _rotationLerpMultiplier * Time.deltaTime);
        _rotationVertical = Mathf.Lerp(_rotationVertical, Input.GetAxis(_axisRotationVertical), _rotationLerpMultiplier * Time.deltaTime);
        var fire = Input.GetAxis(_axisFire) >= 1f;

        var normalizedRotation = new Vector2(_rotationHorizontal, _rotationVertical).normalized;
        SetInput(_moveHorizontal, _moveVertical, normalizedRotation.x, normalizedRotation.y, fire);
    }

    void SetInput(float MoveHorizontal, float MoveVertical,
                           float RotationHorizontal = 0f, float RotationVertical = 0f,
                           bool Fire = false)
    {
        _contexts.game.ReplaceInput(MoveHorizontal, MoveVertical,
                                     RotationHorizontal, RotationVertical,
                                     Fire);
    }
}
