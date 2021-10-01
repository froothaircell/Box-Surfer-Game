using UnityEngine;
using UnityEngine.Events;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private UnityEvent settingsEvent = new UnityEvent();

    private bool showSettings = false;

    // Start is called before the first frame update
    void Start()
    {
        showSettings = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleSettings()
    {
        if(!showSettings)
        {
            settingsPanel.SetActive(true);
            settingsEvent.Invoke();
            showSettings = true;
        }
        else
        {
            settingsPanel.SetActive(false);
            settingsEvent.Invoke();
            showSettings = false;
        }
    }
}
