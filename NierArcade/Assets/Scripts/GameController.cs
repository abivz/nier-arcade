using UnityEngine;

using Entitas;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PoolSetup[] _poolSetups;

    Systems _systems;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;

        //  Services
        var services = GetComponents<IService>();
        Logger.Info("[SERVICES] Count = " + services.Length);

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

                                        .Add(new ShieldSystem(contexts,10))

                                         .Add(new GameEventSystems(contexts))
                                         .Add(new DestroyedSystem(contexts, 10));
                                         
                                         

        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    void OnDestroy()
    {
        _systems.TearDown();
    }
}
