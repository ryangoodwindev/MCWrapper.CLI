using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.Data.Models.Control;

namespace MCWrapper.CLI.Ledger.Clients
{
    public interface IMultiChainCliControl : IMultiChainCli
    {
        Task<CliResponse<string>> ClearMemPoolAsync();
        Task<CliResponse<string>> ClearMemPoolAsync(string blockchainName);
        Task<CliResponse<GetBlockchainParamsResult>> GetBlockchainParamsAsync(bool display_names = false, bool with_upgrades = false);
        Task<CliResponse<GetBlockchainParamsResult>> GetBlockchainParamsAsync(string blockchainName, bool display_names = false, bool with_upgrades = false);
        Task<CliResponse<GetInfoResult>> GetInfoAsync();
        Task<CliResponse<GetInfoResult>> GetInfoAsync(string blockchainName);
        Task<CliResponse<GetRuntimeParamsResult>> GetRuntimeParamsAsync();
        Task<CliResponse<GetRuntimeParamsResult>> GetRuntimeParamsAsync(string blockchainName);
        Task<CliResponse<object>> HelpAsync(string command = "getinfo");
        Task<CliResponse<object>> HelpAsync(string blockchainName, string command = "getinfo");
        Task<CliResponse<object>> PauseAsync(string tasks = "incoming,mining");
        Task<CliResponse<object>> PauseAsync(string blockchainName, string tasks = "incoming,mining");
        Task<CliResponse<object>> ResumeAsync(string tasks = "incoming,mining");
        Task<CliResponse<object>> ResumeAsync(string blockchainName, string tasks = "incoming,mining");
        Task<CliResponse<object>> SetLastBlockAsync([Optional] object hash_or_height);
        Task<CliResponse<object>> SetLastBlockAsync(string blockchainName, [Optional] object hash_or_height);
        Task<CliResponse<object>> SetRuntimeParamAsync(string parameter_name, object parameter_value);
        Task<CliResponse<object>> SetRuntimeParamAsync(string blockchainName, string parameter_name, object parameter_value);
        Task<CliResponse<string>> StopAsync();
        Task<CliResponse<string>> StopAsync(string blockchainName);
    }
}