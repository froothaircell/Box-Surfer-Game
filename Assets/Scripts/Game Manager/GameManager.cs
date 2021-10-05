using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class boolEvent : UnityEvent<bool>
{

}

[System.Serializable]
public class intEvent : UnityEvent<int>
{

}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private UnityEvent
        RunEvent = new UnityEvent(),
        WinEvent = new UnityEvent(),
        RestartEvent = new UnityEvent(),
        SettingsEnabled = new UnityEvent(),
        SettingsDisabled = new UnityEvent();
    [SerializeField]
    private boolEvent StoppageOrDeathEvent = new boolEvent();
    [SerializeField]
    private intEvent UpdateScoreEvent = new intEvent();

    private int state = 0;
    private int prevState; // only used for settings state. Can be used for any event that needs to remember the previous state
    private int diamondScore;
    private bool scoreIncrease;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        diamondScore = 0;
        scoreIncrease = false;
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
                if(scoreIncrease)
                {
                    UpdateScore(diamondScore);
                }
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
            bool win = false; 

            if(state == 2)
            {
                win = true;
            }
            state = 3;
            StoppageOrDeathEvent.Invoke(win);
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

    // Score increments called by diamonds upon collision with player
    public void ScoreIncrement()
    {
        diamondScore++;
        scoreIncrease = true;
        Debug.Log("Diamond score = " + diamondScore);
    }
    
    // Update score in the UI side
    private void UpdateScore(int score)
    {
        UpdateScoreEvent.Invoke(score);
        scoreIncrease = false;
        Debug.Log("Score updated on UI side");
    }
}
