using UnityEngine;
using UnityEngine.UI;

public class CameraUIController : MonoBehaviour
{
    // References to the sliders
    [SerializeField]
    private Slider 
        camRotationHSlider, 
        camRotationVSlider,
        lookRotationXSlider,
        lookRotationYSlider,
        lookRotationZSlider,
        distanceSlider;

    // References to the scripts for each movement
    [SerializeField]
    private CameraRotationHV cameraRotationHV;
    [SerializeField]
    private CameraOffsets cameraOffsets;
    [SerializeField]
    private CameraZoom cameraZoom;

    // Default values for the sliders
    private float
        camH,
        camV,
        lookX,
        lookY,
        lookZ,
        distance;

    // Allowed ranges of each of the movements
    private float camRotationHRange = 90f + 90f;
    private float camRotationVRange = 90f + 90f;
    private float lookRotationXRange = 90f + 90f;
    private float lookRotationYRange = 90f + 90f;
    private float lookRotationZRange = 90f + 90f;
    private float zoomRange = 15f - 0.01f; // Don't make the range exceed 0 into negative values. It creates errors

    private Quaternion currentPivotRotation;
    private Quaternion currentCameraRotation;


    private void Start()
    {
        // Set the value of the sliders according
        // to the state of each of the camera positions
        // NOTE: Ternary operators are necessary to
        // cater to euler angle issues
        camH = camRotationHSlider.value = 
            ((cameraRotationHV.CameraPivotRotation.eulerAngles.y > 180 ? 
            cameraRotationHV.CameraPivotRotation.eulerAngles.y - 360f : 
            cameraRotationHV.CameraPivotRotation.eulerAngles.y) + 90f) / camRotationHRange;
        camV = camRotationVSlider.value = 
            ((cameraRotationHV.CameraPivotRotation.eulerAngles.x > 180 ?
            cameraRotationHV.CameraPivotRotation.eulerAngles.x - 360f :
            cameraRotationHV.CameraPivotRotation.eulerAngles.x) + 90f) / camRotationVRange;
        lookX = lookRotationXSlider.value = 
            ((cameraOffsets.ChildCameraRotation.eulerAngles.x > 180 ?
            cameraOffsets.ChildCameraRotation.eulerAngles.x - 360f :
            cameraOffsets.ChildCameraRotation.eulerAngles.x) + 90f) / lookRotationXRange;
        lookY = lookRotationYSlider.value =
            ((cameraOffsets.ChildCameraRotation.eulerAngles.y > 180 ?
            cameraOffsets.ChildCameraRotation.eulerAngles.y - 360f :
            cameraOffsets.ChildCameraRotation.eulerAngles.y) + 90f) / lookRotationYRange;
        lookZ = lookRotationZSlider.value =
            ((cameraOffsets.ChildCameraRotation.eulerAngles.z > 180 ?
            cameraOffsets.ChildCameraRotation.eulerAngles.z - 360f :
            cameraOffsets.ChildCameraRotation.eulerAngles.z) + 90f) / lookRotationZRange;
        distance = distanceSlider.value = 
            (cameraZoom.CurrentZoomValue - 0.01f) / zoomRange;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    // Functions to be called on the on the onValueChanged of each slider

    // Horizontal Camera Pivot Rotation
    public void HCamRotation(float value)
    {
        Debug.Log("Horizontal rotation begun with value: " + value);
        // Activate horizontal camera rotation
        float realValue = (value * camRotationHRange) - 90f;
        cameraRotationHV.RotateCameraH(realValue);
    }

    public void HCamIncrease()
    {
        camRotationHSlider.value += 0.0015f;
    }

    public void HCamDecrease()
    {
        camRotationHSlider.value -= 0.0015f;
    }

    // Vertical Camera Pivot Rotation
    public void VCamRotation(float value)
    {
        // Activate vertical camera rotation
        float realValue = (value * camRotationVRange) - 90f;
        cameraRotationHV.RotateCameraV(realValue);
    }

    public void VCamIncrease()
    {
        camRotationVSlider.value += 0.0015f;
    }

    public void VCamDecrease()
    {
        camRotationVSlider.value -= 0.0015f;
    }

    // Camera X-axis Rotation
    public void XLookRotation(float value)
    {
        // Activate vertical camera rotation
        float realValue = (value * lookRotationXRange) - 90f;
        cameraOffsets.RotateX(realValue);
    }

    public void XLookIncrease()
    {
        lookRotationXSlider.value += 0.0015f;
    }

    public void XLookDecrease()
    {
        lookRotationXSlider.value -= 0.0015f;
    }

    // Camera Y-axis Rotation
    public void YLookRotation(float value)
    {
        // Activate horizontal look rotation
        float realValue = (value * lookRotationYRange) - 90f;
        cameraOffsets.RotateY(realValue);
    }

    public void YLookIncrease()
    {
        lookRotationYSlider.value += 0.0015f;
    }

    public void YLookDecrease()
    {
        lookRotationYSlider.value -= 0.0015f;
    }

    // Camera Z-axis Rotation
    public void ZLookRotation(float value)
    {
        // Activate horizontal look rotation
        float realValue = (value * lookRotationZRange) - 90f;
        cameraOffsets.RotateZ(realValue);
    }

    public void ZLookIncrease()
    {
        lookRotationZSlider.value += 0.0015f;
    }

    public void ZLookDecrease()
    {
        lookRotationZSlider.value -= 0.0015f;
    }

    // Camera Distance
    public void Zoom(float value)
    {
        // Activate zoom
        float realValue = (value * zoomRange) + 0.01f;
        cameraZoom.ZoomInOutManual(realValue);
    }

    public void ZoomIncrease()
    {
        distanceSlider.value += 0.0015f;
    }

    public void ZoomDecrease()
    {
        distanceSlider.value -= 0.0015f;
    }

    public void ResetDefaults()
    {
        camRotationHSlider.value = camH;
        camRotationVSlider.value = camV;
        lookRotationXSlider.value = lookX;
        lookRotationYSlider.value = lookY;
        lookRotationZSlider.value = lookZ;
        distanceSlider.value = distance;
    }
}
