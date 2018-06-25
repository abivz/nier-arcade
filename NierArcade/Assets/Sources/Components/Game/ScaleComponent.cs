using UnityEngine;

using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self, priority: 1)]
public sealed class ScaleComponent : IComponent
{
    public Vector3 Value;
}
