using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class InputSystem : IInitializeSystem, IExecuteSystem
{
    readonly InputContext _inputContext;
    readonly IGroup<GameEntity> _interactiveComponents;
    readonly List<GameEntity> _buffer;

    public InputSystem(Contexts contexts, int bufferCapacity)
    {
        _inputContext = contexts.input;
        _interactiveComponents = contexts.game.GetGroup(GameMatcher.Interactive);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Initialize()
    {
        _inputContext.SetInput(0f, 0f, 0f, 0f, false);
    }

    public void Execute()
    {
        foreach (var e in _interactiveComponents.GetEntities(_buffer))
        {
            var input = _inputContext.inputEntity.input;

            if (e.hasMove)
            {
                var move = e.move;
                e.ReplaceMove(move.MoveType, new Vector2(input.MoveHorizontal, input.MoveVertical), move.Speed);
            }

            if (e.hasRotation)
            {
                if (input.RotationHorizontal != 0.0f || input.RotationVertical != 0.0f)
                {
                    var direction = new Vector2(input.RotationHorizontal, input.RotationVertical).normalized;
                    e.ReplaceRotation(direction.GetAngleDeg());
                }
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
