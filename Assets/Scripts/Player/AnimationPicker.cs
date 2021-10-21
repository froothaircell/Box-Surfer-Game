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

    private Animator animator;
    private bool isDeadOrHasStopped = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isDeadOrHasStopped = false;

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
            animator.SetTrigger("Death");
        }
        else
        {
            confettiAnimations.SetActive(true);
            animator.SetTrigger("Chicken");
        }
    }
}
