using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    // Events to be subscirbed to
    public event UnityAction OnBoxAddition;
    public event UnityAction OnPlayerReset;
    public event UnityAction<bool> OnPlayerStopOrDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBox()
    {
        OnBoxAddition?.Invoke();
    }

    public void ResetPlayer()
    {
        OnPlayerReset?.Invoke();
    }

    public void PlayerStoppedOrKilled(bool win)
    {
        OnPlayerStopOrDeath?.Invoke(win);
    }
}
