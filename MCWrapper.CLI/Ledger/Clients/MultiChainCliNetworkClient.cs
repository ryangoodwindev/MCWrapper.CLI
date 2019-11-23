using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Options;
using MCWrapper.Data.Models.Network;
using MCWrapper.Ledger.Actions;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    public class MultiChainCliNetworkClient : MultiChainCliClient, IMultiChainCliNetwork
    {
        /// <summary>
        /// Create a new NetworkCLIClient instance with parameters
        /// 
        /// <para>
        ///     MutliChain methods implemented:
        ///     addnode, getaddednodeinfo, getchunkqueueinfo, getchunkqueuetotals, 
        ///     getconnectioncount, getnettotals, getnetworkinfo, getpeerinfo, ping
        /// </para>
        /// </summary>
        /// <param name="options"></param>
        public MultiChainCliNetworkClient(IOptions<CliOptions> options)
            : base(options) { }

        /// <summary>
        /// 
        /// <para>Attempts add or remove a node from the addnode list.</para>
        /// <para>Or try a connection to a node once.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="node">The node (see getpeerinfo for nodes)</param>
        /// <param name="action">'add' to add a node to the list, 'remove' to remove a node from the list, 'onetry' to try a connection to the node once</param>
        /// <returns></returns>
        public Task<CliResponse<object>> AddNodeAsync(string blockchainName, string node, string action) =>
            TransactAsync<object>(blockchainName, NetworkAction.AddNodeMethod, new[] { node, action });

        /// <summary>
        /// 
        /// <para>Attempts add or remove a node from the addnode list.</para>
        /// <para>Or try a connection to a node once.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="node">The node (see getpeerinfo for nodes)</param>
        /// <param name="action">'add' to add a node to the list, 'remove' to remove a node from the list, 'onetry' to try a connection to the node once</param>
        /// <returns></returns>
        public Task<CliResponse<object>> AddNodeAsync(string node, string action) =>
            AddNodeAsync(CliOptions.ChainName, node, action);

        /// <summary>
        /// 
        /// <para>Returns information about the given added node, or all added nodes</para>
        /// <para>(note that onetry addnodes are not listed here)</para>
        /// <para>If dns is false, only a list of added nodes will be provided, otherwise connected information will also be available.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="dns">If false, only a list of added nodes will be provided, otherwise connected information will also be available</param>
        /// <param name="node">If provided, return information about this specific node,otherwise all nodes are returned</param>
        /// <returns></returns>
        public Task<CliResponse<GetAddNodeInfoResult[]>> GetAddedNodeInfoAsync(string blockchainName, bool dns, [Optional] string node) =>
            TransactAsync<GetAddNodeInfoResult[]>(blockchainName, NetworkAction.GetAddedNodeInfoMethod, new[] { $"{dns}", node });

        /// <summary>
        /// 
        /// <para>Returns information about the given added node, or all added nodes</para>
        /// <para>(note that onetry addnodes are not listed here)</para>
        /// <para>If dns is false, only a list of added nodes will be provided, otherwise connected information will also be available.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="dns">If false, only a list of added nodes will be provided, otherwise connected information will also be available</param>
        /// <param name="node">If provided, return information about this specific node,otherwise all nodes are returned</param>
        /// <returns></returns>
        public Task<CliResponse<GetAddNodeInfoResult[]>> GetAddedNodeInfoAsync(bool dns, [Optional] string node) =>
            GetAddedNodeInfoAsync(CliOptions.ChainName, dns, node);

        /// <summary>
        ///
        /// <para>Returns data about each current chunk queue status.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetChunkQueueInfoAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, NetworkAction.GetChunkQueueInfoMethod);

        /// <summary>
        ///
        /// <para>Returns data about each current chunk queue status.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> GetChunkQueueInfoAsync() =>
            GetChunkQueueTotalsAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns chunks delivery statistics.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetChunkQueueTotalsAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, NetworkAction.GetChunkQueueTotalsMethod);

        /// <summary>
        /// 
        /// <para>Returns chunks delivery statistics.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> GetChunkQueueTotalsAsync() =>
            GetChunkQueueTotalsAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns the number of connections to other nodes.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetConnectionCountAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, NetworkAction.GetConnectionCountMethod);

        /// <summary>
        /// 
        /// <para>Returns the number of connections to other nodes.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> GetConnectionCountAsync() =>
            GetConnectionCountAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns information about network traffic, including bytes in, bytes out, and current time.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetNetTotalsAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, NetworkAction.GetNetTotalsMethod);

        /// <summary>
        /// 
        /// <para>Returns information about network traffic, including bytes in, bytes out, and current time.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> GetNetTotalsAsync() =>
            GetNetTotalsAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns an object containing various state info regarding P2P networking.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<GetNetworkInfoResult>> GetNetworkInfoAsync(string blockchainName) =>
            TransactAsync<GetNetworkInfoResult>(blockchainName, NetworkAction.GetNetworkInfoMethod);

        /// <summary>
        /// 
        /// <para>Returns an object containing various state info regarding P2P networking.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<GetNetworkInfoResult>> GetNetworkInfoAsync() =>
            GetNetworkInfoAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns data about each connected network node as a json array of objects.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetPeerInfoAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, NetworkAction.GetPeerInfoMethod);

        /// <summary>
        /// 
        /// <para>Returns data about each connected network node as a json array of objects.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> GetPeerInfoAsync() =>
            GetPeerInfoAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Requests that a ping be sent to all other nodes, to measure ping time.</para>
        /// <para>Results provided in getpeerinfo, pingtime and pingwait fields are decimal seconds.</para>
        /// <para>Ping command is handled in queue with all other commands, so it measures processing backlog, not just network ping.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> PingAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, NetworkAction.PingMethod);

        /// <summary>
        /// 
        /// <para>Requests that a ping be sent to all other nodes, to measure ping time.</para>
        /// <para>Results provided in getpeerinfo, pingtime and pingwait fields are decimal seconds.</para>
        /// <para>Ping command is handled in queue with all other commands, so it measures processing backlog, not just network ping.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> PingAsync() =>
            PingAsync(CliOptions.ChainName);
    }
}
