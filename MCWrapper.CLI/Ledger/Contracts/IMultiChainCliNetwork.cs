using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.Data.Models.Network;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods established by the IMultiChainCliNetwork contract</para>
    /// 
    /// addnode, getaddednodeinfo, getchunkqueueinfo, getchunkqueuetotals, 
    /// getconnectioncount, getnettotals, getnetworkinfo, getpeerinfo, ping
    /// 
    /// </summary>
    public interface IMultiChainCliNetwork : IMultiChainCli
    {
        Task<CliResponse<object>> AddNodeAsync(string node, string action);
        Task<CliResponse<object>> AddNodeAsync(string blockchainName, string node, string action);
        Task<CliResponse<GetAddNodeInfoResult[]>> GetAddedNodeInfoAsync(bool dns, [Optional] string node);
        Task<CliResponse<GetAddNodeInfoResult[]>> GetAddedNodeInfoAsync(string blockchainName, bool dns, [Optional] string node);
        Task<CliResponse<object>> GetChunkQueueInfoAsync();
        Task<CliResponse<object>> GetChunkQueueInfoAsync(string blockchainName);
        Task<CliResponse<object>> GetChunkQueueTotalsAsync();
        Task<CliResponse<object>> GetChunkQueueTotalsAsync(string blockchainName);
        Task<CliResponse<object>> GetConnectionCountAsync();
        Task<CliResponse<object>> GetConnectionCountAsync(string blockchainName);
        Task<CliResponse<object>> GetNetTotalsAsync();
        Task<CliResponse<object>> GetNetTotalsAsync(string blockchainName);
        Task<CliResponse<GetNetworkInfoResult>> GetNetworkInfoAsync();
        Task<CliResponse<GetNetworkInfoResult>> GetNetworkInfoAsync(string blockchainName);
        Task<CliResponse<object>> GetPeerInfoAsync();
        Task<CliResponse<object>> GetPeerInfoAsync(string blockchainName);
        Task<CliResponse<object>> PingAsync();
        Task<CliResponse<object>> PingAsync(string blockchainName);
    }
}