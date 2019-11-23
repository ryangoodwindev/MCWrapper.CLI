using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods established by the IMultiChainCliMining contract</para>
    ///
    /// getblocktemplate, getmininginfo, getnetworkhashps,
    /// prioritisetransaction, submitblock
    /// 
    /// </summary>
    public interface IMultiChainCliMining : IMultiChainCli
    {
        Task<CliResponse<object>> GetBlockTemplateAsync(string json_request_object);
        Task<CliResponse<object>> GetBlockTemplateAsync(string blockchainName, string json_request_object);
        Task<CliResponse<object>> GetMiningInfoAsync();
        Task<CliResponse<object>> GetMiningInfoAsync(string blockchainName);
        Task<CliResponse<object>> GetNetworkHashPsAsync(int blocks = 120, int height = -1);
        Task<CliResponse<object>> GetNetworkHashPsAsync(string blockchainName, int blocks = 120, int height = -1);
        Task<CliResponse<object>> PrioritiseTransactionAsync(string txid, double priority_delta, double fee_delta);
        Task<CliResponse<object>> PrioritiseTransactionAsync(string blockchainName, string txid, double priority_delta, double fee_delta);
        Task<CliResponse<object>> SubmitBlockAsync(string hex_data, string json_parameters_object = "");
        Task<CliResponse<object>> SubmitBlockAsync(string blockchainName, string hex_data, string json_parameters_object = "");
    }
}