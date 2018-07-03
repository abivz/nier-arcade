using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public sealed class WaveComponent : IComponent
{
    public float Radius;
    public float Speed;
}
