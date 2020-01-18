using MCWrapper.CLI.Options;

namespace MCWrapper.CLI.Ledger.Contracts
{
    /// <summary>
    /// Command Line Interface Basic Contract
    /// </summary>
    public interface IMultiChainCli
    {
        /// <summary>
        /// Every CLI client should include CliOptions which will support the IOptions pattern within the IConfiguration pipeline
        /// </summary>
        CliOptions CliOptions { get; set; }
    }
}
