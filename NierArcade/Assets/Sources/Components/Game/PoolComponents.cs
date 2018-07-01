using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class PoolComponent : IComponent
{
    public int Id;
    public bool Active;
}