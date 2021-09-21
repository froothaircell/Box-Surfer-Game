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
        minTrailThreshold = 35.5f, // For clamping the vertical position of the trail
        maxTrailThreshold = 100f;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private BoxManagement boxManager;
    [SerializeField]
    private Transform trailPosition;

    private Transform boxCastPosition;
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
        boxCastPosition = transform.GetChild(0);
    }

    // Keep repositioning the y-value of the trail
    // and set it as active only if the player is grounded 
    private void FixedUpdate()
    {
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
        if(boxCastPosition)
        {
            if(hitDetected)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(boxCastPosition.position, -transform.up * hit.distance);
                Gizmos.DrawWireCube(boxCastPosition.position - (transform.up * correctedValue), transform.lossyScale/3);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(boxCastPosition.position, -transform.up * maxDistance);
            }
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
            //Debug.Log("Collision Started");
            baseOnGround = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            //Debug.Log("Collision sustained");
            result = collision.GetContact(0).point.y + trailOffset;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            //Debug.Log("Exiting collision");
            baseOnGround = false;
        }
    }

    // Check if the player is grounded. If the base
    // cube is in a collision but the boxcast has a
    // longer distance then that is taken as a false
    // result
    private bool IsGrounded()
    {
        hitDetected = Physics.BoxCast(
            boxCastPosition.position,
            transform.lossyScale/3,
            -transform.up,
            out hit,
            transform.rotation,
            maxDistance,
            layerMask);
        if (hitDetected)
        {
            correctedValue = hit.distance + 0.13f;
        }
        actualDistanceFromGround = correctedValue - (0.8f * boxManager.BoxSize);
        if(baseOnGround)
        {
            if(actualDistanceFromGround > distanceThreshold)
            {
                //Debug.Log("base is getting off ground");
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if(actualDistanceFromGround > distanceThreshold)
            {
                //Debug.Log("Entire body off ground");
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
