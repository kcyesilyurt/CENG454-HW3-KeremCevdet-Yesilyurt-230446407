using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [Header("Observed Systems")]
    [SerializeField] private CoreHealth coreHealth;
    [SerializeField] private WaveManager waveManager;

    [Header("UI")]
    [SerializeField] private HUDController hudController;

    private bool isGameOver;

    public bool IsGameOver => isGameOver;

    private void OnEnable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnCoreDestroyed += HandleCoreDestroyed;
        }

        if (waveManager != null)
        {
            waveManager.OnAllWavesCompleted += HandleAllWavesCompleted;
        }
    }

    private void OnDisable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnCoreDestroyed -= HandleCoreDestroyed;
        }

        if (waveManager != null)
        {
            waveManager.OnAllWavesCompleted -= HandleAllWavesCompleted;
        }
    }

    private void HandleCoreDestroyed()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        if (hudController != null)
        {
            hudController.SetMessage("CORE DESTROYED - YOU LOSE");
        }

        Debug.Log("Game Over: Core destroyed.");
    }

    private void HandleAllWavesCompleted()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        if (hudController != null)
        {
            hudController.SetMessage("FACILITY DEFENDED - YOU WIN");
        }

        Debug.Log("Game Won: All waves completed.");
    }
}