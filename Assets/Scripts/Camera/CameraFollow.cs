using UnityEngine;

/// <summary>
/// changes the transform of the pivot for the camera according to the player's
/// rotation and position. Please note that the camera consists of 2 pivots, one
/// for the camera follow script and another as a child of the first for the
/// arbitrary offsets
/// </summary>
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
        followPosition;


    private void Start()
    {
        transform.position = LevelMetaData.LevelDataInstance.LevelInfo.playerSpawnPosition;
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
            transform.SetPositionAndRotation(smoothedPosition, smoothedRotation);
        }
    }
}
