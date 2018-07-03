using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class WaveSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _waveComponents;
    readonly List<GameEntity> _buffer;

    public WaveSystem(Contexts contexts, int bufferCapacity)
    {
        _waveComponents = contexts.game.GetGroup(GameMatcher.Wave);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Execute()
    {
        foreach (var e in _waveComponents.GetEntities(_buffer))
        {
            if ( ! e.hasView)
                continue;
            
            var wave = e.wave;
            var view = e.view.View;

            var scale = view.GetGameObject().transform.localScale.x * wave.Speed * Time.deltaTime;

            if (scale >= wave.Radius)
                e.isDestroyed = true;
            else
                view.GetGameObject().transform.localScale += new Vector3(scale, scale);
        }
    }
}
