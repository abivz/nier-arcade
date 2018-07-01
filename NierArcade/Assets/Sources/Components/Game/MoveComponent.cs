using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public sealed class MoveComponent : IComponent
{
    public float X;
    public float Y;
    public float Z;
    public float Speed;
}
