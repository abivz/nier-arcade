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

    Contexts _contexts;

    void Awake()
    {
        _contexts = Contexts.sharedInstance;
    }

    void Update()
    {
        var moveHorizontal  = Input.GetAxis(_axisMoveHorizontal);
        var moveVertical    = Input.GetAxis(_axisMoveVertical);
        var rotationHorizontal = Input.GetAxis(_axisRotationHorizontal);
        var rotationVertical = Input.GetAxis(_axisRotationVertical);
        var fire = Input.GetAxis(_axisFire) >= 1f;

        SetInput(moveHorizontal, moveVertical, rotationHorizontal, rotationVertical, fire);
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
