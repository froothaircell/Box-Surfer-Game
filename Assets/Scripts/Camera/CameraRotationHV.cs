using UnityEngine;

public class CameraRotationHV : MonoBehaviour
{
    [SerializeField]
    private float
        camAngleInputX = 0f,
        camAngleInputY = 0f,
        rotationSmoothingFactor = 0.125f;

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
        camAngleInputX = 0f;
        camAngleInputY = 0f;
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

    private void RotateCameraPivot(float angleX, float angleY)
    {
        cameraPivotPosition.localRotation = Quaternion.Euler(
            angleX,
            angleY,
            cameraPivotPosition.localRotation.eulerAngles.z);
    }
}
