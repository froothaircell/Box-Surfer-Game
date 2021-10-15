﻿using UnityEngine;

/// <summary>
/// Removes a yellow box upon collision with the front face or, if not yellow
/// box exists, kills the player via event
/// </summary>
public class ElevatedPlatformSensor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = null;
        }
        else if (collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            GameManager.GameManagerInstance.StoppageOrDeath();
        }

    }
}
