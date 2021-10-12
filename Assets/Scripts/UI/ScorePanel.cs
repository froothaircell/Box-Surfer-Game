using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.GetChild(0).GetComponent<Text>();
    }

    private void Start()
    {
        ProgressManager.Instance.OnScoreUpdate += UpdateScore;
    }

    private void OnDestroy()
    {
        if(ProgressManager.Instance != null)
        {
            ProgressManager.Instance.OnScoreUpdate -= UpdateScore;
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
