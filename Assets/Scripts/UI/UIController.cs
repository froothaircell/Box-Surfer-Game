using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject
        restartButton,
        settingsButton,
        settingsPanel;

    private void Awake()
    {
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
}
