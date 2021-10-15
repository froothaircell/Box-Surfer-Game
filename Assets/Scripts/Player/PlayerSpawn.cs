using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = LevelMetaData.LevelDataInstance.LevelInfo.playerSpawnPosition;
    }
}
