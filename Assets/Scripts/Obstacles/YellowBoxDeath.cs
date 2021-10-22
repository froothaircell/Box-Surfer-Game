using UnityEngine;

/// <summary>
/// Script that destroys a yellow box if it comes into contact with a water
/// surface
/// </summary>
public class YellowBoxDeath : MonoBehaviour
{
    private bool firstRun;

    private void Start()
    {
        firstRun = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4 && !firstRun)
        {
            firstRun = true;
            GameManager.GameManagerInstance.PoolManagerInstance.CollectPlayerBox2(transform);
        }
    }
}
