using System.Collections.Generic;

using UnityEngine;

using Entitas;

public class ViewService : MonoBehaviour, IViewService, IService
{
    [SerializeField]
    int _prefabsCapacity = 10;

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
        _prefabs = new Dictionary<string, GameObject>(_prefabsCapacity);
        _instance = this;
    }

    public IView CreateView(string Name, AssetSource Source, IView parent = null)
    {
        if (Source == AssetSource.Resources)
        {
            if ( ! _prefabs.ContainsKey(Name))
                _prefabs.Add(Name, Resources.Load<GameObject>(Name));
        }

        var go = parent == null ? Instantiate(_prefabs[Name]) : Instantiate(_prefabs[Name], parent.GetGameObject().transform);
        var view = go.GetComponent<IView>();

        return view;
    }

    public IView CreateViewAndLink(IEntity entity, IContext context, string Name, AssetSource Source, IView parent = null)
    {
        var view = CreateView(Name, Source, parent);

        view.Link(entity, context);
        return view;
    }
}
