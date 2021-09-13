using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private int newBoxSize;
    // Start is called before the first frame update
    void Start()
    {
        boxSize = 0;
        newBoxSize = boxSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(newBoxSize > boxSize)
        {
            int iter = 0;
            while(newBoxSize > boxSize && iter < 10)
            {
                Debug.Log("In the while loop");
                float boxHeight = 36f + boxSize * (0.8f);
                Debug.Log(boxHeight);
                character.position = new Vector3(character.position.x, boxHeight + 4.0f, character.position.z);
                charCube.position = new Vector3(charCube.position.x, boxHeight + 2.0f, charCube.position.z);
                Debug.Log(transform.position);
                GameObject newBoxProperties = Instantiate(boxPrefab, new Vector3(transform.position.x, boxHeight, transform.position.z), Quaternion.identity, transform);
                Destroy(newBoxProperties.GetComponent<YellowBoxSensor>());
                newBoxProperties.GetComponent<Rigidbody>().isKinematic = false;
                newBoxProperties.tag = "Player";
                boxSize++;
                iter++;
            }
        }
    }

    public void addBoxes()
    {
        newBoxSize++;
    }
}
