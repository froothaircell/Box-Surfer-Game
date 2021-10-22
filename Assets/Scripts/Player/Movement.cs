using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the movement of the player
/// </summary>
public class Movement : MonoBehaviour
{
    public Transform localPlayerPosition;

    [SerializeField]
    private float
        smoothingValue = 1.8f,
        speedFactor = 10f,
        turnSpeed = 10f,
        distanceClamp = 4f, 
        rotationSpeed = 3f;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private bool 
        isLeft = true, 
        touchControls;

    // To reset position for debugging pusposes
    Vector3 InitPos; 
    private float travelDistance;
    private bool
        firstClick = false, // Depends on the game manager state
        isRotating = false,
        isDeadOrHasStopped = false,
        settingsOpened = false;
    private Vector3 initialMousePosition;
    private IEnumerator coroutine;

    public float SpeedFactor
    {
        get { return speedFactor; }
    }

    private void Awake()
    {
        firstClick = false;
        isRotating = false;
        isDeadOrHasStopped = false;
    }

    private void Start()
    {
        // Add listeners to events
        GameManager.GameManagerInstance.OnRun += StartMoving;
        GameManager.GameManagerInstance.OnSettingsOpened += PauseForSettings;
        GameManager.GameManagerInstance.OnSettingsClosed += PlayOnSettingsClosed;
        GameManager.GameManagerInstance.PlayerManagerInstance.OnPlayerStopOrDeath += KillOrCelebrate;

        InitPos = transform.position;
        Input.multiTouchEnabled = false;

        travelDistance = 0f;
        coroutine = Turn90Degrees();
    }

    
    private void FixedUpdate()
    {
        // Use toggle in the inspector to use touch controls or mouse controls;
        if(touchControls)
        {
            // Check if character isn't already dead and settings aren't opened
            if(!isDeadOrHasStopped && !settingsOpened) 
            {
                if(firstClick)
                {
                    // Speed for the turn is kept the same, hence we have an if condition here
                    if(isRotating)
                    {
                        // Move character forwards
                        transform.Translate(
                            turnSpeed * Time.deltaTime * transform.forward,
                            Space.World);
                    }
                    else
                    {
                        // Move character forwards
                        transform.Translate(
                            speedFactor * Time.deltaTime * transform.forward,
                            Space.World);
                    }
                }

                // Check for touch support
                if (Input.touchSupported && Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);


                    // If the screen is being pressed persistently or swiped
                    if(touch.phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        // Check the travel distance of the touch between frames
                        travelDistance =  - touch.deltaPosition.x / 100;


                        // If the touch position moved from the initial recorded position
                        TurnCharacter(
                            ref travelDistance,
                            ref localPlayerPosition,
                            distanceClamp);
                    }
                }
            }
        }
        else
        {
            if(!isDeadOrHasStopped && !settingsOpened)
            {
                if (firstClick)
                {
                    // Reset position
                    if (Input.GetButtonDown("Jump"))
                    {
                        transform.position = InitPos;
                    }

                    // Record initial position of click
                    if (Input.GetButtonDown("Fire1"))
                    {
                        initialMousePosition = Input.mousePosition;
                    }

                    // Calculate distance of mouse cursor from original point in
                    // the last frame to determine movement
                    if (Input.GetButton("Fire1"))
                    {
                        travelDistance = (initialMousePosition.x - Input.mousePosition.x) / 100;
                        initialMousePosition = Input.mousePosition;

                        // Speed for the turn is kept the same, hence we have an if condition here
                        if (isRotating)
                        {
                            // Move character forwards
                            transform.Translate(
                                turnSpeed * Time.deltaTime * transform.forward,
                                Space.World);
                        }
                        else
                        {
                            // Move character forwards
                            transform.Translate(
                                speedFactor * Time.deltaTime * transform.forward,
                                Space.World);
                        }

                        // If the mouse moved from the initial recorded position
                        TurnCharacter(
                            ref travelDistance,
                            ref localPlayerPosition,
                            distanceClamp);
                    }

                    // Record the last click position of the mouse as the initial position
                    if (Input.GetButtonUp("Fire1"))
                    {
                        initialMousePosition = Input.mousePosition;
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        // Remove listeners if game manager hasn't been destroyed
        if(GameManager.GameManagerInstance != null)
        {
            GameManager.GameManagerInstance.OnRun -= StartMoving;
            GameManager.GameManagerInstance.OnSettingsOpened -= PauseForSettings;
            GameManager.GameManagerInstance.OnSettingsClosed -= PlayOnSettingsClosed;
            GameManager.GameManagerInstance.PlayerManagerInstance.OnPlayerStopOrDeath -= KillOrCelebrate;
        }
    }

    // To be referenced from the scroll bar in the config menu
    public void UpdateSpeedFactor(float value)
    {
        speedFactor = value;
    }

    // Activated by base cube's OnTriggerEnter to check for the turn trigger 
    public void OnTriggerEnterChild(Collider other, bool isLeft)
    {
        if(other.CompareTag("Path Interactables"))
        {
            this.isLeft = isLeft;
            isRotating = true;
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
    }

    // Activated by the game manager on state changes
    private void StartMoving()
    {
        firstClick = true;
    }

    // Kill the character using the set unity event
    private void KillOrCelebrate(bool win)
    {
        isDeadOrHasStopped = true;
        if(!win)
        {
            Destroy(GetComponentInChildren<ConfigurableJoint>());
            rb.isKinematic = true;
        }
    }

    // Kill movement in the case that settings are opened
    // NOTE: These functions are kept seperate instead of flipping the boolean
    // in case there might be more use cases for these
    private void PauseForSettings() 
    {
        settingsOpened = true;
    }

    private void PlayOnSettingsClosed()
    {
        settingsOpened = false;
    }

    // Turn the character by manipulating the local transform in the child
    // object (Player Direction Modifier)
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
            if(i > 90)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                isRotating = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
