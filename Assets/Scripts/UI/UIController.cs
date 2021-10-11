using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject
        scorePanel,
        restartButton,
        settingsButton,
        settingsPanel;

    private void Awake()
    {
        scorePanel.SetActive(true);
        restartButton.SetActive(false);
        settingsButton.SetActive(true);
        settingsPanel.SetActive(false);
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
