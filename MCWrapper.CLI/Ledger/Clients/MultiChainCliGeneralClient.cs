using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.CLI.Options;
using MCWrapper.Data.Models.Blockchain;
using MCWrapper.Ledger.Actions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using static Newtonsoft.Json.JsonConvert;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    ///
    /// <para>MutliChain Core methods implemented by the MultiChainCliGeneralClient concrete class</para>
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
    public class MultiChainCliGeneralClient : MultiChainCliClient, IMultiChainCliGeneral
    {
        /// <summary>
        /// Create a new MultiChainCliGeneralClient instance
        /// </summary>
        /// <param name="options"></param>
        public MultiChainCliGeneralClient(IOptions<CliOptions> options)
            : base(options) { }

        /// <summary>
        ///
        /// <para>Returns information about a single blockchain asset referenced by issue txid, asset reference, or asset name.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="asset_identifier">One of: issue txid, asset reference, asset name</param>
        /// <param name="verbose">If true, returns list of all issue transactions, including follow-ons</param>
        /// <returns></returns>
        public Task<CliResponse<GetAssetInfoResult>> GetAssetInfoAsync(string blockchainName, string asset_identifier, bool verbose = false) =>
            TransactAsync<GetAssetInfoResult>(blockchainName, BlockchainAction.GetAssetInfoMethod, new[] { asset_identifier, $"{verbose}".ToLower() });

        /// <summary>
        ///
        /// <para>Returns information about a single blockchain asset referenced by issue txid, asset reference, or asset name.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="asset_identifier">One of: issue txid, asset reference, asset name</param>
        /// <param name="verbose">If true, returns list of all issue transactions, including follow-ons</param>
        /// <returns></returns>
        public Task<CliResponse<GetAssetInfoResult>> GetAssetInfoAsync(string asset_identifier, bool verbose = false) =>
            GetAssetInfoAsync(CliOptions.ChainName, asset_identifier, verbose);

        /// <summary>
        ///
        /// <para>Returns a hex encoded hash of the best (tip) block in the longest block chain.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetBestBlockHashAsync(string blockchainName) =>
            TransactAsync<string>(blockchainName, BlockchainAction.GetBestBlockHashMethod);

        /// <summary>
        ///
        /// <para>Returns a hex encoded hash of the best (tip) block in the longest block chain.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<string>> GetBestBlockHashAsync() =>
            GetBestBlockHashAsync(CliOptions.ChainName);

        /// <summary>
        ///
        /// <para>Returns an object containing various state info regarding block chain processing.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockchainInfoResult>> GetBlockchainInfoAsync(string blockchainName) =>
            TransactAsync<GetBlockchainInfoResult>(blockchainName, BlockchainAction.GetBlockChainInfoMethod);

        /// <summary>
        ///
        /// <para>Returns an object containing various state info regarding block chain processing.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<GetBlockchainInfoResult>> GetBlockchainInfoAsync() =>
            GetBlockchainInfoAsync(CliOptions.ChainName);

        /// <summary>
        ///
        /// <para>Returns the number of blocks in the longest block chain.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<long>> GetBlockCountAsync(string blockchainName) =>
            TransactAsync<long>(blockchainName, BlockchainAction.GetBlockCountMethod);

        /// <summary>
        ///
        /// <para>Returns the number of blocks in the longest block chain.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<long>> GetBlockCountAsync() =>
            GetBlockCountAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data for block.</para>
        /// <para>Blockchain name is inferred from CliOptions as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target MultiChain network</param>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetBlockEncodedAsync(string blockchainName, string hashOrHeight) =>
            TransactAsync<string>(blockchainName, BlockchainAction.GetBlockMethod, new[] { hashOrHeight, "false" });

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data for block.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetBlockEncodedAsync(string hashOrHeight) =>
            GetBlockEncodedAsync(CliOptions.ChainName, hashOrHeight);

        /// <summary>
        ///
        /// <para>Returns hash of block in best-block-chain at index provided.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target MultiChain network</param>
        /// <param name="index">The integer block index</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetBlockHashAsync(string blockchainName, int index) =>
            TransactAsync<string>(blockchainName, BlockchainAction.GetBlockHashMethod, new[] { $"{index}" });

        /// <summary>
        ///
        /// <para>Returns hash of block in best-block-chain at index provided.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="index">The integer block index</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetBlockHashAsync(int index) =>
            GetBlockHashAsync(CliOptions.ChainName, index);

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target MultiChain network</param>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockVerboseResult>> GetBlockVerboseAsync(string blockchainName, string hashOrHeight) =>
            TransactAsync<GetBlockVerboseResult>(blockchainName, BlockchainAction.GetBlockMethod, new[] { hashOrHeight, "true" });

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is inferred from CliOptions as parameter.</para>
        /// 
        /// </summary>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockVerboseResult>> GetBlockVerboseAsync(string hashOrHeight) =>
            GetBlockVerboseAsync(CliOptions.ChainName, hashOrHeight);

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target MultiChain network</param>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockV1Result>> GetBlockV1Async(string blockchainName, string hashOrHeight) =>
            TransactAsync<GetBlockV1Result>(blockchainName, BlockchainAction.GetBlockMethod, new[] { hashOrHeight, 1.ToString() });

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is inferred from CliOptions as parameter.</para>
        /// 
        /// </summary>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockV1Result>> GetBlockV1Async(string hashOrHeight) =>
            GetBlockV1Async(CliOptions.ChainName, hashOrHeight);

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target MultiChain network</param>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockV2Result>> GetBlockV2Async(string blockchainName, string hashOrHeight) =>
            TransactAsync<GetBlockV2Result>(blockchainName, BlockchainAction.GetBlockMethod, new[] { hashOrHeight, 2.ToString() });

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is inferred from CliOptions as parameter.</para>
        /// 
        /// </summary>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockV2Result>> GetBlockV2Async(string hashOrHeight) =>
            GetBlockV2Async(CliOptions.ChainName, hashOrHeight);

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target MultiChain network</param>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockV3Result>> GetBlockV3Async(string blockchainName, string hashOrHeight) =>
            TransactAsync<GetBlockV3Result>(blockchainName, BlockchainAction.GetBlockMethod, new[] { hashOrHeight, 3.ToString() });

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is inferred from CliOptions as parameter.</para>
        /// 
        /// </summary>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockV3Result>> GetBlockV3Async(string hashOrHeight) =>
            GetBlockV3Async(CliOptions.ChainName, hashOrHeight);

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target MultiChain network</param>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockV4Result>> GetBlockV4Async(string blockchainName, string hashOrHeight) =>
            TransactAsync<GetBlockV4Result>(blockchainName, BlockchainAction.GetBlockMethod, new[] { hashOrHeight, 4.ToString() });

        /// <summary>
        /// 
        /// <para>Returns hex-encoded data or json object for block.</para>
        /// <para>Blockchain name is inferred from CliOptions as parameter.</para>
        /// 
        /// </summary>
        /// <param name="hashOrHeight">Block hash or height index</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockV4Result>> GetBlockV4Async(string hashOrHeight) =>
            GetBlockV4Async(CliOptions.ChainName, hashOrHeight);

        /// <summary>
        ///
        /// <para>Return information about all known tips in the block tree, including the main chain as well as orphaned branches.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<IList<GetChainTipsResult>>> GetChainTipsAsync(string blockchainName) =>
            TransactAsync<IList<GetChainTipsResult>>(blockchainName, BlockchainAction.GetChainTipsMethod);

        /// <summary>
        ///
        /// <para>Return information about all known tips in the block tree, including the main chain as well as orphaned branches.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<IList<GetChainTipsResult>>> GetChainTipsAsync() =>
            GetChainTipsAsync(CliOptions.ChainName);

        /// <summary>
        ///
        /// <para>Returns the proof-of-work difficulty as a multiple of the minimum difficulty.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<double>> GetDifficultyAsync(string blockchainName) =>
            TransactAsync<double>(blockchainName, BlockchainAction.GetDifficultyMethod);

        /// <summary>
        ///
        /// <para>Returns the proof-of-work difficulty as a multiple of the minimum difficulty.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<double>> GetDifficultyAsync() =>
            GetDifficultyAsync(CliOptions.ChainName);

        /// <summary>
        ///
        /// <para>Returns code for specified filter</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="filter_identifier">One of: create txid, filter reference, filter name</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetFilterCodeAsync(string blockchainName, string filter_identifier) =>
            TransactAsync<string>(blockchainName, BlockchainAction.GetFilterCodeMethod, new[] { filter_identifier });

        /// <summary>
        ///
        /// <para>Returns code for specified filter</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="filter_identifier">One of: create txid, filter reference, filter name</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetFilterCodeAsync(string filter_identifier) =>
            GetFilterCodeAsync(CliOptions.ChainName, filter_identifier);

        /// <summary>
        ///
        /// <para>Returns information about the last or recent blocks in the active chain.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="skip">The number of blocks back to skip. Default 0</param>
        /// <returns></returns>
        public Task<CliResponse<GetLastBlockInfoResult>> GetLastBlockInfoAsync(string blockchainName, int skip = 0) =>
            TransactAsync<GetLastBlockInfoResult>(blockchainName, BlockchainAction.GetLastBlockInfoMethod, new[] { $"{skip}" });

        /// <summary>
        ///
        /// <para>Returns information about the last or recent blocks in the active chain.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="skip">The number of blocks back to skip. Default 0</param>
        /// <returns></returns>
        public Task<CliResponse<GetLastBlockInfoResult>> GetLastBlockInfoAsync(int skip = 0) =>
            GetLastBlockInfoAsync(CliOptions.ChainName, skip);

        /// <summary>
        ///
        /// <para>Returns details on the active state of the TX memory pool.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<GetMemPoolInfoResult>> GetMemPoolInfoAsync(string blockchainName) =>
            TransactAsync<GetMemPoolInfoResult>(blockchainName, BlockchainAction.GetMemPoolInfoMethod);

        /// <summary>
        ///
        /// <para>Returns details on the active state of the TX memory pool.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<GetMemPoolInfoResult>> GetMemPoolInfoAsync() =>
            GetMemPoolInfoAsync(CliOptions.ChainName);

        /// <summary>
        ///
        /// <para>Returns all transaction ids in memory pool as a json array of string transaction ids.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="verbose">true for a json object, false for array of transaction ids</param>
        /// <returns></returns>
        public Task<CliResponse<GetRawMemPoolResult>> GetRawMemPoolAsync(string blockchainName, bool verbose = false) =>
            TransactAsync<GetRawMemPoolResult>(blockchainName, BlockchainAction.GetRawMemPoolMethod, new[] { $"{verbose}".ToLower() });

        /// <summary>
        ///
        /// <para>Returns all transaction ids in memory pool as a json array of string transaction ids.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="verbose">true for a json object, false for array of transaction ids</param>
        /// <returns></returns>
        public Task<CliResponse<GetRawMemPoolResult>> GetRawMemPoolAsync(bool verbose = false) =>
            GetRawMemPoolAsync(CliOptions.ChainName, verbose);

        /// <summary>
        ///
        /// <para>Returns information about a single stream.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of: create txid, stream reference, stream name</param>
        /// <param name="verbose">If true, returns list of creators</param>
        /// <returns></returns>
        public Task<CliResponse<GetStreamInfoResult>> GetStreamInfoAsync(string blockchainName, string stream_identifier, bool verbose = false) =>
            TransactAsync<GetStreamInfoResult>(blockchainName, BlockchainAction.GetStreamInfoMethod, new[] { stream_identifier, $"{verbose}".ToLower() });

        /// <summary>
        ///
        /// <para>Returns information about a single stream.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="stream_identifier">One of: create txid, stream reference, stream name</param>
        /// <param name="verbose">If true, returns list of creators</param>
        /// <returns></returns>
        public Task<CliResponse<GetStreamInfoResult>> GetStreamInfoAsync(string stream_identifier, bool verbose = false) =>
            GetStreamInfoAsync(CliOptions.ChainName, stream_identifier, verbose);

        /// <summary>
        ///
        /// <para>Returns details about an unspent transaction output.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="n">vout value</param>
        /// <param name="include_mem_pool">Whether to included the mem pool</param>
        /// <returns></returns>
        public Task<CliResponse<GetTxOutResult>> GetTxOutAsync(string blockchainName, string txid, int n, bool include_mem_pool = true) =>
            TransactAsync<GetTxOutResult>(blockchainName, BlockchainAction.GetTxOutMethod, new[] { txid, $"{n}", $"{include_mem_pool}".ToLower() });

        /// <summary>
        ///
        /// <para>Returns details about an unspent transaction output.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="txid">The transaction id</param>
        /// <param name="n">vout value</param>
        /// <param name="include_mem_pool">Whether to included the mem pool</param>
        /// <returns></returns>
        public Task<CliResponse<GetTxOutResult>> GetTxOutAsync(string txid, int n, bool include_mem_pool = true) =>
            GetTxOutAsync(CliOptions.ChainName, txid, n, include_mem_pool);

        /// <summary>
        ///
        /// <para>Returns statistics about the unspent transaction output set. Note this call may take some time.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<GetTxOutSetInfoResult>> GetTxOutSetInfoAsync(string blockchainName) =>
            TransactAsync<GetTxOutSetInfoResult>(blockchainName, BlockchainAction.GetTxOutSetInfoMethod);

        /// <summary>
        ///
        /// <para>Returns statistics about the unspent transaction output set. Note this call may take some time.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<GetTxOutSetInfoResult>> GetTxOutSetInfoAsync() =>
            GetTxOutSetInfoAsync(CliOptions.ChainName);

        /// <summary>
        ///
        /// <para>Returns list of defined assets.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="asset_identifiers">(string, optional) Asset identifier - one of the following: issue txid, asset reference, asset name, or (array, optional) A json array of asset identifiers</param>
        /// <param name="verbose">If true, returns list of all issue transactions, including follow-ons</param>
        /// <param name="count">The number of assets to display</param>
        /// <param name="start">Start from specific asset, 0 based, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListAssetsResult>>> ListAssetsAsync(string blockchainName, [Optional] object asset_identifiers, [Optional] bool verbose, [Optional] int count, [Optional] int start) =>
            asset_identifiers switch
            {
                string strType => TransactAsync<IList<ListAssetsResult>>(blockchainName, BlockchainAction.ListAssetsMethod, new[] { strType, $"{verbose}".ToLower(), $"{count}", $"{start}" }),
                object objType => TransactAsync<IList<ListAssetsResult>>(blockchainName, BlockchainAction.ListAssetsMethod, new[] { SerializeObject(objType), $"{verbose}".ToLower(), $"{count}", $"{start}" })
            };

        /// <summary>
        ///
        /// <para>Returns list of defined assets.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="asset_identifiers">(string, optional) Asset identifier - one of the following: issue txid, asset reference, asset name, or (array, optional) A json array of asset identifiers</param>
        /// <param name="verbose">If true, returns list of all issue transactions, including follow-ons</param>
        /// <param name="count">The number of assets to display</param>
        /// <param name="start">Start from specific asset, 0 based, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListAssetsResult>>> ListAssetsAsync([Optional] object asset_identifiers, [Optional] bool verbose, [Optional] int count, [Optional] int start) =>
            ListAssetsAsync(CliOptions.ChainName, asset_identifiers, verbose, count, start);

        /// <summary>
        ///
        /// <para>Returns list of block information objects</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="block_set_identifier">
        ///     <para>Comma delimited list of block identifiers: block height, block hash, block height range, e.g. block-from - block-to, number of last blocks in the active chain (if negative)</para>
        ///     <para>or</para>
        ///     <para>String that represents a "block-set-identifier"</para>
        ///     <para>or</para>
        ///     <para>A json array of block identifiers block-set-identifier</para>
        ///     <para>or</para>
        ///     <para>A json object with time range
        ///     {
        ///       "starttime" : start-time      (numeric,required) Start time.
        ///       "endtime" : end-time          (numeric,required) End time.
        ///     }</para>
        /// </param>
        /// <param name="verbose">If true, returns more information</param>
        /// <returns></returns>
        /// <returns></returns>
        public Task<CliResponse<IList<ListBlocksResult>>> ListBlocksAsync(string blockchainName, object block_set_identifier, bool verbose) =>
            block_set_identifier switch
            {
                string strType => TransactAsync<IList<ListBlocksResult>>(blockchainName, BlockchainAction.ListBlocksMethod, new[] { strType, $"{verbose}".ToLower() }),
                object objType => TransactAsync<IList<ListBlocksResult>>(blockchainName, BlockchainAction.ListBlocksMethod, new[] { SerializeObject(objType), $"{verbose}".ToLower() })
            };

        /// <summary>
        ///
        /// <para>Returns list of block information objects</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="block_set_identifier">
        ///     <para>Comma delimited list of block identifiers: block height, block hash, block height range, e.g. block-from - block-to, number of last blocks in the active chain (if negative)</para>
        ///     <para>or</para>
        ///     <para>String that represents a "block-set-identifier"</para>
        ///     <para>or</para>
        ///     <para>A json array of block identifiers block-set-identifier</para>
        ///     <para>or</para>
        ///     <para>A json object with time range
        ///     {
        ///       "starttime" : start-time      (numeric,required) Start time.
        ///       "endtime" : end-time          (numeric,required) End time.
        ///     }</para>
        /// </param>
        /// <param name="verbose">If true, returns more information</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListBlocksResult>>> ListBlocksAsync(object block_set_identifier, bool verbose) =>
            ListBlocksAsync(CliOptions.ChainName, block_set_identifier, verbose);

        /// <summary>
        ///
        /// <para>Returns a list of all permissions which have been explicitly granted to addresses.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="permissions">Permission strings, comma delimited. Possible values: connect,send,receive,issue,mine,admin,activate,create. Default: all</param>
        /// <param name="addresses">
        ///     <para>(string, optional, default "*") The addresses to retrieve permissions for. "*" for all addresses</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of addresses to return permissions for</para>
        /// </param>
        /// <param name="verbose">If true, returns list of pending grants</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListPermissionsResult>>> ListPermissionsAsync(string blockchainName, [Optional] string permissions, [Optional] object addresses, [Optional] bool verbose) =>
            addresses switch
            {
                string strType => TransactAsync<IList<ListPermissionsResult>>(blockchainName, BlockchainAction.ListPermissionsMethod, new[] { permissions, strType, $"{verbose}".ToLower() }),
                object objType => TransactAsync<IList<ListPermissionsResult>>(blockchainName, BlockchainAction.ListPermissionsMethod, new[] { permissions, SerializeObject(objType), $"{verbose}".ToLower() })
            };

        /// <summary>
        ///
        /// <para>Returns a list of all permissions which have been explicitly granted to addresses.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="permissions">Permission strings, comma delimited. Possible values: connect,send,receive,issue,mine,admin,activate,create. Default: all</param>
        /// <param name="addresses">
        ///     <para>(string, optional, default "*") The addresses to retrieve permissions for. "*" for all addresses</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of addresses to return permissions for</para>
        /// </param>
        /// <param name="verbose">If true, returns list of pending grants</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListPermissionsResult>>> ListPermissionsAsync([Optional] string permissions, [Optional] object addresses, [Optional] bool verbose) =>
            ListPermissionsAsync(CliOptions.ChainName, permissions, addresses, verbose);

        /// <summary>
        ///
        /// <para>Returns list of defined stream filters</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="filter_identifers">
        ///     <para> (string, optional, default=*) Filter identifier - one of: create txid, filter reference, filter name.</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of filter identifiers</para>
        /// </param>
        /// <param name="verbose">If true, returns list of creators and approval details</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListStreamFiltersResult>>> ListStreamFiltersAsync(string blockchainName, [Optional] object filter_identifers, [Optional] bool verbose) =>
            filter_identifers switch
            {
                string strType => TransactAsync<IList<ListStreamFiltersResult>>(blockchainName, BlockchainAction.ListStreamFiltersMethod, new[] { strType, $"{verbose}".ToLower() }),
                object objType => TransactAsync<IList<ListStreamFiltersResult>>(blockchainName, BlockchainAction.ListStreamFiltersMethod, new[] { SerializeObject(objType), $"{verbose}".ToLower() })
            };

        /// <summary>
        ///
        /// <para>Returns list of defined stream filters</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="filter_identifers">
        ///     <para> (string, optional, default=*) Filter identifier - one of: create txid, filter reference, filter name.</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of filter identifiers</para>
        /// </param>
        /// <param name="verbose">If true, returns list of creators and approval details</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListStreamFiltersResult>>> ListStreamFiltersAsync([Optional] object filter_identifers, [Optional] bool verbose) =>
            ListStreamFiltersAsync(CliOptions.ChainName, filter_identifers, verbose);

        /// <summary>
        ///
        /// <para>Returns list of defined streams</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifiers">
        ///     <para>(string, optional, default=*, all streams) Stream identifier - one of the following: issue txid, stream reference, stream name</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of stream identifiers</para>
        /// </param>
        /// <param name="verbose">If true, returns stream list of creators</param>
        /// <param name="count">The number of streams to display</param>
        /// <param name="start">Start from specific stream, 0 based, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListStreamsResult>>> ListStreamsAsync(string blockchainName, [Optional] object stream_identifiers, [Optional] bool verbose, [Optional] int count, [Optional] int start) =>
            stream_identifiers switch
            {
                string strType => TransactAsync<IList<ListStreamsResult>>(blockchainName, BlockchainAction.ListStreamsMethod, new[] { strType, $"{verbose}".ToLower(), $"{count}", $"{start}" }),
                object objType => TransactAsync<IList<ListStreamsResult>>(blockchainName, BlockchainAction.ListStreamsMethod, new[] { SerializeObject(objType), $"{verbose}".ToLower(), $"{count}", $"{start}" })
            };

        /// <summary>
        ///
        /// <para>Returns list of defined streams</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="stream_identifiers">
        ///     <para>(string, optional, default=*, all streams) Stream identifier - one of the following: issue txid, stream reference, stream name</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of stream identifiers</para>
        /// </param>
        /// <param name="verbose">If true, returns stream list of creators</param>
        /// <param name="count">The number of streams to display</param>
        /// <param name="start">Start from specific stream, 0 based, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListStreamsResult>>> ListStreamsAsync([Optional] object stream_identifiers, [Optional] bool verbose, [Optional] int count, [Optional] int start) =>
            ListStreamsAsync(CliOptions.ChainName, stream_identifiers, verbose, count, start);

        /// <summary>
        ///
        /// <para>Returns list of defined tx filters</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="filter_identifiers">
        ///     <para>(string, optional, default=*) Filter identifier - one of: create txid, filter reference, filter name</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of filter identifiers</para>
        /// </param>
        /// <param name="verbose">If true, returns list of creators and approval details</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListTxFiltersResult>>> ListTxFiltersAsync(string blockchainName, [Optional] object filter_identifiers, [Optional] bool verbose) =>
            filter_identifiers switch
            {
                string strType => TransactAsync<IList<ListTxFiltersResult>>(blockchainName, BlockchainAction.ListTxFiltersMethod, new[] { strType, $"{verbose}".ToLower() }),
                object objType => TransactAsync<IList<ListTxFiltersResult>>(blockchainName, BlockchainAction.ListTxFiltersMethod, new[] { SerializeObject(objType), $"{verbose}".ToLower() })
            };

        /// <summary>
        ///
        /// <para>Returns list of defined tx filters</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="filter_identifiers">
        ///     <para>(string, optional, default=*) Filter identifier - one of: create txid, filter reference, filter name</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of filter identifiers</para>
        /// </param>
        /// <param name="verbose">If true, returns list of creators and approval details</param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListTxFiltersResult>>> ListTxFiltersAsync([Optional] object filter_identifiers, [Optional] bool verbose) =>
            ListTxFiltersAsync(CliOptions.ChainName, filter_identifiers, verbose);

        /// <summary>
        ///
        /// <para>Returns list of defined upgrades</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="upgrade_identifier">
        ///     <para>(string, optional, default=*, all upgrades) Upgrade identifier - one of the following: upgrade txid, upgrade name.</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of upgrade identifiers</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListUpgradesResult>>> ListUpgradesAsync(string blockchainName, [Optional] string upgrade_identifier) =>
            TransactAsync<IList<ListUpgradesResult>>(blockchainName, BlockchainAction.ListUpgradesMethod, new[] { upgrade_identifier });

        /// <summary>
        ///
        /// <para>Returns list of defined upgrades</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="upgrade_identifiers">
        ///     <para>(string, optional, default=*, all upgrades) Upgrade identifier - one of the following: upgrade txid, upgrade name.</para>
        ///     <para>or</para>
        ///     <para>(array, optional) A json array of upgrade identifiers</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListUpgradesResult>>> ListUpgradesAsync(string blockchainName, [Optional] string[] upgrade_identifiers) => 
            TransactAsync<IList<ListUpgradesResult>>(blockchainName, BlockchainAction.ListUpgradesMethod, new[] { SerializeObject(upgrade_identifiers) });

        /// <summary>
        ///
        /// <para>Returns list of defined upgrades</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="upgrade_identifier">
        ///     <para>(string, optional, default=*, all upgrades) Upgrade identifier - one of the following: upgrade txid, upgrade name.</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListUpgradesResult>>> ListUpgradesAsync([Optional] string upgrade_identifier) =>
            ListUpgradesAsync(CliOptions.ChainName, upgrade_identifier);

        /// <summary>
        ///
        /// <para>Returns list of defined upgrades</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="upgrade_identifiers">
        ///     <para>(array, optional) A json array of upgrade identifiers</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<IList<ListUpgradesResult>>> ListUpgradesAsync([Optional] string[] upgrade_identifiers) =>
            ListUpgradesAsync(CliOptions.ChainName, upgrade_identifiers);

        /// <summary>
        ///
        /// <para>Compile an existing filter and optionally test it on a transaction</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="filter_identifier">One of: create txid, filter reference, filter name</param>
        /// <param name="tx_hex">
        ///     <para>The transaction hex string to filter, otherwise filter compiled only</para>
        ///     <para>or</para>
        ///     <para>The transaction id</para>
        /// </param>
        /// <param name="vout">The output number, if omitted and txid/tx-hex is specified, found automatically</param>
        /// <returns></returns>
        public Task<CliResponse<RunStreamFilterResult>> RunStreamFilterAsync(string blockchainName, string filter_identifier, [Optional] string tx_hex, [Optional] int vout)
        {
            if (string.IsNullOrEmpty(tx_hex))
                return TransactAsync<RunStreamFilterResult>(blockchainName, BlockchainAction.RunStreamFilterMethod, new[] { filter_identifier });
            else
                return TransactAsync<RunStreamFilterResult>(blockchainName, BlockchainAction.RunStreamFilterMethod, new[] { filter_identifier, tx_hex, $"{vout}" });
        }

        /// <summary>
        ///
        /// <para>Compile an existing filter and optionally test it on a transaction</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="filter_identifier">One of: create txid, filter reference, filter name</param>
        /// <param name="tx_hex">
        ///     <para>The transaction hex string to filter, otherwise filter compiled only</para>
        ///     <para>or</para>
        ///     <para>The transaction id</para>
        /// </param>
        /// <param name="vout">The output number, if omitted and txid/tx-hex is specified, found automatically</param>
        /// <returns></returns>
        public Task<CliResponse<RunStreamFilterResult>> RunStreamFilterAsync(string filter_identifier, [Optional] string tx_hex, [Optional] int vout) =>
            RunStreamFilterAsync(CliOptions.ChainName, filter_identifier, tx_hex, vout);

        /// <summary>
        ///
        /// <para>Compile an existing filter and optionally test it on a transaction</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="filter_identifier">One of: create txid, filter reference, filter name</param>
        /// <param name="tx_hex">
        ///     <para>(string, optional) The transaction hex string to filter, otherwise filter compiled only</para>
        ///     <para>or</para>
        ///     <para>(string, optional) The transaction id</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<RunTxFilterResult>> RunTxFilterAsync(string blockchainName, [Optional] string filter_identifier, [Optional] string tx_hex)
        {
            if (string.IsNullOrEmpty(tx_hex))
                return TransactAsync<RunTxFilterResult>(blockchainName, BlockchainAction.RunTxFilterMethod, new[] { filter_identifier });

            return TransactAsync<RunTxFilterResult>(blockchainName, BlockchainAction.RunTxFilterMethod, new[] { filter_identifier, tx_hex });
        }

        /// <summary>
        ///
        /// <para>Compile an existing filter and optionally test it on a transaction</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="filter_identifier">One of: create txid, filter reference, filter name</param>
        /// <param name="tx_hex">
        ///     <para>(string, optional) The transaction hex string to filter, otherwise filter compiled only</para>
        ///     <para>or</para>
        ///     <para>(string, optional) The transaction id</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<RunTxFilterResult>> RunTxFilterAsync([Optional] string filter_identifier, [Optional] string tx_hex) =>
            RunTxFilterAsync(CliOptions.ChainName, filter_identifier, tx_hex);

        /// <summary>
        ///
        /// <para>Compile a test filter and optionally test it on a transaction</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="restrictions">A json object with filter restrictions</param>
        /// <param name="javascript_code">JavaScript filter code, see help filter</param>
        /// <param name="tx_hex">
        ///     <para>(string, optional) The transaction hex string to filter, otherwise filter compiled only</para>
        ///     <para>or</para>
        ///     <para>(string, optional) The transaction id</para>
        /// </param>
        /// <param name="vout">The output number, if omitted and txid/tx-hex is specified, found automatically</param>
        /// <returns></returns>
        public Task<CliResponse<TestStreamFilterResult>> TestStreamFilterAsync(string blockchainName, object restrictions, string javascript_code, [Optional] string tx_hex, [Optional] int vout)
        {
            if (string.IsNullOrEmpty(tx_hex))
                return TransactAsync<TestStreamFilterResult>(blockchainName, BlockchainAction.TestStreamFilterMethod, new[] { SerializeObject(restrictions), javascript_code });
            else
                return TransactAsync<TestStreamFilterResult>(blockchainName, BlockchainAction.TestStreamFilterMethod, new[] { SerializeObject(restrictions), javascript_code, tx_hex, $"{vout}" });
        }

        /// <summary>
        ///
        /// <para>Compile a test filter and optionally test it on a transaction</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="restrictions">A json object with filter restrictions</param>
        /// <param name="javascript_code">JavaScript filter code, see help filter</param>
        /// <param name="tx_hex">
        ///     <para>(string, optional) The transaction hex string to filter, otherwise filter compiled only</para>
        ///     <para>or</para>
        ///     <para>(string, optional) The transaction id</para>
        /// </param>
        /// <param name="vout">The output number, if omitted and txid/tx-hex is specified, found automatically</param>
        /// <returns></returns>
        public Task<CliResponse<TestStreamFilterResult>> TestStreamFilterAsync(object restrictions, string javascript_code, [Optional] string tx_hex, [Optional] int vout) =>
            TestStreamFilterAsync(CliOptions.ChainName, restrictions, javascript_code, tx_hex, vout);

        /// <summary>
        ///
        /// <para>Compile a test filter and optionally test it on a transaction</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="restrictions">
        ///     <para>(object, required)  a json object with filter restrictions</para>
        ///     <para>{</para>
        ///       <para>"for": "entity-identifier"    (string, optional) Asset/stream identifier - one of: create txid, stream reference, stream name.</para>
        ///        <para> or</para>
        ///       <para>"for": entity-identifier(s)   (array, optional) A json array of asset/stream identifiers</para>
        ///     <para>}</para>
        /// </param>
        /// <param name="javascript_code">JavaScript filter code, see help filters</param>
        /// <param name="tx_hex">
        ///     <para> (string, optional) The transaction hex string to filter, otherwise filter compiled only</para>
        ///     <para>or</para>
        ///     <para>(string, optional) The transaction id</para></param>
        public Task<CliResponse<TestTxFilterResult>> TestTxFilterAsync(string blockchainName, object restrictions, string javascript_code, [Optional] string tx_hex)
        {
            if (string.IsNullOrEmpty(tx_hex))
                return TransactAsync<TestTxFilterResult>(blockchainName, BlockchainAction.TestTxFilterMethod, new[] { SerializeObject(restrictions), javascript_code });

            return TransactAsync<TestTxFilterResult>(blockchainName, BlockchainAction.TestTxFilterMethod, new[] { SerializeObject(restrictions), javascript_code, tx_hex });
        }

        /// <summary>
        ///
        /// <para>Compile a test filter and optionally test it on a transaction</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="restrictions">
        ///     <para>(object, required)  a json object with filter restrictions</para>
        ///     <para>{</para>
        ///       <para>"for": "entity-identifier"    (string, optional) Asset/stream identifier - one of: create txid, stream reference, stream name.</para>
        ///        <para> or</para>
        ///       <para>"for": entity-identifier(s)   (array, optional) A json array of asset/stream identifiers</para>
        ///     <para>}</para>
        /// </param>
        /// <param name="javascript_code">JavaScript filter code, see help filters</param>
        /// <param name="tx_hex">
        ///     <para> (string, optional) The transaction hex string to filter, otherwise filter compiled only</para>
        ///     <para>or</para>
        ///     <para>(string, optional) The transaction id</para></param>
        /// <returns></returns>
        public Task<CliResponse<TestTxFilterResult>> TestTxFilterAsync(object restrictions, string javascript_code, [Optional] string tx_hex) =>
            TestTxFilterAsync(CliOptions.ChainName, restrictions, javascript_code, tx_hex);

        /// <summary>
        /// Verify blockchain database
        ///
        /// <para>
        ///     Arguments:
        /// </para>
        /// <para>
        ///     1. checklevel (numeric, optional, 0-4, default=3) How thorough the block verification is.
        /// </para>
        /// <para>
        ///     2. numblocks (numeric, optional, default=288, 0=all) The number of blocks to check.
        /// </para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="check_level">(numeric, optional, 0-4, default=3) How thorough the block verification is</param>
        /// <param name="num_blocks">(numeric, optional, default=288, 0=all) The number of blocks to check</param>
        /// <returns></returns>
        public Task<CliResponse<bool>> VerifyChainAsync(string blockchainName, [Optional] int check_level, [Optional] int num_blocks) =>
            TransactAsync<bool>(blockchainName, BlockchainAction.VerifyChainMethod, new[] { $"{check_level}", $"{num_blocks}" });

        /// <summary>
        /// Verify blockchain database
        ///
        /// <para>
        ///     Arguments:
        /// </para>
        /// <para>
        ///     1. checklevel (numeric, optional, 0-4, default=3) How thorough the block verification is.
        /// </para>
        /// <para>
        ///     2. numblocks (numeric, optional, default=288, 0=all) The number of blocks to check.
        /// </para>
        ///
        /// </summary>
        /// <param name="check_level">(numeric, optional, 0-4, default=3) How thorough the block verification is</param>
        /// <param name="num_blocks">(numeric, optional, default=288, 0=all) The number of blocks to check</param>
        /// <returns></returns>
        public Task<CliResponse<bool>> VerifyChainAsync([Optional] int check_level, [Optional] int num_blocks) =>
            VerifyChainAsync(CliOptions.ChainName, check_level, num_blocks);

        /// <summary>
        /// Verify an addresses permission against a specific <paramref name="blockchainName"/>
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">The address to verify permission for</param>
        /// <param name="permission">Permission string. Possible values: connect,send,receive,issue,mine,admin,activate,create</param>
        /// <returns></returns>
        public Task<CliResponse<bool>> VerifyPermissionAsync(string blockchainName, string address, string permission) =>
            TransactAsync<bool>(blockchainName, BlockchainAction.VerifyPermissionMethod, new[] { address, permission });

        /// <summary>
        /// Verify an addresses permission against a specific blockchain name
        /// </summary>
        /// <param name="address">The address to verify permission for</param>
        /// <param name="permission">Permission string. Possible values: connect,send,receive,issue,mine,admin,activate,create</param>
        /// <returns></returns>
        public Task<CliResponse<bool>> VerifyPermissionAsync(string address, string permission) =>
            VerifyPermissionAsync(CliOptions.ChainName, address, permission);
    }
}