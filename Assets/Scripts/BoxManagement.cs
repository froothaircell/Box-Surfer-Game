using UnityEngine;

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
    private Transform trailPosition;

    private int newBoxSize;

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

                // Set positions of character and base cube according to the
                // box height
                character.position = new Vector3(
                    character.position.x, 
                    boxHeight + 1.0f, 
                    character.position.z);
                charCube.position = new Vector3(
                    charCube.position.x, 
                    boxHeight + 0.5f, 
                    charCube.position.z);

                // Instantiate the new box with the requisite properties and
                // increase iteration value and the box size
                GameObject newBoxProperties = Instantiate(
                    boxPrefab,
                    new Vector3(
                        transform.position.x,
                        boxHeight,
                        transform.position.z),
                    Quaternion.identity,
                    transform);
                
                //Destroy(newBoxProperties.GetComponent<YellowBoxSensor>());
                Destroy(newBoxProperties.transform.GetChild(0).gameObject);
                newBoxProperties.GetComponent<Rigidbody>().isKinematic = false;
                newBoxProperties.tag = "Player";
                boxSize++;
                iter++;

                // Adjust the trail position according to the bottom box 
                trailPosition.position = new Vector3(trailPosition.position.x, transform.GetChild(0).position.y - 0.48f, trailPosition.position.z);
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

    public void RemoveBoxes()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void RepositionTrail()
    {
        trailPosition.position = new Vector3(trailPosition.position.x, transform.GetChild(0).position.y - 0.20f, trailPosition.position.z);
        if (transform.childCount == 0)
        {
            trailPosition.position = new Vector3(trailPosition.position.x, charCube.position.y - 0.48f, trailPosition.position.z);
        }
    }
}
