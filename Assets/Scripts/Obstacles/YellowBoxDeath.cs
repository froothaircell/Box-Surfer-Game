using UnityEngine;

/// <summary>
/// Script that destroys a yellow box if it comes into contact with a water
/// surface
/// </summary>
public class YellowBoxDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            Destroy(gameObject);
        }
    }
}
