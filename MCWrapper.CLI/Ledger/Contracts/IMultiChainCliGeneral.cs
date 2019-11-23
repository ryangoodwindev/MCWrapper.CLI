using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.Data.Models.Blockchain;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    ///
    /// <para>MutliChain Core methods established by the IMultiChainCliGeneral contract</para>
    ///
    /// getassetinfo, getbestblockhash, getblock, getblockchaininfo,
    /// getblockcount, getblockhash, getchaintips, getdifficulty,
    /// getfiltercode, getlastblockinfo, getmempoolinfo, getrawmempool,
    /// getstreaminfo, gettxout, gettxoutsetinfo, listassets, listblocks,
    /// listpermissions, liststreamfilters, liststreams, listtxfilters,
    /// listupgrades, runstreamfilter, runtxfilter, teststreamfilter,
    /// testtxfilter, verifychain, verifypermission
    ///
    /// </summary>
    public interface IMultiChainCliGeneral : IMultiChainCli
    {
        Task<CliResponse<GetAssetInfoResult>> GetAssetInfoAsync(string asset_identifier, bool verbose = false);
        Task<CliResponse<GetAssetInfoResult>> GetAssetInfoAsync(string blockchainName, string asset_identifier, bool verbose = false);
        Task<CliResponse<string>> GetBestBlockHashAsync();
        Task<CliResponse<string>> GetBestBlockHashAsync(string blockchainName);
        Task<CliResponse<T>> GetBlockAsync<T>(object hash_or_height, [Optional] object verbose);
        Task<CliResponse<T>> GetBlockAsync<T>(string blockchainName, object hash_or_height, [Optional] object verbose);
        Task<CliResponse<GetBlockchainInfoResult>> GetBlockchainInfoAsync();
        Task<CliResponse<GetBlockchainInfoResult>> GetBlockchainInfoAsync(string blockchainName);
        Task<CliResponse<long>> GetBlockCountAsync();
        Task<CliResponse<long>> GetBlockCountAsync(string blockchainName);
        Task<CliResponse<string>> GetBlockHashAsync(int index);
        Task<CliResponse<string>> GetBlockHashAsync(string blockchainName, int index);
        Task<CliResponse<GetChainTipsResult[]>> GetChainTipsAsync();
        Task<CliResponse<GetChainTipsResult[]>> GetChainTipsAsync(string blockchainName);
        Task<CliResponse<double>> GetDifficultyAsync();
        Task<CliResponse<double>> GetDifficultyAsync(string blockchainName);
        Task<CliResponse<string>> GetFilterCodeAsync(string filter_identifier);
        Task<CliResponse<string>> GetFilterCodeAsync(string blockchainName, string filter_identifier);
        Task<CliResponse<GetLastBlockInfoResult>> GetLastBlockInfoAsync(int skip = 0);
        Task<CliResponse<GetLastBlockInfoResult>> GetLastBlockInfoAsync(string blockchainName, int skip = 0);
        Task<CliResponse<GetMemPoolInfoResult>> GetMemPoolInfoAsync();
        Task<CliResponse<GetMemPoolInfoResult>> GetMemPoolInfoAsync(string blockchainName);
        Task<CliResponse<GetRawMemPoolResult>> GetRawMemPoolAsync(bool verbose = false);
        Task<CliResponse<GetRawMemPoolResult>> GetRawMemPoolAsync(string blockchainName, bool verbose = false);
        Task<CliResponse<GetStreamInfoResult>> GetStreamInfoAsync(string stream_identifier, bool verbose = false);
        Task<CliResponse<GetStreamInfoResult>> GetStreamInfoAsync(string blockchainName, string stream_identifier, bool verbose = false);
        Task<CliResponse<GetTxOutResult>> GetTxOutAsync(string txid, int n, bool include_mem_pool = true);
        Task<CliResponse<GetTxOutResult>> GetTxOutAsync(string blockchainName, string txid, int n, bool include_mem_pool = true);
        Task<CliResponse<GetTxOutSetInfoResult>> GetTxOutSetInfoAsync();
        Task<CliResponse<GetTxOutSetInfoResult>> GetTxOutSetInfoAsync(string blockchainName);
        Task<CliResponse<ListAssetsResult[]>> ListAssetsAsync([Optional] object asset_identifiers, [Optional] bool verbose, [Optional] int count, [Optional] int start);
        Task<CliResponse<ListAssetsResult[]>> ListAssetsAsync(string blockchainName, [Optional] object asset_identifiers, [Optional] bool verbose, [Optional] int count, [Optional] int start);
        Task<CliResponse<ListBlocksResult[]>> ListBlocksAsync(object block_set_identifier, bool verbose);
        Task<CliResponse<ListBlocksResult[]>> ListBlocksAsync(string blockchainName, object block_set_identifier, bool verbose);
        Task<CliResponse<ListPermissionsResult[]>> ListPermissionsAsync([Optional] string permissions, [Optional] object addresses, [Optional] bool verbose);
        Task<CliResponse<ListPermissionsResult[]>> ListPermissionsAsync(string blockchainName, [Optional] string permissions, [Optional] object addresses, [Optional] bool verbose);
        Task<CliResponse<ListStreamFiltersResult[]>> ListStreamFiltersAsync([Optional] object filter_identifers, [Optional] bool verbose);
        Task<CliResponse<ListStreamFiltersResult[]>> ListStreamFiltersAsync(string blockchainName, [Optional] object filter_identifers, [Optional] bool verbose);
        Task<CliResponse<ListStreamsResult[]>> ListStreamsAsync([Optional] object stream_identifiers, [Optional] bool verbose, [Optional] int count, [Optional] int start);
        Task<CliResponse<ListStreamsResult[]>> ListStreamsAsync(string blockchainName, [Optional] object stream_identifiers, [Optional] bool verbose, [Optional] int count, [Optional] int start);
        Task<CliResponse<ListTxFiltersResult[]>> ListTxFiltersAsync([Optional] object filter_identifiers, [Optional] bool verbose);
        Task<CliResponse<ListTxFiltersResult[]>> ListTxFiltersAsync(string blockchainName, [Optional] object filter_identifiers, [Optional] bool verbose);
        Task<CliResponse<object>> ListUpgradesAsync([Optional] object upgrade_identifiers);
        Task<CliResponse<object>> ListUpgradesAsync(string blockchainName, [Optional] object upgrade_identifiers);
        Task<CliResponse<RunStreamFilterResult>> RunStreamFilterAsync(string filter_identifier, [Optional] string tx_hex, [Optional] int vout);
        Task<CliResponse<RunStreamFilterResult>> RunStreamFilterAsync(string blockchainName, string filter_identifier, [Optional] string tx_hex, [Optional] int vout);
        Task<CliResponse<RunTxFilterResult>> RunTxFilterAsync([Optional] string filter_identifier, [Optional] string tx_hex);
        Task<CliResponse<RunTxFilterResult>> RunTxFilterAsync(string blockchainName, [Optional] string filter_identifier, [Optional] string tx_hex);
        Task<CliResponse<TestStreamFilterResult>> TestStreamFilterAsync(object restrictions, string javascript_code, [Optional] string tx_hex, [Optional] int vout);
        Task<CliResponse<TestStreamFilterResult>> TestStreamFilterAsync(string blockchainName, object restrictions, string javascript_code, [Optional] string tx_hex, [Optional] int vout);
        Task<CliResponse<TestTxFilterResult>> TestTxFilterAsync(object restrictions, string javascript_code, [Optional] string tx_hex);
        Task<CliResponse<TestTxFilterResult>> TestTxFilterAsync(string blockchainName, object restrictions, string javascript_code, [Optional] string tx_hex);
        Task<CliResponse<bool>> VerifyChainAsync([Optional] int check_level, [Optional] int num_blocks);
        Task<CliResponse<bool>> VerifyChainAsync(string blockchainName, [Optional] int check_level, [Optional] int num_blocks);
        Task<CliResponse<bool>> VerifyPermissionAsync(string address, string permission);
        Task<CliResponse<bool>> VerifyPermissionAsync(string blockchainName, string address, string permission);
    }
}