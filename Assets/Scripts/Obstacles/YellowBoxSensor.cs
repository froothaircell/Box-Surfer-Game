using UnityEngine;
using UnityEngine.Events;

// Checks for player and adds a yellow box accordingly via event
public class YellowBoxSensor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent yellowEvent = new UnityEvent();

    private bool addBox = false;

    private void Start()
    {
        addBox = false;
    }

    private void Update()
    {
        if(addBox)
        {
            yellowEvent.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        // if the player is detected, add a box
        if (collision.collider.CompareTag("Player")
            || collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            addBox = true;
        }

    }
}
