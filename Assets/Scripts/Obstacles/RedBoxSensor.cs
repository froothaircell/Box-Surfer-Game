﻿using UnityEngine;

// Removes a yellow box upon collision with the front
// face or, if no yellow boxes exist, kills the player
// via event 
public class RedBoxSensor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = null;
        }
        else if(collision.collider.CompareTag("Player Base") 
            || collision.collider.CompareTag("Character"))
        {
            Debug.Log("Heading into the game manager for death ");
            GameManager.Instance.StoppageOrDeath();
        }    

    }
}
