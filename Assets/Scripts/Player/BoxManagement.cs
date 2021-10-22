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

    private int newBoxSize;
    private float boxLength;
    private float baseHeight;

    public int BoxSize
    {
        get { return boxSize; }
        private set { boxSize = value; }
    }

    private void Start()
    {
        boxSize = 0;
        baseHeight = LevelMetaData.LevelDataInstance.LevelInfo.levelAltitude + 4.5f;
        newBoxSize = boxSize;
        boxLength = boxPrefab.transform.localScale.x;
        GameManager.GameManagerInstance.PoolManagerInstance.OnPlayerCubeSpawn += AddBoxes;
    }

    private void Update()
    {
        /*if(newBoxSize > boxSize)
        {
            int iter = 0;
            // As a precaution this loop can only run a maximum of 10 times
            while(newBoxSize > boxSize && iter < 10)
            {
                // NOTE: This is using a fixed height from the ground. It may
                // need to be changed once we introduce ramps
                float boxHeight = baseHeight + boxSize * (boxLength);
                
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

                // Optimize by using pooling
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
        newBoxSize = boxSize;*/
    }

    private void OnDestroy()
    {
        if(GameManager.GameManagerInstance != null)
        {
            GameManager.GameManagerInstance.PoolManagerInstance.OnPlayerCubeSpawn -= AddBoxes;
        }
    }

    // Run by a yellow box whenever it collides with the player
    public void AddBoxes(Transform boxPosition)
    {
        // newBoxSize++;

        // Make the new box a child object
        boxPosition.parent = transform;

        // NOTE: This is using a fixed height from the ground. It may
        // need to be changed once we introduce ramps
        float boxHeight = baseHeight + boxSize * (boxLength);

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

        // first fix position locally and then set the correct height
        boxPosition.localPosition = new Vector3(0f, 0f, 0f);
        boxPosition.position = new Vector3(
            transform.position.x,
            boxHeight - 0.15f,
            transform.position.z);

        // enable the disabled components and make it non kinematic
        boxPosition.GetComponent<BoxCollider>().enabled = true;
        boxPosition.GetComponent<Rigidbody>().isKinematic = false;
        boxPosition.GetComponentInChildren<MeshRenderer>().enabled = true;

        boxSize++;
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
