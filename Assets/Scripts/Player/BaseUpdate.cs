using UnityEngine;

/// <summary>
/// This function updates the base position of the player by following a
/// provided transform
/// </summary>
public class BaseUpdate : MonoBehaviour
{
    [SerializeField]
    private Transform BasePosition;
    [SerializeField]
    private float 
        yOffset = 0f,
        zOffset = 0f;

    private void Awake()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            BasePosition.localPosition.y + yOffset,
            BasePosition.localPosition.z + zOffset);
    }

    private void Update()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            BasePosition.localPosition.y + yOffset, 
            BasePosition.localPosition.z + zOffset);
    }
}
