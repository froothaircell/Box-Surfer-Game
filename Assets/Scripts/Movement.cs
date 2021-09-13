using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 currentPlayerPosition;
    private float travelDistance;
    //private Rigidbody rb;

    [SerializeField]
    private float smoothingValue = 1.8f;
    [SerializeField]
    private float speedFactor = 0.5f;
    [SerializeField]
    private float distanceClamp = 4f;
    private Vector3 newPosition;
    private bool isDead = false;

    private void Awake()
    {
        isDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = Vector3.zero;
        travelDistance = 0f;
    }

    private void Update()
    {
        if(!isDead)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                initialPosition = Input.mousePosition;
                currentPlayerPosition = transform.position;
            }
            if(Input.GetButton("Fire1"))
            {
                travelDistance = (initialPosition.x - Input.mousePosition.x)/100;
                //Debug.Log("The current travel distance is: " + travelDistance);
            
                transform.Translate(transform.forward * speedFactor * Time.deltaTime, Space.World);
                if(travelDistance != 0)
                {
                    float newXPosition = Mathf.Clamp(currentPlayerPosition.x - travelDistance, -distanceClamp, distanceClamp);
                    newPosition = new Vector3(
                        newXPosition, 
                        transform.position.y,
                        transform.position.z);
                    travelDistance = 0;
                }
                else
                {
                    newPosition = transform.position;
                }
                // Debug.Log(newPosition);
                transform.position = new Vector3(
                    Mathf.LerpUnclamped(transform.position.x, newPosition.x, smoothingValue),
                    transform.position.y,
                    transform.position.z);
            }
            if(Input.GetButtonUp("Fire1"))
            {
                initialPosition = Vector3.zero;
                // travelDistance = 0f;
            }
        }
    }

    public void Kill()
    {
        isDead = true;
        Destroy(GetComponentInChildren<ConfigurableJoint>());
    }
}
