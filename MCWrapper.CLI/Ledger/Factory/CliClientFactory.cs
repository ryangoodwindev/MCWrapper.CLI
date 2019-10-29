namespace MCWrapper.CLI.Ledger.Clients
{
    public class CliClientFactory
    {
        public CliClientFactory(BlockchainCliClient blockchainClient, GenerateCliClient generateClient, OffChainCliClient offChainClient,
            ControlCliClient controlClient, NetworkCliClient networkClient, UtilityCliClient utilityClient, MiningCliClient miningClient,
            WalletCliClient walletClient, RawCliClient rawClient, ForgeClient forge)
        {
            BlockchainClient = blockchainClient;
            GenerateClient = generateClient;
            OffChainClient = offChainClient;
            ControlClient = controlClient;
            NetworkClient = networkClient;
            UtilityClient = utilityClient;
            MiningClient = miningClient;
            WalletClient = walletClient;
            RawClient = rawClient;
            Forge = forge;
        }

        public BlockchainCliClient BlockchainCliClient => BlockchainClient;
        private readonly BlockchainCliClient BlockchainClient;

        public GenerateCliClient GenerateCliClient => GenerateClient;
        private readonly GenerateCliClient GenerateClient;

        public OffChainCliClient OffChainCliClient => OffChainClient;
        private readonly OffChainCliClient OffChainClient;

        public ControlCliClient ControlCliClient => ControlClient;
        private readonly ControlCliClient ControlClient;

        public NetworkCliClient NetworkCliClient => NetworkClient;
        private readonly NetworkCliClient NetworkClient;

        public UtilityCliClient UtilityCliClient => UtilityClient;
        private readonly UtilityCliClient UtilityClient;

        public MiningCliClient MiningCliClient => MiningClient;
        private readonly MiningCliClient MiningClient;

        public WalletCliClient WalletCliClient => WalletClient;
        private readonly WalletCliClient WalletClient;

        public RawCliClient RawCliClient => RawClient;
        private readonly RawCliClient RawClient;

        public ForgeClient ForgeClient => Forge;
        private readonly ForgeClient Forge;
    }
}
