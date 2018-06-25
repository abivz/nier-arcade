using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class EnemySystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _enemyComponents;
    readonly IGroup<GameEntity> _playerComponents;
    readonly List<GameEntity> _enemyBuffer;
    readonly List<GameEntity> _playerBuffer;

    public EnemySystem(Contexts contexts, int bufferCapacity)
    {
        _enemyComponents = contexts.game.GetGroup(GameMatcher.Enemy);
        _enemyBuffer = new List<GameEntity>(bufferCapacity);

        _playerComponents = contexts.game.GetGroup(GameMatcher.Player);
        _playerBuffer = new List<GameEntity>(bufferCapacity);
    }

    public void Execute()
    {
        foreach (var enemyEntity in _enemyComponents.GetEntities(_enemyBuffer))
        {
            if ( ! enemyEntity.hasView)
                continue;

            var enemyEntityView = enemyEntity.view.Value;

            GameEntity player = null;
            float distance = 100f;

            foreach (var playerEntity in _playerComponents.GetEntities(_playerBuffer))
            {
                var playerEntityView = playerEntity.view.Value;

                var d = Vector3.Distance(enemyEntityView.GetPosition(), playerEntityView.GetPosition());
                if (d < distance)
                {
                    player = playerEntity;
                    distance = d;
                }
                    
            }

            if (player == null)
                return;

            if (enemyEntity.hasMove)
            {
                var move = enemyEntity.move;
                enemyEntity.ReplaceMove(move.MoveType, (player.view.Value.GetPosition() - enemyEntityView.GetPosition()).normalized, move.Speed);
            }

            if (enemyEntity.hasRotation)
            {
                var playerPosition = player.view.Value.GetPosition();
                var enemyPosition = enemyEntity.view.Value.GetPosition();

                var dx = playerPosition.x - enemyPosition.x;
                var dy = playerPosition.y - enemyPosition.y;
                var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg - 90;

                enemyEntity.ReplaceRotation(Mathf.Lerp(enemyEntity.rotation.Angle, angle, 3f * Time.deltaTime));
            }
        }
    }
}
