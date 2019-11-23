using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.Data.Models.Utility;

namespace MCWrapper.CLI.Ledger.Clients
{
    public interface IMultiChainCliUtility : IMultiChainCli
    {
        Task<CliResponse<int>> AppendBinaryCacheAsync(string identifier, string data_hex);
        Task<CliResponse<int>> AppendBinaryCacheAsync(string blockchainName, string identifier, string data_hex);
        Task<CliResponse<string>> CreateBinaryCacheAsync();
        Task<CliResponse<string>> CreateBinaryCacheAsync(string blockchainName);
        Task<CliResponse<CreateKeyPairsResult[]>> CreateKeyPairsAsync(int count = 1);
        Task<CliResponse<CreateKeyPairsResult[]>> CreateKeyPairsAsync(string blockchainName, int count = 1);
        Task<CliResponse<CreateMultiSigResult>> CreateMultiSigAsync(int n_required, string[] keys);
        Task<CliResponse<CreateMultiSigResult>> CreateMultiSigAsync(string blockchainName, int n_required, string[] keys);
        Task<CliResponse<object>> DeleteBinaryCacheAsync(string identifier);
        Task<CliResponse<object>> DeleteBinaryCacheAsync(string blockchainName, string identifier);
        Task<CliResponse<object>> EstimateFeeAsync(int n_blocks);
        Task<CliResponse<object>> EstimateFeeAsync(string blockchainName, int n_blocks);
        Task<CliResponse<object>> EstimatePriorityAsync(int n_blocks);
        Task<CliResponse<object>> EstimatePriorityAsync(string blockchainName, int n_blocks);
        Task<CliResponse<ValidateAddressResult>> ValidateAddressAsync(string address_pubkey_privkey);
        Task<CliResponse<ValidateAddressResult>> ValidateAddressAsync(string blockchainName, string address_pubkey_privkey);
        Task<CliResponse<bool>> VerifyMessageAsync(string address, string signature, string message);
        Task<CliResponse<bool>> VerifyMessageAsync(string blockchainName, string address, string signature, string message);
    }
}