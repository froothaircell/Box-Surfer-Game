using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(applicationIsQuitting)
            {
                return null;
            }

            if(instance)
            {

            }

            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if(instance == null)
                {
                    GameObject gm = new GameObject();
                    gm.name = typeof(GameManager).Name;
                    instance = gm.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    // Events
    public event UnityAction OnRun;
    public event UnityAction OnWin;
    public event UnityAction OnRestart;
    public event UnityAction OnSettingsOpened;
    public event UnityAction OnSettingsClosed;
    public event UnityAction<bool> OnStopOrDeath;

    private int state = 0;
    private int prevState; // only used for settings state. Can be used for any event that needs to remember the previous state
    private static bool applicationIsQuitting = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    private void OnDestroy()
    {
        if(gameObject.GetComponent<GameManager>().GetInstanceID() == Instance.GetInstanceID())
        {
            applicationIsQuitting = true;
        }
    }

    public void Run()
    {
        if(state == 0)
        {
            state = 1;
            OnRun?.Invoke();
        }
    }

    public void Win()
    {
        if(state == 1)
        {
            state = 2;
            OnWin?.Invoke();
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
            OnStopOrDeath?.Invoke(win);
        }
    }

    public void Restart()
    {
        if(state == 3)
        {
            state = 0;
            OnRestart?.Invoke();
            applicationIsQuitting = false;
        }
    }

    public void SettingsToggled()
    {
        if(state >= 0 && state < 4)
        {
            prevState = state;
            OnSettingsOpened?.Invoke();
            state = 4;
        }
        else if(state == 4)
        {
            state = prevState;
            OnSettingsClosed?.Invoke();
        }
    }
}
