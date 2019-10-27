namespace MCWrapper.CLI.Constants
{
    /// <summary>
    /// Cammands that can be passed to multichaind.exe and/or multichain-util.exe
    /// </summary>
    public struct RuntimeCommand
    {
        /// <summary>
        /// HTTPS swtich starts the specified MultiChain Core blockchain and requires HTTPS JSON-RPC calls;
        /// <para>If this switch is thrown while starting a blockchain there must be a valid HTTPS certificate available to node and/or network</para>
        /// </summary>
        public const string UseRpcSSL = "-rpcssl";

        /// <summary>
        /// Indicates the blockchain should run unobtrusively in the background,
        /// rather than under the direct control of a user, waiting to be activated 
        /// by the occurance of a specific event or condition.
        /// </summary>
        public const string Daemon = "-daemon";

        /// <summary>
        /// Stops the specified blockchain
        /// </summary>
        public const string Stop = "stop";

        /// <summary>
        /// Creates the specified blockchain
        /// </summary>
        public const string Create = "create";

        /// <summary>
        /// Cold swith directs commands to the cold node instance of the specificied blockchain if such does exist
        /// </summary>
        public const string Cold = "-cold";
    }
}
