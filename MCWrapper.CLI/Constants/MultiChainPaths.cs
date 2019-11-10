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
            var _multiChainFolder = string.Empty;
            if (OSDetection.IsWindows())
            {
                var defaultLocation = string.IsNullOrEmpty(multiChainHotDirectory)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    : multiChainHotDirectory;

                _multiChainFolder = Path.Combine(defaultLocation, "MultiChain");
            }
            else if (OSDetection.IsLinux()|| OSDetection.IsMacOS())
            {
                _multiChainFolder = string.IsNullOrEmpty(multiChainHotDirectory)
                    ? "./multichain"
                    : multiChainHotDirectory;
            }

            return Path.Combine(_multiChainFolder, blockchainName);
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
            var _multiChainFolder = string.Empty;
            if (OSDetection.IsWindows())
            {
                var defaultLocation = string.IsNullOrEmpty(multiChainColdDirectory)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    : multiChainColdDirectory;

                _multiChainFolder = Path.Combine(defaultLocation, "MultiChainCold");
            }
            else if (OSDetection.IsLinux() || OSDetection.IsMacOS())
            {
                _multiChainFolder = string.IsNullOrEmpty(multiChainColdDirectory)
                    ? "./multichain-cold"
                    : multiChainColdDirectory;
            }

            return Path.Combine(_multiChainFolder, blockchainName);
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
        public static string GetHotWalletParamsDatPath([Optional] string multiChainHotDirectory, string blockchainName) => 
            Path.Combine(GetHotWalletPath(multiChainHotDirectory, blockchainName), "params.dat");

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
        public static string GetColdWalletParamsDatPath([Optional] string multiChainColdDirectory, string blockchainName) =>
            Path.Combine(GetColdWalletPath(multiChainColdDirectory, blockchainName), "params.dat");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multichainExeDirectory">Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.</param>
        /// <returns></returns>
        public static string GetMultiChainDExePath(string multichainExeDirectory)
        {
            // determine if the user passed a 'multichainExeDirectly' value,
            // then determine if the Directory exists.
            if (!string.IsNullOrEmpty(multichainExeDirectory)
                && Directory.Exists(Path.Combine(multichainExeDirectory, MultiChainFilenames.MultiChainDExe)))
                return Path.Combine(multichainExeDirectory, MultiChainFilenames.MultiChainDExe);
            else if (OSDetection.IsWindows())
            {
                // if the user didn't pass a directory or the directory doesn't exist then,
                // determine if the first default location exists 'C:\multichaind.exe'.
                if (Directory.Exists(Path.Combine(DEFAULT_WIN_EXE_PATH_0, MultiChainFilenames.MultiChainDExe)))
                    return Path.Combine(DEFAULT_WIN_EXE_PATH_0, MultiChainFilenames.MultiChainDExe);
                // if the first default directory doesn't exist then,
                // determine if the second default location exists 'C:\multichain\multichaind.exe'.
                else if (Directory.Exists(Path.Combine(DEFAULT_WIN_EXE_PATH_1, MultiChainFilenames.MultiChainDExe)))
                    return Path.Combine(DEFAULT_WIN_EXE_PATH_1, MultiChainFilenames.MultiChainDExe);
                else
                    throw new DirectoryNotFoundException();
            }
            else if (OSDetection.IsLinux() || OSDetection.IsMacOS())
            {
                if (Directory.Exists(Path.Combine(DEFAULT_LINUX_EXE_PATH, MultiChainFilenames.MultiChainDExe)))
                    return Path.Combine(DEFAULT_LINUX_EXE_PATH, MultiChainFilenames.MultiChainDExe);
                else
                    throw new DirectoryNotFoundException();
            }

            return string.Empty;
        }

        /// <summary>
        /// Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.
        /// </summary>
        /// <param name="multichainExeDirectory"></param>
        /// <returns></returns>
        public static string GetMultiChainCliExePath(string multichainExeDirectory)
        {
            // determine if the user passed a 'multichainExeDirectly' value,
            // then determine if the Directory exists.
            if (!string.IsNullOrEmpty(multichainExeDirectory)
                && Directory.Exists(Path.Combine(multichainExeDirectory, MultiChainFilenames.MultiChainCliExe)))
                return Path.Combine(multichainExeDirectory, MultiChainFilenames.MultiChainCliExe);
            else if (OSDetection.IsWindows())
            {
                // if the user didn't pass a directory or the directory doesn't exist then,
                // determine if the first default location exists 'C:\multichain-cli.exe'.
                if (Directory.Exists(Path.Combine(DEFAULT_WIN_EXE_PATH_0, MultiChainFilenames.MultiChainCliExe)))
                    return Path.Combine(DEFAULT_WIN_EXE_PATH_0, MultiChainFilenames.MultiChainCliExe);
                // if the first default directory doesn't exist then,
                // determine if the second default location exists 'C:\multichain\multichain-cli.exe'.
                else if (Directory.Exists(Path.Combine(DEFAULT_WIN_EXE_PATH_1, MultiChainFilenames.MultiChainCliExe)))
                    return Path.Combine(DEFAULT_WIN_EXE_PATH_1, MultiChainFilenames.MultiChainCliExe);
                else
                    throw new DirectoryNotFoundException();
            }
            else if (OSDetection.IsLinux() || OSDetection.IsMacOS())
            {
                if (Directory.Exists(Path.Combine(DEFAULT_LINUX_EXE_PATH, MultiChainFilenames.MultiChainCliExe)))
                    return Path.Combine(DEFAULT_LINUX_EXE_PATH, MultiChainFilenames.MultiChainCliExe);
                else
                    throw new DirectoryNotFoundException();
            }

            return string.Empty;
        }

        /// <summary>
        /// Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.
        /// </summary>
        /// <param name="multichainExeDirectory"></param>
        /// <returns></returns>
        public static string GetMultiChainDColdExePath(string multichainExeDirectory)
        {
            // determine if the user passed a 'multichainExeDirectly' value,
            // then determine if the Directory exists.
            if (!string.IsNullOrEmpty(multichainExeDirectory)
                && Directory.Exists(Path.Combine(multichainExeDirectory, MultiChainFilenames.MultiChainDColdExe)))
                return Path.Combine(multichainExeDirectory, MultiChainFilenames.MultiChainDColdExe);
            else if (OSDetection.IsWindows())
            {
                // if the user didn't pass a directory or the directory doesn't exist then,
                // determine if the first default location exists 'C:\multichaind-cold.exe'.
                if (Directory.Exists(Path.Combine(DEFAULT_WIN_EXE_PATH_0, MultiChainFilenames.MultiChainDColdExe)))
                    return Path.Combine(DEFAULT_WIN_EXE_PATH_0, MultiChainFilenames.MultiChainDColdExe);
                // if the first default directory doesn't exist then,
                // determine if the second default location exists 'C:\multichain\multichaind-cold.exe'.
                else if (Directory.Exists(Path.Combine(DEFAULT_WIN_EXE_PATH_1, MultiChainFilenames.MultiChainDColdExe)))
                    return Path.Combine(DEFAULT_WIN_EXE_PATH_1, MultiChainFilenames.MultiChainDColdExe);
                else
                    throw new DirectoryNotFoundException();
            }
            else if (OSDetection.IsLinux() || OSDetection.IsMacOS())
            {
                if (Directory.Exists(Path.Combine(DEFAULT_LINUX_EXE_PATH, MultiChainFilenames.MultiChainDColdExe)))
                    return Path.Combine(DEFAULT_LINUX_EXE_PATH, MultiChainFilenames.MultiChainDColdExe);
                else
                    throw new DirectoryNotFoundException();
            }

            return string.Empty;
        }

        /// <summary>
        /// Optionally consumers may override the default directory detected by MCWrapper.CLI with their own file path value.
        /// </summary>
        /// <param name="multichainExeDirectory"></param>
        /// <returns></returns>
        public static string GetMultiChainUtilExePath(string multichainExeDirectory)
        {
            // determine if the user passed a 'multichainExeDirectly' value,
            // then determine if the Directory exists.
            if (!string.IsNullOrEmpty(multichainExeDirectory)
                && Directory.Exists(Path.Combine(multichainExeDirectory, MultiChainFilenames.MultiChainUtilExe)))
                return Path.Combine(multichainExeDirectory, MultiChainFilenames.MultiChainUtilExe);
            else if (OSDetection.IsWindows())
            {
                // if the user didn't pass a directory or the directory doesn't exist then,
                // determine if the first default location exists 'C:\multichain-util.exe'.
                if (Directory.Exists(Path.Combine(DEFAULT_WIN_EXE_PATH_0, MultiChainFilenames.MultiChainUtilExe)))
                    return Path.Combine(DEFAULT_WIN_EXE_PATH_0, MultiChainFilenames.MultiChainUtilExe);
                // if the first default directory doesn't exist then,
                // determine if the second default location exists 'C:\multichain\multichain-util.exe'.
                else if (Directory.Exists(Path.Combine(DEFAULT_WIN_EXE_PATH_1, MultiChainFilenames.MultiChainUtilExe)))
                    return Path.Combine(DEFAULT_WIN_EXE_PATH_1, MultiChainFilenames.MultiChainUtilExe);
                else
                    throw new DirectoryNotFoundException();
            }
            else if (OSDetection.IsLinux() || OSDetection.IsMacOS())
            {
                if (Directory.Exists(Path.Combine(DEFAULT_LINUX_EXE_PATH, MultiChainFilenames.MultiChainUtilExe)))
                    return Path.Combine(DEFAULT_LINUX_EXE_PATH, MultiChainFilenames.MultiChainUtilExe);
                else
                    throw new DirectoryNotFoundException();
            }

            return string.Empty;
        }
    }
}
