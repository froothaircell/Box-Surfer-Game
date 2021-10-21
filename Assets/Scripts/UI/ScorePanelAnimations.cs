using UnityEngine;
using DG.Tweening;

/// <summary>
/// Animates the score panel and UI whenever a purple diamond is collected
/// </summary>
public class ScorePanelAnimations : MonoBehaviour
{
    [SerializeField]
    private RectTransform diamondSprite;
    [SerializeField]
    private float scaleFactor = 1.5f,
        heartBeatTransitionDuration = 0.25f,
        positionMoveTransitionDuration = 1f;
    [SerializeField]
    private GameObject diamondPrefab;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        GameManager.GameManagerInstance.ProgressManagerInstance.OnDiamondAnimationUpdate += TriggerAnimations;
    }

    private void OnDestroy()
    {
        if(GameManager.GameManagerInstance?.ProgressManagerInstance != null)
        {
            GameManager.GameManagerInstance.ProgressManagerInstance.OnDiamondAnimationUpdate -= TriggerAnimations;
        }    
    }

    private void TriggerAnimations(Vector3 worldPosition)
    {
        HeartBeatAnimation();
        MovingToRootAnimation(worldPosition);
    }

    public void HeartBeatAnimation()
    {
        DOTween.To(
            () => diamondSprite.localScale,
            x => diamondSprite.localScale = x,
            new Vector3(scaleFactor, scaleFactor, 1), 
            heartBeatTransitionDuration)
            .OnComplete(
            () =>
            {
                diamondSprite.DOScale(new Vector3(1, 1, 1), heartBeatTransitionDuration);
            });
    }

    public void MovingToRootAnimation(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCam.WorldToScreenPoint(worldPosition);
        GameObject item = Instantiate(diamondPrefab, transform);
        item.transform.position = screenPosition;
        DOTween.To(
            () => item.transform.position,
            x => item.transform.position = x,
            diamondSprite.position,
            positionMoveTransitionDuration)
            .OnComplete(
            () =>
            {
                Destroy(item);
            });
    }
}
