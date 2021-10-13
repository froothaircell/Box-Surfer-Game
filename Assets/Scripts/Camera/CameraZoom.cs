using UnityEngine;

/// <summary>
/// Modifies the distance of the camera from a parent zoom object by a certain
/// value. The camera is always facing in the direction of the parent position
/// unless offset accordingly
/// </summary>
public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private float
        autoZoomFactor = 0.3f,
        zoomSmoothingFactor = 0.125f;
    [SerializeField]
    private BoxManagement boxManager;

    private Transform 
        childTransform,
        baseCameraTransform;
    private Vector3 newPositionZoom;
    private float currentZoomValue = 0f; // to remember the previous zoom value
    private bool applicationStarted = false;

    public float CurrentZoomValue
    {
        get { return currentZoomValue; }
    }

    private void Awake()
    {
        childTransform = transform.GetChild(0);
        baseCameraTransform = transform.GetChild(0).GetChild(0);
        currentZoomValue = Vector3.Distance(transform.position, childTransform.position);
    }

    // Start is called before the first frame update
    private void Start()
    {
        applicationStarted = true;
    }

    // Update is called once per frame
    private void Update()
    {
        ZoomInOutAuto();
    }

    private void OnDrawGizmos()
    {
        if (applicationStarted)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, childTransform.position);
        }
    }

    private Vector3 Zoom(Vector3 center, Vector3 initialPosition, float zoomOffset)
    {
        Vector3 vectorFromCenter = initialPosition - center;
        float magnitude = vectorFromCenter.magnitude;
        vectorFromCenter *= (magnitude + zoomOffset) / magnitude;
        vectorFromCenter = center + vectorFromCenter;
        return vectorFromCenter;
    }

    public void ZoomInOutManual(float zoom)
    {
        // Calculate new position after arbitrary zoom value is provided
        newPositionZoom = Zoom(
            transform.position,
            childTransform.position,
            zoom - currentZoomValue); // use difference in zoom values

        childTransform.position = newPositionZoom;
        currentZoomValue = zoom;
    }

    public void ZoomInOutAuto()
    {
        float zoomOffsetTotal = boxManager.BoxSize * autoZoomFactor;

        // Calculate new position after arbitrary zoom value is provided
        baseCameraTransform.position = Vector3.Lerp(
            baseCameraTransform.position,
            Zoom(
                transform.position,
                childTransform.position,
                zoomOffsetTotal), // use difference in zoom values
            zoomSmoothingFactor);
    }
}
