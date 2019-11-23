namespace MCWrapper.CLI.Ledger.Clients
{
    public class MultiChainCliClientFactory
    {
        public MultiChainCliClientFactory(IMultiChainCliGenerate multiChainCliGenerate, 
            IMultiChainCliGeneral multiChainCliGeneral,
            IMultiChainCliOffChain multiChainCliOffChain,
            IMultiChainCliControl multiChainCliControl, 
            IMultiChainCliNetwork multiChainCliNetwork,
            IMultiChainCliUtility multiChainCliUtility,
            IMultiChainCliMining multiChainCliMining,
            IMultiChainCliWallet multiChainCliWallet, 
            IMultiChainCliRaw multiChainCliRaw, 
            IForge multiChainCliForge)
        {
            _multiChainCliGeneral = multiChainCliGeneral;
            _multiChainCliGenerate = multiChainCliGenerate;
            OffChainClient = multiChainCliOffChain;
            ControlClient = multiChainCliControl;
            NetworkClient = multiChainCliNetwork;
            UtilityClient = multiChainCliUtility;
            MiningClient = multiChainCliMining;
            WalletClient = multiChainCliWallet;
            RawClient = multiChainCliRaw;
            Forge = multiChainCliForge;
        }

        public IMultiChainCliGenerate MultiChainCliGenerateClient => _multiChainCliGenerate;
        private readonly IMultiChainCliGenerate _multiChainCliGenerate;

        public IMultiChainCliGeneral MultiChainCliGeneralClient => _multiChainCliGeneral;
        private readonly IMultiChainCliGeneral _multiChainCliGeneral;

        public IMultiChainCliOffChain OffChainCliClient => OffChainClient;
        private readonly IMultiChainCliOffChain OffChainClient;

        public IMultiChainCliControl ControlCliClient => ControlClient;
        private readonly IMultiChainCliControl ControlClient;

        public IMultiChainCliNetwork NetworkCliClient => NetworkClient;
        private readonly IMultiChainCliNetwork NetworkClient;

        public IMultiChainCliUtility UtilityCliClient => UtilityClient;
        private readonly IMultiChainCliUtility UtilityClient;

        public IMultiChainCliMining MiningCliClient => MiningClient;
        private readonly IMultiChainCliMining MiningClient;

        public IMultiChainCliWallet WalletCliClient => WalletClient;
        private readonly IMultiChainCliWallet WalletClient;

        public IMultiChainCliRaw RawCliClient => RawClient;
        private readonly IMultiChainCliRaw RawClient;

        public IForge ForgeClient => Forge;
        private readonly IForge Forge;
    }
}
