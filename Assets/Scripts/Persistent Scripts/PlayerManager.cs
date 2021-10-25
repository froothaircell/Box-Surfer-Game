using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using StateMachine;

public class PlayerManager : IStateMachine
{
    // Events to be subscirbed to
    public event UnityAction OnBoxAddition;
    public event UnityAction OnPlayerReset;
    public event UnityAction<bool> OnPlayerStopOrDeath;

    public void AddBox()
    {
        GameManager.GameManagerInstance.PoolManagerInstance.SpawnPlayerBox();
        // OnBoxAddition?.Invoke();
    }

    public void ResetPlayer()
    {
        if(CurrentState == states["Death"] || CurrentState == states["Success"])
            MoveNext(commands["Restart"]);
        if(CurrentState == states["Idle"] || CurrentState == states["Death"] || CurrentState == states["Success"])
            OnPlayerReset?.Invoke();
    }

    public void PlayerStoppedOrKilled(bool win)
    {
        if (win)
            MoveNext(commands["Win"]);
        else
            MoveNext(commands["Die"]);
        OnPlayerStopOrDeath?.Invoke(win);
    }
}
