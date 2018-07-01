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
            if ( ! e.hasView)
                continue;

            var view = e.view.View;
            if ( ! view.GetGameObject().activeSelf)
                continue;

            var move = e.move;
            var rb2d = view.GetRigidbody2D();
            rb2d.velocity = new Vector2(move.X, move.Y) * move.Speed;
        }
    }
}
