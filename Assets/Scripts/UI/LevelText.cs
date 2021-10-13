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
        ProgressManager.Instance.OnLevelUpdate += UpdateLevel;
        ProgressManager.Instance.LevelUpdate();
    }

    private void OnDestroy()
    {
        if(ProgressManager.Instance != null)
        {
            ProgressManager.Instance.OnLevelUpdate -= UpdateLevel;
        }
    }

    private void UpdateLevel(int level)
    {
        levelTextController.text = "Level " + level.ToString();
    }
}
