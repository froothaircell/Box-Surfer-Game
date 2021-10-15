using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Opens settings panel and restricts movement while open
/// </summary>
public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    private bool showSettings = false;

    private void Start()
    {
        showSettings = false;
    }

    public void ToggleSettings()
    {
        if(!showSettings)
        {
            settingsPanel.SetActive(true);
            GameManager.GameManagerInstance.SettingsToggled();
            showSettings = true;
        }
        else
        {
            settingsPanel.SetActive(false);
            GameManager.GameManagerInstance.SettingsToggled();
            showSettings = false;
        }
    }
}
