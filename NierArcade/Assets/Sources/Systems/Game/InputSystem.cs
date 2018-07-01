using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class InputSystem : IInitializeSystem, IExecuteSystem
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _interactiveComponents;
    readonly List<GameEntity> _buffer;

    public InputSystem(Contexts contexts, int bufferCapacity)
    {
        _gameContext = contexts.game;
        _interactiveComponents = _gameContext.GetGroup(GameMatcher.Interactive);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Initialize()
    {
        _gameContext.SetInput(0f, 0f, 0f, 0f, false);
    }

    public void Execute()
    {
        foreach (var e in _interactiveComponents.GetEntities(_buffer))
        {
            var input = _gameContext.inputEntity.input;
            var view = e.view.View;

            if (input.RotationHorizontal != 0.0f || input.RotationVertical != 0.0f)
            {
                var direction = new Vector2(input.RotationHorizontal, input.RotationVertical).normalized;
                view.SetRotation(Quaternion.AngleAxis(direction.GetAngleDeg() - 90, Vector3.forward));
            }

            if (e.hasMove)
            {
                var move = e.move;
                e.ReplaceMove(input.MoveHorizontal, input.MoveVertical, 0f, move.Speed);
            }

            if (e.hasWeapon)
            {
                var weapon = e.weapon;

                if (input.Fire != weapon.Active)
                    weapon.Active = input.Fire;
            }
        }
    }
}
