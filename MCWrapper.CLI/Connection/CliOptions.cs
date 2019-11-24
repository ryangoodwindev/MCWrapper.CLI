using MCWrapper.Ledger.Entities.Extensions;

namespace MCWrapper.CLI.Options
{
    /// <summary>
    /// CliOptions values are required for proper blockchain interaction to occur via the MCWrapper CLI client.
    ///
    /// <para>
    ///     The MCWrapper CLI client requires the following CliOptions be assigned a valid value
    ///         ChainName;
    ///         ChainAdminAddress;
    ///         ChainBurnAddress
    ///         ChainBinaryLocation
    ///             (ChainBinaryLocation property will map MCWraper to the MultiChain Core binary file directory);
    ///         ChainDefaultLocation
    ///             (ChainDefaultLocation proerty will map MCWrapper to a directly where the target
    ///              blockchain hot node resides);
    ///         ChainDefaultColdNodeLocation;
    ///             (ChainDefaultColdNodeLocation proerty will map MCWrapper to a directly where the target
    ///              blockchain cold node resides);
    /// </para>
    /// </summary>
    public class CliOptions
    {
        /// <summary>
        /// Create a new CliOptions object
        /// </summary>
        public CliOptions() { }

        /// <summary>
        /// Create a new CliOptions object
        /// No arguments
        /// </summary>
        public CliOptions(bool loadFromEnvironment)
        {
            if (loadFromEnvironment)
            {
                ChainName = nameof(ChainName).GetEnvironmentVariable();
                ChainBurnAddress = nameof(ChainBurnAddress).GetEnvironmentVariable();
                ChainAdminAddress = nameof(ChainAdminAddress).GetEnvironmentVariable();
                ChainBinaryLocation = nameof(ChainBinaryLocation).GetEnvironmentVariable();
                ChainDefaultLocation = nameof(ChainDefaultLocation).GetEnvironmentVariable();
                ChainDefaultColdNodeLocation = nameof(ChainDefaultColdNodeLocation).GetEnvironmentVariable();
            }
        }

        /// <summary>
        /// Multichain blockchain name as declared in the params.dat file;
        /// 
        /// <para>
        ///     ChainName value is required for the CLI client to 
        ///     function as expected when using methods that infer
        ///     the blockchain name value;
        /// </para>
        /// 
        /// <para>
        ///     When using methods that support using the blockchain
        ///     name explicitly with the CLI clients, the 
        ///     ChainName value is not necessary or required.
        /// </para>
        /// 
        /// </summary>
        public string ChainName { get; set; } = string.Empty;

        /// <summary>
        /// Your blockchain node administror's public key.
        ///
        /// <para>
        ///     ChainAdminAddress is required for the CLI client to function as expected;
        /// </para>
        /// </summary>
        public string ChainAdminAddress { get; set; } = string.Empty;

        /// <summary>
        /// Blockchain address used for 'burning' Assets/Streams. 
        /// This is not a required property, however, it is nice to have the value available
        /// at the code level in case assets/streams do need to be burned.
        /// 
        /// <para>
        ///     ChainBurnAddress is not required for the CLI client to function as expected;
        /// </para>
        /// </summary>
        public string ChainBurnAddress { get; set; } = string.Empty;

        /// <summary>
        /// Local directory the application may locate the Multichain library files (multichaind.exe, multichain-util.exe, and multichain-cli.exe, etc..)
        /// 
        /// <para>
        ///     ChainBinaryLocation is required for the CLI and Forge clients to function as expected;
        /// </para>
        /// </summary>
        public string ChainBinaryLocation { get; set; } = string.Empty;

        /// <summary>
        /// Default directory that blockchains are found
        /// 
        /// <para>
        ///     ChainDefaultLocation is required for the CLI client to function as expected;
        /// </para>
        /// <para>
        ///     Generally, this location in Windows is %AppData%\MultiChain, the full path is 
        ///     C:\Users\{Current User}\AppData\Roaming\MultiChain
        /// </para>
        /// <para>Generally, this location in Linux is ~/.multichain</para>
        /// </summary>
        public string ChainDefaultLocation { get; set; } = string.Empty;

        /// <summary>
        /// Default  directory that cold nodes are found
        /// <para>
        ///     ChainDefaultColdNodeLocation is not required for the RPC client to function as expected;
        /// </para>
        /// <para>
        ///     ChainDefaultColdNodeLocation is required for the CLI and Forge clients to function as expected;
        /// </para>
        /// <para>
        ///     Generally, this location in Windows is %AppData%\MultiChainCold, the full path is 
        ///     C:\Users\{Current User}\AppData\Roaming\MultiChainCold
        /// </para>
        /// <para>Generally, this location in Linux is ~/.multichain-cold</para>
        /// </summary>
        public string ChainDefaultColdNodeLocation { get; set; } = string.Empty;
    }
}