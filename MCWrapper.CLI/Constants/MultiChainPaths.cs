using System.IO;

namespace MCWrapper.CLI.Constants
{
    public struct MultiChainPaths
    {
        // MultiChain/<blockchainName> directory
        public static string GetHotWalletPath(string chainDefaultLocation, string blockchainName) =>
            Path.Combine(chainDefaultLocation, blockchainName);

        // MultiChainCold/<blockchainName> directory
        public static string GetColdWalletPath(string chainDefaultColdNodeLocation, string blockchainName) =>
            Path.Combine(chainDefaultColdNodeLocation, blockchainName);

        // MultiChain/<blockchainName>/params.dat location
        public static string GetHotWalletParamsDatPath(string chainDefaultLocation, string blockchainName) =>
            Path.Combine(GetHotWalletPath(chainDefaultLocation, blockchainName), "params.dat");

        // MultichainCold/<blockchainName>/params.dat
        public static string GetColdWalletParamsDatPath(string chainDefaultColdNodeLocation, string blockchainName) =>
            Path.Combine(GetColdWalletPath(chainDefaultColdNodeLocation, blockchainName), "params.dat");

        // multichaind.exe
        public static string GetMultiChainDExePath(string chainBinaryLocation) =>
            Path.Combine(chainBinaryLocation, MultiChainFilenames.MultiChainDExe);

        // multichain-cli.exe
        public static string GetMultiChainCliExePath(string chainBinaryLocation) =>
            Path.Combine(chainBinaryLocation, MultiChainFilenames.MultiChainCliExe);

        // multichaind-cold.exe
        public static string GetMultiChainDColdExePath(string chainBinaryLocation) =>
            Path.Combine(chainBinaryLocation, MultiChainFilenames.MultiChainDColdExe);

        // multichain-util.exe
        public static string GetMultiChainUtilExePath(string chainBinaryLocation) =>
            Path.Combine(chainBinaryLocation, MultiChainFilenames.MultiChainUtilExe);
    }
}
