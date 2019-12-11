using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Options;
using MCWrapper.Ledger.Actions;
using MCWrapper.Ledger.Entities.Extensions;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods implemented by the MultiChainCliOffChainClient concrete class</para>
    /// 
    /// purgepublisheditems, purgestreamitems, retrievestreamitems
    /// 
    /// OffChain services to support MultiChain Enterprise users
    /// I do not have access to an Enterprise version of MultiChain 
    /// so no unit testing can be performed against these methods
    /// 
    /// </summary>
    public class MultiChainCliOffChainClient : MultiChainCliClient, IMultiChainCliOffChain
    {
        /// <summary>
        /// Create a new MultiChainCliOffChainClient instance
        /// </summary>
        /// <param name="options"></param>
        public MultiChainCliOffChainClient(IOptions<CliOptions> options)
            : base(options) { }

        /// <summary>
        ///
        /// <para>Available only in Enterprise Edition.</para>
        /// <para>Purges offchain items published by this node</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="items">
        ///     <para>1. "txids" (string, required) "all" or list of transactions, comma delimited</para>
        ///     <paa>or</paa>
        ///     <para>1. txids (array, required) Array of transactions IDs</para>
        ///     <para>or</para>
        ///     <para>1. txouts (array, required) Array of transaction outputs</para>
        ///     <para>or</para>
        ///     <para>1. blocks                           (object, required) List of transactions in block range</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> PurgePublishedItemsAsync(string blockchainName, object items) =>
            TransactAsync<object>(blockchainName, OffChainAction.PurgePublishedItems, new[] { items.Serialize() });

        /// <summary>
        ///
        /// <para>Available only in Enterprise Edition.</para>
        /// <para>Purges offchain items published by this node</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="items">
        ///     <para>1. "txids" (string, required) "all" or list of transactions, comma delimited</para>
        ///     <paa>or</paa>
        ///     <para>1. txids (array, required) Array of transactions IDs</para>
        ///     <para>or</para>
        ///     <para>1. txouts (array, required) Array of transaction outputs</para>
        ///     <para>or</para>
        ///     <para>1. blocks                           (object, required) List of transactions in block range</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> PurgePublishedItemsAsync(object items) =>
            PurgePublishedItemsAsync(CliOptions.ChainName, items);

        /// <summary>
        ///
        /// <para>Available only in Enterprise Edition.</para>
        /// <para>Purges offchain data for specific items in the stream.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream">One of: create txid, stream reference, stream name</param>
        /// <param name="items">
        ///     <para>"txids" (string, required) "all" or list of transactions, comma delimited</para>
        ///     <para>or</para>
        ///     <para>txids (array, required) Array of transactions IDs</para>
        ///     <para>or</para>
        ///     <para>txouts (array, required) Array of transaction outputs</para>
        ///     <para>or</para>
        ///     <para>blocks (object, required) List of transactions in block range</para>
        ///     <para>or</para>
        ///     <para>query (object, required) Query (AND logic)</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> PurgeStreamItemsAsync(string blockchainName, string stream, object items) =>
            TransactAsync<object>(blockchainName, OffChainAction.PurgeStreamItems, new[] { stream, items.Serialize() });

        /// <summary>
        ///
        /// <para>Available only in Enterprise Edition.</para>
        /// <para>Purges offchain data for specific items in the stream.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="stream">One of: create txid, stream reference, stream name</param>
        /// <param name="items">
        ///     <para>"txids" (string, required) "all" or list of transactions, comma delimited</para>
        ///     <para>or</para>
        ///     <para>txids (array, required) Array of transactions IDs</para>
        ///     <para>or</para>
        ///     <para>txouts (array, required) Array of transaction outputs</para>
        ///     <para>or</para>
        ///     <para>blocks (object, required) List of transactions in block range</para>
        ///     <para>or</para>
        ///     <para>query (object, required) Query (AND logic)</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> PurgeStreamItemsAsync(string stream, object items) =>
            PurgeStreamItemsAsync(CliOptions.ChainName, stream, items);

        /// <summary>
        ///
        /// <para>Available only in Enterprise Edition.</para>
        /// <para>Schedules retrieval of offchain data for specific items in the stream</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream">One of: create txid, stream reference, stream name</param>
        /// <param name="items">
        ///     <para>"txids" (string, required) "all" or list of transactions, comma delimited</para>
        ///     <para>or</para>
        ///     <para>txids (array, required) Array of transactions IDs</para>
        ///     <para>or</para>
        ///     <para>txouts (array, required) Array of transaction outputs</para>
        ///     <para>or</para>
        ///     <para>blocks (object, required) List of transactions in block range</para>
        ///     <para>or</para>
        ///     <para>query (object, required) Query (AND logic)</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> RetrieveStreamItemsAsync(string blockchainName, string stream, object items) =>
            TransactAsync<object>(blockchainName, OffChainAction.RetrieveStreamItems, new[] { stream, items.Serialize() });

        /// <summary>
        ///
        /// <para>Available only in Enterprise Edition.</para>
        /// <para>Schedules retrieval of offchain data for specific items in the stream</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="stream">One of: create txid, stream reference, stream name</param>
        /// <param name="items">
        ///     <para>"txids" (string, required) "all" or list of transactions, comma delimited</para>
        ///     <para>or</para>
        ///     <para>txids (array, required) Array of transactions IDs</para>
        ///     <para>or</para>
        ///     <para>txouts (array, required) Array of transaction outputs</para>
        ///     <para>or</para>
        ///     <para>blocks (object, required) List of transactions in block range</para>
        ///     <para>or</para>
        ///     <para>query (object, required) Query (AND logic)</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> RetrieveStreamItemsAsync(string stream, object items) =>
            RetrieveStreamItemsAsync(CliOptions.ChainName, stream, items);
    }
}