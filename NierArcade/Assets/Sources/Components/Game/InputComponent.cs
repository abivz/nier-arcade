using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique, Event(EventTarget.Any)]
public sealed class InputComponent : IComponent
{
    public float MoveHorizontal;
    public float MoveVertical;
    public float RotationHorizontal;
    public float RotationVertical;
    public bool Fire;
}
