using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shoots a spherecast from the bottom of the player
// object and determines if the player is grounded or
// not with the use of distance thresholds
public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private float maxDistance, 
        distanceThreshold, 
        trailOffset, 
        trailSmoothing, 
        minTrailThreshold = 35.5f, 
        maxTrailThreshold = 100f;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private BoxManagement boxManager;
    [SerializeField]
    private Transform trailPosition;

    private TrailRenderer trailRenderer;
    private RaycastHit hit;
    private bool hitDetected;
    private bool baseOnGround;
    private float correctedValue, actualDistanceFromGround, result;


    private void Awake()
    {
        hitDetected = false;
        baseOnGround = false;
        trailRenderer = trailPosition.GetComponent<TrailRenderer>();
    }

    // Keep repositioning the y-value of the trail
    // and set it as active only if the player is grounded 
    private void FixedUpdate()
    {
        // bool rando = IsGrounded();
        // Debug.Log("The status of grounding is: " + rando);
        RepositionTrail();
        if(IsGrounded())
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }

    // Provides ease of debugging
    private void OnDrawGizmos()
    {
        if(hitDetected)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -transform.up * hit.distance);
            Gizmos.DrawWireCube(transform.position - (transform.up * correctedValue), transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, -transform.up * maxDistance);
        }
    }

    // Check if the base cube is connecting with
    // the ground. An analog OnCollisionExit is
    // here for reversing the effects of the enter
    // function
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 8)
        {
            baseOnGround = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            result = collision.GetContact(0).point.y + trailOffset;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            baseOnGround = false;
        }
    }

    // Check if the player is grounded
    private bool IsGrounded()
    {
        if(baseOnGround)
        {
            return true;
        }
        else
        {
            hitDetected = Physics.BoxCast(
                transform.position,
                transform.lossyScale,
                -transform.up,
                out hit,
                transform.rotation,
                maxDistance,
                layerMask);
            if (hitDetected)
            {
                correctedValue = hit.distance + 0.4f;
            }
            actualDistanceFromGround = correctedValue - (0.8f * boxManager.BoxSize);
            if(actualDistanceFromGround > distanceThreshold)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    // Set position of the trail renderer according to the
    private void RepositionTrail()
    {
        if(!baseOnGround)
        {
            result = hit.point.y + trailOffset;
        }
        result = Mathf.Clamp(result, minTrailThreshold, maxTrailThreshold);
        trailPosition.position = Vector3.Lerp(
            trailPosition.position,
            new Vector3(
            trailPosition.position.x,
            result,
            trailPosition.position.z),
            trailSmoothing);
    }
}
