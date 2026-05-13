using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [Header("Observed Systems")]
    [SerializeField] private CoreHealth coreHealth;

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
    }

    private void OnDisable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnCoreDestroyed -= HandleCoreDestroyed;
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
}