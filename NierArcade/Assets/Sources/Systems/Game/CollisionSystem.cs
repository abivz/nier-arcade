using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class CollisionSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _collisionComponents;
    readonly List<GameEntity> _buffer;

    public CollisionSystem(Contexts contexts, int bufferCapacity) : base(contexts.game)
    {
        _contexts = contexts;
        _collisionComponents = contexts.game.GetGroup(GameMatcher.Collision);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Collision);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCollision;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var collision = e.collision;

            if (collision.Entity.hasPool)
                collision.Entity.ReplacePool(collision.Entity.pool.Id, false);

            
            if (collision.Tag.Equals(ArcadeGameObjectTags.Bullet.ToString()))
            {
                if (collision.Entity.hasHealth)
                {
                    collision.Entity.ReplaceHealth(collision.Entity.health.Value - 1);
                    // if (collision.Entity.isPlayer)
                    // {
                    //     var wave = _contexts.game.CreateEntity();
                    //     wave.AddAsset("PlayerWave", AssetSource.Resources);
                    //     wave.AddPosition(collision.Entity.view.Value.GetPosition());
                    //     wave.AddScale(Vector3.one / 2f);
                    //     wave.AddWave(0.5f, 5f, 12f);
                    // }
                }
            }
        }
    }

    public void Cleanup()
    {
        foreach (var e in _collisionComponents.GetEntities(_buffer))
            e.Destroy();
    }
}
