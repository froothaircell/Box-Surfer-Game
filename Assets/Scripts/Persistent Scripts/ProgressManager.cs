﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using StateMachine;

/// <summary>
/// Functions as a tracker for the score and current level of the game and
/// loading the next scene upon demand. Triggers certain events relating to the
/// score and level according to the state change invoked by the game manager.
/// Persists across scenes.
/// </summary>
public class ProgressManager : IStateMachine
{
    // Events to be subscribed to
    public event UnityAction<int> OnScoreUpdate;
    public event UnityAction<int> OnLevelUpdate;
    public event UnityAction<Vector3> OnDiamondAnimationUpdate;
    public event UnityAction<bool> OnDeathAnimationUpdate;

    private static int score = 0;
    private static int scoreAtLevelStart = 0;
    private static int level = 1;

    public static int Score
    {
        get { return score; }
    }

    private void Awake()
    {
        level = LevelMetaData.LevelDataInstance.LevelInfo.level;
    }

    public void ResetScore()
    {
        scoreAtLevelStart = score = 0;
    }

    public void StopOrDeathUIAnimations(bool win)
    {
        if (win)
            MoveNext(commands["Win"]);
        else
            MoveNext(commands["Die"]);
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
                scoreAtLevelStart = score;
                SceneManager.LoadScene(levelPath);
                GameManager.GameManagerInstance.ResetState();
                GameManager.GameManagerInstance.PlayerManagerInstance.ResetPlayer();
                GameManager.GameManagerInstance.PoolManagerInstance.ResetPool();
                MoveNext(commands["Restart"]);
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
        if (CurrentState == states["Death"] || CurrentState == states["Success"])
            MoveNext(commands["Restart"]);
        if (CurrentState == states["Idle"] || CurrentState == states["Death"] || CurrentState == states["Success"])
        {
            // Restart level here
            score = scoreAtLevelStart;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            LevelUpdate();
        }
    }

    public void ScoreUpdate()
    {
        OnScoreUpdate?.Invoke(score);
    }

    public void DiamondCollected(Vector3 position)
    {
        score++;
        OnScoreUpdate?.Invoke(score);
        OnDiamondAnimationUpdate?.Invoke(position);
    }
}
