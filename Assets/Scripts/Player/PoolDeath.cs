using UnityEngine;
using UnityEngine.Events;

public class PoolDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if water then kill movement and animation.
        // Also activate the collider underneath the pool first
        if(other.gameObject.layer == 4)
        {
            // Activate pool collider
            other.transform.GetChild(0).GetComponent<MeshCollider>().enabled = true;
            GameManager.instance.StoppageOrDeath();
        }
    }
}
