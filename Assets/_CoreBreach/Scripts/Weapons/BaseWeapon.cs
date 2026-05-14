using UnityEngine;

public class BaseWeapon : IWeapon
{
    private readonly ProjectilePool projectilePool;
    private readonly Transform firePoint;
    private readonly int damage;
    private readonly float fireRate;

    public BaseWeapon(ProjectilePool projectilePool, Transform firePoint, int damage, float fireRate)
    {
        this.projectilePool = projectilePool;
        this.firePoint = firePoint;
        this.damage = damage;
        this.fireRate = fireRate;
    }

    public void Fire()
    {
        if (projectilePool == null || firePoint == null)
        {
            return;
        }

        Projectile projectile = projectilePool.GetProjectile(firePoint.position, firePoint.rotation);
        projectile.Launch(firePoint.forward, damage);
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetFireRate()
    {
        return fireRate;
    }
}