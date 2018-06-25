using UnityEngine;

public class Weapons : MonoBehaviour, IWeapon
{
    [SerializeField]
    Weapon[] _weapons;

    public void SetWeapon(bool Active, float Speed, float Interval)
    {
        for (int i = 0; i < _weapons.Length; i++)
            _weapons[i].SetWeapon(Active, Speed, Interval);
    }
}
