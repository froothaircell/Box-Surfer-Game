using UnityEngine;

/// <summary>
/// Controls the basic movement of the diamond objects
/// </summary>
public class DiamondMovement : MonoBehaviour
{
    [SerializeField]
    private float 
        rotationSpeed,
        translationFrequency,
        translationAmplitude,
        yPositionOffset;
    [SerializeField]
    private bool onFinalStretch = false;

    private float initialHeight = 36.5f;

    private void Start()
    {
        if(onFinalStretch)
        {
            initialHeight = LevelMetaData.LevelDataInstance.LevelInfo.levelAltitude + 10.4f + yPositionOffset;
        }
        else
        {
            initialHeight = LevelMetaData.LevelDataInstance.LevelInfo.levelAltitude + yPositionOffset;
        }
        transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
    }

    private void Update()
    {
        transform.Translate(Mathf.Sin(2 * Mathf.PI * Time.timeSinceLevelLoad * translationFrequency) * translationAmplitude * Vector3.up);
        transform.Rotate(100 * rotationSpeed * Time.deltaTime * Vector3.up);
    }
}
