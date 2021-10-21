using UnityEngine;
using TMPro;

/// <summary>
/// Updates the level text UI element via a listener to an event invoked by the
/// progress manager.
/// </summary>
public class LevelText : MonoBehaviour
{
    private TextMeshProUGUI levelTextController;

    private void Start()
    {
        levelTextController = GetComponentInChildren<TextMeshProUGUI>();
        GameManager.GameManagerInstance.ProgressManagerInstance.OnLevelUpdate += UpdateLevel;
        GameManager.GameManagerInstance.ProgressManagerInstance.LevelUpdate();
    }

    private void OnDestroy()
    {
        if(GameManager.GameManagerInstance?.ProgressManagerInstance != null)
        {
            GameManager.GameManagerInstance.ProgressManagerInstance.OnLevelUpdate -= UpdateLevel;
        }
    }

    private void UpdateLevel(int level)
    {
        levelTextController.text = "Level " + level.ToString();
    }
}
