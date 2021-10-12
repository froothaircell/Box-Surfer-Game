using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    private static ProgressManager instance;

    public static ProgressManager Instance
    {
        get
        {
            Debug.Log("Progress Manager getter called with application quit status: " + applicationIsQuitting);
            if (applicationIsQuitting)
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
                instance = FindObjectOfType<ProgressManager>();

                if(instance == null)
                {
                    Debug.Log("Creating new Progress Manager");
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
        Debug.Log("Progress Manager awoken with ID: " + gameObject.GetInstanceID());
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("Destroying Progress Manager object with ID: " + gameObject.GetInstanceID());
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
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        Debug.Log("Progress Manager destroyed with ID: " + gameObject.GetInstanceID());
        if(gameObject.GetComponent<ProgressManager>().GetInstanceID() == Instance.GetInstanceID())
        {
            applicationIsQuitting = true;
        }
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnStopOrDeath -= StopOrDeathUIAnimations;
        }
    }

    private void StopOrDeathUIAnimations(bool win)
    {
        Debug.Log("We got into the progress manager death function");
        OnDeathAnimationUpdate.Invoke(win);
    }

    public void LevelUpdate()
    {
        OnLevelUpdate.Invoke(level);
    }

    public void DiamondCollected(Vector3 position)
    {
        score++;
        OnScoreUpdate(score);
        OnAnimationUpdate(position);
    }

}
