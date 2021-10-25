using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using StateMachine;

public class PlayerManager : MonoBehaviour
{
    // Events to be subscribed to
    public event UnityAction OnBoxAddition;
    public event UnityAction OnPlayerReset;
    public event UnityAction<bool> OnPlayerStopOrDeath;

    public IStateMachine PlmStateMachine { get; private set; }

    private void Awake()
    {
        PlmStateMachine = new IStateMachine();
    }

    public void AddBox()
    {
        GameManager.GameManagerInstance.PoolManagerInstance.SpawnPlayerBox();
        // OnBoxAddition?.Invoke();
    }

    public void ResetPlayer()
    {
        if(PlmStateMachine.CurrentState == PlmStateMachine.states["Death"] || PlmStateMachine.CurrentState == PlmStateMachine.states["Success"])
            PlmStateMachine.MoveNext(PlmStateMachine.commands["Restart"]);
        if(PlmStateMachine.CurrentState == PlmStateMachine.states["Idle"] || PlmStateMachine.CurrentState == PlmStateMachine.states["Death"] || PlmStateMachine.CurrentState == PlmStateMachine.states["Success"])
            OnPlayerReset?.Invoke();
    }

    public void PlayerStoppedOrKilled(bool win)
    {
        if (win)
            PlmStateMachine.MoveNext(PlmStateMachine.commands["Win"]);
        else
            PlmStateMachine.MoveNext(PlmStateMachine.commands["Die"]);
        OnPlayerStopOrDeath?.Invoke(win);
    }
}
