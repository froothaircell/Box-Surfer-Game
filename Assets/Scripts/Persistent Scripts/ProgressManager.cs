using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Functions as a tracker for the score and current lavel of the game. Triggers
/// certain events relating to the score and level according to the state change
/// invoked by the game manager. Persists across scenes.
/// </summary>
public class ProgressManager : MonoBehaviour
{
    private static ProgressManager instance;

    public static ProgressManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            if(instance)
            {

            }

            if(instance == null)
            {
                instance = FindObjectOfType<ProgressManager>();

                if(instance == null)
                {
                    GameObject pm = new GameObject()
                    {
                        name = typeof(ProgressManager).Name
                    };
                    instance = pm.AddComponent<ProgressManager>();
                }
            }
            return instance;
        }
    }

    // Events to be subscribed to
    public event UnityAction<int> OnScoreUpdate;
    public event UnityAction<int> OnLevelUpdate;
    public event UnityAction<Vector3> OnAnimationUpdate;
    public event UnityAction<bool> OnDeathAnimationUpdate;

    private static bool applicationIsQuitting = false;
    private static int score = 0;
    private static int level = 1;

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
        if(instance.GetInstanceID() == this.gameObject.GetComponent<ProgressManager>().GetInstanceID())
        {
            score = 0;
            level = 1;
        }
    }

    private void Start()
    {
        GameManager.Instance.OnStopOrDeath += StopOrDeathUIAnimations;
        GameManager.Instance.OnRestart += LevelUpdate;
    }

    private void OnDestroy()
    {
        if(gameObject.GetComponent<ProgressManager>().GetInstanceID() == Instance.GetInstanceID())
        {
            applicationIsQuitting = true;
        }
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnStopOrDeath -= StopOrDeathUIAnimations;
            GameManager.Instance.OnRestart -= LevelUpdate;
        }
    }

    private void StopOrDeathUIAnimations(bool win)
    {
        OnDeathAnimationUpdate?.Invoke(win);
    }

    public void LevelUpdate()
    {
        OnLevelUpdate?.Invoke(level);
    }

    public void LevelUpdate(int newLevel)
    {
        level = newLevel;
        OnLevelUpdate?.Invoke(level);
    }

    public void ScoreUpdate()
    {
        OnScoreUpdate(score);
    }

    public void DiamondCollected(Vector3 position)
    {
        score++;
        OnScoreUpdate?.Invoke(score);
        OnAnimationUpdate?.Invoke(position);
    }
}
