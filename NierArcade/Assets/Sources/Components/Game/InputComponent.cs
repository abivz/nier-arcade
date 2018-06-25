using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input, Unique, Event(EventTarget.Self, priority: 1)]
public sealed class InputComponent : IComponent
{
    public float MoveHorizontal;
    public float MoveVertical;
    public float RotationHorizontal;
    public float RotationVertical;
    public bool Fire;
}
