using UnityEngine;

/// <summary>
/// Check if the player collided with water then kill movement and animation.
/// Also activate the collider underneath the pool first
/// </summary>
public class PoolDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.layer == 4)
        {
            // Activate pool collider
            other.transform.GetChild(0).GetComponent<MeshCollider>().enabled = true;
            GameManager.Instance.StoppageOrDeath();
        }
    }
}
