using UnityEngine;
using System.Collections;

public class CameraRotationHV : MonoBehaviour
{
    [SerializeField]
    private float
        rotationSpeed,
        rotationSmoothingFactor = 0.125f;

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

    // Start is called before the first frame update
    private void Start()
    {
        coroutine = WinRotation();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void RotateCameraH(float angleY)
    {
        RotateCameraPivot(cameraPivotPosition.localRotation.eulerAngles.x, angleY);
    }

    public void RotateCameraV(float angleX)
    {
        RotateCameraPivot(angleX, cameraPivotPosition.localRotation.eulerAngles.y);
    }

    public void RotateOnWin(bool win)
    {
        if(win)
        {
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
    }

    private void RotateCameraPivot(float angleX, float angleY)
    {
        cameraPivotPosition.localRotation = Quaternion.Euler(
            angleX,
            angleY,
            cameraPivotPosition.localRotation.eulerAngles.z);
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
