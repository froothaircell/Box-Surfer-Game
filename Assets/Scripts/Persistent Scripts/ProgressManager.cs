using UnityEngine;
using UnityEngine.Events;

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
                    GameObject pm = new GameObject();
                    pm.name = typeof(ProgressManager).Name;
                    instance = pm.AddComponent<ProgressManager>();
                }
            }
            return instance;
        }
    }

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

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.OnStopOrDeath += StopOrDeathUIAnimations;
        GameManager.Instance.OnRestart += LevelUpdate;
    }

    // Update is called once per frame
    private void Update()
    {
        
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
