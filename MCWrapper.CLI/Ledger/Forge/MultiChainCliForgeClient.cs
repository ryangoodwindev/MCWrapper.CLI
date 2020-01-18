using MCWrapper.CLI.Helpers;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.CLI.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// Start and kill the multichaind process directly;
    /// This class implements the IForge contract to interact directly with the multichaind.exe binary;
    /// At the moment the Forge only works in Windows environments;
    /// <para>
    ///     Path to MultiChain library needs to be loaded via IOptions pipeline using ForgeOptions type
    /// </para>
    /// <para>
    ///     MacOS and Linux OS detection will be added in version 1.1.3;
    /// </para>
    /// </summary>
    public class MultiChainCliForgeClient : MultiChainCliMachinist, IMultiChainCliForge
    {
        /// <summary>
        /// Create a new blockchain Forge instance
        /// </summary>
        public MultiChainCliForgeClient(IOptions<CliOptions> options)
            : base(options) { }

        /// <summary>
        /// Create a new MultiChain blockchain in the local Windows environment;
        /// Passed <paramref name="blockchainName"/> value will be used as the new blockchain name;
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public new Task<ForgeResponse> CreateBlockchainAsync(string blockchainName) =>
            base.CreateBlockchainAsync(blockchainName);

        /// <summary>
        ///  Start a MultiChain blockchain present on the local Windows environment and use HTTP connections;
        /// Passed <paramref name="blockchainName"/> value will be used as the local blockchain name;
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="useSsl"></param>
        /// <param name="runtimeParams"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StartBlockchainAsync(string blockchainName, [Optional] bool useSsl, [Optional] Dictionary<string, object> runtimeParams)
        {
            var paramsBuilder = new StringBuilder();
            runtimeParams ??= new Dictionary<string, object>();

            foreach (var param in runtimeParams)
                paramsBuilder.AppendFormat($"-{param.Key}={param.Value} ");

            return StartBlockchainAsync(blockchainName, useSsl, paramsBuilder.ToString());
        }

        /// <summary>
        /// Stop a MultiChain blockchain running on the local Windows environment
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public new Task<ForgeResponse> StopBlockchainAsync(string blockchainName) =>
            base.StopBlockchainAsync(blockchainName);

        /// <summary>
        /// Create a local MultiChain blockchain cold node/wallet
        /// Create a Cold Node
        ///
        /// If Hot Wallet path does not exist throw exception 
        /// since we won't be able to access the Hot Wallet's
        /// params.dat file which is required to configure
        /// the Cold Wallet properly.
        /// 
        /// Next, verify whether or not the MultiChainCold
        /// directory exists in the /AppData/Roaming folder.
        /// If the Cold Node directory does not exist, we
        /// create it. If we end up creating this folder then
        /// we store the returned DirectoryInfo as coldNodeDirectory 
        /// since we will need to create the Code Wallet 
        /// sub-directory within the new Cold Node folder.
        ///
        /// Next, verify whether or not a folder named after
        /// the passed blockchainName value exists in the 
        /// Cold Wallet path. If the Cold Wallet path does
        /// not exist, we create it.
        ///
        /// Finally, we check if a params.dat file is available
        /// in the Cold Wallet path. If not, then we copy it
        /// from the Hot Node path.
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public async Task<bool> CreateColdNodeAsync(string blockchainName)
        {
            try
            {
                // verify if hot node params.dat file exists; FileNotFoundException is thrown if not.
                var hotNodeParamsDatPath = MultiChainPathHelper.GetHotWalletParamsDatPath(CliOptions.ChainDefaultLocation, blockchainName);

                // verify if cold node path exists to receive new cold node params.dat file.
                var coldeNodeParamsDatPath = MultiChainPathHelper.GetColdWalletParamsDatPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName);

                // if we get this far we know the hot node params.dat file exists, let us read the file into an array
                var params_dat = await File.ReadAllLinesAsync(hotNodeParamsDatPath);

                // if we get this far we know the hot node params.dat file exists and that the cold node directory also has been
                // created or already exists.
                if (!File.Exists(coldeNodeParamsDatPath))
                    await File.WriteAllLinesAsync(coldeNodeParamsDatPath, params_dat);

                return File.Exists(coldeNodeParamsDatPath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Start an existing local MultiChain blockchain cold node/wallet
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public new Task<ForgeResponse> StartColdNodeAsync(string blockchainName) =>
            base.StartColdNodeAsync(blockchainName);

        /// <summary>
        /// Stop a MultiChain cold node running on the local Windows environment
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public new Task<ForgeResponse> StopColdNodeAsync(string blockchainName) =>
            base.StopColdNodeAsync(blockchainName);

        /// <summary>
        /// Connect to a remote MultiChain node. 
        /// Returns process id to caller
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <param name="useSSL"></param>
        /// <returns></returns>
        public new Task<ForgeResponse> ConnectToRemoteNodeAsync(string blockchainName, string ipAddress, string port, [Optional] bool useSSL) =>
            base.ConnectToRemoteNodeAsync(blockchainName, ipAddress, port, useSSL);
    }
}