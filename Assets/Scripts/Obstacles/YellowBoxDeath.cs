using UnityEngine;

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
