using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.Data.Models.Raw;

namespace MCWrapper.CLI.Ledger.Clients
{
    public interface IMultiChainCliRaw : IMultiChainCli
    {
        Task<CliResponse<object>> AppendRawChangeAsync(string tx_hex, string address, [Optional] double native_fee);
        Task<CliResponse<object>> AppendRawChangeAsync(string blockchainName, string tx_hex, string address, [Optional] double native_fee);
        Task<CliResponse<object>> AppendRawDataAsync(string tx_hex, object data);
        Task<CliResponse<object>> AppendRawDataAsync(string blockchainName, string tx_hex, object data);
        Task<CliResponse<object>> AppendRawTransactionAsync(string tx_hex, object[] transactions, object addresses, [Optional] object[] data, string action = "");
        Task<CliResponse<object>> AppendRawTransactionAsync(string blockchainName, string tx_hex, object[] transactions, object addresses, [Optional] object[] data, string action = "");
        Task<CliResponse<object>> CreateRawTransactionAsync(object[] transactions, object addresses, [Optional] object[] data, string action = "");
        Task<CliResponse<object>> CreateRawTransactionAsync(string blockchainName, object[] transactions, object addresses, [Optional] object[] data, string action = "");
        Task<CliResponse<DecodeRawTransactionResult>> DecodeRawTransactionAsync(string tx_hex);
        Task<CliResponse<DecodeRawTransactionResult>> DecodeRawTransactionAsync(string blockchainName, string tx_hex);
        Task<CliResponse<object>> DecodeScriptAsync(string script_hex);
        Task<CliResponse<object>> DecodeScriptAsync(string blockchainName, string script_hex);
        Task<CliResponse<object>> GetRawTransactionAsync(string txid, [Optional] bool verbose);
        Task<CliResponse<object>> GetRawTransactionAsync(string blockchainName, string txid, [Optional] bool verbose);
        Task<CliResponse<object>> SendRawTransactionAsync(string tx_hex, bool allow_high_fees = false);
        Task<CliResponse<object>> SendRawTransactionAsync(string blockchainName, string tx_hex, bool allow_high_fees = false);
        Task<CliResponse<SignRawTransactionResult>> SignRawTransactionAsync(string tx_hex, [Optional] object[] prevtxs, [Optional] object[] privatekeys, [Optional] string sighashtype);
        Task<CliResponse<SignRawTransactionResult>> SignRawTransactionAsync(string blockchainName, string tx_hex, [Optional] object[] prevtxs, [Optional] object[] privatekeys, [Optional] string sighashtype);
    }
}