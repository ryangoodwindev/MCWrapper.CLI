using MCWrapper.Ledger.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MCWrapper.CLI.Options
{
    /// <summary>
    /// Some, not all, CliOptions values are required for proper Multichain interaction to occur via the various MCWrapper.CLI clients.
    ///
    /// <para>
    ///     The MCWrapper.CLI clients require the following CliOptions be assigned a valid value.
    /// </para>
    /// <list type="bullet">
    ///     <item>ChainName - Target MultiChain blockchain name</item>
    ///     <item>ChainBurnAddress - Target blockchain burn address</item>
    ///     <item>ChainAdminAddress - Target blockchain admin address</item>
    /// </list>
    /// 
    /// <para>
    ///     The MCWrapper.CLI clients do not require the following CliOptions be assigned a valid value,
    ///     since the values are auto-detected when not speicied explicitly by the consumer when configuring
    ///     the dependency injection pipeline.
    /// </para>
    /// <list type="bullet">
    ///     <item>ChainBinaryLocation - Property should map MCWrapper.CLI to the local filesystem directory where the MultiChain Core binary files are stored.</item>
    ///     <item>ChainDefaultLocation - Property should map MCWrapper.CLI to the local filesystem directory where the target MultiChain hot nodes are stored.</item>
    ///     <item>ChainDefaultColdNodeLocation - Property should map MCWrapper.CLI to the local filesystem directory where the target MultiChain cold nodes are stored.</item>
    /// </list>
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
                ChainDefaultColdNodeLocation = nameof(ChainDefaultColdNodeLocation).GetEnvironmentVariable();
                ChainDefaultLocation = nameof(ChainDefaultLocation).GetEnvironmentVariable();
                ChainBinaryLocation = nameof(ChainBinaryLocation).GetEnvironmentVariable();
                ChainAdminAddress = nameof(ChainAdminAddress).GetEnvironmentVariable();
                ChainBurnAddress = nameof(ChainBurnAddress).GetEnvironmentVariable();
                ChainName = nameof(ChainName).GetEnvironmentVariable();
            }
        }

        /// <summary>
        /// Create a new CliOptions object
        /// </summary>
        /// <param name="chainName">Target blockchain name</param>
        /// <param name="chainBurnAddress">Target blockchain burn address</param>
        /// <param name="chainAdminAddress">Target blockchain admin address</param>
        /// <param name="chainBinaryLocation">Optional: This location is auto detected unless otherwise specified</param>
        /// <param name="chainDefaultLocation">Optional: This location is auto detected unless otherwise specified</param>
        /// <param name="chainDefaultColdNodeLocation">Optional: This location is auto detected unless otherwise specified</param>
        public CliOptions(string chainName,
            string chainBurnAddress,
            string chainAdminAddress,
            string chainBinaryLocation = "",
            string chainDefaultLocation = "",
            string chainDefaultColdNodeLocation = "")
        {
            ChainDefaultColdNodeLocation = chainDefaultColdNodeLocation;
            ChainDefaultLocation = chainDefaultLocation;
            ChainBinaryLocation = chainBinaryLocation;
            ChainAdminAddress = chainAdminAddress;
            ChainBurnAddress = chainBurnAddress;
            ChainName = chainName;
        }

        /// <summary>
        /// 
        /// Default directory that cold nodes are found locally.
        /// 
        /// <para>
        ///     ChainDefaultColdNodeLocation is an optional property since this
        ///     location is auto detected by MCWrapper.CLI unless otherwise
        ///     specified by the consumer. Default locations
        ///     are listed below according to the supported platform.
        /// </para>
        /// <para>
        ///     The default location on Windows is %AppData%\MultiChainCold
        /// </para>
        /// <para>
        ///     The default location on Linux is /home/.multichain-cold
        /// </para>
        /// </summary>
        public string ChainDefaultColdNodeLocation { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// Default directory that hot nodes are found locally.
        /// 
        /// <para>
        ///     ChainDefaultLocation is an optional property since this
        ///     location is auto detected by MCWrapper.CLI unless otherwise
        ///     specified by the consumer. Default locations
        ///     are listed below according to the supported platform.
        /// </para>
        /// <para>
        ///     The default location on Windows is %AppData%\MultiChain
        /// </para>
        /// <para>
        ///     The default location on Linux is /home/.multichain
        /// </para>
        /// </summary>
        public string ChainDefaultLocation { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// Local directory the application may locate the Multichain library
        /// files (multichaind, multichain-util, and multichain-cli, and multichaind-cold)
        /// 
        /// <para>
        ///     ChainBinaryLocation is an optional property since this
        ///     location is auto detected by MCWrapper.CLI unless otherwise
        ///     specified by the consumer. Default locations
        ///     are listed below according to the supported platform.
        /// </para>
        /// <para>
        ///     The default locations on Windows are C:\ or C:\multichain
        /// </para>
        /// <para>
        ///     The default location on Linux is /usr/bin/local
        /// </para>
        /// </summary>
        public string ChainBinaryLocation { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// Blockchain address used for 'burning' Assets/Streams. 
        /// This is not a required property, however, it is nice to have the value available
        /// at the code level in case assets/streams do need to be burned.
        /// 
        /// </summary>
        public string ChainBurnAddress { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// ChainAdminAddress is required for the CLI client to function as expected;
        /// 
        /// <para>
        ///     Your blockchain node administror's public key. If you do not want to use an
        ///     address with actual admin permissions, subtitute any other node address 
        ///     that possesses grant, create, send, and receive permissions.
        /// </para>
        /// </summary>
        public string ChainAdminAddress { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// Multichain blockchain name as declared in the target MultiChain node's params.dat file;
        /// 
        /// <para>!! Important !! Read below!!</para>
        /// <para>
        ///     When using methods that support using the blockchain
        ///     name explicitly with the CLI clients, the 
        ///     ChainName value is not necessary or required.
        /// </para>
        /// 
        /// <para>
        ///     ChainName value is required for the CLI client to 
        ///     function as expected when using methods that infer
        ///     the blockchain name value;
        /// </para>
        /// 
        /// </summary>
        public string ChainName { get; set; } = string.Empty;
    }
}