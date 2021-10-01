using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffsets : MonoBehaviour
{
    [SerializeField]
    private Transform basePosition;
    [SerializeField]
    private float
        xCamOffset = 0f,
        yCamOffset = 3f,
        zCamOffset = -8f,
        xCamAngleOffset = 0f,
        yCamAngleOffset = 0f,
        zCamAngleOffset = 0f;

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

    // Start is called before the first frame update
    private void Start()
    {
        applicationStarted = true;
    }

    // Update is called once per frame
    private void Update()
    {

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
