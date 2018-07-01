using Entitas;

[Game]
public sealed class ShieldComponent : IComponent
{
    public string Name;
    
    public AssetSource Source;

    IView _view;

    public void SetShieldView(IView view)
    {
        _view = view;
    }

    public IView GetShieldView()
    {
        return _view;
    }
}