using UnityEngine;

public class DiamondMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float translationFrequency;
    [SerializeField]
    private float translationAmplitude;
    [SerializeField]
    private float initialHeight = 36.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * translationAmplitude * Mathf.Sin(2 * Mathf.PI * Time.timeSinceLevelLoad * translationFrequency));
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * 100);
    }
}
