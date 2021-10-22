using System;
using System.Collections.Generic;
using UnityEngine;

namespace Templates
{
    public enum State
    {
        Idle,
        Active,
        Sucess,
        Death,
        Paused
    }

    public enum Command
    {
        Run,
        Pause,
        Unpause,
        Win,
        Die,
        Restart
    }

    [System.Serializable]
    public abstract class StateMachineTemplate : MonoBehaviour
    {
        // Definition of the state transition object
        public class StateTransition
        {
            readonly State currentState;
            readonly Command command;

            public StateTransition(State currentState, Command command)
            {
                this.currentState = currentState;
                this.command = command;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * currentState.GetHashCode() + 31 * command.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.currentState == other.currentState && this.command == other.command;
            }

            public override string ToString()
            {
                return "Current State: " + currentState + " Command: " + command;
            }
        }

        // List of all possible transitions
        public Dictionary<StateTransition, State> transitions;
        public State CurrentState { get; private set; }

        public StateMachineTemplate()
        {
            CurrentState = State.Idle;
            AllocateValidTransitions();
        }

        // Allocate the requisite transitions from the inherited class
        public virtual void AllocateValidTransitions()
        {
            
        }

        public State GetNext(Command command)
        {
            // Debug.Log(CurrentState);
            StateTransition transition = new StateTransition(CurrentState, command);
            State nextState;
            if (!transitions.TryGetValue(transition, out nextState))
            {
                throw new Exception("Invalid Transition: " + CurrentState + " -> " + command);
            }
            // Debug.Log(nextState);
            return nextState;
        }

        public State MoveNext(Command command)
        {
            CurrentState = GetNext(command);
            return CurrentState;
        }
    }
}