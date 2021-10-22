using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    // Events to be subscribed to
    public event UnityAction<Transform> OnPlayerCubeSpawn;

    [SerializeField]
    private Transform 
        playerCubePool,
        destroyedPlayerCubePool,
        destroyedYellowCubePool,
        destroyedDiamondPool;
    [SerializeField]
    private GameObject playerCubePrefab;

    private int boxOrderDeficit;

    private void Start()
    {
        boxOrderDeficit = 0;
        StartCoroutine(RefillPool());
        StartCoroutine(BoxOrderDeficitManagement());
    }

    public void SpawnPlayerBox()
    {
        if(playerCubePool.childCount > 0)
        {
            OnPlayerCubeSpawn.Invoke(playerCubePool.GetChild(0));
        }
        else
        {
            boxOrderDeficit++;
        }
    }

    // two different functions are made for collect player box for the two
    // different methods of collection depending on what the box collides with
    // (red box or pool)
    public void CollectPlayerBox1(Transform playerBoxReference)
    {
        playerBoxReference.GetComponent<Rigidbody>().isKinematic = true;
        playerBoxReference.parent = destroyedPlayerCubePool;
    }

    public void CollectPlayerBox2(Transform playerBoxReference)
    {
        playerBoxReference.GetComponent<BoxCollider>().enabled = false;
        playerBoxReference.GetComponent<Rigidbody>().isKinematic = true;
        playerBoxReference.GetComponentInChildren<MeshRenderer>().enabled = false;
        playerBoxReference.parent = destroyedPlayerCubePool;
        playerBoxReference.localPosition = new Vector3(0f, 0f, 0f);
    }

    public void CollectYellowBox(Transform yellowBoxReference)
    {
        yellowBoxReference.GetComponent<BoxCollider>().enabled = false;
        yellowBoxReference.GetComponent<Rigidbody>().isKinematic = true;
        yellowBoxReference.GetComponentInChildren<MeshRenderer>().enabled = false;
        yellowBoxReference.parent = destroyedYellowCubePool;
        yellowBoxReference.localPosition = new Vector3(0f, 0f, 0f);
    }

    public void CollectDiamonds(Transform diamondReference)
    {
        diamondReference.GetComponent<CapsuleCollider>().enabled = false;
        diamondReference.GetComponentInParent<MeshRenderer>().enabled = false;
        diamondReference.GetComponentInParent<DiamondMovement>().enabled = false;
        diamondReference.parent = destroyedDiamondPool;
        diamondReference.localPosition = new Vector3(0f, 0f, 0f);
    }

    // NOTE: Could use a bit of optimization
    public void ResetPool()
    {
        foreach(Transform child in transform)
        {
            foreach(Transform subChild in child)
            {
                GameObject.Destroy(subChild.gameObject);
            }
        }
    }
    
    IEnumerator RefillPool()
    {
        while(true)
        {
            if (playerCubePool.childCount <= 2)
            {
                for(int i = 0; i < 10; i++)
                {
                    GameObject newBox = Instantiate(playerCubePrefab, playerCubePool, false);
                    newBox.GetComponent<Rigidbody>().isKinematic = true;
                    newBox.GetComponent<BoxCollider>().enabled = false;
                    newBox.GetComponentInChildren<MeshRenderer>().enabled = false;

                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
                yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator BoxOrderDeficitManagement()
    {
        while(true)
        {
            if(boxOrderDeficit > 0)
            {
                GameObject newBox = Instantiate(playerCubePrefab, playerCubePool, false);
                OnPlayerCubeSpawn.Invoke(newBox.transform);
                boxOrderDeficit--;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
