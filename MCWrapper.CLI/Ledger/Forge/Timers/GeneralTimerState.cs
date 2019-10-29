namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// Timer state used for tracking multichain-cli.exe interactions
    /// </summary>
    public class GeneralTimerState
    {
        /// <summary>
        /// Track timer lifetime; 
        /// How many times has the timer completed?
        /// </summary>
        public int Counter { get; set; }

        /// <summary>
        /// Is the task associated with the timer been completed? Was it successful?
        /// </summary>
        public bool TimerJobSuccess { get; set; }

        /// <summary>
        /// Path to the MultiChain params.dat file necessary during some timer based tasks.
        /// </summary>
        public string ParamsDatPath { get; set; }

        /// <summary>
        /// Path to the MultiChain multichain.conf file necessary during some timer based tasks.
        /// </summary>
        public string MultiChainConfPath { get; set; }

        /// <summary>
        /// Create a new TimerState instance
        /// </summary>
        public GeneralTimerState()
        {
            Counter = 0;
            TimerJobSuccess = false;
            ParamsDatPath = string.Empty;
            MultiChainConfPath = string.Empty;
        }

        /// <summary>
        /// Create a new parameterized TimerState instance
        /// </summary>
        /// <param name="paramsDatPath"></param>
        /// <param name="multichainConfPath"></param>
        public GeneralTimerState(string paramsDatPath, string multichainConfPath)
        {
            Counter = 0;
            ParamsDatPath = paramsDatPath;
            MultiChainConfPath = multichainConfPath;
        }
    }
}
