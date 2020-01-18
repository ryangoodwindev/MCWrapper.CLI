using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Options;
using MCWrapper.Data.Models.Raw;
using MCWrapper.Ledger.Actions;
using Microsoft.Extensions.Options;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using static Newtonsoft.Json.JsonConvert;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods implemented by the MultiChainCliRawClient concrete class</para>
    ///
    /// appendrawchange, appendrawdata, appendrawtransaction,
    /// createrawtransaction, decoderawtransaction, decodescript,
    /// getrawtransaction, sendrawtransaction, signrawtransaction
    /// 
    /// </summary>
    public class MultiChainCliRawClient : MultiChainCliClient, IMultiChainCliRaw
    {
        /// <summary>
        /// Create a new MultiChainCliRawClient instance 
        /// </summary>
        /// <param name="options"></param>
        public MultiChainCliRawClient(IOptions<CliOptions> options)
            : base(options) { }

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
        public Task<CliResponse<string>> AppendRawChangeAsync(string blockchainName, string tx_hex, string address, [Optional] double native_fee) =>
            TransactAsync<string>(blockchainName, RawAction.AppendRawChangeMethod, new[] { tx_hex, address, $"{native_fee}" });

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
        public Task<CliResponse<string>> AppendRawChangeAsync(string tx_hex, string address, [Optional] double native_fee) =>
            AppendRawChangeAsync(CliOptions.ChainName, tx_hex, address, native_fee);

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
        public Task<CliResponse<string>> AppendRawDataAsync(string blockchainName, string tx_hex, object data) =>
            TransactAsync<string>(blockchainName, RawAction.AppendRawDataMethod, new[] { tx_hex, SerializeObject(data) });

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
        public Task<CliResponse<string>> AppendRawDataAsync(string tx_hex, object data) =>
            AppendRawDataAsync(CliOptions.ChainName, tx_hex, data);

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
        public Task<CliResponse> AppendRawTransactionAsync(string blockchainName, string tx_hex, object[] transactions, object addresses, [Optional] object[] data, string action = "") =>
            TransactAsync(blockchainName, RawAction.AppendRawTransactionMethod, new[] { tx_hex, SerializeObject(transactions), SerializeObject(addresses), SerializeObject(data), action });

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
        public Task<CliResponse> AppendRawTransactionAsync(string tx_hex, object[] transactions, object addresses, [Optional] object[] data, string action = "") =>
            AppendRawTransactionAsync(CliOptions.ChainName, tx_hex, transactions, addresses, data, action);

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
        public Task<CliResponse> CreateRawTransactionAsync(string blockchainName, object[] transactions, object addresses, [Optional] object[] data, string action = "")
        {
            action ??= string.Empty;
            data ??= Array.Empty<object>();

            return TransactAsync(blockchainName, RawAction.CreateRawTransactionMethod, new[] { SerializeObject(transactions), SerializeObject(addresses), SerializeObject(data), action });
        }

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
        public Task<CliResponse> CreateRawTransactionAsync(object[] transactions, object addresses, [Optional] object[] data, string action = "") =>
            CreateRawTransactionAsync(CliOptions.ChainName, transactions, addresses, data, action);

        /// <summary>
        /// 
        /// <para>Return a JSON object representing the serialized, hex-encoded transaction.</para>
        /// 
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <returns></returns>
        public Task<CliResponse<DecodeRawTransactionResult>> DecodeRawTransactionAsync(string blockchainName, string tx_hex) =>
            TransactAsync<DecodeRawTransactionResult>(blockchainName, RawAction.DecodeRawTransactionMethod, new[] { tx_hex });

        /// <summary>
        /// 
        /// <para>Return a JSON object representing the serialized, hex-encoded transaction.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <returns></returns>
        public Task<CliResponse<DecodeRawTransactionResult>> DecodeRawTransactionAsync(string tx_hex) =>
            DecodeRawTransactionAsync(CliOptions.ChainName, tx_hex);

        /// <summary>
        /// 
        /// <para>Decode a hex-encoded script.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="script_hex">The hex encoded script</param>
        /// <returns></returns>
        public Task<CliResponse<DecodeScriptResult>> DecodeScriptAsync(string blockchainName, string script_hex) =>
            TransactAsync<DecodeScriptResult>(blockchainName, RawAction.DecodeScriptMethod, new[] { script_hex });

        /// <summary>
        /// 
        /// <para>Decode a hex-encoded script.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="script_hex">The hex encoded script</param>
        /// <returns></returns>
        public Task<CliResponse<DecodeScriptResult>> DecodeScriptAsync(string script_hex) =>
            DecodeScriptAsync(CliOptions.ChainName, script_hex);

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
        public Task<CliResponse> GetRawTransactionAsync(string blockchainName, string txid, [Optional] bool verbose) =>
            TransactAsync(blockchainName, RawAction.GetRawTransactionMethod, new[] { txid, $"{verbose}".ToLower() });

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
        public Task<CliResponse> GetRawTransactionAsync(string txid, [Optional] bool verbose) =>
            GetRawTransactionAsync(CliOptions.ChainName, txid, verbose);

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
        public Task<CliResponse<string>> SendRawTransactionAsync(string blockchainName, string tx_hex, bool allow_high_fees = false) =>
            TransactAsync<string>(blockchainName, RawAction.SendRawTransactionMethod, new[] { tx_hex, $"{allow_high_fees}".ToLower() });

        /// <summary>
        /// 
        /// <para>Submits raw transaction (serialized, hex-encoded) to local node and network.</para>
        /// <para>Also see createrawtransaction and signrawtransaction calls.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="tx_hex">The hex string of the raw transaction)</param>
        /// <param name="allow_high_fees">Allow high fees</param>
        public Task<CliResponse<string>> SendRawTransactionAsync(string tx_hex, bool allow_high_fees = false) =>
            SendRawTransactionAsync(CliOptions.ChainName, tx_hex, allow_high_fees);

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
        public Task<CliResponse<SignRawTransactionResult>> SignRawTransactionAsync(string blockchainName, string tx_hex, [Optional] object[] prevtxs, [Optional] object[] privatekeys, [Optional] string sighashtype)
        {
            prevtxs ??= Array.Empty<object>();
            privatekeys ??= Array.Empty<object>();
            sighashtype ??= string.Empty;

            if (prevtxs == Array.Empty<object>() && privatekeys == Array.Empty<object>())
                return TransactAsync<SignRawTransactionResult>(blockchainName, RawAction.SignRawTransactionMethod, new[] { tx_hex });

            if (string.IsNullOrEmpty(sighashtype))
                return TransactAsync<SignRawTransactionResult>(blockchainName, RawAction.SignRawTransactionMethod, new[] { tx_hex, SerializeObject(prevtxs), SerializeObject(privatekeys) });

            return TransactAsync<SignRawTransactionResult>(blockchainName, RawAction.SignRawTransactionMethod, new[] { tx_hex, SerializeObject(prevtxs), SerializeObject(privatekeys), sighashtype });
        }

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
        public Task<CliResponse<SignRawTransactionResult>> SignRawTransactionAsync(string tx_hex, [Optional] object[] prevtxs, [Optional] object[] privatekeys, [Optional] string sighashtype) =>
            SignRawTransactionAsync(CliOptions.ChainName, tx_hex, prevtxs, privatekeys, sighashtype);
    }
}