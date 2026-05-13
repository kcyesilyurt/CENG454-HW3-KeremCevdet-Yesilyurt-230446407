using UnityEngine;

public interface IWeapon
{
    void Fire();
    int GetDamage();
    float GetFireRate();
}