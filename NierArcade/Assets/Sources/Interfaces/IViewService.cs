using Entitas;

public interface IViewService
{
    IView CreateView(string Name, AssetSource Source, IView parent = null);
    IView CreateViewAndLink(IEntity entity, IContext context, string Name, AssetSource Source, IView parent = null);
}