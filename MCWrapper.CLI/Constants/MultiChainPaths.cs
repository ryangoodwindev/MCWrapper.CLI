using MCWrapper.Ledger.Entities.Extensions;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MCWrapper.CLI.Constants
{
    public static class MultiChainPaths
    {
        /// <summary>
        /// <para>
        ///     If no <paramref name="multiChainHotDirectory"/> is passed then the following default locations are used.
        /// </para>
        /// 
        /// Windows - C:\Users\{CurrentUser}\AppData\Roaming\MultiChain\{<paramref name="blockchainName"/>}
        /// Linux - ./multichain/<blockchainName>
        /// MacOS - ./multichain/<blockchainName> 
        /// FreeBSD - ./multichain/<blockchainName>
        /// 
        /// </summary>
        /// <param name="multiChainHotDirectory">On Linux this is generally './multichain'. On Windows this is generally the 'AppData/Roaming' folder for the current user.</param>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public static string GetHotWalletPath([Optional] string multiChainHotDirectory, string blockchainName)
        {
            var _multiChainFolder = string.Empty;
            if (OSDetection.IsWindows())
            {
                var defaultLocation = string.IsNullOrEmpty(multiChainHotDirectory)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    : multiChainHotDirectory;

                _multiChainFolder = Path.Combine(defaultLocation, "MultiChain");
            }
            else if (OSDetection.IsLinux())
            {
                throw new NotImplementedException("Linux is not implemented yet. Sorry.");
            }
            else if (OSDetection.IsMacOS())
            {
                throw new NotImplementedException("MacOs is not implemented yet. Sorry.");
            }

            return Path.Combine(_multiChainFolder, blockchainName);
        }

        /// <summary>
        /// <para>
        ///     If no <paramref name="multiChainColdDirectory"/> is passed then the following default locations are used.
        /// </para>
        /// 
        /// Windows - C:\Users\{CurrentUser}\AppData\Roaming\MultiChainCold\{<paramref name="blockchainName"/>}
        /// Linux - ./multichain-cold/<blockchainName>
        /// MacOS - ./multichain-cold/<blockchainName> 
        /// FreeBSD - ./multichain-cold/<blockchainName>
        /// 
        /// </summary>
        /// <param name="multiChainColdDirectory"></param>
        /// <param name="blockchainName"></param>
        public static string GetColdWalletPath([Optional] string multiChainColdDirectory, string blockchainName)
        {
            var _multiChainFolder = string.Empty;
            if (OSDetection.IsWindows())
            {
                var defaultLocation = string.IsNullOrEmpty(multiChainColdDirectory)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    : multiChainColdDirectory;

                _multiChainFolder = Path.Combine(defaultLocation, "MultiChainCold");
            }
            else if (OSDetection.IsLinux())
            {
                throw new NotImplementedException("Linux is not implemented yet. Sorry.");
            }
            else if (OSDetection.IsMacOS())
            {
                throw new NotImplementedException("MacOs is not implemented yet. Sorry.");
            }

            return Path.Combine(_multiChainFolder, blockchainName);
        }

        // MultiChain/<blockchainName>/params.dat location
        public static string GetHotWalletParamsDatPath([Optional] string chainDefaultLocation, string blockchainName)
        {
            return Path.Combine(GetHotWalletPath(chainDefaultLocation, blockchainName), "params.dat");
        }

        // MultichainCold/<blockchainName>/params.dat
        public static string GetColdWalletParamsDatPath([Optional] string chainDefaultColdNodeLocation, string blockchainName) =>
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
