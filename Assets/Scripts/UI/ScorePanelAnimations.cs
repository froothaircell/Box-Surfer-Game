using UnityEngine;
using DG.Tweening;

public class ScorePanelAnimations : MonoBehaviour
{
    [SerializeField]
    private Transform diamondSprite;
    [SerializeField]
    private float scaleFactor = 1.5f,
        transitionDuration = 0.25f;

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
            () => diamondSprite.localScale,
            x => diamondSprite.localScale = x,
            new Vector3(scaleFactor, scaleFactor, 1), transitionDuration)
            .OnComplete(
            ()=>
            {
                diamondSprite.DOScale(new Vector3(1, 1, 1), transitionDuration);
            });
    }
}
