using UnityEngine;
using UnityEngine.Events;

// Removes a yellow box upon collision with the front
// face or, if no yellow boxes exist, kills the player
// via event
public class ElevatedPlatformSensor : MonoBehaviour
{
    [SerializeField]
    UnityEvent PlatformDeathEvent = new UnityEvent();
    [SerializeField]
    UnityEvent TrailRepositioningEvent = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("We got collision");
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = null;
            TrailRepositioningEvent.Invoke();
        }
        else if (collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            PlatformDeathEvent.Invoke();
        }

    }
}
