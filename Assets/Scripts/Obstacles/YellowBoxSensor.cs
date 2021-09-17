using UnityEngine;
using UnityEngine.Events;

// Checks for player and adds a yellow box accordingly via event
public class YellowBoxSensor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent yellowEvent = new UnityEvent();

    private GameObject myParent;

    private void Start()
    {
        myParent = transform.parent.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the player is detected, add a box
        if (collision.collider.CompareTag("Player") 
            || collision.collider.CompareTag("Player Base") 
            || collision.collider.CompareTag("Character"))
        {
            yellowEvent.Invoke();
            Destroy(myParent);
        }
    }
}
