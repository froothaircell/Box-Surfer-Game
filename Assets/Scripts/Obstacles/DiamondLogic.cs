using UnityEngine;
using UnityEngine.UI;

public class DiamondLogic : MonoBehaviour
{
    private bool isDestroyed;

    private void Start()
    {
        isDestroyed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")
            || other.CompareTag("Player Base")
            || other.CompareTag("Character"))
        {
            if (!isDestroyed)
            {
                isDestroyed = true;
                GameManager.GameManagerInstance.ProgressManagerInstance.DiamondCollected(transform.position);
                GameManager.GameManagerInstance.PoolManagerInstance.CollectDiamonds(transform);
            }
        }
    }
}
