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

[System.Serializable]
public class vector3Event : UnityEvent<Vector3>
{

}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            Debug.Log("Getter called with application quit status: " + applicationIsQuitting);
            if(applicationIsQuitting)
            {
                return null;
            }

            if(instance)
            {
                Debug.Log(instance.GetInstanceID());
            }

            if(instance == null)
            {
                Debug.Log("This object does not have an instance");
                instance = FindObjectOfType<GameManager>();

                if(instance == null)
                {
                    Debug.Log("Creating a new Game Manager");
                    GameObject gm = new GameObject();
                    gm.name = typeof(GameManager).Name;
                    instance = gm.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private intEvent UpdateScoreEvent = new intEvent();
    [SerializeField]
    private vector3Event PositionBasedAnimation = new vector3Event();

    // Events
    public event UnityAction OnRun;
    public event UnityAction OnWin;
    public event UnityAction OnRestart;
    public event UnityAction OnSettingsOpened;
    public event UnityAction OnSettingsClosed;
    public event UnityAction<bool> OnStopOrDeath;

    private int state = 0;
    private int prevState; // only used for settings state. Can be used for any event that needs to remember the previous state
    private int diamondScore;
    private static bool applicationIsQuitting = false;

    private void Awake()
    {
        Debug.Log("Game Manager awoken with ID: " + gameObject.GetInstanceID());
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("Destroying object with ID: " + gameObject.GetInstanceID());
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        diamondScore = 0;
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
        Debug.Log("Game Manager destroyed with ID: " + gameObject.GetInstanceID());
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
        Debug.Log("We got this far");
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
            // SettingsEnabled.Invoke();
            OnSettingsOpened?.Invoke();
            state = 4;
        }
        else if(state == 4)
        {
            state = prevState;
            // SettingsDisabled.Invoke();
            OnSettingsClosed?.Invoke();
        }
    }

    // Score increments called by diamonds upon collision with player
    /*public void DiamondCollected(Vector3 position)
    {
        diamondScore++;
        Debug.Log("Diamond score = " + diamondScore);
        UpdateScoreEvent.Invoke(diamondScore);
        Vector3 screenPosition = mainCam.WorldToScreenPoint(position);
        PositionBasedAnimation.Invoke(screenPosition);
    }*/
}
