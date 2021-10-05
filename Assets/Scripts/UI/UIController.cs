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

    public void EnableRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void DisableRestartButton()
    {
        restartButton.SetActive(false);
    }

    public void DisableSettingsButton(bool win)
    {
        if(win)
        {
            settingsButton.SetActive(false);
            settingsPanel.SetActive(false);
        }
    }
}
