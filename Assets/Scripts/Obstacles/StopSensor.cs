using UnityEngine;

public class StopSensor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")
            || collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            GameManager.instance.StoppageOrDeath();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
