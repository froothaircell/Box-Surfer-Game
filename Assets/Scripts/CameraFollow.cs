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
    private float followSmoothingValue = 0.125f;
    [SerializeField]
    private float lookSmoothingValue = 0.125f;
    [SerializeField]
    private float distanceSmoothingValue = 0.125f;

    private void Awake()
    {
        mainCam = transform.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = camTarget.position;
        transform.LookAt(lookTarget); 
    }

    private void Update()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, camTarget.position, followSmoothingValue * Time.deltaTime);
        transform.position = smoothedPosition;
        Quaternion desiredRotation = Quaternion.LookRotation(lookTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, lookSmoothingValue * Time.deltaTime);
    }
}
