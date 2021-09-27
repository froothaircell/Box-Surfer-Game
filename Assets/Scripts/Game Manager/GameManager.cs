using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent
        RunEvent = new UnityEvent(),
        WinEvent = new UnityEvent(),
        StoppageOrDeathEvent = new UnityEvent(),
        RestartEvent = new UnityEvent(),
        SettingsEnabled = new UnityEvent(),
        SettingsDisabled = new UnityEvent();
    

    private int state = 0;
    private int prevState; // only used for settings state. Can be used for any event that needs to remember the previous state
    

    // Start is called before the first frame update
    private void Start()
    {
        state = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        switch(state)
        {
            case 0: // Start of game
                if(Input.GetButtonDown("Fire1") || Input.touchCount > 0)
                {
                    Run();
                }
                break;
            case 1: // Player running
                break;
            case 2: // Player won
                break;
            case 3: // Player died
                break;
            case 4: // Settings opened
                break;
            default:
                break;
        }
    }

    public void Run()
    {
        if(state == 0)
        {
            state = 1;
            RunEvent.Invoke();
        }
    }

    public void Win()
    {
        if(state == 1)
        {
            state = 2;
            WinEvent.Invoke();
        }
    }

    public void StoppageOrDeath()
    {
        if(state == 1 || state == 2)
        {
            state = 3;
            StoppageOrDeathEvent.Invoke();
        }
    }

    public void Restart()
    {
        if(state == 3)
        {
            state = 0;
            RestartEvent.Invoke();
        }
    }

    public void SettingsOpened()
    {
        if(state >= 0 && state < 4)
        {
            prevState = state;
            SettingsEnabled.Invoke();
            state = 4;
        }
        else if(state == 4)
        {
            state = prevState;
            SettingsDisabled.Invoke();
        }
    }    
}
