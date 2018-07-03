using System.Collections;

using UnityEngine;

using Entitas;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PoolSetup[] _poolSetups;

    [SerializeField]
    ArcadeLevel[] _levels;

    Systems _systems;

    bool _levelLoading;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;

        //  Services
        var services = GetComponents<IService>();

        foreach (var service in services)
            service.Initialize(contexts);

        //  Systems
        _systems = new Systems().Add(new InputSystem(contexts, 10))
                                         .Add(new EnemySystem(contexts, 10))
                                         .Add(new WeaponSystem(contexts, 10, 100, _poolSetups))
                                         .Add(new WaveSystem(contexts, 10))
                                         .Add(new MoveSystem(contexts, 10))
                                         .Add(new CollisionSystem(contexts, 10))
                                         .Add(new HealthSystem(contexts, 10))

                                        .Add(new GameplaySystem(contexts, 10))

                                        .Add(new ShieldSystem(contexts, 10))

                                         .Add(new GameEventSystems(contexts))
                                         .Add(new DestroyedSystem(contexts, 10));



        _systems.Initialize();
    }

    void Update()
    {
        if (_levelLoading)
            return;
        
        _systems.Execute();
        _systems.Cleanup();
    }

    void OnDestroy()
    {
        _systems.TearDown();
    }

    public void LoadLevel(int index)
    {
        if (_levelLoading)
            return;

        //  Unlink views
        foreach (var entity in Contexts.sharedInstance.game.GetEntities())
            if (entity.hasView)
                entity.isDestroyed = true;

        StartCoroutine(LevelLoading(index));
    }

    IEnumerator LevelLoading(int index)
    {
        bool hasView = false;
        foreach (var entity in Contexts.sharedInstance.game.GetEntities())
        {
            if (entity.hasView)
            {
                hasView = true;
                break;
            }
        }

        if (hasView)
            yield return null;

        _levelLoading = true;

        //  Load level
        var level = _levels[index];

        //  Map
        var mapEntity = Contexts.sharedInstance.game.CreateEntity();
        var mapView = ViewService.sharedInstance.CreateViewAndLink(mapEntity, Contexts.sharedInstance.game, level.MapName, level.MapSource);
        var mapViewScale = mapView.GetGameObject().transform.localScale;
        mapViewScale.x = level.MapSizeX;
        mapViewScale.y = level.MapSizeY;
        mapView.GetGameObject().transform.localScale = mapViewScale;
        mapEntity.AddView(mapView);

        //  Objects
        foreach (var levelObject in level.Objects)
        {
            var levelObjectEntity = Contexts.sharedInstance.game.CreateEntity();
            var source = levelObject.Source == AssetSource.None ? level.DefaultSource : levelObject.Source;
            var levelObjectEntityView = ViewService.sharedInstance.CreateViewAndLink(levelObjectEntity, Contexts.sharedInstance.game, levelObject.Name, source);

            levelObjectEntityView.SetPosition(new Vector3(levelObject.PositionX, levelObject.PositionY));

            levelObjectEntity.AddView(levelObjectEntityView);

            yield return null;
        }

        _levelLoading = false;

        yield return true;
    }
}
