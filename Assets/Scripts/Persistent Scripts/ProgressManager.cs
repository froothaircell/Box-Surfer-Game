using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Functions as a tracker for the score and current lavel of the game. Triggers
/// certain events relating to the score and level according to the state change
/// invoked by the game manager. Persists across scenes.
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

    public void ResetScore()
    {
        score = 0;
        level = 1;
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
        level = newLevel;
        OnLevelUpdate?.Invoke(level);
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
