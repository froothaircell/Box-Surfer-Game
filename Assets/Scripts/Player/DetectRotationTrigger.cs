using UnityEngine;

// Detects a left turn trigger and invokes a parent
// function to turn the character by 90 degrees applies
// the same function to the camera rotation
public class DetectRotationTrigger : MonoBehaviour
{
    [SerializeField]
    CameraFollow cameraRotation;
    Movement parentMovement;
    bool isLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        parentMovement = GetComponentInParent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parentMovement.OnTriggerEnterChild(other, isLeft);
        //cameraRotation.OnTriggerEnterChild(other, isLeft);
    }
}
