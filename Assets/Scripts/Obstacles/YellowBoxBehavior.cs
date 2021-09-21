using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBoxBehavior : MonoBehaviour
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
