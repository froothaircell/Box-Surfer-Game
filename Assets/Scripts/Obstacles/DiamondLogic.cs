using UnityEngine;
using UnityEngine.Events;

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
                Debug.Log("collision with player detected");
                GameManager.instance.ScoreIncrement();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
