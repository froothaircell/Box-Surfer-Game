using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fetches and updates the score on the panel via a listener to an event
/// invoked by the progress manager
/// </summary>
public class ScorePanel : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.GetChild(0).GetComponent<Text>();
    }

    private void Start()
    {
        GameManager.GameManagerInstance.ProgressManagerInstance.OnScoreUpdate += UpdateScore;

        // Run Score Update function once at start
        GameManager.GameManagerInstance.ProgressManagerInstance.ScoreUpdate();
    }

    private void OnDestroy()
    {
        if(GameManager.GameManagerInstance?.ProgressManagerInstance != null)
        {
            GameManager.GameManagerInstance.ProgressManagerInstance.OnScoreUpdate -= UpdateScore;
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}