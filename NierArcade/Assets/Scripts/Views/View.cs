using UnityEngine;

using Entitas;
using Entitas.Unity;

public class View : CachedMonoBehaviour, IView, IViewParticleCollision
{
    GameEntity _entity;

    protected GameEntity Entity
    {
        get
        {
            return _entity;
        }
    }

    GameContext _context;

    protected GameContext Context
    {
        get
        {
            return _context;
        }
    }

    [SerializeField]
    bool _selfInstance;

    [SerializeField]
    Transform _rotationTransform;

    [SerializeField]
    Transform _positionTransform;

    void Awake()
    {
        if ( ! _selfInstance)
            return;

        if (_entity != null)
            return;

        _context = Contexts.sharedInstance.game;
        _entity = _context.CreateEntity();
        _entity.AddView(this);

        Init();
    }

    void OnDestroy()
    {
        if (_entity != null)
            gameObject.Unlink();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_context != null)
            _context.CreateEntity().AddCollision(_entity, collision.gameObject.tag);
    }

    void Init()
    {
        gameObject.Link(_entity, _context);

        var attachedComponents = GetComponents<BaseComponentMonoBehaviour>();
        for (int i = 0; i < attachedComponents.Length; i++)
            _entity.AddComponent(attachedComponents[i].Index, attachedComponents[i].Component);
    }

    public virtual void Link(IEntity entity, IContext context)
    {
        if (_entity != null)
            return;

        _context = (GameContext)context;
        _entity = (GameEntity)entity;

        Init();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return cachedRigidbody2D;
    }

    public void OnViewParticleCollision(string tag)
    {
        _context.CreateEntity().AddCollision(_entity, tag);
    }

    public void SetPosition(Vector3 position)
    {
        if (_positionTransform != null)
            _positionTransform.position = position;
        else
            cachedTransform.position = position;
    }

    public Vector3 GetPosition()
    {
        if (_positionTransform != null)
            return _positionTransform.position;

        return cachedTransform.position;
    }

    public void SetRotation(Quaternion rotation)
    {
        if (_rotationTransform != null)
            _rotationTransform.rotation = rotation;
        else
            cachedTransform.rotation = rotation;
    }

    public Quaternion GetRotation()
    {
        if (_rotationTransform != null)
            return _rotationTransform.rotation;

        return cachedTransform.rotation;
    }
}
