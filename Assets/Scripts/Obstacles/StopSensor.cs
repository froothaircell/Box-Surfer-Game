using UnityEngine;

/// <summary>
/// Stops the player if it reaches the end of the track (past the win position)
/// </summary>
public class StopSensor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")
            || collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            GameManager.GameManagerInstance.StoppageOrDeath();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
