using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform
        camTarget,
        camRotationTarget,
        staticBase;
    [SerializeField]
    private float
        yOffset = 3f,
        followSmoothingValue = 0.125f;
    [SerializeField]
    private bool
        followPosition,
        lookAtPosition;


    // Start is called before the first frame update
    private void Start()
    {
        transform.position = camTarget.position;
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // If true, camera takes the position and rotation specified
        if (followPosition)
        {
            Vector3 smoothedPosition = Vector3.Lerp(
                    transform.position,
                    new Vector3(
                        camTarget.position.x, 
                        staticBase.position.y + yOffset, 
                        camTarget.position.z),
                    followSmoothingValue * Time.deltaTime);
            Quaternion smoothedRotation = Quaternion.Slerp(
                transform.rotation,
                camRotationTarget.rotation,
                followSmoothingValue * Time.deltaTime);
            transform.position = smoothedPosition;
            transform.rotation = smoothedRotation;
        }
    }
}
