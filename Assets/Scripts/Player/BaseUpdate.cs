using UnityEngine;

/// <summary>
/// This function updates the base position 
/// of the player
/// </summary>
public class BaseUpdate : MonoBehaviour
{
    [SerializeField]
    private Transform BaseCube;
    [SerializeField]
    private float 
        yOffset = 0f,
        zOffset = 0f;

    private void Awake()
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
