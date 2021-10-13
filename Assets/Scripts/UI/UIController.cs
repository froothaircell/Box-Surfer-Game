using UnityEngine;

/// <summary>
/// Overarching controller for the UI that enables or disables UI elements
/// according to events invoked by the game manager
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject
        scorePanel,
        restartButton,
        settingsButton,
        settingsPanel,
        finalPanel;

    private void Awake()
    {
        scorePanel.SetActive(true);
        restartButton.SetActive(false);
        settingsButton.SetActive(true);
        settingsPanel.SetActive(false);
        finalPanel.SetActive(true);
    }

    private void Start()
    {
        // Add listeners to events
        GameManager.Instance.OnStopOrDeath += UpdateUIByState;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnStopOrDeath -= UpdateUIByState;
        }
    }

    private void UpdateUIByState(bool win)
    {
        EnableRestartButton();
        DisableSettingsButton(win);
    }

    private void EnableFinalPanel()
    {
        finalPanel.SetActive(true);
    }

    private void EnableRestartButton()
    {
        restartButton.SetActive(true);
    }

    private void DisableRestartButton()
    {
        restartButton.SetActive(false);
    }

    private void DisableSettingsButton(bool win)
    {
        if(win)
        {
            settingsButton.SetActive(false);
            settingsPanel.SetActive(false);
        }
    }
}
