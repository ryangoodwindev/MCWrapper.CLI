namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// MultiChainCliClientFactory provides access to a collection of MultiChainCliClients
    /// </summary>
    public interface IMultiChainCliClientFactory
    {
        /// <summary>
        /// Provides access to Generate (native currency or coins) MultChain Core methods
        /// </summary>
        IMultiChainCliGenerate MultiChainCliGenerateClient { get; }

        /// <summary>
        /// Provides access to OffChain MultChain Core methods
        /// </summary>
        IMultiChainCliOffChain MultiChainCliOffChainClient { get; }

        /// <summary>
        /// Provides access to Control MultChain Core methods
        /// </summary>
        IMultiChainCliControl MultiChainCliControlClient { get; }

        /// <summary>
        /// Provides access to General MultChain Core methods
        /// </summary>
        IMultiChainCliGeneral MultiChainCliGeneralClient { get; }

        /// <summary>
        /// Provides access to Network MultChain Core methods
        /// </summary>
        IMultiChainCliNetwork MultiChainCliNetworkClient { get; }

        /// <summary>
        /// Provides access to Utility MultChain Core methods
        /// </summary>
        IMultiChainCliUtility MultiChainCliUtilityClient { get; }

        /// <summary>
        /// Provides access to Mining MultChain Core methods
        /// </summary>
        IMultiChainCliMining MultiChainCliMiningClient { get; }

        /// <summary>
        /// Provides access to Wallet MultChain Core methods
        /// </summary>
        IMultiChainCliWallet MultiChainCliWalletClient { get; }

        /// <summary>
        /// Provides access to the custom MultiChain Cli Forge client
        /// </summary>
        IMultiChainCliForge MultiChainCliForgeClient { get; }

        /// <summary>
        /// Provides access to Raw MultChain Core methods
        /// </summary>
        IMultiChainCliRaw MultiChainCliRawClient { get; }

        /// <summary>
        /// Get a required IMultiChainRpc instance. Must be derived from IMultiChainRpc
        /// 
        /// <para>Options available:</para>
        /// <para>
        ///     IMultiChainCliGenerate, IMultiChainCliOffChain, IMultiChainCliControl, IMultiChainCliGeneral,
        ///     IMultiChainCliNetwork, IMultiChainCliUtility, IMultiChainCliMining, IMultiChainCliWallet,
        ///     IMultiChainCliRaw
        /// </para>
        /// 
        /// </summary>
        /// <typeparam name="IMultiChainCli">Return a service derived from IMultiChainCli</typeparam>
        /// <returns></returns>
        IMultiChainCli GetRequiredCliClient<IMultiChainCli>();
    }
}