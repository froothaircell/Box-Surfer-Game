using UnityEngine;

/// <summary>
/// Detects a left turn trigger and invokes a parent function to turn the
/// character by 90 degrees applies the same function to the camera rotation
/// </summary>
public class DetectRotationTrigger : MonoBehaviour
{
    private Movement parentMovement;
    private bool isLeft = true;

    public bool IsLeft 
    { 
        get { return isLeft; }  
        set { isLeft = value; } 
    }

    private void Start()
    {
        parentMovement = GetComponentInParent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parentMovement.OnTriggerEnterChild(other, IsLeft);
    }
}
