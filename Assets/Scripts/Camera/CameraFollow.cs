using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField]
    private Transform camTarget;
    [SerializeField]
    private Transform lookTarget;
    [SerializeField]
    private float
        followSmoothingValue = 0.125f,
        lookSmoothingValue = 0.125f,
        distanceSmoothingValue = 0.125f,
        rotationSpeed = 3.0f,
        rotationSmoothingValue = 0.125f;
    [SerializeField]
    private bool 
        followPosition, 
        lookAtPosition,
        isLeft;

    private bool isRotating = false;
    private IEnumerator coroutine;

    private void Awake()
    {
        mainCam = transform.GetComponent<Camera>();
        isRotating = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = camTarget.position;
        //coroutine = Rotate90Degrees();
    }

    private void Update()
    {
        if(followPosition)
        {
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                camTarget.position, 
                followSmoothingValue * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        if(lookAtPosition)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(lookTarget.position - transform.position);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.Euler(
                    transform.rotation.eulerAngles.x, 
                    desiredRotation.eulerAngles.y, 
                    transform.eulerAngles.z), 
                lookSmoothingValue * Time.deltaTime);
        }
    }
}
