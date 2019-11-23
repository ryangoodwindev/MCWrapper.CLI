using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;

namespace MCWrapper.CLI.Ledger.Clients
{
    public interface IMultiChainCliGenerate : IMultiChainCli
    {
        Task<CliResponse<bool>> GetGeneratedAsync();
        Task<CliResponse<bool>> GetGeneratedAsync(string blockchainName);
        Task<CliResponse<int>> GetHashesPerSecAsync();
        Task<CliResponse<int>> GetHashesPerSecAsync(string blockchainName);
        Task<CliResponse<object>> SetGenerateAsync(bool generate, int gen_proc_limit);
        Task<CliResponse<object>> SetGenerateAsync(string blockchainName, bool generate, int gen_proc_limit);
    }
}