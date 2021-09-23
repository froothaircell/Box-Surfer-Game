using UnityEngine;

public class PoolDeath : MonoBehaviour
{
    [SerializeField]
    private Movement movementScript;
    [SerializeField]
    private AnimationPicker animationPickerScript;

    private void OnTriggerEnter(Collider other)
    {
        // Check if water then kill movement and animation.
        // Also activate the collider underneath the pool first
        if(other.gameObject.layer == 4)
        {
            // Activate pool collider
            other.transform.GetChild(0).GetComponent<MeshCollider>().enabled = true;

            if(movementScript && animationPickerScript)
            {
                movementScript.KillOrCelebrate();
                animationPickerScript.KillOrCelebrate();
            }
        }
    }
}
