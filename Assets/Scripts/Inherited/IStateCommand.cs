using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class IStateCommand : MonoBehaviour
    {
        // Class for the state, with functions
        public class IState
        {
            public string State { get; private set; }
            public IState(string state)
            {
                State = state;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * State.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                IState other = obj as IState;
                return other != null && State == other.State;
            }

            public virtual void OnEnter() { }
            public virtual void OnExit() { }
            public virtual void OnUpdate() { }
        }

        // Class for commands for transitioning between classes
        public class ICommand
        {
            public string Command { get; private set; }
            public ICommand(string command)
            {
                Command = command;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * Command.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                ICommand other = obj as ICommand;
                return other != null && Command == other.Command;
            }
        }
    }
}
