using UnityEngine;

public class NextLevelButtonLogic : MonoBehaviour
{
    private LevelMetaData levelInfo;

    // Start is called before the first frame update
    void Start()
    {
        levelInfo = FindObjectOfType<LevelMetaData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        if(levelInfo)
        {
            if(levelInfo.LevelInfo.level >= 1 && levelInfo.LevelInfo.level < 5)
            {
                // Load next scene
            }
            else
            {
                // if we get to level 5 load back level 3
            }
        }
    }
}
