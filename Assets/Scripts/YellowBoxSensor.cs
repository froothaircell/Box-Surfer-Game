using UnityEngine;
using UnityEngine.Events;

// Checks for player and adds a yellow box accordingly via event
public class YellowBoxSensor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent yellowEvent = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        // Only used for debugging purposes
        if(Input.GetButtonDown("Jump"))
        {
            yellowEvent.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            yellowEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
