using UnityEngine;

/// <summary>
/// This monobehacior script simply stores an instance of the level info
/// </summary>
public class LevelMetaData : MonoBehaviour
{
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
