using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUpdate : MonoBehaviour
{
    [SerializeField]
    private Transform BaseCube;
    [SerializeField]
    private float 
        yOffset = 0f,
        zOffset = 0f;

    private void Start()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            BaseCube.localPosition.y + yOffset,
            BaseCube.localPosition.z + zOffset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            BaseCube.localPosition.y + yOffset, 
            BaseCube.localPosition.z + zOffset);
    }
}
