using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Check if player enters range and trigger win event
/// </summary>
public class WinSensor : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 2f;
    [SerializeField]
    private UnityEvent winEvent = new UnityEvent();

    private RaycastHit hit;
    private bool hitDetected;

    private void LateUpdate()
    {
        CheckForPlayer();
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
