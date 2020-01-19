using MCWrapper.CLI.Options;

namespace MCWrapper.CLI.Ledger.Contracts
{
    /// <summary>
    /// MCWrapper MultiChain Command Line Interface contract
    /// requires a <seealso cref="CliOptions"/> be available 
    /// to consumers.
    /// </summary>
    public interface IMultiChainCli
    {
        /// <summary>
        /// 
        /// Every IMultiChainCli client should include CliOptions which will 
        /// support the IOptions pattern within the IConfiguration 
        /// pipeline for each IMultiChainCli client.
        /// 
        /// </summary>
        CliOptions CliOptions { get; set; }
    }
}