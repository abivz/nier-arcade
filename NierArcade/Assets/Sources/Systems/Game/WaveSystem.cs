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
            var wave = e.wave;

            var val = wave.Value + wave.Speed * Time.deltaTime;

            e.ReplaceWave(val, wave.Radius, wave.Speed);

            if (val >= wave.Radius)
                e.isDestroyed = true;
            else if (e.hasScale)
                e.ReplaceScale(Vector3.one * val);
        }
    }
}
