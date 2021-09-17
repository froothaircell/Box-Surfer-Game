using UnityEngine;
using UnityEngine.Events;

public class WinSensor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent winEvent = new UnityEvent();
    [SerializeField]
    private float maxDistance = 2f;

    private RaycastHit hit;
    private bool hitDetected;

    private void LateUpdate()
    {
        CheckForPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")
            || collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            winEvent.Invoke();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    // Provides ease of debugging
    private void OnDrawGizmos()
    {
        if (hitDetected)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position - (transform.forward * hit.distance), transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, -transform.forward * maxDistance);
        }
    }

    private void CheckForPlayer()
    {
        hitDetected = Physics.BoxCast(
            transform.position,
            transform.lossyScale,
            -transform.forward,
            out hit,
            transform.rotation,
            maxDistance);
        if(hitDetected 
            && (hit.collider.CompareTag("Player")
            || hit.collider.CompareTag("Player Base")
            || hit.collider.CompareTag("Character")))
        {
            winEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
