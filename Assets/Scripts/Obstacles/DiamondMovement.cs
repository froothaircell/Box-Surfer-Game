using UnityEngine;

public class DiamondMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float translationFrequency;
    [SerializeField]
    private float translationAmplitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Mathf.Sin(2 * Mathf.PI * Time.fixedTime));
        transform.Translate(Vector3.up * translationAmplitude * Mathf.Sin(2 * Mathf.PI * Time.fixedTime * translationFrequency));
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * 100);
    }
}
