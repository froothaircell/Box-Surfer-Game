using UnityEngine;
using UnityEngine.Events;

// Checks for player and removes a yellow box accordingly or,
// if no boxes are present, kill the character via event 
public class RedBoxSensor : MonoBehaviour
{
    [SerializeField]
    UnityEvent DeathEvent = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = null;
        }
        else if(collision.collider.CompareTag("Player Base") 
            || collision.collider.CompareTag("Character"))
        {
            DeathEvent.Invoke();
        }    

    }
}
