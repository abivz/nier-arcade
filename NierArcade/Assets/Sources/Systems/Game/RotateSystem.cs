using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class RotateSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _rotateComponents;
    readonly List<GameEntity> _buffer;

    public RotateSystem(Contexts contexts, int bufferCapacity)
    {
        _rotateComponents = contexts.game.GetGroup(GameMatcher.Rotate);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Execute()
    {
        foreach (var e in _rotateComponents.GetEntities(_buffer))
        {
            if (!e.hasView)
                continue;

            var view = e.view.View;
            if (!view.GetGameObject().activeSelf)
                continue;

            var rotate = e.rotate;
            var rotation = view.GetRotation().eulerAngles;
            view.SetRotation(Quaternion.Euler(rotation.x, rotation.y, rotation.z + rotate.Speed * Time.deltaTime));
        }
    }
}
