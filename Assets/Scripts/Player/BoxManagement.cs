using UnityEngine;

/// <summary>
/// Manage boxes under the player and add according to requirement
/// </summary>
public class BoxManagement : MonoBehaviour
{
    [SerializeField]
    private int boxSize;
    [SerializeField]
    private GameObject boxPrefab;
    [SerializeField]
    private Transform 
        character,
        charCube;
    [SerializeField]
    private GroundCheck groundChecker;

    private int newBoxSize;

    public int BoxSize
    {
        get { return boxSize; }
        set { boxSize = value; }
    }

    private void Start()
    {
        boxSize = 0;
        newBoxSize = boxSize;
    }

    private void Update()
    {
        if(newBoxSize > boxSize)
        {
            int iter = 0;
            // As a precaution this loop can only run a maximum of 10 times
            while(newBoxSize > boxSize && iter < 10)
            {
                // NOTE: This is using a fixed height from the ground. It may
                // need to be changed once we introduce ramps
                float boxHeight = 36f + boxSize * (0.8f);
                
                // Set positions of character and base cube according to the box
                // height
                character.position = new Vector3(
                    character.position.x, 
                    boxHeight + 0.85f + 0.25f, 
                    character.position.z);
                charCube.position = new Vector3(
                    charCube.position.x, 
                    boxHeight + 0.42f + 0.25f, 
                    charCube.position.z);

                Instantiate(
                    boxPrefab,
                    new Vector3(
                        transform.position.x,
                        boxHeight - 0.15f,
                        transform.position.z),
                    Quaternion.identity,
                    transform);
                
                boxSize++;
                iter++;
            }
        }
        boxSize = transform.childCount;
        newBoxSize = boxSize;
    }

    // Run by a yellow box whenever it collides with the player
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
