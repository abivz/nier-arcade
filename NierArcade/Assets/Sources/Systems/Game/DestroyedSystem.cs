using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class DestroyedSystem : ICleanupSystem
{
    readonly IGroup<GameEntity> _destroyedComponents;
    readonly List<GameEntity> _buffer;

    public DestroyedSystem(Contexts contexts, int bufferCapacity)
    {
        _destroyedComponents = contexts.game.GetGroup(GameMatcher.Destroyed);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Cleanup()
    {
        foreach (var e in _destroyedComponents.GetEntities(_buffer))
        {
            if (e.hasView)
                Object.Destroy(e.view.Value.GetGameObject());
            
            e.Destroy();
        }
    }
}
