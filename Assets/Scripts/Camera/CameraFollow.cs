using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform camTarget;
    [SerializeField]
    private Transform lookTarget;
    [SerializeField]
    private float
        followSmoothingValue = 0.125f,
        lookSmoothingValue = 0.125f;
    // distanceSmoothingValue = 0.125f,
    // rotationSmoothingValue = 0.125f,
    // rotationSpeed = 3.0f;
    [SerializeField]
    private bool
        followPosition,
        lookAtPosition;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = camTarget.position;
    }

    private void LateUpdate()
    {
        if(followPosition)
        {
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                camTarget.position, 
                followSmoothingValue * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        if(lookAtPosition)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(lookTarget.position - transform.position);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.Euler(
                    transform.rotation.eulerAngles.x, 
                    desiredRotation.eulerAngles.y, 
                    transform.rotation.eulerAngles.z), 
                lookSmoothingValue * Time.deltaTime);
            // Debug.Log(transform.rotation.eulerAngles);
        }
    }
}
