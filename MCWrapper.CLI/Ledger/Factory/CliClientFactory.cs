using MCWrapper.CLI.Ledger.Clients;
using MCWrapper.CLI.Ledger.Forge;

namespace MCWrapper.CLI.Ledger.Factory
{
    public class CliClientFactory
    {
        private readonly BlockchainCliClient BlockchainClient;
        private readonly ControlCliClient ControlClient;
        private readonly GenerateCliClient GenerateClient;
        private readonly MiningCliClient MiningClient;
        private readonly NetworkCliClient NetworkClient;
        private readonly OffChainCliClient OffChainClient;
        private readonly RawCliClient RawClient;
        private readonly UtilityCliClient UtilityClient;
        private readonly WalletCliClient WalletClient;
        private readonly ForgeClient ForgeClient;

        public CliClientFactory(BlockchainCliClient blockchainClient,
                                ControlCliClient controlClient,
                                GenerateCliClient generateClient,
                                MiningCliClient miningClient,
                                NetworkCliClient networkClient,
                                OffChainCliClient offChainClient,
                                RawCliClient rawClient,
                                UtilityCliClient utilityClient,
                                WalletCliClient walletClient,
                                ForgeClient forgeClient)
        {
            BlockchainClient = blockchainClient;
            ControlClient = controlClient;
            GenerateClient = generateClient;
            MiningClient = miningClient;
            NetworkClient = networkClient;
            OffChainClient = offChainClient;
            RawClient = rawClient;
            UtilityClient = utilityClient;
            WalletClient = walletClient;
            ForgeClient = forgeClient;
        }

        public BlockchainCliClient GetBlockchainClient() => BlockchainClient;
        public ControlCliClient GetControlClient() => ControlClient;
        public GenerateCliClient GetGenerateClient() => GenerateClient;
        public MiningCliClient GetMiningClient() => MiningClient;
        public NetworkCliClient GetNetworkClient() => NetworkClient;
        public OffChainCliClient GetOffChainClient() => OffChainClient;
        public RawCliClient GetRawClient() => RawClient;
        public UtilityCliClient GetUtilityClient() => UtilityClient;
        public WalletCliClient GetWalletClient() => WalletClient;
        public ForgeClient GetForgeClient() => ForgeClient;
    }
}
