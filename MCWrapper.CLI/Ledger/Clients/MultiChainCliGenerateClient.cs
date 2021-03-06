﻿using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.CLI.Options;
using MCWrapper.Ledger.Actions;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods implemented by the MultiChainCliGenerateClient concrete class</para>
    ///
    /// getgenerate, gethashespersec, setgenerate
    /// 
    /// </summary>
    public class MultiChainCliGenerateClient : MultiChainCliClient, IMultiChainCliGenerate
    {
        /// <summary>
        /// Create a new MultiChainCliGenerateClient instance
        /// </summary>
        /// <param name="options"></param>
        public MultiChainCliGenerateClient(IOptions<CliOptions> options)
            : base(options) { }

        /// <summary>
        /// 
        /// <para>Return if the server is set to generate coins or not. The default is false.</para>
        /// <para>It is set with the command line argument -gen (or bitcoin.conf setting gen)</para>
        /// <para>It can also be set with the setgenerate call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns>If the server is set to generate coins or not</returns>
        public Task<CliResponse<bool>> GetGeneratedAsync(string blockchainName) =>
            TransactAsync<bool>(blockchainName, GenerateAction.GetGenerateMethod);

        /// <summary>
        /// 
        /// <para>Return if the server is set to generate coins or not. The default is false.</para>
        /// <para>It is set with the command line argument -gen (or bitcoin.conf setting gen)</para>
        /// <para>It can also be set with the setgenerate call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns>If the server is set to generate coins or not</returns>
        public Task<CliResponse<bool>> GetGeneratedAsync() =>
            GetGeneratedAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns a recent hashes per second performance measurement while generating.</para>
        /// <para>See the getgenerate and setgenerate calls to turn generation on and off.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns>(numeric) The recent hashes per second when generation is on (will return 0 if generation is off)</returns>
        public Task<CliResponse<int>> GetHashesPerSecAsync(string blockchainName) =>
            TransactAsync<int>(blockchainName, GenerateAction.GetHashesPerSecMethod);

        /// <summary>
        /// 
        /// <para>Returns a recent hashes per second performance measurement while generating.</para>
        /// <para>See the getgenerate and setgenerate calls to turn generation on and off.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns>(numeric) The recent hashes per second when generation is on (will return 0 if generation is off)</returns>
        public Task<CliResponse<int>> GetHashesPerSecAsync() =>
            GetHashesPerSecAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Set 'generate' true or false to turn generation on or off.</para>
        /// <para>Generation is limited to 'genproclimit' processors, -1 is unlimited.</para>
        /// <para>See the getgenerate call for the current setting.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="generate">Set to true to turn on generation, off to turn off.</param>
        /// <param name="gen_proc_limit">Set the processor limit for when generation is on. Can be -1 for unlimited.</param>
        /// <returns>String value identifying this transaction</returns>
        public Task<CliResponse> SetGenerateAsync(string blockchainName, bool generate, int gen_proc_limit) =>
            TransactAsync(blockchainName, GenerateAction.SetGenerateMethod, new[] { $"{generate}".ToLower(), $"{gen_proc_limit}" });

        /// <summary>
        /// 
        /// <para>Set 'generate' true or false to turn generation on or off.</para>
        /// <para>Generation is limited to 'genproclimit' processors, -1 is unlimited.</para>
        /// <para>See the getgenerate call for the current setting.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="generate">Set to true to turn on generation, off to turn off.</param>
        /// <param name="gen_proc_limit">Set the processor limit for when generation is on. Can be -1 for unlimited.</param>
        /// <returns>String value identifying this transaction</returns>
        public Task<CliResponse> SetGenerateAsync(bool generate, int gen_proc_limit) =>
            SetGenerateAsync(CliOptions.ChainName, generate, gen_proc_limit);
    }
}
