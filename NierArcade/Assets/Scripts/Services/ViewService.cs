using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class ViewService : MonoBehaviour, IViewService, IService
{
    [SerializeField]
    int _prefabCapacity = 10;

    GameContext _gameContext;

    Dictionary<string, GameObject> _prefabs;

    static IViewService _instance;

    public static IViewService sharedInstance
    {
        get
        {
            return _instance;
        }
    }

    public void Initialize(Contexts contexts)
    {
        _gameContext = contexts.game;
        _prefabs = new Dictionary<string, GameObject>(_prefabCapacity);
        _instance = this;
    }

    public IView CreateView(IEntity entity, string Name, AssetSource Source)
    {
        if (Source == AssetSource.Resources)
        {
            if ( ! _prefabs.ContainsKey(Name))
                _prefabs.Add(Name, Resources.Load<GameObject>(Name));

            var go = Instantiate(_prefabs[Name]);
            var view = go.GetComponent<IView>();

            view.Link(entity, _gameContext);
            return view;
        }

        return null;
    }
}
