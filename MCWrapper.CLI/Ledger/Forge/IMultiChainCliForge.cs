using MCWrapper.CLI.Ledger.Contracts;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// Action established by IMultiChainCliForge
    /// </summary>
    public interface IMultiChainCliForge : IMultiChainCli
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <param name="useSSL"></param>
        /// <returns></returns>
        Task<ForgeResponse> ConnectToRemoteNodeAsync(string blockchainName, string ipAddress, string port, [Optional] bool useSSL);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        Task<ForgeResponse> CreateBlockchainAsync(string blockchainName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        Task<bool> CreateColdNodeAsync(string blockchainName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="useSsl"></param>
        /// <param name="runtimeParams"></param>
        /// <returns></returns>
        Task<ForgeResponse> StartBlockchainAsync(string blockchainName, [Optional] bool useSsl, [Optional] Dictionary<string, object> runtimeParams);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        Task<ForgeResponse> StartColdNodeAsync(string blockchainName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        Task<ForgeResponse> StopBlockchainAsync(string blockchainName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        Task<ForgeResponse> StopColdNodeAsync(string blockchainName);
    }
}