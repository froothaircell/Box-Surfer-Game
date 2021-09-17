using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPositionModifier : MonoBehaviour
{
    [SerializeField]
    private Transform baseLocation;
    [SerializeField]
    private BoxManagement boxManager;
    [SerializeField]
    private float
        smoothingFactor = 0.125f,
        zoomSmoothingFactor = 0.125f,
        yOffset = 2.5f,
        zoomOffset = 0.1f;

    private float baseDistance = 0f;
    
    private float zoomOffsetTotal;

    private void Awake()
    {
        zoomOffsetTotal = 0f;
    }

    private void Start()
    {
        baseDistance = Vector3.Distance(transform.position, baseLocation.position);
    }

    // Update is called once per frame
    private void Update()
    {
        zoomOffsetTotal = boxManager.BoxSize * zoomOffset;
        float newDistance = baseDistance + zoomOffsetTotal;
        Debug.Log("The new distance is: " + newDistance);
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position, 
            new Vector3(
                transform.position.x, 
                baseLocation.position.y + yOffset, 
                transform.position.z), 
            smoothingFactor);
        Debug.Log("The first smoothed position is: " + smoothedPosition);
        Vector3 normalizedVector = Vector3.Normalize(smoothedPosition - baseLocation.position);
        Vector3 newCalculatedPosition = baseLocation.position + (normalizedVector * newDistance);
        Debug.Log("The calculated position is: " + newCalculatedPosition);
        smoothedPosition = Vector3.Lerp(
            smoothedPosition, 
            newCalculatedPosition, 
            zoomSmoothingFactor);
        Debug.Log("The second smoothed position is: " + smoothedPosition);
        transform.position = smoothedPosition;
    }
}
