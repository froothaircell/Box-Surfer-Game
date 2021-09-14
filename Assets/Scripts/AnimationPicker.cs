using UnityEngine;

// Adds animation according to trigger calls and variable changes
public class AnimationPicker : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float airSpeedThreshold;

    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for death before carrying out animations
        if(!isDead)
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

    // Function that is called via event to activate death animation
    public void Kill()
    {
        isDead = true;
        animator.SetTrigger("Death");
    }
}
