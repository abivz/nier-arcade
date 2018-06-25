using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class MoveSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _moveComponents;
    readonly List<GameEntity> _buffer;

    public MoveSystem(Contexts contexts, int bufferCapacity)
    {
        _moveComponents = contexts.game.GetGroup(GameMatcher.Move);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Execute()
    {
        foreach (var e in _moveComponents.GetEntities(_buffer))
        {
            if (e.hasPool)
                if ( ! e.pool.Active)
                    continue;

            var move = e.move;

            if (move.MoveType == MoveType.Position)
            {
                var position = e.position.Value;

                var vector = move.Direction * move.Speed * Time.deltaTime;
                e.ReplacePosition(position + vector);
            }
            else if (move.MoveType == MoveType.Velocity)
            {
                var vector = move.Direction * move.Speed;
                e.ReplaceVelocity(vector);
            }
        }
    }
}
