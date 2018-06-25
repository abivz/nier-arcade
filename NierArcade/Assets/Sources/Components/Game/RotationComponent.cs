using UnityEngine;

using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self, priority: 1)]
public sealed class RotationComponent : IComponent
{
    public float Angle;
}
