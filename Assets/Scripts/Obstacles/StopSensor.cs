using UnityEngine;

public class StopSensor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")
            || collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            GameManager.Instance.StoppageOrDeath();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
