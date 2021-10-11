﻿using UnityEngine;
using System.Collections;

// Controls the movement of the player
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
    private bool 
        isLeft = true, 
        touchControls;

    Vector3 InitPos; // For debugging pusposes
    private float travelDistance;
    private bool
        firstClick = false, // Depends on the game manager state
        isRotating = false,
        hasWon = false,
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
        hasWon = false;
        isDeadOrHasStopped = false;

        GameManager.Instance.OnRun += StartMoving;
    }

    // Start is called before the first frame update
    private void Start()
    {
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
            // Check if character isn't already dead and touch is supported
            if(!isDeadOrHasStopped && !settingsOpened) 
            {
                if(firstClick)
                {
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

                if (Input.touchSupported && Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);


                    // If the screen is being pressed persistently or swiped
                    if(touch.phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        // Check the travel distance of the touch between frames
                        travelDistance =  - touch.deltaPosition.x / 100;


                        // If the mouse moved from the initial recorded position
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
                    // Reverse direction of movement
                    if (Input.GetButtonDown("Jump"))
                    {
                        transform.position = InitPos;
                    }

                    // If the mouse button was just pressed down
                    if (Input.GetButtonDown("Fire1"))
                    {
                        initialMousePosition = Input.mousePosition;
                    }

                    // If the mouse button is being pressed persistently
                    if (Input.GetButton("Fire1"))
                    {
                        // Calculate distance of mouse cursor from original
                        // point in the last frame to determine movement
                        travelDistance = (initialMousePosition.x - Input.mousePosition.x) / 100;
                        initialMousePosition = Input.mousePosition;

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

                    // If the mouse button stopped being pressed
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
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnRun -= StartMoving;
        }
    }

    // To be referenced from the scroll bar in the config menu
    public void UpdateSpeedFactor(float value)
    {
        speedFactor = value;
    }

    // Use the base cube's OnTriggerEnter to activate this function 
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

    public void StartMoving()
    {
        firstClick = true;
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

    // Kill movement in the case that settings are opened
    // NOTE: These functions are kept seperate instead of
    // flipping the boolean in case there might be more
    // use cases for these
    public void PauseForSettings() 
    {
        settingsOpened = true;
    }

    public void PlayOnSettingsClosed()
    {
        settingsOpened = false;
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
