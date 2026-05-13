using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("Observed Systems")]
    [SerializeField] private CoreHealth coreHealth;

    [Header("HUD Texts")]
    [SerializeField] private TMP_Text coreHealthText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text messageText;

    private void OnEnable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnHealthChanged += HandleCoreHealthChanged;
        }
    }

    private void OnDisable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnHealthChanged -= HandleCoreHealthChanged;
        }
    }

    private void Start()
    {
        if (coreHealth != null)
        {
            HandleCoreHealthChanged(coreHealth.CurrentHealth, coreHealth.MaxHealth);
        }

        SetWaveText(1, 3);
        ClearMessage();
    }

    private void HandleCoreHealthChanged(int currentHealth, int maxHealth)
    {
        if (coreHealthText != null)
        {
            coreHealthText.text = $"Core HP: {currentHealth} / {maxHealth}";
        }
    }

    public void SetWaveText(int currentWave, int maxWaves)
    {
        if (waveText != null)
        {
            waveText.text = $"Wave: {currentWave} / {maxWaves}";
        }
    }

    public void SetMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    public void ClearMessage()
    {
        SetMessage(string.Empty);
    }
}