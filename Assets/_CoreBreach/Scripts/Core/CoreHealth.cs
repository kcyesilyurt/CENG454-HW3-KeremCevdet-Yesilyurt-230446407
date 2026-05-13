using System;
using UnityEngine;

public class CoreHealth : MonoBehaviour, IDamageable
{
    [Header("Core Health Settings")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public event Action<int, int> OnHealthChanged;
    public event Action OnCoreDestroyed;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        NotifyHealthChanged();
    }


    public void TakeDamage(int amount)
    {
        if (amount <= 0 || currentHealth <= 0)
        {
            return;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        NotifyHealthChanged();

        if (currentHealth <= 0)
        {
            OnCoreDestroyed?.Invoke();
        }
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}