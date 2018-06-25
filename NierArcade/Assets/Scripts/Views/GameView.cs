using UnityEngine;

using Entitas;
using Entitas.Unity;

public class GameView : CachedMonoBehaviour, IView, IPoolListener, IPositionListener, IRotationListener, IScaleListener, IVelocityListener, IViewParticleCollision
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
    Axis _rotationAxis;

    [SerializeField]
    bool _inverseAxis;

    [SerializeField]
    bool _isLocalRotation;

    [SerializeField]
    float _rotationCorrection;

    [SerializeField]
    Transform _rotationTransform;

    public virtual void Link(IEntity entity, IContext context)
    {
        if (_entity != null)
            return;

        _context = (GameContext)context;
        _entity = (GameEntity)entity;

        Init();
    }

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
        gameObject.Unlink();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _context.CreateEntity().AddCollision(_entity, collision.gameObject.tag);
    }

    void Init()
    {
        gameObject.Link(_entity, _context);

        var attachedComponents = GetComponents<BaseComponentMonoBehaviour>();
        for (int i = 0; i < attachedComponents.Length; i++)
            _entity.AddComponent(attachedComponents[i].Index, attachedComponents[i].Component);

        AddListeners();
    }

    void AddListeners()
    {
        if (_entity.hasPool)
            _entity.AddPoolListener(this);

        if (_entity.hasPosition)
            _entity.AddPositionListener(this);

        if (_entity.hasRotation)
            _entity.AddRotationListener(this);

        if (_entity.hasScale)
            _entity.AddScaleListener(this);

        if (_entity.hasVelocity)
            _entity.AddVelocityListener(this);
    }

    public void OnViewParticleCollision(string tag)
    {
        _context.CreateEntity().AddCollision(_entity, tag);
    }

    public void OnPool(GameEntity entity, int Id, bool Active)
    {
        gameObject.SetActive(Active);
        cachedRigidbody2D.simulated = Active;
    }

    public virtual void OnPosition(GameEntity entity, Vector3 Value)
    {
        cachedTransform.position = Value;
    }

    public virtual void OnRotation(GameEntity entity, float Angle)
    {
        var angle = Angle + _rotationCorrection;

        switch (_rotationAxis)
        {
            case Axis.X:
                if (_isLocalRotation)
                    _rotationTransform.localRotation = Quaternion.AngleAxis(angle, _inverseAxis ? -Vector3.right : Vector3.right);
                else
                    _rotationTransform.rotation = Quaternion.AngleAxis(angle, _inverseAxis ? -Vector3.right : Vector3.right);
            break;

            case Axis.Y:
                if (_isLocalRotation)
                    _rotationTransform.localRotation = Quaternion.AngleAxis(angle, _inverseAxis ? -Vector3.up : Vector3.up);
                else
                    _rotationTransform.rotation = Quaternion.AngleAxis(angle, _inverseAxis ? -Vector3.up : Vector3.up);
            break;

            case Axis.Z:
                if (_isLocalRotation)
                    _rotationTransform.localRotation = Quaternion.AngleAxis(angle, _inverseAxis ? -Vector3.forward : Vector3.forward);
                else
                    _rotationTransform.rotation = Quaternion.AngleAxis(angle, _inverseAxis ? -Vector3.forward : Vector3.forward);
            break;
        }
    }

    public virtual void OnScale(GameEntity entity, Vector3 Value)
    {
        cachedTransform.localScale = Value;
    }

    public virtual void OnVelocity(GameEntity entity, Vector3 Value)
    {
        if ( ! cachedRigidbody2D.simulated)
            cachedRigidbody2D.simulated = true;

        cachedRigidbody2D.velocity = Value;
    }

    public virtual void OnWeapon(GameEntity entity, int Id, bool Active, float Speed, float Interval)
    {
        
    }

    public Vector3 GetPosition()
    {
        return cachedTransform.position;
    }

    public Vector3 GetRotation()
    {
        return cachedTransform.rotation.eulerAngles;
    }

    public Vector3 GetScale()
    {
        return cachedTransform.lossyScale;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    [System.Serializable]
    enum Axis
    {
        X,
        Y,
        Z
    }
}
