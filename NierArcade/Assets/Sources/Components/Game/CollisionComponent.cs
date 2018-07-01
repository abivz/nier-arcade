using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class CollisionComponent : IComponent
{
    public GameEntity Entity;
    public string Tag;
}
