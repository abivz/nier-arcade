using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class ShieldSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _shieldComponents;
    readonly List<GameEntity> _buffer;

    public ShieldSystem(Contexts contexts, int bufferCapacity)
    {
        _shieldComponents = contexts.game.GetGroup(GameMatcher.Shield);
        _buffer = new List<GameEntity>(bufferCapacity);
    }

    public void Execute()
    {
        foreach (var e in _shieldComponents.GetEntities(_buffer))
        {
            if ( ! e.hasView)
                continue;

            var shield = e.shield;
            var shieldView = shield.GetShieldView();

            if (shield.GetShieldView() != null)
            {
                shieldView.SetPosition(e.view.View.GetPosition());
            }
            else
            {
                var view = ViewService.sharedInstance.CreateView(shield.Name, shield.Source);
                view.SetPosition(e.view.View.GetPosition());
                shield.SetShieldView(view);

                e.OnComponentRemoved += ComponentRemoved;
            }
        }
    }

    void ComponentRemoved(IEntity entity, int index, IComponent component)
    {
        if (index == GameComponentsLookup.Shield)
        {
            var e = (GameEntity)entity;
            e.OnComponentRemoved -= ComponentRemoved;

            var shield = (ShieldComponent)component;
            var shieldView = shield.GetShieldView();
            if (shieldView != null)
                Object.Destroy(shield.GetShieldView().GetGameObject());
        }
    }
}