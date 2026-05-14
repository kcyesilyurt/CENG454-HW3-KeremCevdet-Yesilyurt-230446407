using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 14f;
    [SerializeField] private float lifeTime = 2.5f;

    private ProjectilePool owningPool;
    private Vector3 moveDirection;
    private int damage;
    private float lifeTimer;
    private bool isActive;

    public void Initialize(ProjectilePool pool)
    {
        owningPool = pool;
    }

    public void Launch(Vector3 direction, int projectileDamage)
    {
        moveDirection = direction.normalized;
        damage = projectileDamage;
        lifeTimer = lifeTime;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        transform.position += moveDirection * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            return;
        }

        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            ReturnToPool();
        }
    }

    public void OnSpawnFromPool()
    {
        gameObject.SetActive(true);
        isActive = false;
        lifeTimer = lifeTime;
        damage = 0;
        moveDirection = Vector3.zero;
    }

    public void OnReturnToPool()
    {
        isActive = false;
        lifeTimer = 0f;
        damage = 0;
        moveDirection = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void ReturnToPool()
    {
        if (owningPool != null)
        {
            owningPool.ReturnProjectile(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}