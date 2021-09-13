using UnityEngine;

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
        if(!isDead)
        {
            if(rb)
            {
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

    public void Kill()
    {
        isDead = true;
        animator.SetTrigger("Death");
    }
}
