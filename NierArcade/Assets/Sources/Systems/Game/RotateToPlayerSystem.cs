using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class RotateToPlayerSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _rotateToPlayerComponents;
    readonly IGroup<GameEntity> _playerComponents;
    readonly List<GameEntity> _rotateToPlayerBuffer;
    readonly List<GameEntity> _playerBuffer;

    public RotateToPlayerSystem(Contexts contexts, int bufferCapacity)
    {
        _rotateToPlayerComponents = contexts.game.GetGroup(GameMatcher.RotateToPlayer);
        _rotateToPlayerBuffer = new List<GameEntity>(bufferCapacity);

        _playerComponents = contexts.game.GetGroup(GameMatcher.Player);
        _playerBuffer = new List<GameEntity>(bufferCapacity);
    }

    public void Execute()
    {
        foreach (var entity in _rotateToPlayerComponents.GetEntities(_rotateToPlayerBuffer))
        {
            if (!entity.hasView)
                continue;

            var entityView = entity.view.View;

            GameEntity player = null;
            float distance = 100f;

            foreach (var playerEntity in _playerComponents.GetEntities(_playerBuffer))
            {
                var playerEntityView = playerEntity.view.View;

                var d = Vector3.Distance(entityView.GetPosition(), playerEntityView.GetPosition());
                if (d < distance)
                {
                    player = playerEntity;
                    distance = d;
                }

            }

            if (player == null)
                continue;

            var rotateToPlayer = entity.rotateToPlayer;

            var playerPosition = player.view.View.GetPosition();
            var entityPosition = entity.view.View.GetPosition();

            var dx = playerPosition.x - entityPosition.x;
            var dy = playerPosition.y - entityPosition.y;
            var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg - 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;

            var entityRotation = entityView.GetRotation().eulerAngles;
            entityView.SetRotation(Quaternion.Euler(entityRotation.x, entityRotation.y, Mathf.Lerp(entityRotation.z, rotation.z, rotateToPlayer.Speed * Time.deltaTime)));
        }
    }
}
