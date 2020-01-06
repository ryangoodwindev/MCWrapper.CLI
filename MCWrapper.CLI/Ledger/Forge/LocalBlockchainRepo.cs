using MCWrapper.Ledger.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace MCWrapper.CLI.Ledger.Forge
{
    /// <summary>
    /// 
    /// </summary>
    public enum BlockchainRepoType
    {
        /// <summary>
        /// 
        /// </summary>
        HotNodes, 
        
        /// <summary>
        /// 
        /// </summary>
        ColdNodes
    }

    /// <summary>
    /// 
    /// </summary>
    public class LocalBlockchainRepo
    {
        /// <summary>
        /// 
        /// </summary>
        public LocalBlockchainRepo() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repoType"></param>
        /// <param name="multiChainNodeDirectory"></param>
        public LocalBlockchainRepo(BlockchainRepoType repoType, [Optional] string multiChainNodeDirectory)
        {
            switch (repoType)
            {
                case BlockchainRepoType.HotNodes:
                    SetRepoAsHotNodes(multiChainNodeDirectory);
                    break;
                case BlockchainRepoType.ColdNodes:
                    SetRepoAsColdNodes(multiChainNodeDirectory);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// True => 'Blockchains' dictionary contains at least one entry.
        /// <para>
        ///     False => 'Blockchains' dictionary contains at no entries.
        /// </para>
        /// </summary>
        public bool BlockchainsFound { get => Blockchains.Count > 0; }

        /// <summary>
        /// Key => Blockchain name
        /// <para>
        ///     Value => Full local file path to blockchain folder
        /// </para>
        /// </summary>
        public Dictionary<string, string> Blockchains { get; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multiChainHotDirectory"></param>
        /// <returns></returns>
        public void SetRepoAsHotNodes([Optional] string multiChainHotDirectory)
        {
            if (!string.IsNullOrEmpty(multiChainHotDirectory))
            {
                if (Directory.Exists(multiChainHotDirectory))
                {
                    var directories = Directory.EnumerateDirectories(multiChainHotDirectory);
                    foreach (var directory in directories)
                        Blockchains.TryAdd(directory, Path.Combine(multiChainHotDirectory, directory));
                }
            }
            else if (OSDetection.IsWindows())
            {
                var winPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MultiChain");
                if (Directory.Exists(winPath))
                {
                    var directories = Directory.EnumerateDirectories(winPath);
                    foreach (var directory in directories)
                        Blockchains.TryAdd(directory, Path.Combine(winPath, directory));
                }
            }
            else if (OSDetection.IsLinux() && OSDetection.IsMacOS())
            {
                var linuxPath = "./multichain";
                if (Directory.Exists(linuxPath))
                {
                    var directories = Directory.EnumerateDirectories(linuxPath);
                    foreach (var directory in directories)
                        Blockchains.TryAdd(directory, Path.Combine(linuxPath, directory));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multiChainColdDirectory"></param>
        /// <returns></returns>
        public void SetRepoAsColdNodes([Optional] string multiChainColdDirectory)
        {
            if (!string.IsNullOrEmpty(multiChainColdDirectory))
            {
                if (Directory.Exists(multiChainColdDirectory))
                {
                    var directories = Directory.EnumerateDirectories(multiChainColdDirectory);
                    foreach (var directory in directories)
                        Blockchains.TryAdd(directory, Path.Combine(multiChainColdDirectory, directory));
                }
            }
            else if (OSDetection.IsWindows())
            {
                var winPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MultiChainCold");
                if (Directory.Exists(winPath))
                {
                    var directories = Directory.EnumerateDirectories(winPath);
                    foreach (var directory in directories)
                        Blockchains.TryAdd(directory, Path.Combine(winPath, directory));
                }
            }
            else if (OSDetection.IsLinux() && OSDetection.IsMacOS())
            {
                var linuxPath = "./multichain-cold";
                if (Directory.Exists(linuxPath))
                {
                    var directories = Directory.EnumerateDirectories(linuxPath);
                    foreach (var directory in directories)
                        Blockchains.TryAdd(directory, Path.Combine(linuxPath, directory));
                }
            }
        }
    }
}
