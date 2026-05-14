using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int initialPoolSize = 20;

    private readonly Queue<Projectile> availableProjectiles = new Queue<Projectile>();

    private void Awake()
    {
        BuildPool();
    }

    private void BuildPool()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("ProjectilePool is missing a Projectile prefab reference.");
            return;
        }

        for (int i = 0; i < initialPoolSize; i++)
        {
            Projectile projectile = CreateProjectile();
            projectile.OnReturnToPool();
            availableProjectiles.Enqueue(projectile);
        }
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform);
        projectile.Initialize(this);
        return projectile;
    }

    public Projectile GetProjectile(Vector3 position, Quaternion rotation)
    {
        Projectile projectile;

        if (availableProjectiles.Count > 0)
        {
            projectile = availableProjectiles.Dequeue();
        }
        else
        {
            projectile = CreateProjectile();
        }

        projectile.transform.SetPositionAndRotation(position, rotation);
        projectile.OnSpawnFromPool();

        return projectile;
    }

    public void ReturnProjectile(Projectile projectile)
    {
        if (projectile == null)
        {
            return;
        }

        projectile.OnReturnToPool();
        projectile.transform.SetParent(transform);
        availableProjectiles.Enqueue(projectile);
    }
}