using UnityEngine;

/// <summary>
/// Removes a yellow box upon collision with the front face or, if no yellow box
/// exists, kills the player via event
/// </summary>
public class RedBoxSensor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.attachedRigidbody.isKinematic = true;
            collision.collider.transform.parent = null;
        }
        else if(collision.collider.CompareTag("Player Base") 
            || collision.collider.CompareTag("Character"))
        {
            GameManager.GameManagerInstance.StoppageOrDeath();
        }    

    }
}
