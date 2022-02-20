using GCT.Core.StateMachine;
using static GCT.Contracts.Data.Enums;

namespace GCT.Infrastructure.StateMachine
{
    

    public class ProcessSM : IProcessSM
    {
        class StateTransition
        {
            readonly ProcessState CurrentState;
            readonly Command Command;

            public StateTransition(ProcessState currentState, Command command)
            {
                CurrentState = currentState;
                Command = command;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj is StateTransition other && this.CurrentState == other.CurrentState && this.Command == other.Command;
            }
        }

        Dictionary<StateTransition, ProcessState> transitions;
        public ProcessState CurrentState { get; private set; }

        public ProcessSM()
        {
            CurrentState = ProcessState.Inactive;
            transitions = new Dictionary<StateTransition, ProcessState>
            {
                { new StateTransition(ProcessState.Inactive, Command.Exit), ProcessState.Terminated },
                { new StateTransition(ProcessState.Inactive, Command.Begin), ProcessState.Active },
                { new StateTransition(ProcessState.Active, Command.End), ProcessState.Inactive },
                { new StateTransition(ProcessState.Active, Command.Pause), ProcessState.Paused },
                { new StateTransition(ProcessState.Paused, Command.End), ProcessState.Inactive },
                { new StateTransition(ProcessState.Paused, Command.Resume), ProcessState.Active }
            };
        }

        public ProcessState GetNext(Command command)
        {
            StateTransition transition = new(CurrentState, command);
            if (!transitions.TryGetValue(transition, out ProcessState nextState))
                throw new Exception("Invalid transition: " + CurrentState + " -> " + command);
            return nextState;
        }

        public ProcessState MoveNext(Command command)
        {
            CurrentState = GetNext(command);
            return CurrentState;
        }
    }
}
