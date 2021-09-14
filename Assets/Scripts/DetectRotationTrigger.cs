using UnityEngine;

// Detects a left turn trigger and invokes a parent function to turn the character by 90 degrees
public class DetectRotationTrigger : MonoBehaviour
{
    Movement parentMovement;

    // Start is called before the first frame update
    void Start()
    {
        parentMovement = GetComponentInParent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parentMovement.OnTriggerEnterChild(other);
    }
}
