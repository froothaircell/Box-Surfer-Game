using System.Collections.Generic;
using UnityEngine;

namespace Templates
{
    public abstract class ManagerTemplate : StateMachineTemplate
    {
        public override void AllocateValidTransitions()
        {
            Debug.Log("Using the overrided function");
            transitions = new Dictionary<StateTransition, State>
            {
                { new StateTransition(State.Idle, Command.Run), State.Active},
                { new StateTransition(State.Active, Command.Pause), State.Paused },
                { new StateTransition(State.Paused, Command.Unpause), State.Active },
                { new StateTransition(State.Active, Command.Win), State.Sucess },
                { new StateTransition(State.Active, Command.Die), State.Death },
                { new StateTransition(State.Sucess, Command.Restart), State.Idle },
                { new StateTransition(State.Death, Command.Restart), State.Idle }
            };
        }
    }
}