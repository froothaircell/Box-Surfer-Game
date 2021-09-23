﻿using UnityEngine;

// Manage boxes under the player and add according to requirement
public class BoxManagement : MonoBehaviour
{
    [SerializeField]
    private int boxSize;
    [SerializeField]
    private GameObject boxPrefab;
    [SerializeField]
    private Transform character;
    [SerializeField]
    private Transform charCube;
    [SerializeField]
    private GroundCheck groundChecker;

    private int newBoxSize;
    // private float boxHeight;

    public int BoxSize
    {
        get { return boxSize; }
        set { boxSize = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        boxSize = 0;
        newBoxSize = boxSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for addition of boxes
        if(newBoxSize > boxSize)
        {
            int iter = 0;
            // As a precaution this loop can only run a maximum of 10 times
            while(newBoxSize > boxSize && iter < 10)
            {
                // Set height of new stack according to total number of boxes
                // NOTE: This is using a fixed height from the ground. It may
                // need to be changed once we introduce ramps
                float boxHeight = 36f + boxSize * (0.8f);
                /*float boxHeight = 
                    ((boxSize * 0.8f) + 0.3f - groundChecker.DistanceFromGround) > 0.5f ? 
                        35.5f + boxSize * (0.8f) : (boxSize * 0.8f) + 0.3f < groundChecker.DistanceFromGround ? 
                            35.5f + boxSize * (0.8f) : 35.5f + groundChecker.DistanceFromGround;
                */
                // Debug.Log("Box Height: " + boxHeight);
                // Debug.Log("Actual Distance from Ground: " + groundChecker.DistanceFromGround);
                // Debug.Log("Calculated Box Height from Ground + Half Base Box: " + ((boxSize * 0.8f) + 0.3f));
                
                // Set positions of character and base cube according to the
                // box height
                character.position = new Vector3(
                    character.position.x, 
                    boxHeight + 0.85f + 0.25f, 
                    character.position.z);
                charCube.position = new Vector3(
                    charCube.position.x, 
                    boxHeight + 0.42f + 0.25f, 
                    charCube.position.z);

                // Instantiate the new box with the requisite properties and
                // increase iteration value and the box size
                GameObject newBoxProperties = Instantiate(
                    boxPrefab,
                    new Vector3(
                        transform.position.x,
                        boxHeight - 0.15f,
                        transform.position.z),
                    Quaternion.identity,
                    transform);
                
                // Destroy(newBoxProperties.GetComponent<YellowBoxSensor>());
                // Destroy(newBoxProperties.transform.GetChild(0).gameObject);
                // newBoxProperties.GetComponent<Rigidbody>().isKinematic = false;
                // newBoxProperties.tag = "Player";
                boxSize++;
                iter++;

                // Adjust the trail position according to the bottom box 
                
                
            }
        }
        boxSize = transform.childCount;
        newBoxSize = boxSize;
    }

    // This function is run by a yellow box whenever it collides with the player
    public void AddBoxes()
    {
        newBoxSize++;
    }

    // This function is for debugging purposes
    public void RemoveBoxes()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
