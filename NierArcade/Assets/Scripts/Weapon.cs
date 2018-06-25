using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Weapon : CachedMonoBehaviour, IWeapon
{
    readonly List<ParticleCollisionEvent> _buffer = new List<ParticleCollisionEvent>(10);

    public void SetWeapon(bool Active, float Speed, float Interval)
    {
        if (Active)
            cachedParticleSystem.Play();
        else
            cachedParticleSystem.Stop();
    }

    void OnParticleCollision(GameObject other)
    {
        var view = other.GetComponent<IViewParticleCollision>();
        if (view == null)
            return;

        cachedParticleSystem.GetCollisionEvents(other, _buffer);

        foreach (var collision in _buffer)
            view.OnViewParticleCollision(gameObject.tag);
    }
}
