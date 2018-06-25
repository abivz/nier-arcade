using Entitas;

public interface IViewService
{
    IView CreateView(IEntity entity, string Name, AssetSource Source);
}