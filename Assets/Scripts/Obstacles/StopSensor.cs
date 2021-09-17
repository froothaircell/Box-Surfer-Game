using UnityEngine;
using UnityEngine.Events;

public class StopSensor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent stopEvent = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")
            || collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            Debug.Log("we got here");
            stopEvent.Invoke();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
