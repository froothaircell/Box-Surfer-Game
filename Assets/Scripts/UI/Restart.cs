using UnityEngine;


/// <summary>
/// Restarts the scene and makes the button viewable under certain conditions
/// </summary>
public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        GameManager.GameManagerInstance.Restart();
    }
}
