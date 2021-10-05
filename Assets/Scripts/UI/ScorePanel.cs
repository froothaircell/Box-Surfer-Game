using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.GetChild(0).GetComponent<Text>();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
