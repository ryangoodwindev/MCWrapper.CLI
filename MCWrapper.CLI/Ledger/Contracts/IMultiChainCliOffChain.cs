using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods established by the IMultiChainCliOffChain contract</para>
    /// 
    /// purgepublisheditems, purgestreamitems, retrievestreamitems
    /// 
    /// OffChain services to support MultiChain Enterprise users
    /// I do not have access to an Enterprise version of MultiChain 
    /// so no unit testing can be performed against these methods
    /// 
    /// </summary>
    public interface IMultiChainCliOffChain : IMultiChainCli
    {
        Task<CliResponse<object>> PurgePublishedItemsAsync(object items);
        Task<CliResponse<object>> PurgePublishedItemsAsync(string blockchainName, object items);
        Task<CliResponse<object>> PurgeStreamItemsAsync(string stream, object items);
        Task<CliResponse<object>> PurgeStreamItemsAsync(string blockchainName, string stream, object items);
        Task<CliResponse<object>> RetrieveStreamItemsAsync(string stream, object items);
        Task<CliResponse<object>> RetrieveStreamItemsAsync(string blockchainName, string stream, object items);
    }
}