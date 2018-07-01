using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public sealed class WeaponComponent : IComponent
{
    public bool Active;
    public float IntervalInS;
    public Gun[] Guns;
}
