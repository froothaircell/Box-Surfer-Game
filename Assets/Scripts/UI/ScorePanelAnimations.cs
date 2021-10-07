using UnityEngine;
using DG.Tweening;

public class ScorePanelAnimations : MonoBehaviour
{
    [SerializeField]
    private RectTransform diamondSprite;
    [SerializeField]
    private float scaleFactor = 1.5f,
        transitionDuration = 0.25f;
    [SerializeField]
    private GameObject diamondPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeartBeatAnimation()
    {
        DOTween.To(
            ()=> diamondSprite.localScale,
            x=> diamondSprite.localScale = x,
            new Vector3(scaleFactor, scaleFactor, 1), transitionDuration)
            .OnComplete(
            ()=>
            {
                diamondSprite.DOScale(new Vector3(1, 1, 1), transitionDuration);
            });
    }

    public void MovingToRootAnimation(Vector3 screenPosition)
    {
        GameObject item = Instantiate(diamondPrefab, transform);
        item.transform.position = screenPosition;
        DOTween.To(
            () => item.transform.position,
            x => item.transform.position = x,
            diamondSprite.position,
            1)
            .OnComplete(
            () =>
            {
                Destroy(item);
            });
    }
}
