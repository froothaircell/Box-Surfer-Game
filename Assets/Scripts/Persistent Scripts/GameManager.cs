using UnityEngine;
using UnityEngine.Events;

enum State
{
    idle,
    run,
    win,
    die,
    pause
}


/// <summary>
/// Functions as a state machine that changes the state of the game according to
/// prompts from objects in the scene and activates subscribable events
/// accordingly. Persists across scenes.
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;
    private static ProgressManager progressManagerInstance;
    private static PlayerManager playerManagerInstance;

    public static GameManager GameManagerInstance
    {
        get
        {
            if(applicationIsQuitting)
            {
                return null;
            }

            // Any initializations for child managers donot need this script as
            // they are directly initialized in the game manager and would be
            // automatically destroyed if the game manager finds a copy of
            // itself (meaning a copy of all the child objects as well)
            if(gameManagerInstance == null)
            {
                gameManagerInstance = FindObjectOfType<GameManager>();

                if(gameManagerInstance == null)
                {
                    GameObject gm = new GameObject()
                    {
                        name = typeof(GameManager).Name
                    };
                    gameManagerInstance = gm.AddComponent<GameManager>();
                }
            }
            return gameManagerInstance;
        }
    }

    public static ProgressManager ProgressManagerInstance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            return progressManagerInstance;
        }
    }

    public static PlayerManager PlayerManagerInstance
    {
        get
        {
            if(applicationIsQuitting)
            {
                return null;
            }

            return playerManagerInstance;
        }
    }

    // Events to be subscribed to
    public event UnityAction OnRun;
    public event UnityAction OnWin;
    public event UnityAction OnRestart;
    public event UnityAction OnSettingsOpened;
    public event UnityAction OnSettingsClosed;
    public event UnityAction<bool> OnStopOrDeath;

    // private int state = 0;
    private State gameState = State.idle;
    // only used for settings state. Can be used for any event that needs to
    // remember the previous state
    private State prevState; 
    
    private static bool applicationIsQuitting = false;

    private void Awake()
    {
        InitGameManager();
        InitProgressManager();
        InitPlayerManager();
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameState = State.idle;
    }

    // Update is called once per frame
    private void Update()
    {
        // Logic to run within a state if required
        switch(gameState)
        {
            case State.idle: // Start of game
                if(Input.GetButtonDown("Fire1") || Input.touchCount > 0)
                {
                    Run();
                }
                break;
            case State.run: // Player running
                break;
            case State.win: // Player won
                break;
            case State.die: // Player died
                break;
            case State.pause: // Settings opened
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        if(gameObject.GetComponent<GameManager>().GetInstanceID() == GameManagerInstance.GetInstanceID())
        {
            applicationIsQuitting = true;
        }
    }

    private void InitGameManager()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitProgressManager()
    {
        progressManagerInstance = GetComponentInChildren<ProgressManager>();
        // If condition ensures that current instance in awake is the first one initialized
        if (progressManagerInstance.GetInstanceID() == this.gameObject.GetComponentInChildren<ProgressManager>().GetInstanceID())
        {
            progressManagerInstance.ResetScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitPlayerManager()
    {
        playerManagerInstance = GetComponentInChildren<PlayerManager>();
        // If condition ensures that current instance in awake is the first one initialized
        if (playerManagerInstance.GetInstanceID() == this.gameObject.GetComponentInChildren<PlayerManager>().GetInstanceID())
        {
            playerManagerInstance.ResetPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Run()
    {
        if(gameState == State.idle)
        {
            gameState = State.run;
            OnRun?.Invoke();
        }
    }

    public void Win()
    {
        if(gameState == State.run)
        {
            gameState = State.win;
            OnWin?.Invoke();
        }
    }

    public void StoppageOrDeath()
    {
        if(gameState == State.run || gameState == State.win)
        {
            bool win = false; 

            if(gameState == State.win)
            {
                win = true;
            }
            gameState = State.die;
            ProgressManagerInstance.StopOrDeathUIAnimations(win);
            PlayerManagerInstance.PlayerStoppedOrKilled(win);
            OnStopOrDeath?.Invoke(win);
        }
    }

    public void Restart()
    {
        if(gameState == State.die)
        {
            gameState = State.idle;
            ProgressManagerInstance.LevelUpdate();
            OnRestart?.Invoke();
            applicationIsQuitting = false;
        }
    }

    public void SettingsToggled()
    {
        if(gameState >= State.idle && gameState < State.pause)
        {
            prevState = gameState;
            OnSettingsOpened?.Invoke();
            gameState = State.pause;
        }
        else if(gameState == State.pause)
        {
            gameState = prevState;
            OnSettingsClosed?.Invoke();
        }
    }
}
