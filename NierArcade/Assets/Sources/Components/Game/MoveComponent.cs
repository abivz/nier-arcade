using UnityEngine;

using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class MoveComponent : IComponent
{
    public MoveType MoveType;
    public Vector3 Direction;
    public float Speed;
}
