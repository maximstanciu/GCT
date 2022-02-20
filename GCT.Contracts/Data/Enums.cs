namespace GCT.Contracts.Data
{
    public class Enums
    {
        public enum ProcessState
        {
            Inactive,
            Active,
            Paused,
            Terminated
        }

        public enum Command
        {
            Begin,
            End,
            Pause,
            Resume,
            Exit
        }
    }
}
