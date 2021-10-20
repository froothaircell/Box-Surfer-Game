using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Functions as a tracker for the score and current level of the game and
/// loading the next scene upon demand. Triggers certain events relating to the
/// score and level according to the state change invoked by the game manager.
/// Persists across scenes.
/// </summary>
public class ProgressManager : MonoBehaviour
{

    // Events to be subscribed to
    public event UnityAction<int> OnScoreUpdate;
    public event UnityAction<int> OnLevelUpdate;
    public event UnityAction<Vector3> OnAnimationUpdate;
    public event UnityAction<bool> OnDeathAnimationUpdate;

    private static int score = 0;
    private static int level = 1;

    private void Awake()
    {
        level = LevelMetaData.LevelDataInstance.LevelInfo.level;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void StopOrDeathUIAnimations(bool win)
    {
        OnDeathAnimationUpdate?.Invoke(win);
    }

    public void LevelUpdate()
    {
        OnLevelUpdate?.Invoke(level);
    }

    public void LevelUpdate(int newLevel)
    {
        int prevLevel = level; // This helps avoid the ui showing the next level when restarting
        level = newLevel;
        // Load next scene
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        string levelPath = "Scenes/Level_" + newLevel;
        Debug.Log(levelPath);
        if(SceneUtility.GetBuildIndexByScenePath(levelPath) > 0)
        {
            Debug.Log("Scene found");
            if (SceneUtility.GetBuildIndexByScenePath(levelPath) < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(levelPath);
                GameManager.GameManagerInstance.ResetState();
                OnLevelUpdate?.Invoke(level);
            }
        }
        else
        {
            level = prevLevel;
            Debug.Log("Scene not found, restarting");
            // Load the same level instead
            GameManager.GameManagerInstance.Restart();
        }
    }

    public void RestartLevel()
    {
        // Restart level here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelUpdate();
    }

    public void ScoreUpdate()
    {
        OnScoreUpdate?.Invoke(score);
    }

    public void DiamondCollected(Vector3 position)
    {
        score++;
        OnScoreUpdate?.Invoke(score);
        OnAnimationUpdate?.Invoke(position);
    }
}
