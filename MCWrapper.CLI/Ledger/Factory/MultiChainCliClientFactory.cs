using MCWrapper.CLI.Ledger.Contracts;
using System;
using System.Collections.Generic;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// MultiChainCliClientFactory provides access to a collection of MultiChainCliClients
    /// </summary>
    public class MultiChainCliClientFactory : IMultiChainCliClientFactory
    {
        // collection of Cli clients
        private readonly Dictionary<Type, IMultiChainCli> _clients;

        /// <summary>
        /// MultiChainCliClientFactory provides access to a collection of MultiChainCliClients
        /// </summary>
        /// <param name="multiChainCliGenerate">Provides access to Generate (native currency or coins) MultChain Core methods</param>
        /// <param name="multiChainCliOffChain">Provides access to OffChain MultChain Core methods</param>
        /// <param name="multiChainCliControl">Provides access to Control MultChain Core methods</param>
        /// <param name="multiChainCliGeneral">Provides access to General MultChain Core methods</param>
        /// <param name="multiChainCliNetwork">Provides access to Network MultChain Core methods</param>
        /// <param name="multiChainCliUtility">Provides access to Utility MultChain Core methods</param>
        /// <param name="multiChainCliMining">Provides access to Mining MultChain Core methods</param>
        /// <param name="multiChainCliWallet">Provides access to Wallet MultChain Core methods</param>
        /// <param name="multiChainCliForge">Provides access to the custom MultiChain Cli Forge client</param>
        /// <param name="multiChainCliRaw">Provides access to Raw MultChain Core methods</param>
        public MultiChainCliClientFactory(IMultiChainCliGenerate multiChainCliGenerate,
            IMultiChainCliOffChain multiChainCliOffChain,
            IMultiChainCliControl multiChainCliControl,
            IMultiChainCliGeneral multiChainCliGeneral,
            IMultiChainCliNetwork multiChainCliNetwork,
            IMultiChainCliUtility multiChainCliUtility,
            IMultiChainCliMining multiChainCliMining,
            IMultiChainCliWallet multiChainCliWallet,
            IMultiChainCliForge multiChainCliForge,
            IMultiChainCliRaw multiChainCliRaw)
        {
            _clients = new Dictionary<Type, IMultiChainCli>();

            _multiChainCliGenerate = multiChainCliGenerate;
            _clients.TryAdd(typeof(IMultiChainCliGenerate), multiChainCliGenerate);

            _multiChainCliOffChain = multiChainCliOffChain;
            _clients.TryAdd(typeof(IMultiChainCliGeneral), multiChainCliOffChain);

            _multiChainCliControl = multiChainCliControl;
            _clients.TryAdd(typeof(IMultiChainCliControl), multiChainCliControl);

            _multiChainCliGeneral = multiChainCliGeneral;
            _clients.TryAdd(typeof(IMultiChainCliGeneral), multiChainCliGeneral);

            _multiChainCliNetwork = multiChainCliNetwork;
            _clients.TryAdd(typeof(IMultiChainCliNetwork), multiChainCliNetwork);

            _multiChainCliUtility = multiChainCliUtility;
            _clients.TryAdd(typeof(IMultiChainCliUtility), multiChainCliUtility);

            _multiChainCliMining = multiChainCliMining;
            _clients.TryAdd(typeof(IMultiChainCliMining), multiChainCliMining);

            _multiChainCliWallet = multiChainCliWallet;
            _clients.TryAdd(typeof(IMultiChainCliWallet), multiChainCliWallet);

            _multiChainCliForge = multiChainCliForge;
            _clients.TryAdd(typeof(IMultiChainCliForge), multiChainCliForge);

            _multiChainCliRaw = multiChainCliRaw;
            _clients.TryAdd(typeof(IMultiChainCliRaw), multiChainCliRaw);
        }

        /// <summary>
        /// Get a required MultiChainCliClient
        /// </summary>
        /// <typeparam name="IMultiChainCli"></typeparam>
        /// <returns></returns>
        public IMultiChainCli GetRequiredCliClient<IMultiChainCli>() =>
            (IMultiChainCli)_clients[typeof(IMultiChainCli)];

        /// <summary>
        /// Provides access to Generate (native currency or coins) MultChain Core methods
        /// </summary>
        public IMultiChainCliGenerate MultiChainCliGenerateClient => _multiChainCliGenerate;
        private readonly IMultiChainCliGenerate _multiChainCliGenerate;

        /// <summary>
        /// Provides access to OffChain MultChain Core methods
        /// </summary>
        public IMultiChainCliOffChain MultiChainCliOffChainClient => _multiChainCliOffChain;
        private readonly IMultiChainCliOffChain _multiChainCliOffChain;

        /// <summary>
        /// Provides access to Control MultChain Core methods
        /// </summary>
        public IMultiChainCliControl MultiChainCliControlClient => _multiChainCliControl;
        private readonly IMultiChainCliControl _multiChainCliControl;

        /// <summary>
        /// Provides access to General MultChain Core methods
        /// </summary>
        public IMultiChainCliGeneral MultiChainCliGeneralClient => _multiChainCliGeneral;
        private readonly IMultiChainCliGeneral _multiChainCliGeneral;

        /// <summary>
        /// Provides access to Network MultChain Core methods
        /// </summary>
        public IMultiChainCliNetwork MultiChainCliNetworkClient => _multiChainCliNetwork;
        private readonly IMultiChainCliNetwork _multiChainCliNetwork;

        /// <summary>
        /// Provides access to Utility MultChain Core methods
        /// </summary>
        public IMultiChainCliUtility MultiChainCliUtilityClient => _multiChainCliUtility;
        private readonly IMultiChainCliUtility _multiChainCliUtility;

        /// <summary>
        /// Provides access to Mining MultChain Core methods
        /// </summary>
        public IMultiChainCliMining MultiChainCliMiningClient => _multiChainCliMining;
        private readonly IMultiChainCliMining _multiChainCliMining;

        /// <summary>
        /// Provides access to Wallet MultChain Core methods
        /// </summary>
        public IMultiChainCliWallet MultiChainCliWalletClient => _multiChainCliWallet;
        private readonly IMultiChainCliWallet _multiChainCliWallet;

        /// <summary>
        /// Provides access to the custom MultiChain Cli Forge client
        /// </summary>
        public IMultiChainCliForge MultiChainCliForgeClient => _multiChainCliForge;
        private readonly IMultiChainCliForge _multiChainCliForge;

        /// <summary>
        /// Provides access to Raw MultChain Core methods
        /// </summary>
        public IMultiChainCliRaw MultiChainCliRawClient => _multiChainCliRaw;
        private readonly IMultiChainCliRaw _multiChainCliRaw;
    }
}
