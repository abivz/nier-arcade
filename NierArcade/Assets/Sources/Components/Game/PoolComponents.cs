using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self, priority: 1)]
public sealed class PoolComponent : IComponent
{
    public int Id;
    public bool Active;
}