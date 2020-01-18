using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.Data.Models.Raw;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods established by the IMultiChainCliRaw contract</para>
    ///
    /// appendrawchange, appendrawdata, appendrawtransaction,
    /// createrawtransaction, decoderawtransaction, decodescript,
    /// getrawtransaction, sendrawtransaction, signrawtransaction
    /// 
    /// </summary>
    public interface IMultiChainCliRaw : IMultiChainCli
    {
        /// <summary>
        /// 
        /// <para>Appends change output to raw transaction, containing any remaining assets / native currency in the inputs that are not already sent to other outputs.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="tx_hex">The hex string of the raw transaction)</param>
        /// <param name="address">The address to send the change to</param>
        /// <param name="native_fee">Native currency value deducted from that amount so it becomes a transaction fee. Default - calculated automatically</param>
        /// <returns></returns>
        Task<CliResponse<string>> AppendRawChangeAsync(string tx_hex, string address, [Optional] double native_fee);

        /// <summary>
        /// 
        /// <para>Appends change output to raw transaction, containing any remaining assets / native currency in the inputs that are not already sent to other outputs.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">The hex string of the raw transaction)</param>
        /// <param name="address">The address to send the change to</param>
        /// <param name="native_fee">Native currency value deducted from that amount so it becomes a transaction fee. Default - calculated automatically</param>
        /// <returns></returns>
        Task<CliResponse<string>> AppendRawChangeAsync(string blockchainName, string tx_hex, string address, [Optional] double native_fee);

        /// <summary>
        /// 
        /// <para>Appends new OP_RETURN output to existing raw transaction</para>
        /// <para>Returns hex-encoded raw transaction.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <param name="data">Data, see help data-all for details</param>
        /// <returns></returns>
        Task<CliResponse<string>> AppendRawDataAsync(string tx_hex, object data);

        /// <summary>
        /// 
        /// <para>Appends new OP_RETURN output to existing raw transaction</para>
        /// <para>Returns hex-encoded raw transaction.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <param name="data">Data, see help data-all for details</param>
        /// <returns></returns>
        Task<CliResponse<string>> AppendRawDataAsync(string blockchainName, string tx_hex, object data);

        // todo need to do more testing to get specific data model

        /// <summary>
        /// 
        /// <para>Append inputs and outputs to raw transaction</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="tx_hex">Source transaction hex string</param>
        /// <param name="transactions">A json array of json objects</param>
        /// <param name="addresses">Object with addresses as keys, see help addresses-all for details</param>
        /// <param name="data">Array of hexadecimal strings or data objects, see help data-all for details</param>
        /// <param name="action">Additional actions: "lock", "sign", "lock,sign", "sign,lock", "send"</param>
        Task<CliResponse> AppendRawTransactionAsync(string tx_hex, object[] transactions, object addresses, [Optional] object[] data, string action = "");

        /// <summary>
        /// 
        /// <para>Append inputs and outputs to raw transaction</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">Source transaction hex string</param>
        /// <param name="transactions">A json array of json objects</param>
        /// <param name="addresses">Object with addresses as keys, see help addresses-all for details</param>
        /// <param name="data">Array of hexadecimal strings or data objects, see help data-all for details</param>
        /// <param name="action">Additional actions: "lock", "sign", "lock,sign", "sign,lock", "send"</param>
        /// <returns></returns>
        Task<CliResponse> AppendRawTransactionAsync(string blockchainName, string tx_hex, object[] transactions, object addresses, [Optional] object[] data, string action = "");

        /// <summary>
        /// 
        /// <para>Create a transaction spending the given inputs.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="transactions">A json array of json objects</param>
        /// <param name="addresses">Object with addresses as keys, see help addresses-all for details</param>
        /// <param name="data">Array of hexadecimal strings or data objects, see help data-all for details</param>
        /// <param name="action">Additional actions: "lock", "sign", "lock,sign", "sign,lock", "send"</param>
        Task<CliResponse> CreateRawTransactionAsync(object[] transactions, object addresses, [Optional] object[] data, string action = "");

        /// <summary>
        /// 
        /// <para>Create a transaction spending the given inputs.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="transactions">A json array of json objects</param>
        /// <param name="addresses">Object with addresses as keys, see help addresses-all for details</param>
        /// <param name="data">Array of hexadecimal strings or data objects, see help data-all for details</param>
        /// <param name="action">Additional actions: "lock", "sign", "lock,sign", "sign,lock", "send"</param>
        /// <returns></returns>
        Task<CliResponse> CreateRawTransactionAsync(string blockchainName, object[] transactions, object addresses, [Optional] object[] data, string action = "");

        // todo need to do more testing to get specific data model

        /// <summary>
        /// 
        /// <para>Return a JSON object representing the serialized, hex-encoded transaction.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <returns></returns>
        Task<CliResponse<DecodeRawTransactionResult>> DecodeRawTransactionAsync(string tx_hex);

        /// <summary>
        /// 
        /// <para>Return a JSON object representing the serialized, hex-encoded transaction.</para>
        /// 
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <returns></returns>
        Task<CliResponse<DecodeRawTransactionResult>> DecodeRawTransactionAsync(string blockchainName, string tx_hex);

        /// <summary>
        /// 
        /// <para>Decode a hex-encoded script.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="script_hex">The hex encoded script</param>
        /// <returns></returns>
        Task<CliResponse<DecodeScriptResult>> DecodeScriptAsync(string script_hex);

        /// <summary>
        /// 
        /// <para>Decode a hex-encoded script.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="script_hex">The hex encoded script</param>
        /// <returns></returns>
        Task<CliResponse<DecodeScriptResult>> DecodeScriptAsync(string blockchainName, string script_hex);

        // todo need to do more testing to get specific data model

        /// <summary>
        /// 
        /// <para>
        ///     NOTE: By default this function only works sometimes. This is when the tx is in the mempool or there is an unspent 
        ///     output in the utxo for this transaction. To make it always work, you need to maintain a transaction index, using the -txindex command line option.
        /// </para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="txid">The transaction id</param>
        /// <param name="verbose">If 0, return a string, other return a json object</param>
        /// <returns></returns>
        Task<CliResponse> GetRawTransactionAsync(string txid, [Optional] bool verbose);

        /// <summary>
        /// 
        /// <para>
        ///     NOTE: By default this function only works sometimes. This is when the tx is in the mempool or there is an unspent 
        ///     output in the utxo for this transaction. To make it always work, you need to maintain a transaction index, using the -txindex command line option.
        /// </para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="verbose">If 0, return a string, other return a json object</param>
        /// <returns></returns>
        Task<CliResponse> GetRawTransactionAsync(string blockchainName, string txid, [Optional] bool verbose);

        // todo need to do more testing to get specific data model

        /// <summary>
        /// 
        /// <para>Submits raw transaction (serialized, hex-encoded) to local node and network.</para>
        /// <para>Also see createrawtransaction and signrawtransaction calls.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="tx_hex">The hex string of the raw transaction)</param>
        /// <param name="allow_high_fees">Allow high fees</param>
        Task<CliResponse<string>> SendRawTransactionAsync(string tx_hex, bool allow_high_fees = false);

        /// <summary>
        /// 
        /// <para>Submits raw transaction (serialized, hex-encoded) to local node and network.</para>
        /// <para>Also see createrawtransaction and signrawtransaction calls.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">The hex string of the raw transaction)</param>
        /// <param name="allow_high_fees">Allow high fees</param>
        /// <returns></returns>
        Task<CliResponse<string>> SendRawTransactionAsync(string blockchainName, string tx_hex, bool allow_high_fees = false);

        /// <summary>
        /// 
        /// <para>Sign inputs for raw transaction (serialized, hex-encoded).</para>
        /// <para>
        ///     The second optional argument (may be null) is an array of previous transaction outputs that
        ///     this transaction depends on but may not yet be in the block chain.
        /// </para>
        /// 
        /// <para>
        ///      The third optional argument (may be null) is an array of base58-encoded private
        ///     keys that, if given, will be the only keys used to sign the transaction.
        /// </para>
        /// 
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <param name="prevtxs">An json array of previous dependent transaction outputs</param>
        /// <param name="privatekeys">A json array of base58-encoded private keys for signing</param>
        /// <param name="sighashtype">The signature hash type. Must be one of: "All", "NONE", "SINGLE", "ALL|ANYONECANPAY", "NONE|ANYONECANPAY", "SINGLE|ANYONECANPAY"</param>
        /// <returns></returns>
        Task<CliResponse<SignRawTransactionResult>> SignRawTransactionAsync(string tx_hex, [Optional] object[] prevtxs, [Optional] object[] privatekeys, [Optional] string sighashtype);

        /// <summary>
        /// 
        /// <para>Sign inputs for raw transaction (serialized, hex-encoded).</para>
        /// <para>
        ///     The second optional argument (may be null) is an array of previous transaction outputs that
        ///     this transaction depends on but may not yet be in the block chain.
        /// </para>
        /// 
        /// <para>
        ///      The third optional argument (may be null) is an array of base58-encoded private
        ///     keys that, if given, will be the only keys used to sign the transaction.
        /// </para>
        /// 
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <param name="prevtxs">An json array of previous dependent transaction outputs</param>
        /// <param name="privatekeys">A json array of base58-encoded private keys for signing</param>
        /// <param name="sighashtype">The signature hash type. Must be one of: "All", "NONE", "SINGLE", "ALL|ANYONECANPAY", "NONE|ANYONECANPAY", "SINGLE|ANYONECANPAY"</param>
        /// <returns></returns>
        Task<CliResponse<SignRawTransactionResult>> SignRawTransactionAsync(string blockchainName, string tx_hex, [Optional] object[] prevtxs, [Optional] object[] privatekeys, [Optional] string sighashtype);
    }
}