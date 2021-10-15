using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the next scene by fetching the level info
/// </summary>
public class NextLevelButtonLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Cycle between levels 3 to 5 if we reach level 5
    public void LoadNextLevel()
    {
        if(LevelMetaData.LevelDataInstance.LevelInfo)
        {
            int currentLevel = LevelMetaData.LevelDataInstance.LevelInfo.level;
            if(currentLevel >= 1 && currentLevel < 5)
            {
                Debug.Log("Loading next level. Level number: " + (currentLevel + 1));
                GameManager.ProgressManagerInstance.LevelUpdate(currentLevel + 1);
            }
            else
            {
                // if we get to level 5 load back level 3
                GameManager.ProgressManagerInstance.LevelUpdate(3);
            }
        }
    }
}
