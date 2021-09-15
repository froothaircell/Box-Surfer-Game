using UnityEngine;

// Adds animation according to trigger calls and variable changes
public class AnimationPicker : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float airSpeedThreshold;

    private Animator animator;
    private bool hasWon = false;
    private bool isDeadOrHasStopped = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hasWon = false;
        isDeadOrHasStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for death before carrying out animations
        if(!isDeadOrHasStopped)
        {
            if(rb)
            {
                // Attaches the AirSpeed float to the downwards velocity
                // of the object. A threshold is applied to remove undue
                // rigidity
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

    // Function that is called via event to set the win flag
    public void Win()
    {
        hasWon = true; 
    }

    // Function that is called via event to activate death animation
    public void KillOrCelebrate()
    {
        isDeadOrHasStopped = true;
        if(!hasWon)
        {
            animator.SetTrigger("Death");
        }
        else
        {
            animator.SetTrigger("Chicken");
        }
    }
}
