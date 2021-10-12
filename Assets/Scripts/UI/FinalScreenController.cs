using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FinalScreenController : MonoBehaviour
{
    [SerializeField]
    private RectTransform
        stateDependentText,
        finalScoreText,
        diamondSprite;
    [SerializeField]
    private GameObject finalScore;
    [SerializeField]
    private Image background;
    [SerializeField]
    private float 
        backgroundFadeInDuration,
        finalTextSlideInDuration,
        scaleDuration;

    private TextMeshProUGUI finalScoreTextController;
    private TextMeshProUGUI stateDependentTextController;

    // Start is called before the first frame update
    void Start()
    {
        stateDependentTextController = stateDependentText.GetComponent<TextMeshProUGUI>();
        finalScoreTextController = finalScoreText.GetComponent<TextMeshProUGUI>();
        ProgressManager.Instance.OnScoreUpdate += UpdateScore;
        ProgressManager.Instance.OnDeathAnimationUpdate += TriggerAnimations;
        
        // Initial UI configuration
        finalScore.SetActive(false);
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
        stateDependentText.localPosition = new Vector3(0f, 2100f, 0f);
        finalScore.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(ProgressManager.Instance != null)
        {
            ProgressManager.Instance.OnScoreUpdate -= UpdateScore;
            ProgressManager.Instance.OnDeathAnimationUpdate -= TriggerAnimations;
        }
    }

    private void UpdateScore(int newScore)
    {
        finalScoreTextController.text = newScore.ToString();
    }

    private void TriggerAnimations(bool win)
    {
        Debug.Log("Inside the final panel animations function");
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
