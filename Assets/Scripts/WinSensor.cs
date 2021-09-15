using UnityEngine;
using UnityEngine.Events;

public class WinSensor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent winEvent = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") 
            || collision.collider.CompareTag("Player Base") 
            || collision.collider.CompareTag("Character"))
        {
            winEvent.Invoke();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
