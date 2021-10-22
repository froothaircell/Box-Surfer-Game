using UnityEngine;

/// <summary>
/// Adds animations according to event invocations and kinematic changes
/// </summary>
public class AnimationPicker : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GameObject confettiAnimations;
    [SerializeField]
    private float airSpeedThreshold;

    private bool death = false;
    private Animator animator;
    private bool isDeadOrHasStopped = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isDeadOrHasStopped = false;
        death = false;
    }

    private void Start()
    {
        GameManager.GameManagerInstance.PlayerManagerInstance.OnPlayerStopOrDeath += KillOrCelebrate;
    }

    private void Update()
    {
        if(!isDeadOrHasStopped)
        {
            if(rb)
            {
                // Attaches the AirSpeed float to the downwards velocity of the
                // object. A threshold is applied to remove undue rigidity
                animator.SetFloat("AirSpeed", rb.velocity.y);
                if (rb.velocity.y < -airSpeedThreshold)
                {
                    animator.SetBool("HasLanded", false);
                    animator.SetTrigger("Falling");
                    Debug.Log("Running fall animation");
                }
                else
                {
                    animator.SetBool("HasLanded", true);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if(GameManager.GameManagerInstance != null)
        {
            GameManager.GameManagerInstance.PlayerManagerInstance.OnPlayerStopOrDeath -= KillOrCelebrate;
        }
    }

    private void KillOrCelebrate(bool win)
    {
        isDeadOrHasStopped = true;
        if(!win)
        {
            // rb.velocity = new Vector3(0f, 0f, 0f);
            animator.SetFloat("AirSpeed", 0f);
            // animator.SetBool("HasLanded", true);
            animator.SetTrigger("Death");
            Debug.Log("Death trigger set");
        }
        else
        {
            confettiAnimations.SetActive(true);
            animator.SetTrigger("Chicken");
        }
    }
}
