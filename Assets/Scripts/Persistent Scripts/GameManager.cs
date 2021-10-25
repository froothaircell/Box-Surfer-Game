using UnityEngine;
using UnityEngine.Events;
using StateMachine;
using System.Collections.Generic;

enum tempState
{
    idle,
    run,
    win,
    stopDie,
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

    [SerializeField]
    private ProgressManager progressManagerInstance;
    [SerializeField]
    private PlayerManager playerManagerInstance;
    [SerializeField]
    private PoolManager poolManagerInstance;
    [Header("Deadzone Offsets")]
    [SerializeField]
    private float top;
    [SerializeField]
    private float
        bottom,
        left,
        right;
    
    private float
        deadzoneTop = Screen.height,
        deadzoneBottom = 0f,
        deadzoneLeft = 0f,
        deadzoneRight = Screen.width;

    
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
    public ProgressManager ProgressManagerInstance
    {
        get
        {
            return progressManagerInstance;
        }
        private set
        {
            progressManagerInstance = value;
        }
    }
    public PlayerManager PlayerManagerInstance 
    {
        get
        {
            return playerManagerInstance;
        } 
        private set
        {
            playerManagerInstance = value;
        }
    }
    public PoolManager PoolManagerInstance
    {
        get
        {
            return poolManagerInstance;
        }
        private set
        {
            poolManagerInstance = value;
        }
    }
    
    // Events to be subscribed to
    public event UnityAction OnRun;
    public event UnityAction OnWin;
    public event UnityAction OnRestart;
    public event UnityAction OnSettingsOpened;
    public event UnityAction OnSettingsClosed;
    public event UnityAction<bool> OnStopOrDeath;

    private tempState gameState = tempState.idle;
    // only used for settings state. Can be used for any event that needs to
    // remember the previous state
    private tempState prevState;

    public IStateMachine GmStateMachine { get; private set; }
    
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
        gameState = tempState.idle;
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log(ProgressManager.Score);
        // Logic to run within a state if required
        switch(gameState)
        {
            case tempState.idle: // Start of game
                if (Input.GetButtonDown("Fire1") 
                    || Input.touchCount > 0)
                {
#if !UNITY_EDITOR
                    Vector2 touchPosition = Input.GetTouch(0).position;
                    if(touchPosition.x > deadzoneLeft + left
                    && touchPosition.x < deadzoneRight - right
                    && touchPosition.y > deadzoneBottom + bottom
                    && touchPosition.y < deadzoneTop - top)
                    {
                        Run();
                    }
#else
                    Run();
#endif
                }
                break;
            case tempState.run: // Player running
                break;
            case tempState.win: // Player won
                break;
            case tempState.stopDie: // Player died
                break;
            case tempState.pause: // Settings opened
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
            GmStateMachine = new IStateMachine();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitProgressManager()
    {
        ProgressManagerInstance = GetComponentInChildren<ProgressManager>();
        // If condition ensures that current instance in awake is the first one initialized
        if (gameObject.GetComponent<GameManager>().GetInstanceID() == GameManagerInstance.GetInstanceID())
        {
            ProgressManagerInstance.ResetScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitPlayerManager()
    {
        PlayerManagerInstance = GetComponentInChildren<PlayerManager>();
        // If condition ensures that current instance in awake is the first one initialized
        if (gameObject.GetComponent<GameManager>().GetInstanceID() == GameManagerInstance.GetInstanceID())
        {
            PlayerManagerInstance.ResetPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Run()
    {
        GmStateMachine.MoveNext(GmStateMachine.commands["Run"]);
        PlayerManagerInstance.PlmStateMachine.MoveNext(PlayerManagerInstance.PlmStateMachine.commands["Run"]);
        ProgressManagerInstance.PrmStateMachine.MoveNext(ProgressManagerInstance.PrmStateMachine.commands["Run"]);
        if(gameState == tempState.idle)
        {
            gameState = tempState.run;
            OnRun?.Invoke();
        }
    }

    public void Win()
    {
        GmStateMachine.MoveNext(GmStateMachine.commands["Win"]);
        if(gameState == tempState.run)
        {
            gameState = tempState.win;
            OnWin?.Invoke();
        }
    }

    public void StoppageOrDeath()
    {
        if(gameState == tempState.run || gameState == tempState.win)
        {
            bool win = false; 

            if(gameState == tempState.win)
            {
                win = true;
            }
            if(!win)
            {
                // Debug.Log("Death command called (bruh)");
                GmStateMachine.MoveNext(GmStateMachine.commands["Die"]);
            }
            gameState = tempState.stopDie;
            ProgressManagerInstance.StopOrDeathUIAnimations(win);
            PlayerManagerInstance.PlayerStoppedOrKilled(win);
            OnStopOrDeath?.Invoke(win);
        }
    }

    // NOTE: Order of reset matters here
    public void Restart()
    {
        GmStateMachine.MoveNext(GmStateMachine.commands["Restart"]);
        if(gameState == tempState.stopDie)
        {
            gameState = tempState.idle;
            PoolManagerInstance.ResetPool();
            ProgressManagerInstance.RestartLevel();
            PlayerManagerInstance.ResetPlayer();
            OnRestart?.Invoke();
            applicationIsQuitting = false;
        }
    }

    public void ResetState()
    {
        GmStateMachine.MoveNext(GmStateMachine.commands["Restart"]);
        if(gameState == tempState.stopDie)
        {
            gameState = tempState.idle;
            applicationIsQuitting = false;
        }
    }

    public void SettingsToggled()
    {
        if(gameState >= tempState.idle && gameState < tempState.pause)
        {
            GmStateMachine.MoveNext(GmStateMachine.commands["Pause"]);
            prevState = gameState;
            OnSettingsOpened?.Invoke();
            gameState = tempState.pause;
        }
        else if(gameState == tempState.pause)
        {
            GmStateMachine.MoveNext(GmStateMachine.commands["Unpause"]);
            gameState = prevState;
            OnSettingsClosed?.Invoke();
        }
    }
}
