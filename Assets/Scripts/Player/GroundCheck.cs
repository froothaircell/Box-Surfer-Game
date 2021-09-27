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
    private Transform
        trailPosition,
        frontBoxCastPosition,
        leftBoxCastPosition,
        rightBoxCastPosition;

    private TrailRenderer trailRenderer;
    private RaycastHit frontHit;
    private RaycastHit leftHit;
    private RaycastHit rightHit;
    private bool frontHitDetected;
    private bool leftHitDetected;
    private bool rightHitDetected;
    private bool baseOnGround;
    private float 
        frontCorrectedValue,
        leftCorrectedValue,
        rightCorrectedValue,
        actualDistanceFromGround,
        result;

    public float DistanceFromGround
    {
        get { return frontCorrectedValue; }
    }

    private void Awake()
    {
        frontHitDetected = false;
        baseOnGround = false;
        trailRenderer = trailPosition.GetComponent<TrailRenderer>();
        // boxCastPosition = transform.GetChild(0);
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
        if(frontBoxCastPosition)
        {
            if(frontHitDetected)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(frontBoxCastPosition.position, -transform.up * frontHit.distance);
                Gizmos.DrawWireCube(frontBoxCastPosition.position - (transform.up * frontCorrectedValue), transform.lossyScale/3);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(frontBoxCastPosition.position, -transform.up * maxDistance);
            }
            if (leftHitDetected)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(leftBoxCastPosition.position, -transform.up * leftHit.distance);
                Gizmos.DrawWireCube(leftBoxCastPosition.position - (transform.up * frontCorrectedValue), transform.lossyScale / 3);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(leftBoxCastPosition.position, -transform.up * maxDistance);
            }
            if (rightHitDetected)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(rightBoxCastPosition.position, -transform.up * rightHit.distance);
                Gizmos.DrawWireCube(rightBoxCastPosition.position - (transform.up * frontCorrectedValue), transform.lossyScale / 3);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(rightBoxCastPosition.position, -transform.up * maxDistance);
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
        frontHitDetected = Physics.BoxCast(
            frontBoxCastPosition.position,
            transform.lossyScale/3,
            -transform.up,
            out frontHit,
            transform.rotation,
            maxDistance,
            layerMask);

        leftHitDetected = Physics.BoxCast(
            leftBoxCastPosition.position,
            transform.lossyScale / 3,
            -transform.up,
            out leftHit,
            transform.rotation,
            maxDistance,
            layerMask);

        rightHitDetected = Physics.BoxCast(
            rightBoxCastPosition.position,
            transform.lossyScale / 3,
            -transform.up,
            out rightHit,
            transform.rotation,
            maxDistance,
            layerMask);

        // Check hits on front, left and right
        if (frontHitDetected)
        {
            frontCorrectedValue = frontHit.distance + 0.13f;
        }
        if (leftHitDetected)
        {
            leftCorrectedValue = leftHit.distance + 0.13f;
        }
        if (rightHitDetected)
        {
            rightCorrectedValue = rightHit.distance + 0.13f;
        }
        actualDistanceFromGround = frontCorrectedValue - (0.8f * boxManager.BoxSize);

        // Check if the difference between any of the box casts exceeds a certain threshold
        if(frontHitDetected && leftHitDetected && rightHitDetected &&
            Mathf.Abs(frontCorrectedValue - rightCorrectedValue) < 0.5f && 
            Mathf.Abs(frontCorrectedValue - rightCorrectedValue) < 0.5f)
        {
            if(baseOnGround)
            {
                if(actualDistanceFromGround > distanceThreshold || !frontHitDetected)
                {
                    //Debug.Log("base is getting off ground");
                    return false;
                }
                else
                {
                    if(frontHit.collider.gameObject.layer == 9)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                if(actualDistanceFromGround > distanceThreshold || !frontHitDetected)
                {
                    //Debug.Log("Entire body off ground");
                    return false;
                }
                else
                {
                    if (frontHit.collider.gameObject.layer == 9)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        else
        {
            return false;
        }
    }

    // Set position of the trail renderer according to the
    private void RepositionTrail()
    {
        if(!baseOnGround)
        {
            result = frontHit.point.y + trailOffset;
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
