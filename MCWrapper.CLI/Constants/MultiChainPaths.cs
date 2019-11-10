using MCWrapper.Ledger.Entities.Extensions;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MCWrapper.CLI.Constants
{
    public static class MultiChainPaths
    {
        private const string DEFAULT_WIN_EXE_PATH_0 = @"C:\";
        private const string DEFAULT_WIN_EXE_PATH_1 = @"C:\multichain";
        private const string DEFAULT_LINUX_EXE_PATH = @"/user/local/bin";

        /// <summary>
        /// Get the local file path where a specific MultiChain blockchain hot node resides.
        /// <para>
        ///     If no <paramref name="multiChainHotDirectory"/> is passed then the following default locations are used.
        /// </para>
        /// 
        /// Windows - C:\Users\{CurrentUser}\AppData\Roaming\MultiChain\{<paramref name="blockchainName"/>}
        /// <para>
        ///     Linux - ./multichain/{<paramref name="blockchainName"/>}
        /// </para>
        /// <para>
        ///     MacOS - ./multichain/{<paramref name="blockchainName"/>}
        /// </para>
        /// 
        /// </summary>
        /// <param name="multiChainHotDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <param name="blockchainName">Name of the target blockchain.</param>
        /// <returns></returns>
        public static string GetHotWalletPath([Optional] string multiChainHotDirectory, string blockchainName)
        {
            var failedFilePath = string.Empty;
            if (!string.IsNullOrEmpty(multiChainHotDirectory))
            {
                var userPath = Path.Combine(multiChainHotDirectory, blockchainName);
                if (Directory.Exists(userPath))
                    return userPath;
                else
                    failedFilePath = userPath;
            }
            else if (OSDetection.IsWindows())
            {
                var winPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MultiChain", blockchainName);
                if (Directory.Exists(winPath))
                    return winPath;
                else
                    failedFilePath = winPath;
            }
            else if (OSDetection.IsLinux() || OSDetection.IsMacOS())
            {
                var linuxPath = Path.Combine("./multichain", blockchainName);
                if (Directory.Exists(linuxPath))
                    return linuxPath;
                else
                    failedFilePath = linuxPath;
            }

            throw new DirectoryNotFoundException($"MultiChain hot node was not found at, {failedFilePath}");
        }

        /// <summary>
        /// Get the local file path where a specific MultiChain blockchain cold node resides.
        /// <para>
        ///     If no <paramref name="multiChainColdDirectory"/> is passed then the following default locations are used.
        /// </para>
        /// 
        /// Windows - C:\Users\{CurrentUser}\AppData\Roaming\MultiChainCold\{<paramref name="blockchainName"/>}
        /// <para>
        ///     Linux - ./multichain-cold/{<paramref name="blockchainName"/>}
        /// </para>
        /// <para>
        ///     MacOS - ./multichain-cold/{<paramref name="blockchainName"/>}
        /// </para>
        /// 
        /// </summary>
        /// <param name="multiChainColdDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <param name="blockchainName">Name of the target blockchain.</param>
        /// <returns></returns>
        public static string GetColdWalletPath([Optional] string multiChainColdDirectory, string blockchainName)
        {
            var failedFilePath = string.Empty;
            if (!string.IsNullOrEmpty(multiChainColdDirectory))
            {
                var userPath = Path.Combine(multiChainColdDirectory, blockchainName);
                if (Directory.Exists(userPath))
                    return userPath;
                else
                    failedFilePath = userPath;
            }
            else if (OSDetection.IsWindows())
            {
                var winPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MultiChainCold", blockchainName);
                if (Directory.Exists(winPath))
                    return winPath;
                else
                    failedFilePath = winPath;
            }
            else if (OSDetection.IsLinux() || OSDetection.IsMacOS())
            {
                var linuxPath = Path.Combine("./multichain-cold", blockchainName);
                if (Directory.Exists(linuxPath))
                    return linuxPath;
                else
                    failedFilePath = linuxPath;
            }

            throw new DirectoryNotFoundException($"MultiChain cold node was not found at, {failedFilePath}");
        }

        /// <summary>
        /// Get the local file path where a specific MultiChain blockchain hot node params.dat file resides.
        /// <para>
        ///     If no <paramref name="multiChainHotDirectory"/> is passed then the following default locations are used.
        /// </para>
        /// 
        /// Windows - C:\Users\{CurrentUser}\AppData\Roaming\MultiChain\{<paramref name="blockchainName"/>}\params.dat
        /// <para>
        ///     Linux - ./multichain/{<paramref name="blockchainName"/>}/params.dat
        /// </para>
        /// <para>
        ///     MacOS - ./multichain/{<paramref name="blockchainName"/>}/params.dat
        /// </para>
        /// 
        /// </summary>
        /// <param name="multiChainHotDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <param name="blockchainName">Name of the target blockchain.</param>
        /// <returns></returns>
        public static string GetHotWalletParamsDatPath([Optional] string multiChainHotDirectory, string blockchainName)
        {
            var hotParamsDat = Path.Combine(GetHotWalletPath(multiChainHotDirectory, blockchainName), "params.dat");
            if (File.Exists(hotParamsDat))
                return hotParamsDat;
            else
                throw new FileNotFoundException($"{blockchainName} blockchain hot node params.dat file not found. Location: {hotParamsDat}");
        }

        /// <summary>
        /// Get the local file path where a specific MultiChain blockchain cold node params.dat file resides.
        /// <para>
        ///     If no <paramref name="multiChainColdDirectory"/> is passed then the following default locations are used.
        /// </para>
        /// 
        /// Windows - C:\Users\{CurrentUser}\AppData\Roaming\MultiChainCold\{<paramref name="blockchainName"/>}\params.dat
        /// <para>
        ///     Linux - ./multichain-cold/{<paramref name="blockchainName"/>}/params.dat
        /// </para>
        /// <para>
        ///     MacOS - ./multichain-cold/{<paramref name="blockchainName"/>}/params.dat
        /// </para>
        /// 
        /// </summary>
        /// <param name="multiChainColdDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <param name="blockchainName">Name of the target blockchain.</param>
        /// <returns></returns>
        public static string GetColdWalletParamsDatPath([Optional] string multiChainColdDirectory, string blockchainName)
        {
            var coldParamsDat = Path.Combine(GetColdWalletPath(multiChainColdDirectory, blockchainName), "params.dat");
            if (File.Exists(coldParamsDat))
                return coldParamsDat;
            else
                throw new FileNotFoundException($"{blockchainName} blockchain cold node params.dat file not found. Location: {coldParamsDat}");
        }

        /// <summary>
        /// Verify and return a valid filepath to access multichaind.exe. 'DirectoryNotFoundException' returned on file not found.
        /// <para>
        ///     1. If the consumer has passed a 'multichainExeDirectory' the location is verified and returned.
        /// </para>
        /// <para>
        ///     2. If the consumer did not pass a 'multichainExeDirectory' value or 'multichainExeDirectory' doesn't exist then we attempt to detect the current Operating System.
        ///     <para>
        ///         - If Windows is detected then we determine if either of the default locations exist 'C:\multichaind.exe' or 'C:\multichain\multichaind.exe.
        ///     </para>
        ///     <para>
        ///         - If Linux is detected then we determine if the default location exists '/user/bin/local/multichaind'.
        ///     </para>
        /// </para>
        /// <para>
        ///     3. If none of the verified paths a valid then a 'DirectoryNotFoundException' is thrown.
        /// </para>
        /// </summary>
        /// <param name="multichainExeDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <returns></returns>
        public static string GetMultiChainDExePath([Optional] string multichainExeDirectory) =>
            GetValidatedMultiChainExePath(multichainExeDirectory, MultiChainFilenames.MultiChainDExe);

        /// <summary>
        /// Verify and return a valid filepath to access multichain-cli.exe. 'DirectoryNotFoundException' returned on file not found.
        /// <para>
        ///     1. If the consumer has passed a 'multichainExeDirectory' the location is verified and returned.
        /// </para>
        /// <para>
        ///     2. If the consumer did not pass a 'multichainExeDirectory' value or 'multichainExeDirectory' doesn't exist then we attempt to detect the current Operating System.
        ///     <para>
        ///         - If Windows is detected then we determine if either of the default locations exist 'C:\multichain-cli.exe' or 'C:\multichain\multichain-cli.exe.
        ///     </para>
        ///     <para>
        ///         - If Linux is detected then we determine if the default location exists '/user/bin/local/multichain-cli'.
        ///     </para>
        /// </para>
        /// <para>
        ///     3. If none of the verified paths a valid then a 'DirectoryNotFoundException' is thrown.
        /// </para>
        /// </summary>
        /// <param name="multichainExeDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <returns></returns>
        public static string GetMultiChainCliExePath([Optional] string multichainExeDirectory) =>
            GetValidatedMultiChainExePath(multichainExeDirectory, MultiChainFilenames.MultiChainCliExe);

        /// <summary>
        /// Verify and return a valid filepath to access multichaind-cold.exe. 'DirectoryNotFoundException' returned on file not found.
        /// <para>
        ///     1. If the consumer has passed a 'multichainExeDirectory' the location is verified and returned.
        /// </para>
        /// <para>
        ///     2. If the consumer did not pass a 'multichainExeDirectory' value or 'multichainExeDirectory' doesn't exist then we attempt to detect the current Operating System.
        ///     <para>
        ///         - If Windows is detected then we determine if either of the default locations exist 'C:\multichaind-cold.exe' or 'C:\multichain\multichaind-cold.exe.
        ///     </para>
        ///     <para>
        ///         - If Linux is detected then we determine if the default location exists '/user/bin/local/multichaind-cold'.
        ///     </para>
        /// </para>
        /// <para>
        ///     3. If none of the verified paths a valid then a 'DirectoryNotFoundException' is thrown.
        /// </para>
        /// </summary>
        /// <param name="multichainExeDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <returns></returns>
        public static string GetMultiChainDColdExePath([Optional] string multichainExeDirectory) =>
            GetValidatedMultiChainExePath(multichainExeDirectory, MultiChainFilenames.MultiChainDColdExe);

        /// <summary>
        /// Verify and return a valid filepath to access multichain-util.exe. 'DirectoryNotFoundException' returned on file not found.
        /// <para>
        ///     1. If the consumer has passed a 'multichainExeDirectory' the location is verified and returned.
        /// </para>
        /// <para>
        ///     2. If the consumer did not pass a 'multichainExeDirectory' value or 'multichainExeDirectory' doesn't exist then we attempt to detect the current Operating System.
        ///     <para>
        ///         - If Windows is detected then we determine if either of the default locations exist 'C:\multichain-util.exe' or 'C:\multichain\multichain-util.exe.
        ///     </para>
        ///     <para>
        ///         - If Linux is detected then we determine if the default location exists '/user/bin/local/multichain-util'.
        ///     </para>
        /// </para>
        /// <para>
        ///     3. If none of the verified paths a valid then a 'DirectoryNotFoundException' is thrown.
        /// </para>
        /// </summary>
        /// <param name="multichainExeDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <returns></returns>
        public static string GetMultiChainUtilExePath([Optional] string multichainExeDirectory) => 
            GetValidatedMultiChainExePath(multichainExeDirectory, MultiChainFilenames.MultiChainUtilExe);

        // Helper method to enforce DRY
        private static string GetValidatedMultiChainExePath(string multichainExeDirectory, string filename)
        {
            var failedFilePath = string.Empty;
            if (!string.IsNullOrEmpty(multichainExeDirectory))
            {
                var userPath = Path.Combine(multichainExeDirectory, filename);
                if (File.Exists(userPath))
                    return userPath;
                else
                    failedFilePath = userPath;
            }
            else if (OSDetection.IsWindows())
            {
                var winPath_0 = Path.Combine(DEFAULT_WIN_EXE_PATH_0, filename);
                var winPath_1 = Path.Combine(DEFAULT_WIN_EXE_PATH_1, filename);
                if (File.Exists(winPath_0))
                    return winPath_0;
                else if (File.Exists(winPath_1))
                    return winPath_1;
                else
                    failedFilePath = $"{winPath_0}, {winPath_1}";
            }
            else if (OSDetection.IsLinux() || OSDetection.IsMacOS())
            {
                var linuxPath = Path.Combine(DEFAULT_LINUX_EXE_PATH, filename);
                if (File.Exists(linuxPath))
                    return linuxPath;
                else
                    failedFilePath = linuxPath;
            }

            throw new FileNotFoundException($"{filename} file is not found at: {failedFilePath}");
        }
    }
}