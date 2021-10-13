using UnityEngine;

/// <summary>
/// This script provides options to arbitrarily offset the camera object itself
/// by a linear distance or by an angle. Note that these rotations differ in
/// that they directly modify the main camera's rotation and not that of a pivot
/// </summary>
public class CameraOffsets : MonoBehaviour
{
    [SerializeField]
    private Transform basePosition;

    private Transform childCamera;
    private bool applicationStarted = false;

    public Quaternion ChildCameraRotation
    {
        get { return childCamera.localRotation; }
    }

    private void Awake()
    {
        childCamera = transform.GetChild(0).GetChild(0);
    }

    private void Start()
    {
        applicationStarted = true;
    }

    private void OnDrawGizmos()
    {
        if(applicationStarted)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, childCamera.position);
        }
    }

    private void AddPositionOffsets(
        float xPos,
        float yPos,
        float zPos)
    {
        // Add offsets to camera position
        childCamera.localPosition = new Vector3(
            xPos,
            basePosition.position.y - 35.5f + yPos,
            zPos);
    }

    private void AddRotationOffsets(
        float xRot,
        float yRot,
        float zRot)
    {
        // Add offsets to camera rotation
        childCamera.localRotation = Quaternion.Euler(
            xRot,
            yRot,
            zRot);
    }
    
    public void RotateX(float angleX)
    {
        AddRotationOffsets(
            angleX,
            childCamera.localRotation.eulerAngles.y,
            childCamera.localRotation.eulerAngles.z);
    }

    public void RotateY(float angleY)
    {
        AddRotationOffsets(
            childCamera.localRotation.eulerAngles.x,
            angleY, 
            childCamera.localRotation.eulerAngles.z);
    }

    public void RotateZ(float angleZ)
    {
        AddRotationOffsets(
            childCamera.localRotation.eulerAngles.x,
            childCamera.localRotation.eulerAngles.y,
            angleZ);
    }
}
