using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/// <summary>
/// Controls the animations of the post death or victory screen via events 
/// </summary>
public class FinalScreenController : MonoBehaviour
{
    [SerializeField]
    private RectTransform
        stateDependentText,
        finalScoreText;
    [SerializeField]
    private GameObject 
        finalScore,
        nextLevelButton;
    [SerializeField]
    private Image background;
    [SerializeField]
    private readonly float 
        backgroundFadeInDuration,
        finalTextSlideInDuration,
        scaleDuration;

    private TextMeshProUGUI finalScoreTextController;
    private TextMeshProUGUI stateDependentTextController;

    private void Start()
    {
        stateDependentTextController = stateDependentText.GetComponent<TextMeshProUGUI>();
        finalScoreTextController = finalScoreText.GetComponent<TextMeshProUGUI>();
        GameManager.GameManagerInstance.ProgressManagerInstance.OnScoreUpdate += UpdateScore;
        GameManager.GameManagerInstance.ProgressManagerInstance.OnDeathAnimationUpdate += TriggerAnimations;
        
        // Initial UI configuration
        finalScore.SetActive(false);
        nextLevelButton.SetActive(false);
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
        stateDependentText.localPosition = new Vector3(0f, 2100f, 0f);
        finalScore.transform.localScale = new Vector3(0f, 0f, 0f);
        nextLevelButton.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    private void OnDestroy()
    {
        if(GameManager.GameManagerInstance?.ProgressManagerInstance != null)
        {
            GameManager.GameManagerInstance.ProgressManagerInstance.OnScoreUpdate -= UpdateScore;
            GameManager.GameManagerInstance.ProgressManagerInstance.OnDeathAnimationUpdate -= TriggerAnimations;
        }
    }

    private void UpdateScore(int newScore)
    {
        finalScoreTextController.text = newScore.ToString();
    }

    private void TriggerAnimations(bool win)
    {
        DOTween.To(
            () => background.color, 
            x => background.color = x,
            new Color(background.color.r, background.color.g, background.color.b, 70f/255f),
            backgroundFadeInDuration)
            .OnComplete(
            () =>
            {
                if(win)
                {
                    finalScore.SetActive(true);
                    nextLevelButton.SetActive(true);
                    stateDependentTextController.text = "Great Job!";
                    DOTween.To(
                        () => stateDependentText.localPosition,
                        x => stateDependentText.localPosition = x,
                        new Vector3(0f, 950f, 0f),
                        finalTextSlideInDuration)
                    .OnComplete(
                        () =>
                        {
                            DOTween.To(
                                () => finalScore.transform.localScale,
                                x => finalScore.transform.localScale = x,
                                new Vector3(1, 1, 1),
                                scaleDuration);
                            DOTween.To(
                                () => nextLevelButton.transform.localScale,
                                x => nextLevelButton.transform.localScale = x,
                                new Vector3(1f, 1f, 1f),
                                scaleDuration);
                        });
                }
                else
                {
                    finalScore.SetActive(false);
                    stateDependentTextController.text = "You Failed!";
                    DOTween.To(
                        () => stateDependentText.localPosition,
                        x => stateDependentText.localPosition = x,
                        new Vector3(0f, 950f, 0f),
                        finalTextSlideInDuration);
                }
            });
    }
}
