using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Entitas;

public class GameplaySystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _coreComponents;
    readonly IGroup<GameEntity> _enemyComponents;
    readonly IGroup<GameEntity> _playerComponents;
    readonly List<GameEntity> _buffer;

    public GameplaySystem(Contexts contexts, int bufferCapacity)
    {
        _coreComponents = contexts.game.GetGroup(GameMatcher.Core);
        _enemyComponents = contexts.game.GetGroup(GameMatcher.Enemy);
        _playerComponents = contexts.game.GetGroup(GameMatcher.Player);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Execute()
    {
        var playerCount = _playerComponents.GetEntities(_buffer).Count;

        var enemyCount = _enemyComponents.GetEntities(_buffer).Count;

        var cores = _coreComponents.GetEntities(_buffer);

        foreach (var core in cores)
        {
            if (enemyCount == 0)
            {
                if (core.hasShield)
                    core.RemoveShield();
            }
        }
    }
}