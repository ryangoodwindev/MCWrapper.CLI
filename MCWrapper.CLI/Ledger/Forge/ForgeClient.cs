﻿using MCWrapper.CLI.Constants;
using MCWrapper.CLI.Options;
using MCWrapper.Ledger.Entities.ErrorHandling;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
    public class ForgeClient : Machinist
    {
        /// <summary>
        /// Create a new blockchain Forge instance
        /// </summary>
        public ForgeClient(IOptions<CliOptions> options) 
            : base(options) { }

        /// <summary>
        /// Create a new MultiChain blockchain in the local Windows environment;
        /// Passed <paramref name="blockchainName"/> value will be used as the new blockchain name;
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> CreateBlockchainAsync(string blockchainName) => 
            Task.Run(() => CreateBlockchain(blockchainName));

        /// <summary>
        /// Start a MultiChain blockchain present on the local Windows environment and use HTTP connections;
        /// Passed <paramref name="blockchainName"/> value will be used as the local blockchain name;
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StartBlockchainAsync(string blockchainName,
            [Optional] bool useSsl, [Optional] Dictionary<string, object> runtimeParams)
        {
            var paramsBuilder = new StringBuilder();
            runtimeParams ??= new Dictionary<string, object>();

            foreach (var param in runtimeParams)
                paramsBuilder.AppendFormat("-{0}={1} ", param.Key, param.Value);

            return Task.Run(() => StartBlockchain(blockchainName, useSsl, paramsBuilder.ToString()));
        }

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
        public async Task CreateColdNodeAsync(string blockchainName)
        {
            if (!Directory.Exists(MultiChainPaths.GetHotWalletPath(CliOptions.ChainDefaultLocation, blockchainName)) || !File.Exists(MultiChainPaths.GetHotWalletParamsDatPath(CliOptions.ChainDefaultLocation, blockchainName)))
                throw new DirectoryNotFoundException($"Hot Node path is not found. Unable to access params.dat file: {MultiChainPaths.GetHotWalletPath(CliOptions.ChainDefaultLocation, blockchainName)}");

            if (!Directory.Exists(CliOptions.ChainDefaultColdNodeLocation))
                Directory.CreateDirectory(CliOptions.ChainDefaultColdNodeLocation).CreateSubdirectory(blockchainName);
            else if (!Directory.Exists(MultiChainPaths.GetColdWalletPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName)))
                Directory.CreateDirectory(MultiChainPaths.GetColdWalletPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName));

            if (File.Exists(MultiChainPaths.GetColdWalletParamsDatPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName)))
                throw new ServiceException($"Sorry, looks like the params.dat file already exists in the Cold Node Wallet named: {blockchainName}");

            var params_dat = await File.ReadAllLinesAsync(MultiChainPaths.GetHotWalletParamsDatPath(CliOptions.ChainDefaultLocation, blockchainName));

            await File.WriteAllLinesAsync(MultiChainPaths.GetColdWalletParamsDatPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName), params_dat);
        }

        /// <summary>
        /// Start an existing local MultiChain blockchain cold node/wallet
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StartColdNodeAsync(string blockchainName)
        {
            // check if cold node path exists
            // check if cold node path contains a copy of the hot node's params.dat file
            _ = !Directory.Exists(MultiChainPaths.GetColdWalletPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName))
                    ? throw new ServiceException("Sorry, we can't find the MultiChainCold folder. Please be sure to run CreateColdNode first or create the folder manually.")
                    : !File.Exists(MultiChainPaths.GetColdWalletParamsDatPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName))
                    ? throw new ServiceException($"Sorry, it seems there is no params.dat file found in the Cold Node wallet you are trying to use for blockchain {blockchainName}")
                    : false;

            return Task.Run(() => StartColdNode(blockchainName));
        }

        /// <summary>
        /// Connect to a remote MultiChain node. 
        /// Returns process id to caller
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public Task<ForgeResponse> ConnectToRemoteNodeAsync(string blockchainName, string ipAddress, string port, [Optional] bool useSSL) =>
            Task.Run(() => ConnectToRemoteNode(blockchainName, ipAddress, port, useSSL));

        /// <summary>
        /// Stop a MultiChain blockchain running on the local Windows environment
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StopBlockchainAsync(string blockchainName) => 
            Task.Run(() => StopBlockchain(blockchainName));

        /// <summary>
        /// Stop a MultiChain cold node running on the local Windows environment
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StopColdNodeAsync(string blockchainName) => 
            Task.Run(() => StopColdNode(blockchainName));
    }
}