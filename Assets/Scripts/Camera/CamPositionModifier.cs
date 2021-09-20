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
        verticalSmoothingFactor = 0.125f,
        zoomSmoothingFactor = 0.125f,
        yOffset = 2.5f,
        zoomOffsetFactor = 0.1f;

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
    private void LateUpdate()
    {
        // Calculate the new zoom distance from the
        // player according to the number of boxes
        zoomOffsetTotal = boxManager.BoxSize * zoomOffsetFactor;
        float newDistance = baseDistance + zoomOffsetTotal;
        
        // Change position according to the y
        // position of the base of the player
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position, 
            new Vector3(
                transform.position.x, 
                baseLocation.position.y + yOffset, 
                transform.position.z), 
            verticalSmoothingFactor);
        
        // Calculate the normalized vector from the
        // base towards the player and use that to
        // calculate the new position of the camera
        Vector3 normalizedVector = Vector3.Normalize(smoothedPosition - baseLocation.position);
        Vector3 newCalculatedPosition = baseLocation.position + (normalizedVector * newDistance);
        
        // Change position according to calculated
        // position for the camera according to
        // the zoomed out position
        smoothedPosition = Vector3.Lerp(
            smoothedPosition, 
            newCalculatedPosition, 
            zoomSmoothingFactor);
        transform.position = smoothedPosition;
    }
}
