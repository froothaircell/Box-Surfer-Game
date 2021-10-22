using UnityEngine;

/// <summary>
/// Checks for player and adds a yellow box accordingly via event
/// </summary>
public class YellowBoxSensor : MonoBehaviour
{
    private bool addBox = false;

    private void Start()
    {
        addBox = false;
    }

    private void Update()
    {
        if(addBox)
        {
            GameManager.GameManagerInstance.PlayerManagerInstance.AddBox();
            GameManager.GameManagerInstance.PoolManagerInstance.CollectYellowBox(transform);
            addBox = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        // if the player is detected, add a box
        if (collision.collider.CompareTag("Player")
            || collision.collider.CompareTag("Player Base")
            || collision.collider.CompareTag("Character"))
        {
            addBox = true;
        }

    }
}
