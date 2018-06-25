using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self, priority: 0)]
public sealed class PositionComponent : IComponent
{
    public UnityEngine.Vector3 Value;
}
