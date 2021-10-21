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
                Debug.Log("Diamonds detected the collision");
                isDestroyed = true;
                GameManager.GameManagerInstance.ProgressManagerInstance.DiamondCollected(transform.position);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
