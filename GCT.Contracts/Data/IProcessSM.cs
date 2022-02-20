using static GCT.Contracts.Data.Enums;

namespace GCT.Core.StateMachine
{
    public interface IProcessSM
    {
        ProcessState GetNext(Command command);
        ProcessState MoveNext(Command command);
    }
}
