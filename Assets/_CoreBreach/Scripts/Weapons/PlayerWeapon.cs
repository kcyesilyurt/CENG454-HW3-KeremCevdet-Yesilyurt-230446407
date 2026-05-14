using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon References")]
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private Transform firePoint;

    [Header("Weapon Settings")]
    [SerializeField] private int damage = 25;
    [SerializeField] private float fireRate = 0.25f;

    private IWeapon weapon;
    private float fireCooldown;

    private void Awake()
    {
        weapon = new BaseWeapon(projectilePool, firePoint, damage, fireRate);
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            TryFire();
        }
    }

    private void TryFire()
    {
        if (weapon == null || fireCooldown > 0f)
        {
            return;
        }

        weapon.Fire();
        fireCooldown = weapon.GetFireRate();
    }
}