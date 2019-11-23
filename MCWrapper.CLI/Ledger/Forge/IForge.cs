using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    public interface IForge
    {
        Task<ForgeResponse> ConnectToRemoteNodeAsync(string blockchainName, string ipAddress, string port, [Optional] bool useSSL);
        Task<ForgeResponse> CreateBlockchainAsync(string blockchainName);
        Task<bool> CreateColdNodeAsync(string blockchainName);
        Task<ForgeResponse> StartBlockchainAsync(string blockchainName, [Optional] bool useSsl, [Optional] Dictionary<string, object> runtimeParams);
        Task<ForgeResponse> StartColdNodeAsync(string blockchainName);
        Task<ForgeResponse> StopBlockchainAsync(string blockchainName);
        Task<ForgeResponse> StopColdNodeAsync(string blockchainName);
    }
}