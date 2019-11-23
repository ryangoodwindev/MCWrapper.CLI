using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;

namespace MCWrapper.CLI.Ledger.Clients
{
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