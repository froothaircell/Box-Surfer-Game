using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    private TextMeshProUGUI levelTextController;

    // Start is called before the first frame update
    void Start()
    {
        levelTextController = GetComponentInChildren<TextMeshProUGUI>();
        ProgressManager.Instance.OnLevelUpdate += UpdateLevel;
        ProgressManager.Instance.LevelUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
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
