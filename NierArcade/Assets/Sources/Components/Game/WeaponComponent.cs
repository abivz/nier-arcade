using UnityEngine;

using Entitas;

[Game]
public sealed class WeaponComponent : IComponent
{
    public bool Active;
    public Gun[] Guns;
}
