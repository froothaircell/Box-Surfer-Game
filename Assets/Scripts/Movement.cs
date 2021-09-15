using UnityEngine;
using System.Collections;

// Controls the movement of the player
public class Movement : MonoBehaviour
{
    public Transform localPlayerPosition;

    [SerializeField]
    private float smoothingValue = 1.8f;
    [SerializeField]
    private float speedFactor = 0.5f;
    [SerializeField]
    private float distanceClamp = 4f;
    [SerializeField]
    private float rotationSpeed = 3f;
    [SerializeField]
    private bool isLeft = true;

    private float travelDistance;
    private bool isRotating = false;
    private bool hasWon = false;
    private bool isDeadOrHasStopped = false;
    private Vector3 initialMousePosition;
    private IEnumerator coroutine;

    private void Awake()
    {
        isRotating = false;
        hasWon = false;
        isDeadOrHasStopped = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        initialMousePosition = Vector3.zero;
        travelDistance = 0f;
        coroutine = Turn90Degrees();
    }

    private void Update()
    {
        // Check if character isn't already dead
        if(!isDeadOrHasStopped)
        {
            // If the mouse button just got pressed down
            if(Input.GetButtonDown("Fire1"))
            {
                initialMousePosition = Input.mousePosition;
            }

            // If the mouse button is being pressed persistently
            if(Input.GetButton("Fire1"))
            {
                // Calculate distance of mouse cursor from original
                // point in the last frame to determine movement
                // NOTE: The forward movement will not be in the
                // control of the player in the final build. Be
                // sure to change that
                travelDistance = (initialMousePosition.x - Input.mousePosition.x)/100;
                initialMousePosition = Input.mousePosition;

                // Move character forwards
                transform.Translate(
                    speedFactor * Time.deltaTime * transform.forward,
                    Space.World);

                // If the mouse moved from the initial recorded position
                TurnCharacter(
                    ref travelDistance,
                    ref localPlayerPosition,
                    distanceClamp);
            }

            // If the mouse button stopped being pressed
            if(Input.GetButtonUp("Fire1"))
            {
                initialMousePosition = Vector3.zero;
            }
        }

    }

    // Turn the character by manipulating the local transform in the child object (Player Direction Modifier)
    private void TurnCharacter(
        ref float travelDistance,
        ref Transform localPlayerPosition,
        float distanceClamp)
    {
        if (travelDistance != 0)
        {
            float newXPosition = Mathf.Clamp(
                localPlayerPosition.localPosition.x - travelDistance,
                -distanceClamp,
                distanceClamp);

            travelDistance = 0f;
            localPlayerPosition.localPosition = new Vector3(
                Mathf.LerpUnclamped(
                    localPlayerPosition.localPosition.x,
                    newXPosition,
                    smoothingValue),
                localPlayerPosition.localPosition.y,
                localPlayerPosition.localPosition.z);
        }
    }

    // Use the base cube's OnTriggerEnter to activate this function 
    public void OnTriggerEnterChild(Collider other)
    {
        if(other.CompareTag("Path Interactables"))
        {
            isRotating = true;
            StartCoroutine(coroutine);
        }
    }

    // Set the win flag using the set unity event
    public void Win()
    {
        hasWon = true;
    }

    // Kill the character using the set unity event
    public void KillOrCelebrate()
    {
        isDeadOrHasStopped = true;
        if(!hasWon)
        {
            Destroy(GetComponentInChildren<ConfigurableJoint>());
        }
    }

    // Turn the character 90 degrees
    private IEnumerator Turn90Degrees()
    {
        float i = 0;
        while(i < 90 && isRotating)
        {
            if(isLeft)
            {
                i += Mathf.Abs(-rotationSpeed * Time.deltaTime);
                transform.Rotate(
                    0.0f,
                    -rotationSpeed * Time.deltaTime,
                    0.0f,
                    Space.Self);
            }
            else
            {
                i += Mathf.Abs(rotationSpeed * Time.deltaTime);
                transform.Rotate(
                    0.0f,
                    rotationSpeed * Time.deltaTime,
                    0.0f, 
                    Space.Self);
            }
            yield return new WaitForEndOfFrame();
        }
        isRotating = false;
    }
}
