using UnityEngine;
using System.Collections;

/// <summary>
/// This script provides a options to rotate the pivot of the camera in the
/// horizontal and vertical axis 
/// </summary>
public class CameraRotationHV : MonoBehaviour
{
    [SerializeField]
    private float
        rotationSpeed;

    private IEnumerator coroutine;

    private Transform cameraPivotPosition;

    public Quaternion CameraPivotRotation
    {
        get { return cameraPivotPosition.localRotation; }
    }

    private void Awake()
    {
        cameraPivotPosition = transform.GetChild(0);
    }

    private void Start()
    {
        coroutine = WinRotation();
        GameManager.Instance.OnStopOrDeath += RotateOnWin;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnStopOrDeath -= RotateOnWin;
        }
    }

    public void RotateCameraH(float angleY)
    {
        RotateCameraPivot(cameraPivotPosition.localRotation.eulerAngles.x, angleY);
    }

    public void RotateCameraV(float angleX)
    {
        RotateCameraPivot(angleX, cameraPivotPosition.localRotation.eulerAngles.y);
    }

    private void RotateCameraPivot(float angleX, float angleY)
    {
        cameraPivotPosition.localRotation = Quaternion.Euler(
            angleX,
            angleY,
            cameraPivotPosition.localRotation.eulerAngles.z);
    }

    private void RotateOnWin(bool win)
    {
        if(win)
        {
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator WinRotation()
    {
        while(true)
        {
            cameraPivotPosition.Rotate(Vector3.up * rotationSpeed, Space.World);
            yield return new WaitForEndOfFrame();
        }
    }
}
