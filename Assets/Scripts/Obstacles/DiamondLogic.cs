using UnityEngine;
using UnityEngine.UI;

public class DiamondLogic : MonoBehaviour
{
    private bool isDestroyed;

    private void Start()
    {
        isDestroyed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") 
            || collision.collider.CompareTag("Player Base") 
            || collision.collider.CompareTag("Character"))
        {
            if(!isDestroyed)
            {
                isDestroyed = true;
                ProgressManager.Instance.DiamondCollected(transform.position);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
