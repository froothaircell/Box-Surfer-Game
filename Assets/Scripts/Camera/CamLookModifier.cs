using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookModifier : MonoBehaviour
{
    [SerializeField]
    private Transform baseLocation;
    [SerializeField]
    private float smoothingFactor = 0.125f;
    [SerializeField]
    private float yOffset = -2.5f;
    private readonly float baseHeight = 35.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(baseLocation.position.y > baseHeight)
        {
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                new Vector3(
                    transform.position.x,
                    baseLocation.position.y + yOffset,
                    transform.position.z),
                smoothingFactor);
            transform.position = smoothedPosition;
        }
    }
}
