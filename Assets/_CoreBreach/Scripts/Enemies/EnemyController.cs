using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IDamageable
{
    private enum MovementMode
    {
        DirectCoreChase,
        ZigZagCoreApproach
    }

    [Header("Health")]
    [SerializeField] private int maxHealth = 50;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private MovementMode movementMode = MovementMode.DirectCoreChase;
    [SerializeField] private float zigZagStrength = 0.75f;
    [SerializeField] private float zigZagFrequency = 4f;

    [Header("Target")]
    [SerializeField] private Transform coreTarget;

    [Header("Attack")]
    [SerializeField] private int coreDamage = 10;
    [SerializeField] private float attackDistance = 1.4f;
    [SerializeField] private float attackCooldown = 1f;

    private Rigidbody rb;
    private IMovementStrategy movementStrategy;
    private int currentHealth;
    private float attackTimer;
    private bool isDead;

    public event Action<EnemyController> OnEnemyDied;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        movementStrategy = CreateMovementStrategy();
    }

    private void Start()
    {
        if (coreTarget == null)
        {
            GameObject coreObject = GameObject.FindGameObjectWithTag("EnergyCore");

            if (coreObject != null)
            {
                coreTarget = coreObject.transform;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead || coreTarget == null)
        {
            return;
        }

        MoveTowardCore();
    }

    private void Update()
    {
        if (isDead || coreTarget == null)
        {
            return;
        }

        attackTimer -= Time.deltaTime;
        TryAttackCore();
    }

    private IMovementStrategy CreateMovementStrategy()
    {
        switch (movementMode)
        {
            case MovementMode.ZigZagCoreApproach:
                return new ZigZagCoreApproachStrategy(zigZagStrength, zigZagFrequency);

            case MovementMode.DirectCoreChase:
            default:
                return new DirectCoreChaseStrategy();
        }
    }

    private void MoveTowardCore()
    {
        Vector3 moveDirection = movementStrategy.GetMoveDirection(transform, coreTarget);
        Vector3 nextPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.MoveRotation(targetRotation);
        }
    }

    private void TryAttackCore()
    {
        float distanceToCore = Vector3.Distance(transform.position, coreTarget.position);

        if (distanceToCore > attackDistance || attackTimer > 0f)
        {
            return;
        }

        IDamageable coreDamageable = coreTarget.GetComponent<IDamageable>();

        if (coreDamageable != null)
        {
            coreDamageable.TakeDamage(coreDamage);
            attackTimer = attackCooldown;
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || currentHealth <= 0 || isDead)
        {
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        OnEnemyDied?.Invoke(this);
        Destroy(gameObject);
    }
}