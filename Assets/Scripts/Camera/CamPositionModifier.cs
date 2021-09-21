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
    private void FixedUpdate()
    {
        // Calculate the new zoom distance from the
        // player according to the number of boxes
        zoomOffsetTotal = boxManager.BoxSize * zoomOffsetFactor;
        float newDistance = baseDistance + zoomOffsetTotal;
        // Debug.Log("Base distance: " + baseDistance + "\n");
        
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
        // Debug.Log("Normalized vector" + normalizedVector + "\n");
        Vector3 newCalculatedPosition = baseLocation.position + (normalizedVector * newDistance);
        // Debug.Log("Base location position" + baseLocation.position + "\n");
        // Debug.Log((normalizedVector * newDistance) + "\n");
        // Debug.Log("The normalized vector is " + normalizedVector + " and the new calculated position is " + newCalculatedPosition);
        
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
