using UnityEngine;

public class YellowBoxDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("In Trigger. The layer value is: " + other.gameObject.layer);
        if (other.gameObject.layer == 4)
        {
            Destroy(gameObject);
        }
    }
}
