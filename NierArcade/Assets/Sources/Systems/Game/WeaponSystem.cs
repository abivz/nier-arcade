using System;
using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class WeaponSystem : IInitializeSystem, IExecuteSystem
{
    readonly GameContext _gameContext;
    readonly int _poolBufferCapacity;
    readonly PoolSetup[] _setups;
    readonly IGroup<GameEntity> _poolComponents;
    readonly IGroup<GameEntity> _weaponComponents;
    readonly List<GameEntity> _poolBuffer;
    readonly List<GameEntity> _weaponBuffer;

    float timeInS
    {
        get
        {
            return Time.realtimeSinceStartup;
        }
    }

    public WeaponSystem(Contexts contexts, int weaponBufferCapacity, int poolBufferCapacity, PoolSetup[] setups)
    {
        _gameContext = contexts.game;
        _poolBufferCapacity = poolBufferCapacity;
        _setups = setups;

        _poolComponents = _gameContext.GetGroup(GameMatcher.Pool);
        _weaponComponents = _gameContext.GetGroup(GameMatcher.Weapon);
        _poolBuffer = new List<GameEntity>(poolBufferCapacity);
        _weaponBuffer = new List<GameEntity>(weaponBufferCapacity);
    }

    public void Initialize()
    {
        foreach (var setup in _setups)
        {
            for (int i = 0; i < setup.Count; i++)
            {
                var entity = _gameContext.CreateEntity();
                entity.AddView(ViewService.sharedInstance.CreateViewAndLink(entity, _gameContext, setup.Name, setup.Source));
            }
        }
    }

    public void Execute()
    {
        foreach (var weaponEntity in _weaponComponents.GetEntities(_weaponBuffer))
        {
            var weaponEntityView = weaponEntity.view.View;
            var weapon = weaponEntity.weapon;
            if ( ! weapon.Active)
                continue;

            var guns = weapon.Guns;

            if ( ! GunsAvailable(guns))
                continue;

            var gunsShooted = new bool[guns.Length];
            foreach (var poolEntity in _poolComponents.GetEntities(_poolBuffer))
            {
                var entityPoolComponent = poolEntity.pool;

                if (entityPoolComponent.Active)
                    continue;

                for (int i = 0; i < guns.Length; i++)
                {
                    if (gunsShooted[i])
                        continue;

                    var gun = guns[i];

                    if ( ! GunCanShoot(gun))
                        continue;

                    if (entityPoolComponent.Id == gun.PoolId)
                    {
                        Shoot(gun, weaponEntity, poolEntity);
                        gun.SetLastShootTime(timeInS);

                        poolEntity.ReplacePool(entityPoolComponent.Id, true);
                        gunsShooted[i] = true;
                        break;
                    }
                }
            }

            for (int i = 0; i < gunsShooted.Length; i++)
            {
                if (gunsShooted[i])
                    continue;

                var gun = guns[i];

                if ( ! GunCanShoot(gun))
                    continue;

                var poolSetup = GetPoolSetupById(_setups, gun.PoolId);

                var poolEntity = _gameContext.CreateEntity();
                poolEntity.AddView(ViewService.sharedInstance.CreateViewAndLink(poolEntity, _gameContext, poolSetup.Name, poolSetup.Source));

                Shoot(gun, weaponEntity, poolEntity);
                gun.SetLastShootTime(timeInS);

                poolEntity.ReplacePool(poolEntity.pool.Id, true);
                gunsShooted[i] = true;
            }
        }
    }

    bool GunsAvailable(Gun[] guns)
    {
        for(int i = 0; i < guns.Length; i++)
            if (GunCanShoot(guns[i]))
                return true;
        
        return false;
    }

    bool GunCanShoot(Gun gun)
    {
        if ( ! gun.Active)
            return false;

        if (timeInS - gun.GetLastShootTime() >= gun.IntervalInS) 
            return true;

        return false;
    }

    PoolSetup GetPoolSetupById(PoolSetup[] poolSetups, int id)
    {
        for(int i = 0; i < poolSetups.Length; i++)
            if (poolSetups[i].Id == id)
                return poolSetups[i];

        throw new System.IndexOutOfRangeException("Id not exists in PoolSetup");
    }

    void Shoot(Gun gun, GameEntity weaponEntity, GameEntity poolEntity)
    {
        var weaponEntityView = weaponEntity.view.View;
        var poolEntityView = poolEntity.view.View;

        var vector = weaponEntityView.GetRotation() * gun.Direction;
        var direction = new Vector2(vector.x, vector.y).normalized;

        poolEntityView.GetGameObject().SetActive(true);

        poolEntityView.SetPosition(weaponEntityView.GetPosition());
        poolEntityView.SetRotation(weaponEntityView.GetRotation());
        poolEntityView.GetRigidbody2D().velocity = direction * gun.Speed;
    }
}