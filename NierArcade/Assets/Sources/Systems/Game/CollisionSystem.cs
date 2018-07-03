using System.Collections.Generic;

using Entitas;

public class CollisionSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    readonly IGroup<GameEntity> _collisionComponents;
    readonly List<GameEntity> _buffer;

    public CollisionSystem(Contexts contexts, int bufferCapacity) : base(contexts.game)
    {
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

            if (collision.Entity == null)
                continue;

            var view = collision.Entity.view.View;

            if (collision.Entity.hasPool)
            {
                view.GetGameObject().SetActive(false);
                collision.Entity.ReplacePool(collision.Entity.pool.Id, false);
            }

            if (collision.Tag == GameObjectTag.Tag_Bullet)
            {
                if (collision.Entity.hasHealth && !collision.Entity.hasShield)
                {
                    collision.Entity.ReplaceHealth(collision.Entity.health.Health - 1);

                    if (collision.Entity.isPlayer)
                    {
                        
                    }
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
