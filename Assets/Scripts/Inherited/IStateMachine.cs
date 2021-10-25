using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    /*public enum State
    {
        Idle,
        Active,
        Success,
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
    }*/

    [System.Serializable]
    public abstract class IStateMachine : IStateCommand
    {
        // Definition of the state transition object
        public class StateTransition
        {
            readonly IState currentState;
            readonly ICommand command;

            public StateTransition(IState currentState, ICommand command)
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
                return other != null && currentState == other.currentState && command == other.command;
            }

            public override string ToString()
            {
                return "Current State: " + currentState.State + " Command: " + command.Command;
            }
        }

        // List of all possible transitions
        public Dictionary<StateTransition, IState> transitions;
        // List of states and commands
        // Note: Used a dictionary here instead of a list for optimization purposes
        public Dictionary<string, IState> states;
        public Dictionary<string, ICommand> commands;

        public IState CurrentState { get; private set; }

        public IStateMachine()
        {
            AllocateStatesAndCommands();
            if(states.ContainsKey("Idle"))
            {
                CurrentState = states["Idle"];
            }
            AllocateValidTransitions();
        }

        public virtual void AllocateStatesAndCommands()
        {
            states = new Dictionary<string, IState>
            {
                { "Idle", new IState("Idle") },
                { "Active", new IState("Active") },
                { "Success", new IState("Success") },
                { "Death", new IState("Death") },
                { "Paused", new IState("Paused") }
            };

            commands = new Dictionary<string, ICommand>
            {
                { "Run", new ICommand("Run") },
                { "Pause", new ICommand("Pause") },
                { "Unpause", new ICommand("Unpause") },
                { "Win", new ICommand("Win") },
                { "Die", new ICommand("Die") },
                { "Restart", new ICommand("Restart") }
            };
        }

        // Allocate the requisite transitions from the inherited class
        public virtual void AllocateValidTransitions()
        {
            transitions = new Dictionary<StateTransition, IState>
            {
                { new StateTransition(states["Idle"], commands["Run"]), states["Active"]},
                { new StateTransition(states["Active"], commands["Pause"]), states["Paused"] },
                { new StateTransition(states["Paused"], commands["Unpause"]), states["Active"] },
                { new StateTransition(states["Active"], commands["Win"]), states["Success"] },
                { new StateTransition(states["Active"], commands["Die"]), states["Death"] },
                { new StateTransition(states["Success"], commands["Restart"]), states["Idle"] },
                { new StateTransition(states["Death"], commands["Restart"]), states["Idle"] }
            };
            Debug.Log("Heading out of the transition allocator function");
        }

        public IState GetNext(ICommand command)
        {
            // Debug.Log(CurrentState);
            StateTransition transition = new StateTransition(CurrentState, command);
            IState nextState;
            if (!transitions.TryGetValue(transition, out nextState))
            {
                throw new Exception("Invalid Transition: " + CurrentState.State + " -> " + command.Command);
            }
            // Debug.Log(nextState);
            return nextState;
        }

        public string MoveNext(ICommand command)
        {
            CurrentState = GetNext(command);
            return CurrentState.State;
        }
    }
}