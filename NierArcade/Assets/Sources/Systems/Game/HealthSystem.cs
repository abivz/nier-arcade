using System.Collections.Generic;

using Entitas;

public class HealthSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _healthComponents;
    readonly List<GameEntity> _buffer;

    public HealthSystem(Contexts contexts, int bufferCapacity)
    {
        _healthComponents = contexts.game.GetGroup(GameMatcher.Health);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Execute()
    {
        foreach (var e in _healthComponents.GetEntities(_buffer))
            if (e.health.Value <= 0.0f)
                e.isDestroyed = true;
    }
}