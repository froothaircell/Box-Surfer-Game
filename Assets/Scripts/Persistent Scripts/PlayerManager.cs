using UnityEngine.Events;
using Templates;

public class PlayerManager : ManagerTemplate
{
    // Events to be subscirbed to
    public event UnityAction OnBoxAddition;
    public event UnityAction OnPlayerReset;
    public event UnityAction<bool> OnPlayerStopOrDeath;

    public void AddBox()
    {
        OnBoxAddition?.Invoke();
    }

    public void ResetPlayer()
    {
        if(CurrentState == State.Death || CurrentState == State.Sucess)
            MoveNext(Command.Restart);
        if(CurrentState == State.Idle || CurrentState == State.Death || CurrentState == State.Sucess)
            OnPlayerReset?.Invoke();
    }

    public void PlayerStoppedOrKilled(bool win)
    {
        if (win)
            MoveNext(Command.Win);
        else
            MoveNext(Command.Die);
        OnPlayerStopOrDeath?.Invoke(win);
    }
}
