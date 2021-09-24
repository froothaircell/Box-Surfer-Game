﻿using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Restarts the scene and makes the 
/// button viewable under certain conditions
/// </summary>
public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        SceneManager.LoadScene("SampleScene");
    }
}