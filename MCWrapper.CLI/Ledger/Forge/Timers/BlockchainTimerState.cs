namespace MCWrapper.CLI.Ledger.Forge
{
    /// <summary>
    /// Timer state used for tracking multichain-cli.exe interactions
    /// </summary>
    public class BlockchainTimerState
    {
        /// <summary>
        /// Timer lifetime tracker
        /// </summary>
        public int Ticks;

        /// <summary>
        /// Track number of blocks currently available on the selected blockchain
        /// </summary>
        public int BlockCount;

        /// <summary>
        /// Create a new blockchain timer instance
        /// </summary>
        public BlockchainTimerState()
        {
            Ticks = 0;
            BlockCount = 0;
        }
    }
}
