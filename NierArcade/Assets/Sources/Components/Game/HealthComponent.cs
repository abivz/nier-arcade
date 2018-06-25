using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self, priority: 1)]
public sealed class HealthComponent : IComponent
{
    public float Value;
}
