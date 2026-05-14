using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private enum WeaponUpgradeMode
    {
        None,
        DamageBoost,
        FireRateBoost,
        DamageAndFireRateBoost
    }

    [Header("Weapon References")]
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private Transform firePoint;

    [Header("Base Weapon Settings")]
    [SerializeField] private int damage = 25;
    [SerializeField] private float fireRate = 0.25f;

    [Header("Decorator Upgrade Settings")]
    [SerializeField] private WeaponUpgradeMode upgradeMode = WeaponUpgradeMode.None;
    [SerializeField] private int bonusDamage = 25;
    [SerializeField] private float fireRateMultiplier = 0.5f;

    private IWeapon weapon;
    private float fireCooldown;

    private void Awake()
    {
        BuildWeapon();
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            TryFire();
        }
    }

    private void BuildWeapon()
    {
        IWeapon baseWeapon = new BaseWeapon(projectilePool, firePoint, damage, fireRate);

        switch (upgradeMode)
        {
            case WeaponUpgradeMode.DamageBoost:
                weapon = new DamageBoostDecorator(baseWeapon, bonusDamage);
                break;

            case WeaponUpgradeMode.FireRateBoost:
                weapon = new FireRateDecorator(baseWeapon, fireRateMultiplier);
                break;

            case WeaponUpgradeMode.DamageAndFireRateBoost:
                IWeapon damageBoostedWeapon = new DamageBoostDecorator(baseWeapon, bonusDamage);
                weapon = new FireRateDecorator(damageBoostedWeapon, fireRateMultiplier);
                break;

            case WeaponUpgradeMode.None:
            default:
                weapon = baseWeapon;
                break;
        }

        Debug.Log($"Weapon ready. Damage: {weapon.GetDamage()}, Fire Rate: {weapon.GetFireRate()}");
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