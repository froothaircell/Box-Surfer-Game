using UnityEngine;

/// <summary>
/// This monobehavior script simply stores an instance of the level info
/// </summary>
public class LevelMetaData : MonoBehaviour
{
    private static LevelMetaData levelDataInstance;

    public static LevelMetaData LevelDataInstance
    {
        get
        {
            if(levelDataInstance == null)
            {
                levelDataInstance = FindObjectOfType<LevelMetaData>();
                if(levelDataInstance == null)
                {
                    GameObject lmd = new GameObject()
                    {
                        name = "Level Info"
                    };
                    levelDataInstance = lmd.AddComponent<LevelMetaData>();
                }
            }
            return levelDataInstance;
        }
    }

    [SerializeField]
    private LevelDataStructure levelInfo;

    public LevelDataStructure LevelInfo
    {
        get
        {
            return levelInfo;
        }
    }
}
