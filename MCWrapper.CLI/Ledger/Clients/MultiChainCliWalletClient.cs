using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Options;
using MCWrapper.Data.Models.Wallet;
using MCWrapper.Ledger.Actions;
using MCWrapper.Ledger.Entities;
using MCWrapper.Ledger.Entities.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    ///
    /// <para>MutliChain Core methods implemented by the MultiChainCliWalletClient concrete class</para>
    ///
    /// addmultisigaddress, appendrawexchange, approvefrom, backupwallet,
    /// combineunspent, completerawexchange, create, createfrom,
    /// createrawexchange, createrawsendfrom, decoderawexchange,
    /// disablerawtransaction, dumpprivkey, dumpwallet, encryptwallet,
    /// getaccount, getaccountaddress, getaddressbalances, getaddresses,
    /// getaddressesbyaccount, getaddresstransaction, getassetbalances,
    /// getassettransaction, getbalance, getmultibalances, getnewaddress,
    /// getrawchangeaddress, getreceivedbyaccount, getreceivedbyaddress,
    /// getstreamitem, getstreamkeysummary, getstreampublishersummary,
    /// gettotalbalances, gettransaction, gettxoutdata, getunconfirmedbalance,
    /// getwalletinfo, getwallettransaction, grant, grantfrom, grantwithdata,
    /// grantwithdatafrom, importaddress, importprivkey, importwallet, issue,
    /// issuefrom, issuemore, issuemorefrom, keypoolrefill, listaccounts,
    /// listaddresses, listaddressgroupings, listaddresstransactions,
    /// listassettransactions, listlockunspent, listreceivedbyaccount,
    /// listreceivedbyaddress, listsinceblock, liststreamblockitems,
    /// liststreamitems, liststreamkeyitems, liststreamkeys, liststreampublisheritems,
    /// liststreampublishers, liststreamqueryitems, liststreamtxitems,
    /// listtransactions, listunspent, listwallettransactions, lockunspent,
    /// move, preparelockunspent, preparelockunspentfrom, publish, publishfrom,
    /// publishmulti, publishmultifrom, purgepublisheditems, purgestreamitems,
    /// resendwallettransactions, retrievestreamitems, revoke, revokefrom, send,
    /// sendasset, sendassetfrom, sendfrom, sendfromaccount, sendmany, sendwithdata,
    /// sendwithdatafrom, setaccount, settxfee, signmessage, subscribe, trimsubscribe,
    /// txouttobinarycache, unsubscribe, walletlock, walletpassphrase,
    /// walletpassphrasechange,
    ///
    /// </summary>
    public class MultiChainCliWalletClient : MultiChainCliClient, IMultiChainCliWallet
    {
        /// <summary>
        /// Create a new Wallet CLI client
        /// </summary>
        /// <param name="options"></param>
        public MultiChainCliWalletClient(IOptions<CliOptions> options)
            : base(options) { }

        /// <summary>
        /// 
        /// <para>Add a nrequired-to-sign multisignature address to the wallet.</para>>
        /// <para>Each key is a address or hex-encoded public key.</para>
        /// <para> If 'account' is specified, assign address to that account.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="n_required">The number of required signatures out of the n keys or addresses</param>
        /// <param name="keys">A json array of addresses or hex-encoded public keys</param>
        /// <param name="account">An account to assign the addresses to. Accounts are not supported with the current version of MultiChain Core.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> AddMultiSigAddressAsync(string blockchainName, int n_required, string[] keys, [Optional] string account)
        {
            if (string.IsNullOrEmpty(account))
                return TransactAsync<object>(blockchainName, WalletAction.AddMultiSigAddressMethod, new[] { $"{n_required}", keys.Serialize() });
            else
                return TransactAsync<object>(blockchainName, WalletAction.AddMultiSigAddressMethod, new[] { $"{n_required}", keys.Serialize(), account });
        }

        /// <summary>
        /// 
        /// <para>Add a nrequired-to-sign multisignature address to the wallet.</para>>
        /// <para>Each key is a address or hex-encoded public key.</para>
        /// <para> If 'account' is specified, assign address to that account.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="n_required">The number of required signatures out of the n keys or addresses</param>
        /// <param name="keys">A json array of addresses or hex-encoded public keys</param>
        /// <param name="account">An account to assign the addresses to. Accounts are not supported with the current version of MultiChain Core.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> AddMultiSigAddressAsync(int n_required, string[] keys, [Optional] string account) =>
            AddMultiSigAddressAsync(CliOptions.ChainName, n_required, keys, account);

        /// <summary>
        /// 
        /// <para>Adds to the raw atomic exchange transaction in tx-hex given by a previous call to createrawexchange or appendrawexchange.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="hex">The transaction hex string</param>
        /// <param name="txid">Transaction ID of the output prepared by preparelockunspent</param>
        /// <param name="vout">Output index</param>
        /// <param name="ask_assets">
        ///     A json object of assets to ask
        ///     <para>{ "asset-identifier" : asset-quantity, ...  }</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<AppendRawExchangeResult>> AppendRawExchangeAsync(string blockchainName, string hex, string txid, int vout, object ask_assets) =>
            TransactAsync<AppendRawExchangeResult>(blockchainName, WalletAction.AppendRawExchangeMethod, new[] { hex, txid, $"{vout}", ask_assets.Serialize() });

        /// <summary>
        /// 
        /// <para>Adds to the raw atomic exchange transaction in tx-hex given by a previous call to createrawexchange or appendrawexchange.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="hex">The transaction hex string</param>
        /// <param name="txid">Transaction ID of the output prepared by preparelockunspent</param>
        /// <param name="vout">Output index</param>
        /// <param name="ask_assets">
        ///     A json object of assets to ask
        ///     <para>{ "asset-identifier" : asset-quantity, ...  }</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<AppendRawExchangeResult>> AppendRawExchangeAsync(string hex, string txid, int vout, object ask_assets) =>
            AppendRawExchangeAsync(CliOptions.ChainName, hex, txid, vout, ask_assets);

        /// <summary>
        /// 
        /// <para>Approve upgrade, tx filter, or stream filter using specific address.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for approval</param>
        /// <param name="entityIdentifier">
        ///     <para>Upgrade identifier - one of: create txid, upgrade name.</para>
        ///     <para>or</para>
        ///     <para>Tx Filter identifier - one of: create txid, filter reference, filter name.</para>
        ///     <para>or</para>
        ///     <para>Stream Filter identifier - one of: create txid, filter reference, filter name.</para>
        /// </param>
        /// <param name="approve">
        ///     <para>(boolean, required)  Approve or disapprove</para>
        ///     <para>or</para>
        ///     <para>(object, required)  Approve or disapprove</para>
        ///     <para>{ "approve" : approve  (boolean, required) Approve or disapprove "for" : "stream-identifier"   (string, required)  Stream identifier - one of: create txid, stream reference, stream name. }</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> ApproveFromAsync(string blockchainName, string fromAddress, string entityIdentifier, object approve) =>
            TransactAsync<object>(blockchainName, WalletAction.ApproveFromMethod, new[] { fromAddress, entityIdentifier, approve.Serialize() });

        /// <summary>
        /// 
        /// <para>Approve upgrade, tx filter, or stream filter using specific address.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="fromAddress">Address used for approval</param>
        /// <param name="entityIdentifier">
        ///     <para>Upgrade identifier - one of: create txid, upgrade name.</para>
        ///     <para>or</para>
        ///     <para>Tx Filter identifier - one of: create txid, filter reference, filter name.</para>
        ///     <para>or</para>
        ///     <para>Stream Filter identifier - one of: create txid, filter reference, filter name.</para>
        /// </param>
        /// <param name="approve">
        ///     <para>(boolean, required)  Approve or disapprove</para>
        ///     <para>or</para>
        ///     <para>(object, required)  Approve or disapprove</para>
        ///     <para>{ "approve" : approve  (boolean, required) Approve or disapprove "for" : "stream-identifier"   (string, required)  Stream identifier - one of: create txid, stream reference, stream name. }</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> ApproveFromAsync(string fromAddress, string entityIdentifier, object approve) =>
            ApproveFromAsync(CliOptions.ChainName, fromAddress, entityIdentifier, approve);

        /// <summary>
        /// 
        /// <para>Safely copies wallet.dat to destination, which can be a directory or a path with filename.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="destination">The destination directory or file</param>
        /// <returns></returns>
        public Task<CliResponse<object>> BackupWalletAsync(string blockchainName, string destination) =>
            TransactAsync<object>(blockchainName, WalletAction.BackupWalletMethod, new[] { destination });

        /// <summary>
        /// 
        /// <para>Safely copies wallet.dat to destination, which can be a directory or a path with filename.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="destination">The destination directory or file</param>
        /// <returns></returns>
        public Task<CliResponse<object>> BackupWalletAsync(string destination) =>
            BackupWalletAsync(CliOptions.ChainName, destination);

        /// <summary>
        /// 
        /// <para>Optimizes wallet performance by combining unspent txouts.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="addresses">Addresses to optimize (comma delimited). Default - "*", all</param>
        /// <param name="min_conf">The minimum confirmations to filter. Default - 1</param>
        /// <param name="max_combines">Maximal number of transactions to send. Default - 100</param>
        /// <param name="min_inputs">Minimal number of txouts to combine in one transaction. Default - 2</param>
        /// <param name="max_inputs">Maximal number of txouts to combine in one transaction. Default - 100</param>
        /// <param name="max_time">Maximal time for creating combining transactions, at least one transaction will be sent. Default - 15s</param>
        /// <returns></returns>
        public Task<CliResponse<object>> CombineUnspentAsync(string blockchainName, [Optional] string addresses,
                                                                   [Optional] int min_conf, [Optional] int max_combines, [Optional] int min_inputs,
                                                                   [Optional] int max_inputs, [Optional] int max_time) =>
            TransactAsync<object>(blockchainName, WalletAction.CombineUnspentMethod, new[] { addresses, $"{min_conf}", $"{max_combines}", $"{min_inputs}", $"{max_inputs}", $"{max_time}" });

        /// <summary>
        /// 
        /// <para>Optimizes wallet performance by combining unspent txouts.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="addresses">Addresses to optimize (comma delimited). Default - "*", all</param>
        /// <param name="min_conf">The minimum confirmations to filter. Default - 1</param>
        /// <param name="max_combines">Maximal number of transactions to send. Default - 100</param>
        /// <param name="min_inputs">Minimal number of txouts to combine in one transaction. Default - 2</param>
        /// <param name="max_inputs">Maximal number of txouts to combine in one transaction. Default - 100</param>
        /// <param name="max_time">Maximal time for creating combining transactions, at least one transaction will be sent. Default - 15s</param>
        /// <returns></returns>
        public Task<CliResponse<object>> CombineUnspentAsync([Optional] string addresses,
                                                                   [Optional] int min_conf, [Optional] int max_combines, [Optional] int min_inputs,
                                                                   [Optional] int max_inputs, [Optional] int max_time) =>
            CombineUnspentAsync(CliOptions.ChainName, addresses, min_conf, max_combines, min_inputs, max_inputs, max_time);

        /// <summary>
        /// 
        /// <para>Completes existing exchange transaction, adds fee if needed</para>
        /// <para>Returns hex-encoded raw transaction.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="hex">The transaction hex string</param>
        /// <param name="txid">Transaction ID of the output prepared by preparelockunspent</param>
        /// <param name="vout">Output index</param>
        /// <param name="ask_assets">A json object of assets to ask; { "asset-identifier" : asset-quantity, ... }</param>
        /// <param name="data">Data, see help data-with for details</param>
        /// <returns></returns>
        public Task<CliResponse<object>> CompleteRawExchangeAsync(string blockchainName, string hex, string txid, int vout, object ask_assets, [Optional] object data) => data switch
        {
            string s => TransactAsync<object>(blockchainName, WalletAction.CompleteRawExchangeMethod, new[] { hex, txid, $"{vout}", ask_assets.Serialize(), s }),

            object o => TransactAsync<object>(blockchainName, WalletAction.CompleteRawExchangeMethod, new[] { hex, txid, $"{vout}", ask_assets.Serialize(), o.Serialize() }),
        };

        /// <summary>
        /// 
        /// <para>Completes existing exchange transaction, adds fee if needed</para>
        /// <para>Returns hex-encoded raw transaction.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="hex">The transaction hex string</param>
        /// <param name="txid">Transaction ID of the output prepared by preparelockunspent</param>
        /// <param name="vout">Output index</param>
        /// <param name="ask_assets">A json object of assets to ask; { "asset-identifier" : asset-quantity, ... }</param>
        /// <param name="data">Data, see help data-with for details</param>
        /// <returns></returns>
        public Task<CliResponse<object>> CompleteRawExchangeAsync(string hex, string txid, int vout, object ask_assets, [Optional] object data) =>
            CompleteRawExchangeAsync(CliOptions.ChainName, hex, txid, vout, ask_assets, data);

        /// <summary>
        /// 
        /// <para>Creates stream or upgrade</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="entity_type">One of stream, upgrade, tx filter, stream filter</param>
        /// <param name="entity_name">The unique name of the stream, upgrade, tx filter, stream filter</param>
        /// <param name="restrictions_or_open">A json object with optional stream, upgrade, or filter restrictions</param>
        /// <param name="custom_fields">Custom fields objects or JavaScript code string</param>
        /// <returns></returns>
        public Task<CliResponse<string>> CreateAsync(string blockchainName, string entity_type, string entity_name, object restrictions_or_open, [Optional] object custom_fields)
        {
            string restrictOrOpen;

            if (restrictions_or_open.GetType() == typeof(bool))
                restrictOrOpen = restrictions_or_open.ToString()?.ToLower() ?? string.Empty;
            else
                restrictOrOpen = restrictions_or_open.Serialize();

            // must triple escape JavaScript string
            if (custom_fields.GetType() == typeof(string))
            {
                var js = custom_fields.ToString() ?? string.Empty;
                return TransactAsync<string>(blockchainName, WalletAction.CreateMethod, new string[] { entity_type, entity_name, restrictOrOpen, js });
            }
            else
                return TransactAsync<string>(blockchainName, WalletAction.CreateMethod, new string[] { entity_type, entity_name, restrictOrOpen, custom_fields.Serialize() });

        }

        /// <summary>
        /// 
        /// <para>Creates stream or upgrade</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="entity_type">One of stream, upgrade, tx filter, stream filter</param>
        /// <param name="entity_name">The unique name of the stream, upgrade, tx filter, stream filter</param>
        /// <param name="restrictions_or_open">A json object with optional stream, upgrade, or filter restrictions</param>
        /// <param name="custom_fields">Custom fields objects or JavaScript code string</param>
        /// <returns></returns>
        public Task<CliResponse<string>> CreateAsync(string entity_type, string entity_name, object restrictions_or_open, [Optional] object custom_fields) =>
            CreateAsync(CliOptions.ChainName, entity_type, entity_name, restrictions_or_open, custom_fields);

        /// <summary>
        /// 
        /// <para>Creates stream or upgrade using specific address</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address used for creating</param>
        /// <param name="entity_type">One of stream, upgrade, tx filter, stream filter</param>
        /// <param name="entity_name">The unique name of the stream, upgrade, tx filter, stream filter</param>
        /// <param name="restrictions_or_open">A json object with optional stream, upgrade, or filter restrictions</param>
        /// <param name="custom_fields">Custom fields objects or JavaScript code string</param>
        /// <returns></returns>
        public Task<CliResponse<string>> CreateFromAsync(string blockchainName, string from_address, string entity_type, string entity_name, object restrictions_or_open, [Optional] object custom_fields) => custom_fields switch
        {
            string s => TransactAsync<string>(blockchainName, WalletAction.CreateFromMethod, new string[] { from_address, entity_type, entity_name, restrictions_or_open.Serialize(), s }),

            object o => TransactAsync<string>(blockchainName, WalletAction.CreateFromMethod, new string[] { from_address, entity_type, entity_name, restrictions_or_open.Serialize(), o.Serialize() }),
        };

        /// <summary>
        /// 
        /// <para>Creates stream or upgrade using specific address</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="from_address">Address used for creating</param>
        /// <param name="entity_type">One of stream, upgrade, tx filter, stream filter</param>
        /// <param name="entity_name">The unique name of the stream, upgrade, tx filter, stream filter</param>
        /// <param name="restrictions_or_open">A json object with optional stream, upgrade, or filter restrictions</param>
        /// <param name="custom_fields">Custom fields objects or JavaScript code string</param>
        /// <returns></returns>
        public Task<CliResponse<string>> CreateFromAsync(string from_address, string entity_type, string entity_name, object restrictions_or_open, [Optional] object custom_fields) =>
            CreateFromAsync(CliOptions.ChainName, from_address, entity_type, entity_name, restrictions_or_open, custom_fields);

        /// <summary>
        /// 
        /// <para>Creates new exchange transaction</para>
        /// <para>Note that the transaction should be completed by appendrawexchange</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="txid">Transaction ID of the output prepared by preparelockunspent</param>
        /// <param name="vout">Output index</param>
        /// <param name="ask_assets">A json object of assets to ask</param>
        /// <returns></returns>
        public Task<CliResponse<string>> CreateRawExchangeAsync(string blockchainName, string txid, int vout, object ask_assets) =>
            TransactAsync<string>(blockchainName, WalletAction.CreateRawExchangeMethod, new[] { txid, $"{vout}", ask_assets.Serialize() });

        /// <summary>
        /// 
        /// <para>Creates new exchange transaction</para>
        /// <para>Note that the transaction should be completed by appendrawexchange</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="txid">Transaction ID of the output prepared by preparelockunspent</param>
        /// <param name="vout">Output index</param>
        /// <param name="ask_assets">A json object of assets to ask</param>
        /// <returns></returns>
        public Task<CliResponse<string>> CreateRawExchangeAsync(string txid, int vout, object ask_assets) =>
            CreateRawExchangeAsync(CliOptions.ChainName, txid, vout, ask_assets);

        /// <summary>
        /// 
        /// <para>Create a transaction using the given sending address.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address to send from</param>
        /// <param name="addresses">Object with addresses as keys, see help addresses-all for details</param>
        /// <param name="data">Array of hexadecimal strings or data objects, see help data-all for details</param>
        /// <param name="action">Default is "". Additional actions: "lock", "sign", "lock,sign", "sign,lock", "send"</param>
        /// <returns></returns>
        public Task<CliResponse<object>> CreateRawSendFromAsync(string blockchainName, string from_address, object addresses, [Optional] object[] data, [Optional] string action)
        {
            if (data == null && string.IsNullOrEmpty(action))
                return TransactAsync<object>(blockchainName, WalletAction.CreateRawSendFromMethod, new[] { from_address, addresses.Serialize() });
            else if (string.IsNullOrEmpty(action) && data != null)
                return TransactAsync<object>(blockchainName, WalletAction.CreateRawSendFromMethod, new[] { from_address, addresses.Serialize(), data.Serialize() });

            data ??= Array.Empty<object>();

            return TransactAsync<object>(blockchainName, WalletAction.CreateRawSendFromMethod, new[] { from_address, addresses.Serialize(), data.Serialize(), action });
        }

        /// <summary>
        /// 
        /// <para>Create a transaction using the given sending address.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="from_address">Address to send from</param>
        /// <param name="addresses">Object with addresses as keys, see help addresses-all for details</param>
        /// <param name="data">Array of hexadecimal strings or data objects, see help data-all for details</param>
        /// <param name="action">Default is "". Additional actions: "lock", "sign", "lock,sign", "sign,lock", "send"</param>
        /// <returns></returns>
        public Task<CliResponse<object>> CreateRawSendFromAsync(string from_address, object addresses, [Optional] object[] data, [Optional] string action) =>
            CreateRawSendFromAsync(CliOptions.ChainName, from_address, addresses, data, action);

        /// <summary>
        /// 
        /// <para>Return a JSON object representing the serialized, hex-encoded exchange transaction.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">The exchange transaction hex string</param>
        /// <param name="verbose">If true, returns array of all exchanges created by createrawexchange or appendrawexchange</param>
        /// <returns></returns>
        public Task<CliResponse<DecodeRawExchangeResult>> DecodeRawExchangeAsync(string blockchainName, string tx_hex, bool verbose) =>
            TransactAsync<DecodeRawExchangeResult>(blockchainName, WalletAction.DecodeRawExchangeMethod, new[] { tx_hex, $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// <para>Return a JSON object representing the serialized, hex-encoded exchange transaction.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="tx_hex">The exchange transaction hex string</param>
        /// <param name="verbose">If true, returns array of all exchanges created by createrawexchange or appendrawexchange</param>
        /// <returns></returns>
        public Task<CliResponse<DecodeRawExchangeResult>> DecodeRawExchangeAsync(string tx_hex, bool verbose) =>
            DecodeRawExchangeAsync(CliOptions.ChainName, tx_hex, verbose);

        /// <summary>
        /// 
        /// <para>Disable raw transaction by spending one of its inputs and sending it back to the wallet.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <returns></returns>
        public Task<CliResponse<object>> DisableRawTransactionAsync(string blockchainName, string tx_hex) =>
            TransactAsync<object>(blockchainName, WalletAction.DisableRawTransactionMethod, new[] { tx_hex });

        /// <summary>
        /// 
        /// <para>Disable raw transaction by spending one of its inputs and sending it back to the wallet.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="tx_hex">The transaction hex string</param>
        /// <returns></returns>
        public Task<CliResponse<object>> DisableRawTransactionAsync(string tx_hex) =>
            DisableRawTransactionAsync(CliOptions.ChainName, tx_hex);

        /// <summary>
        /// 
        /// <para>Reveals the private key corresponding to 'address'.</para>
        /// <para>Then the importprivkey can be used with this output</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">The MultiChain address for the private key</param>
        /// <returns></returns>
        public Task<CliResponse<object>> DumpPrivKeyAsync(string blockchainName, string address) =>
            TransactAsync<object>(blockchainName, WalletAction.DumpPrivKeyMethod, new[] { address });

        /// <summary>
        /// 
        /// <para>Reveals the private key corresponding to 'address'.</para>
        /// <para>Then the importprivkey can be used with this output</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="address">The MultiChain address for the private key</param>
        /// <returns></returns>
        public Task<CliResponse<object>> DumpPrivKeyAsync(string address) =>
            DumpPrivKeyAsync(CliOptions.ChainName, address);

        /// <summary>
        /// 
        /// <para>Dumps all wallet keys in a human-readable format.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="filename">The filename</param>
        /// <returns></returns>
        public Task<CliResponse<object>> DumpWalletAsync(string blockchainName, string filename) =>
            TransactAsync<object>(blockchainName, WalletAction.DumpWalletMethod, new[] { filename });

        /// <summary>
        /// 
        /// <para>Dumps all wallet keys in a human-readable format.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="filename">The filename</param>
        /// <returns></returns>
        public Task<CliResponse<object>> DumpWalletAsync(string filename) =>
            DumpWalletAsync(CliOptions.ChainName, filename);

        /// <summary>
        /// <para>Encrypts the wallet with 'passphrase'. This is for first time encryption.</para>
        /// <para>
        ///     After this, any calls that interact with private keys such as sending or signing
        ///     will require the passphrase to be set prior the making these calls.
        ///     Use the walletpassphrase call for this, and then walletlock call.
        ///     If the wallet is already encrypted, use the walletpassphrasechange call.
        ///     Note that this will shutdown the server.
        /// </para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="passphrase">The pass phrase to encrypt the wallet with. It must be at least 1 character, but should be long</param>
        /// <returns></returns>
        public Task<CliResponse<object>> EncryptWalletAsync(string blockchainName, string passphrase) =>
            TransactAsync<object>(blockchainName, WalletAction.EncryptWalletMethod, new[] { passphrase });

        /// <summary>
        /// <para>Encrypts the wallet with 'passphrase'. This is for first time encryption.</para>
        /// <para>
        ///     After this, any calls that interact with private keys such as sending or signing
        ///     will require the passphrase to be set prior the making these calls.
        ///     Use the walletpassphrase call for this, and then walletlock call.
        ///     If the wallet is already encrypted, use the walletpassphrasechange call.
        ///     Note that this will shutdown the server.
        /// </para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="passphrase">The pass phrase to encrypt the wallet with. It must be at least 1 character, but should be long</param>
        /// <returns></returns>
        public Task<CliResponse<object>> EncryptWalletAsync(string passphrase) =>
            EncryptWalletAsync(CliOptions.ChainName, passphrase);

        /// <summary>
        /// 
        /// <para>Returns the current address for receiving payments to this account.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="account">The account name for the address. It can also be set to the empty string "" to represent the default account. The account does not need to exist, it will be created and a new address created if there is no account by the given name.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetAccountAddressAsync(string blockchainName, string account) =>
            TransactAsync<object>(blockchainName, WalletAction.GetAccountAddressMethod, new[] { account });

        /// <summary>
        /// 
        /// <para>Returns the current address for receiving payments to this account.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="account">The account name for the address. It can also be set to the empty string "" to represent the default account. The account does not need to exist, it will be created and a new address created if there is no account by the given name.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetAccountAddressAsync(string account) =>
            GetAccountAddressAsync(CliOptions.ChainName, account);

        /// <summary>
        /// 
        /// <para>Returns the account associated with the targeted address.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">The address for account lookup</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetAccountAsync(string blockchainName, string address) =>
            TransactAsync<object>(blockchainName, WalletAction.GetAccountMethod, new[] { address });

        /// <summary>
        /// 
        /// <para>Returns the account associated with the targeted address.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="address">The address for account lookup</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetAccountAsync(string address) =>
            GetAccountAsync(CliOptions.ChainName, address);

        /// <summary>
        /// 
        /// <para>Returns asset balances for specified address</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">Address to return balance for</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_locked">Also take locked outputs into account</param>
        /// <returns></returns>
        public Task<CliResponse<GetAddressBalancesResult[]>> GetAddressBalancesAsync(string blockchainName, string address, int min_conf = 1, bool include_locked = false) =>
            TransactAsync<GetAddressBalancesResult[]>(blockchainName, WalletAction.GetAddressBalancesMethod, new[] { address, $"{min_conf}", $"{include_locked}".ToLower() });

        /// <summary>
        /// 
        /// <para>Returns asset balances for specified address</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="address">Address to return balance for</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_locked">Also take locked outputs into account</param>
        /// <returns></returns>
        public Task<CliResponse<GetAddressBalancesResult[]>> GetAddressBalancesAsync(string address, int min_conf = 1, bool include_locked = false) =>
            GetAddressBalancesAsync(CliOptions.ChainName, address, min_conf, include_locked);

        /// <summary>
        /// 
        /// <para>Returns the list of all addresses in the wallet.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="verbose">The account name</param>
        /// <returns></returns>
        public Task<CliResponse<GetAddressesResult[]>> GetAddressesAsync(string blockchainName, bool verbose) =>
            TransactAsync<GetAddressesResult[]>(blockchainName, WalletAction.GetAddressesMethod, new[] { $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// <para>Returns the list of all addresses in the wallet.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="verbose">The account name</param>
        /// <returns></returns>
        public Task<CliResponse<GetAddressesResult[]>> GetAddressesAsync(bool verbose) =>
            GetAddressesAsync(CliOptions.ChainName, verbose);

        /// <summary>
        /// 
        /// <para>Returns the list of addresses for the given account.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="account">The account name</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetAddressesByAccountAsync(string blockchainName, string account) =>
            TransactAsync<object>(blockchainName, WalletAction.GetAddressesByAccountMethod, new[] { $"{ account }" });

        /// <summary>
        /// 
        /// <para>Returns the list of addresses for the given account.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="account">The account name</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetAddressesByAccountAsync(string account) =>
            GetAddressesByAccountAsync(CliOptions.ChainName, account);

        /// <summary>
        /// 
        /// <para>Provides information about transaction txid related to address in this node's wallet</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">Address used for balance calculation</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="verbose">If true, returns detailed array of inputs and outputs and raw hex of transactions</param>
        /// <returns></returns>
        public Task<CliResponse<GetAddressTransactionResult>> GetAddressTransactionAsync(string blockchainName, string address, string txid, bool verbose) =>
            TransactAsync<GetAddressTransactionResult>(blockchainName, WalletAction.GetAddressTransactionMethod, new[] { address, txid, $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// <para>Provides information about transaction txid related to address in this node's wallet</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="address">Address used for balance calculation</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="verbose">If true, returns detailed array of inputs and outputs and raw hex of transactions</param>
        /// <returns></returns>
        public Task<CliResponse<GetAddressTransactionResult>> GetAddressTransactionAsync(string address, string txid, bool verbose) =>
            GetAddressTransactionAsync(CliOptions.ChainName, address, txid, verbose);

        /// <summary>
        /// 
        /// <para>If account is not specified, returns the server's total available asset balances.</para>
        /// <para>If account is specified, returns the balances in the account.</para>
        /// <para>Note that the account "" is not the same as leaving the parameter out.</para>
        /// <para>The server total may be different to the balance in the default "" account.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="account">The selected account, or "*" for entire wallet. It may be the default account using ""</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_watch_only">Also include balance in watchonly addresses (see 'importaddress')</param>
        /// <param name="include_locked">Also take locked outputs into account</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetAssetBalancesAsync(string blockchainName, [Optional] string account, [Optional] int min_conf, [Optional] bool include_watch_only, [Optional] bool include_locked) =>
            TransactAsync<object>(blockchainName, WalletAction.GetAssetBalancesMethod, new[] { account, $"{min_conf}", $"{include_watch_only}", $"{include_locked}" });

        /// <summary>
        /// 
        /// <para>If account is not specified, returns the server's total available asset balances.</para>
        /// <para>If account is specified, returns the balances in the account.</para>
        /// <para>Note that the account "" is not the same as leaving the parameter out.</para>
        /// <para>The server total may be different to the balance in the default "" account.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="account">The selected account, or "*" for entire wallet. It may be the default account using ""</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_watch_only">Also include balance in watchonly addresses (see 'importaddress')</param>
        /// <param name="include_locked">Also take locked outputs into account</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetAssetBalancesAsync([Optional] string account, [Optional] int min_conf, [Optional] bool include_watch_only, [Optional] bool include_locked) =>
            GetAssetBalancesAsync(CliOptions.ChainName, account, min_conf, include_watch_only, include_locked);

        /// <summary>
        /// 
        /// <para>Retrieves a specific transaction txid involving asset.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="asset_identifier">One of the following: asset txid, asset reference, asset name</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <returns></returns>
        public Task<CliResponse<GetAssetTransactionResult>> GetAssetTransactionAsync(string blockchainName, string asset_identifier, string txid, bool verbose) =>
            TransactAsync<GetAssetTransactionResult>(blockchainName, WalletAction.GetAssetTransactionMethod, new[] { asset_identifier, txid, $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// <para>Retrieves a specific transaction txid involving asset.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="asset_identifier">One of the following: asset txid, asset reference, asset name</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <returns></returns>
        public Task<CliResponse<GetAssetTransactionResult>> GetAssetTransactionAsync(string asset_identifier, string txid, bool verbose) =>
            GetAssetTransactionAsync(CliOptions.ChainName, asset_identifier, txid, verbose);

        /// <summary>
        /// 
        /// <para>If account is not specified, returns the server's total available balance.</para>
        /// <para>If account is specified, returns the balance in the account.</para>
        /// <para>Note that the account "" is not the same as leaving the parameter out.</para>
        /// <para>The server total may be different to the balance in the default "" account.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="account">The selected account, or "*" for entire wallet. It may be the default account using ""</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_watch_only">Also include balance in watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetBalanceAsync(string blockchainName, [Optional] string account, [Optional] int min_conf, [Optional] bool include_watch_only) =>
            TransactAsync<object>(blockchainName, WalletAction.GetBalanceMethod, new[] { account, $"{min_conf}", $"{include_watch_only}" });

        /// <summary>
        /// 
        /// <para>If account is not specified, returns the server's total available balance.</para>
        /// <para>If account is specified, returns the balance in the account.</para>
        /// <para>Note that the account "" is not the same as leaving the parameter out.</para>
        /// <para>The server total may be different to the balance in the default "" account.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="account">The selected account, or "*" for entire wallet. It may be the default account using ""</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_watch_only">Also include balance in watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetBalanceAsync([Optional] string account, [Optional] int min_conf, [Optional] bool include_watch_only) =>
            GetBalanceAsync(CliOptions.ChainName, account, min_conf, include_watch_only);

        /// <summary>
        /// 
        /// <para>Returns asset balances for specified address</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="addresses">Address(es) to return balance for, comma delimited. Default - all or A json array of addresses to return balance for</param>
        /// <param name="assets">Single asset identifier to return balance for, default "*" or Json array of asset identifiers to return balance for</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_locked">Include transactions to watchonly addresses (see 'importaddress')</param>
        /// <param name="include_watch_only">Also take locked outputs into account</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetMultiBalancesAsync(string blockchainName, [Optional] string addresses, [Optional] object[] assets, [Optional] int min_conf, [Optional] bool include_locked, [Optional] bool include_watch_only) =>
            TransactAsync<object>(blockchainName, WalletAction.GetMultiBalancesMethod, new[] { addresses ?? "*", assets == null ? "*" : assets.Serialize(), $"{(min_conf == 0 ? 1 : min_conf)}", $"{include_locked}".ToLower(), $"{include_watch_only}".ToLower() });

        /// <summary>
        /// 
        /// <para>Returns asset balances for specified address</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="addresses">Address(es) to return balance for, comma delimited. Default - all or A json array of addresses to return balance for</param>
        /// <param name="assets">Single asset identifier to return balance for, default "*" or Json array of asset identifiers to return balance for</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_locked">Include transactions to watchonly addresses (see 'importaddress')</param>
        /// <param name="include_watch_only">Also take locked outputs into account</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetMultiBalancesAsync([Optional] string addresses, [Optional] object[] assets, [Optional] int min_conf, [Optional] bool include_locked, [Optional] bool include_watch_only) =>
            GetMultiBalancesAsync(CliOptions.ChainName, addresses, assets, min_conf, include_locked, include_watch_only);

        /// <summary>
        /// 
        /// <para> Returns a new address for receiving payments.</para>
        /// <para>If 'account' is specified (deprecated), it is added to the address book so payments received with the address will be credited to 'account'.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="account">The account name for the address to be linked to. If not provided, the default account "" is used. It can also be set to the empty string "" to represent the default account. The account does not need to exist, it will be created if there is no account by the given name.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetNewAddressAsync(string blockchainName, [Optional] string account)
        {
            if (string.IsNullOrEmpty(account))
                return TransactAsync<string>(blockchainName, WalletAction.GetNewAddressMethod);
            else
                return TransactAsync<string>(blockchainName, WalletAction.GetNewAddressMethod, new[] { account });
        }

        /// <summary>
        /// 
        /// <para> Returns a new address for receiving payments.</para>
        /// <para>If 'account' is specified (deprecated), it is added to the address book so payments received with the address will be credited to 'account'.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="account">The account name for the address to be linked to. If not provided, the default account "" is used. It can also be set to the empty string "" to represent the default account. The account does not need to exist, it will be created if there is no account by the given name.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GetNewAddressAsync([Optional] string account) =>
            GetNewAddressAsync(CliOptions.ChainName, account);

        /// <summary>
        /// 
        /// <para>Returns a new address, for receiving change.</para>
        /// <para>This is for use with raw transactions, NOT normal use.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetRawChangeAddressAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, WalletAction.GetRawChangeAddressMethod);

        /// <summary>
        /// 
        /// <para>Returns a new address, for receiving change.</para>
        /// <para>This is for use with raw transactions, NOT normal use.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> GetRawChangeAddressAsync() =>
            GetRawChangeAddressAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns the total amount received by addresses with account in transactions with at least [minconf] confirmations.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="account">The selected account, may be the default account using ""</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetReceivedByAccountAsync(string blockchainName, string account, int min_conf) =>
            TransactAsync<object>(blockchainName, WalletAction.GetReceivedByAccountMethod, new[] { account, $"{min_conf}" });

        /// <summary>
        /// 
        /// <para>Returns the total amount received by addresses with account in transactions with at least [minconf] confirmations.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="account">The selected account, may be the default account using ""</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetReceivedByAccountAsync(string account, int min_conf) =>
            GetReceivedByAccountAsync(CliOptions.ChainName, account, min_conf);

        /// <summary>
        /// 
        /// <para>Returns the total amount received by the given address in transactions with at least minconf confirmations.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">The address for transactions</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetReceivedByAddressAsync(string blockchainName, string address, int min_conf) =>
            TransactAsync<object>(blockchainName, WalletAction.GetReceivedByAddressMethod, new[] { address, $"{min_conf}" });

        /// <summary>
        /// 
        /// <para>Returns the total amount received by the given address in transactions with at least minconf confirmations.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="address">The address for transactions</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetReceivedByAddressAsync(string address, int min_conf) =>
            GetReceivedByAddressAsync(CliOptions.ChainName, address, min_conf);

        /// <summary>
        /// 
        /// <para>Returns stream item.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="txid">id</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <returns></returns>
        public Task<CliResponse<GetStreamItemResult>> GetStreamItemAsync(string blockchainName, string stream_identifier, string txid, bool verbose) =>
            TransactAsync<GetStreamItemResult>(blockchainName, WalletAction.GetStreamItemMethod, new[] { stream_identifier, txid, $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// <para>Returns stream item.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="txid">id</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <returns></returns>
        public Task<CliResponse<GetStreamItemResult>> GetStreamItemAsync(string stream_identifier, string txid, bool verbose) =>
            GetStreamItemAsync(CliOptions.ChainName, stream_identifier, txid, verbose);

        /// <summary>
        /// 
        /// <para>Returns stream json object items summary for specific key.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="key">Stream key</param>
        /// <param name="mode">
        ///     <para>
        ///         Comma delimited list of the following: jsonobjectmerge (required) - merge json objects, recursive - merge json sub-objects recursively, 
        ///         noupdate -  preserve first value for each key instead of taking the last, omitnull - omit keys with null values, 
        ///         ignoreother - ignore items that cannot be included in summary (otherwise returns an error), 
        ///         ignoremissing - ignore missing offchain items (otherwise returns an error), 
        ///         firstpublishersany - only summarize items by a publisher of first item with this key, 
        ///         firstpublishersall - only summarize items by all publishers of first item with this key       
        ///     </para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetStreamKeySummaryAsync(string blockchainName, string stream_identifier, string key, string mode) =>
            TransactAsync<object>(blockchainName, WalletAction.GetStreamKeySummaryMethod, new[] { stream_identifier, key, mode });

        /// <summary>
        /// 
        /// <para>Returns stream json object items summary for specific key.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="key">Stream key</param>
        /// <param name="mode">
        ///     <para>
        ///         Comma delimited list of the following: 
        ///         jsonobjectmerge (required) - merge json objects, 
        ///         recursive - merge json sub-objects recursively, 
        ///         noupdate -  preserve first value for each key instead of taking the last, omitnull - omit keys with null values, 
        ///         ignoreother - ignore items that cannot be included in summary (otherwise returns an error), 
        ///         ignoremissing - ignore missing offchain items (otherwise returns an error), 
        ///         firstpublishersany - only summarize items by a publisher of first item with this key, 
        ///         firstpublishersall - only summarize items by all publishers of first item with this key       
        ///     </para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetStreamKeySummaryAsync(string stream_identifier, string key, string mode) =>
            GetStreamKeySummaryAsync(CliOptions.ChainName, stream_identifier, key, mode);

        /// <summary>
        /// 
        /// <para>Returns stream json object items summary for specific publisher.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">one of the following: stream txid, stream reference, stream name</param>
        /// <param name="address">Publisher address</param>
        /// <param name="mode">
        ///     <para>
        ///         Comma delimited list of the following: 
        ///         jsonobjectmerge (required) - merge json objects, 
        ///         recursive - merge json sub-objects recursively, 
        ///         noupdate -  preserve first value for each key instead of taking the last, omitnull - omit keys with null values, 
        ///         ignoreother - ignore items that cannot be included in summary (otherwise returns an error), 
        ///         ignoremissing - ignore missing offchain items (otherwise returns an error), 
        ///     </para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetStreamPublisherSummaryAsync(string blockchainName, string stream_identifier, string address, string mode) =>
            TransactAsync<object>(blockchainName, WalletAction.GetStreamPublisherSummaryMethod, new[] { stream_identifier, address, mode });

        /// <summary>
        /// 
        /// <para>Returns stream json object items summary for specific publisher.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifier">one of the following: stream txid, stream reference, stream name</param>
        /// <param name="address">Publisher address</param>
        /// <param name="mode">
        ///     <para>
        ///         Comma delimited list of the following: 
        ///         jsonobjectmerge (required) - merge json objects, 
        ///         recursive - merge json sub-objects recursively, 
        ///         noupdate -  preserve first value for each key instead of taking the last, omitnull - omit keys with null values, 
        ///         ignoreother - ignore items that cannot be included in summary (otherwise returns an error), 
        ///         ignoremissing - ignore missing offchain items (otherwise returns an error), 
        ///     </para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetStreamPublisherSummaryAsync(string stream_identifier, string address, string mode) =>
            GetStreamPublisherSummaryAsync(CliOptions.ChainName, stream_identifier, address, mode);

        /// <summary>
        /// 
        /// <para>Returns a list of all the asset balances in this nodeΓÇÖs wallet, with at least minconf confirmations.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_watch_only">Also include balance in watchonly addresses (see 'importaddress')</param>
        /// <param name="include_locked">Also take locked outputs into account</param>
        /// <returns></returns>
        public Task<CliResponse<GetTotalBalancesResult[]>> GetTotalBalancesAsync(string blockchainName, int min_conf, bool include_watch_only, bool include_locked) =>
            TransactAsync<GetTotalBalancesResult[]>(blockchainName, WalletAction.GetTotalBalancesMethod, new[] { $"{min_conf}", $"{include_watch_only}".ToLower(), $"{include_locked}".ToLower() });

        /// <summary>
        /// 
        /// <para>Returns a list of all the asset balances in this nodeΓÇÖs wallet, with at least minconf confirmations.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="min_conf">Only include transactions confirmed at least this many times</param>
        /// <param name="include_watch_only">Also include balance in watchonly addresses (see 'importaddress')</param>
        /// <param name="include_locked">Also take locked outputs into account</param>
        /// <returns></returns>
        public Task<CliResponse<GetTotalBalancesResult[]>> GetTotalBalancesAsync(int min_conf, bool include_watch_only, bool include_locked) =>
            GetTotalBalancesAsync(CliOptions.ChainName, min_conf, include_watch_only, include_locked);

        /// <summary>
        /// 
        /// <para>Get detailed information about in-wallet transaction txid</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>        
        /// <param name="txid">The transaction id</param>
        /// <param name="include_watch_only">Whether to include watchonly addresses in balance calculation and details[]</param>
        /// <returns></returns>
        public Task<CliResponse<GetTransactionResult>> GetTransactionAsync(string blockchainName, string txid, bool include_watch_only) =>
            TransactAsync<GetTransactionResult>(blockchainName, WalletAction.GetTransactionMethod, new[] { txid, $"{include_watch_only}".ToLower() });

        /// <summary>
        /// 
        /// <para>Get detailed information about in-wallet transaction txid</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="txid">The transaction id</param>
        /// <param name="include_watch_only">Whether to include watchonly addresses in balance calculation and details[]</param>
        /// <returns></returns>
        public Task<CliResponse<GetTransactionResult>> GetTransactionAsync(string txid, bool include_watch_only) =>
            GetTransactionAsync(CliOptions.ChainName, txid, include_watch_only);

        /// <summary>
        /// 
        /// <para>Returns metadata of transaction output.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="vout">vout value</param>
        /// <param name="count_bytes">Number of bytes to return</param>
        /// <param name="start_byte">Start from specific byte</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetTxOutDataAsync(string blockchainName, string txid, int vout, [Optional] int count_bytes, [Optional] int start_byte) =>
            TransactAsync<object>(blockchainName, WalletAction.GetTxOutDataMethod, new[] { txid, $"{vout}", $"{count_bytes}", $"{start_byte}" });

        /// <summary>
        /// 
        /// <para>Returns metadata of transaction output.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="txid">The transaction id</param>
        /// <param name="vout">vout value</param>
        /// <param name="count_bytes">Number of bytes to return</param>
        /// <param name="start_byte">Start from specific byte</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetTxOutDataAsync(string txid, int vout, [Optional] int count_bytes, [Optional] int start_byte) =>
            GetTxOutDataAsync(CliOptions.ChainName, txid, vout, count_bytes, start_byte);

        /// <summary>
        /// 
        /// <para>Returns the server's total unconfirmed balance</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> GetUnconfirmedBalanceAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, WalletAction.GetUnconfirmedBalanceMethod);

        /// <summary>
        /// 
        /// <para>Returns the server's total unconfirmed balance</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> GetUnconfirmedBalanceAsync() =>
            GetUnconfirmedBalanceAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns an object containing various wallet state info.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<GetWalletInfoResult>> GetWalletInfoAsync(string blockchainName) =>
            TransactAsync<GetWalletInfoResult>(blockchainName, WalletAction.GetwalletinfoMethod);

        /// <summary>
        /// 
        /// <para>Returns an object containing various wallet state info.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<GetWalletInfoResult>> GetWalletInfoAsync() =>
            GetWalletInfoAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Get detailed information about in-wallet transaction txid</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="include_watch_only">Whether to include watchonly addresses in balance calculation and details[]</param>
        /// <param name="verbose">If true, returns detailed array of inputs and outputs and raw hex of transactions</param>
        /// <returns></returns>
        public Task<CliResponse<GetWalletTransactionResult>> GetWalletTransactionAsync(string blockchainName, string txid, bool include_watch_only, bool verbose) =>
            TransactAsync<GetWalletTransactionResult>(blockchainName, WalletAction.GetWalletTransactionMethod, new[] { txid, $"{include_watch_only}".ToLower(), $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// <para>Get detailed information about in-wallet transaction txid</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="txid">The transaction id</param>
        /// <param name="include_watch_only">Whether to include watchonly addresses in balance calculation and details[]</param>
        /// <param name="verbose">If true, returns detailed array of inputs and outputs and raw hex of transactions</param>
        /// <returns></returns>
        public Task<CliResponse<GetWalletTransactionResult>> GetWalletTransactionAsync(string txid, bool include_watch_only, bool verbose) =>
            GetWalletTransactionAsync(CliOptions.ChainName, txid, include_watch_only, verbose);

        /// <summary>
        /// 
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="addresses">The multichain addresses to send to (comma delimited)</param>
        /// <param name="permissions">
        ///     Permission strings, comma delimited.
        ///     <para>
        ///         Global: connect,send,receive,issue,mine,admin,activate,creat
        ///     </para>
        ///     <para>
        ///         or per-asset: asset-identifier.issue,admin,activate,send,receive
        ///     </para>
        ///     <para>
        ///         or per-stream: stream-identifier.write,activate,admin
        ///     </para>
        /// </param>
        /// <param name="native_amount">Native currency amount to send. eg 0.1. Default - 0.0</param>
        /// <param name="start_block">Block to apply permissions from (inclusive). Default - 0</param>
        /// <param name="end_block">Block to apply permissions to (exclusive). Default - 4294967295; If -1 is specified default value is used.</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to"> A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GrantAsync(string blockchainName, string addresses, string permissions, [Optional] decimal native_amount, [Optional] int start_block, [Optional] uint end_block, [Optional] string comment, [Optional] string comment_to)
        {
            if (native_amount == 0)
                return TransactAsync<string>(blockchainName, WalletAction.GrantMethod, new[] { addresses, permissions });
            else
                return TransactAsync<string>(blockchainName, WalletAction.GrantMethod, new[] { addresses, permissions, $"{native_amount}", $"{start_block}", $"{end_block}", comment, comment_to });
        }

        /// <summary>
        /// 
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="addresses">The multichain addresses to send to (comma delimited)</param>
        /// <param name="permissions">
        ///     Permission strings, comma delimited.
        ///     <para>
        ///         Global: connect,send,receive,issue,mine,admin,activate,creat
        ///     </para>
        ///     <para>
        ///         or per-asset: asset-identifier.issue,admin,activate,send,receive
        ///     </para>
        ///     <para>
        ///         or per-stream: stream-identifier.write,activate,admin
        ///     </para>
        /// </param>
        /// <param name="native_amount">Native currency amount to send. eg 0.1. Default - 0.0</param>
        /// <param name="start_block">Block to apply permissions from (inclusive). Default - 0</param>
        /// <param name="end_block">Block to apply permissions to (exclusive). Default - 4294967295; If -1 is specified default value is used.</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to"> A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GrantAsync(string addresses, string permissions, [Optional] decimal native_amount, [Optional] int start_block, [Optional] uint end_block, [Optional] string comment, [Optional] string comment_to) =>
            GrantAsync(CliOptions.ChainName, addresses, permissions, native_amount, start_block, end_block, comment, comment_to);

        /// <summary>
        /// Grant permission using specific address.
        ///
        /// Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address used for grant</param>
        /// <param name="addresses">The multichain addresses to send to (comma delimited)</param>
        /// <param name="permissions">
        ///     Permission strings, comma delimited.
        ///     <para>
        ///         Global: connect,send,receive,issue,mine,admin,activate,creat
        ///     </para>
        ///     <para>
        ///         or per-asset: asset-identifier.issue,admin,activate,send,receive
        ///     </para>
        ///     <para>
        ///         or per-stream: stream-identifier.write,activate,admin
        ///     </para>
        /// </param>
        /// <param name="native_amount">Native currency amount to send. eg 0.1. Default - 0.0</param>
        /// <param name="start_block">Block to apply permissions from (inclusive). Default - 0</param>
        /// <param name="end_block">Block to apply permissions to (exclusive). Default - 4294967295; If -1 is specified default value is used.</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to"> A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GrantFromAsync(string blockchainName, string from_address, string addresses, string permissions, [Optional] decimal native_amount, [Optional] int start_block, [Optional] uint end_block, [Optional] string comment, [Optional] string comment_to) =>
            TransactAsync<string>(blockchainName, WalletAction.GrantFromMethod, new[] { from_address, addresses, permissions, $"{native_amount}", $"{start_block}", $"{end_block}", comment, comment_to });

        /// <summary>
        /// Grant permission using specific address.
        ///
        /// Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="from_address">Address used for grant</param>
        /// <param name="addresses">The multichain addresses to send to (comma delimited)</param>
        /// <param name="permissions">
        ///     Permission strings, comma delimited.
        ///     <para>
        ///         Global: connect,send,receive,issue,mine,admin,activate,creat
        ///     </para>
        ///     <para>
        ///         or per-asset: asset-identifier.issue,admin,activate,send,receive
        ///     </para>
        ///     <para>
        ///         or per-stream: stream-identifier.write,activate,admin
        ///     </para>
        /// </param>
        /// <param name="native_amount">Native currency amount to send. eg 0.1. Default - 0.0</param>
        /// <param name="start_block">Block to apply permissions from (inclusive). Default - 0</param>
        /// <param name="end_block">Block to apply permissions to (exclusive). Default - 4294967295; If -1 is specified default value is used.</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to"> A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GrantFromAsync(string from_address, string addresses, string permissions, [Optional] decimal native_amount, [Optional] int start_block, [Optional] uint end_block, [Optional] string comment, [Optional] string comment_to) =>
            GrantFromAsync(CliOptions.ChainName, from_address, addresses, permissions, native_amount, start_block, end_block, comment, comment_to);

        /// <summary>
        /// 
        /// <para>Grant permission(s) with metadata to a given address.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="addresses">The multichain addresses to send to (comma delimited)</param>
        /// <param name="permissions">
        ///     Permission strings, comma delimited.
        ///     <para>
        ///         Global: connect,send,receive,issue,mine,admin,activate,creat
        ///     </para>
        ///     <para>
        ///         or per-asset: asset-identifier.issue,admin,activate,send,receive
        ///     </para>
        ///     <para>
        ///         or per-stream: stream-identifier.write,activate,admin
        ///     </para>
        /// </param>
        /// <param name="object_or_hex">(string or object, required) Data, see help data-with for details.</param>
        /// <param name="native_amount">Native currency amount to send. eg 0.1. Default - 0.0</param>
        /// <param name="start_block">Block to apply permissions from (inclusive). Default - 0</param>
        /// <param name="end_block">Block to apply permissions to (exclusive). Default - 4294967295; If -1 is specified default value is used.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GrantWithDataAsync(string blockchainName, string addresses, string permissions, object object_or_hex, decimal native_amount = 0, int start_block = 0, uint end_block = uint.MaxValue) =>
            (object_or_hex) switch
            {
                string s => TransactAsync<string>(blockchainName, WalletAction.GrantWithDataMethod, new[] { addresses, permissions, s, $"{native_amount}", $"{start_block}", $"{end_block}" }),
                object o => TransactAsync<string>(blockchainName, WalletAction.GrantWithDataMethod, new[] { addresses, permissions, o.Serialize(), $"{native_amount}", $"{start_block}", $"{end_block}" })
            };

        /// <summary>
        /// 
        /// <para>Grant permission(s) with metadata to a given address.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="addresses">The multichain addresses to send to (comma delimited)</param>
        /// <param name="permissions">
        ///     Permission strings, comma delimited.
        ///     <para>
        ///         Global: connect,send,receive,issue,mine,admin,activate,creat
        ///     </para>
        ///     <para>
        ///         or per-asset: asset-identifier.issue,admin,activate,send,receive
        ///     </para>
        ///     <para>
        ///         or per-stream: stream-identifier.write,activate,admin
        ///     </para>
        /// </param>
        /// <param name="object_or_hex">(string or object, required) Data, see help data-with for details.</param>
        /// <param name="native_amount">Native currency amount to send. eg 0.1. Default - 0.0</param>
        /// <param name="start_block">Block to apply permissions from (inclusive). Default - 0</param>
        /// <param name="end_block">Block to apply permissions to (exclusive). Default - 4294967295; If -1 is specified default value is used.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GrantWithDataAsync(string addresses, string permissions, object object_or_hex, decimal native_amount = 0, int start_block = 0, uint end_block = uint.MaxValue) =>
            GrantWithDataAsync(CliOptions.ChainName, addresses, permissions, object_or_hex, native_amount, start_block, end_block);

        /// <summary>
        /// 
        /// <para>Grant permission with metadata using specific address.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address used for grant</param>
        /// <param name="to_addresses">The multichain addresses to send to (comma delimited)</param>
        /// <param name="permissions">
        ///     Permission strings, comma delimited.
        ///     <para>
        ///         Global: connect,send,receive,issue,mine,admin,activate,creat
        ///     </para>
        ///     <para>
        ///         or per-asset: asset-identifier.issue,admin,activate,send,receive
        ///     </para>
        ///     <para>
        ///         or per-stream: stream-identifier.write,activate,admin
        ///     </para>
        /// </param>
        /// <param name="object_or_hex">(string or object, required) Data, see help data-with for details.</param>
        /// <param name="native_amount">Native currency amount to send. eg 0.1. Default - 0.0</param>
        /// <param name="start_block">Block to apply permissions from (inclusive). Default - 0</param>
        /// <param name="end_block">Block to apply permissions to (exclusive). Default - 4294967295; If -1 is specified default value is used.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GrantWithDataFromAsync(string blockchainName, string from_address, string to_addresses, string permissions, object object_or_hex, decimal native_amount = 0, int start_block = 0, uint end_block = uint.MaxValue) =>
            (object_or_hex) switch
            {
                string s => TransactAsync<string>(blockchainName, WalletAction.GrantWithDataFromMethod, new[] { from_address, to_addresses, permissions, s, $"{native_amount}", $"{start_block}", $"{end_block}" }),
                object o => TransactAsync<string>(blockchainName, WalletAction.GrantWithDataFromMethod, new[] { from_address, to_addresses, permissions, o.Serialize(), $"{native_amount}", $"{start_block}", $"{end_block}" })
            };

        /// <summary>
        /// 
        /// <para>Grant permission with metadata using specific address.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="from_address">Address used for grant</param>
        /// <param name="to_addresses">The multichain addresses to send to (comma delimited)</param>
        /// <param name="permissions">
        ///     Permission strings, comma delimited.
        ///     <para>
        ///         Global: connect,send,receive,issue,mine,admin,activate,creat
        ///     </para>
        ///     <para>
        ///         or per-asset: asset-identifier.issue,admin,activate,send,receive
        ///     </para>
        ///     <para>
        ///         or per-stream: stream-identifier.write,activate,admin
        ///     </para>
        /// </param>
        /// <param name="object_or_hex">(string or object, required) Data, see help data-with for details.</param>
        /// <param name="native_amount">Native currency amount to send. eg 0.1. Default - 0.0</param>
        /// <param name="start_block">Block to apply permissions from (inclusive). Default - 0</param>
        /// <param name="end_block">Block to apply permissions to (exclusive). Default - 4294967295; If -1 is specified default value is used.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> GrantWithDataFromAsync(string from_address, string to_addresses, string permissions, object object_or_hex, decimal native_amount = 0, int start_block = 0, uint end_block = uint.MaxValue) =>
            GrantWithDataFromAsync(CliOptions.ChainName, from_address, to_addresses, permissions, object_or_hex, native_amount, start_block, end_block);

        /// <summary>
        /// 
        /// <para>Adds an address or script (in hex) that can be watched as if it were in your wallet but cannot be used to spend.</para>
        /// <para>Note: This call can take minutes to complete if rescan is true.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="addresses">The addresses, comma delimited or a json array of addresses</param>
        /// <param name="label">An optional label</param>
        /// <param name="rescan">(boolean or integer, optional, default=true) Rescan the wallet for transactions. If integer rescan from block, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ImportAddressAsync(string blockchainName, object addresses, [Optional] string label, [Optional] object rescan) =>
            TransactAsync<object>(blockchainName, WalletAction.ImportAddressMethod, new[] { addresses.Serialize(), label, $"{rescan}" });

        /// <summary>
        /// 
        /// <para>Adds an address or script (in hex) that can be watched as if it were in your wallet but cannot be used to spend.</para>
        /// <para>Note: This call can take minutes to complete if rescan is true.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="addresses">The addresses, comma delimited or a json array of addresses</param>
        /// <param name="label">An optional label</param>
        /// <param name="rescan">(boolean or integer, optional, default=true) Rescan the wallet for transactions. If integer rescan from block, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ImportAddressAsync(object addresses, [Optional] string label, [Optional] object rescan) =>
            ImportAddressAsync(CliOptions.ChainName, addresses, label, rescan);

        /// <summary>
        /// 
        /// <para>Adds a private key (as returned by dumpprivkey) to your wallet.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="priv_keys">(string, required) The private key (see dumpprivkey), comma delimited or (array, optional) A json array of private keys</param>
        /// <param name="label">An optional label</param>
        /// <param name="rescan">(boolean or integer, optional, default=true) Rescan the wallet for transactions. If integer rescan from block, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ImportPrivKeyAsync(string blockchainName, object priv_keys, [Optional] string label, [Optional] object rescan) =>
            TransactAsync<object>(blockchainName, WalletAction.ImportPrivKeyMethod, new[] { priv_keys.Serialize(), label, $"{rescan}" });

        /// <summary>
        /// 
        /// <para>Adds a private key (as returned by dumpprivkey) to your wallet.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="priv_keys">(string, required) The private key (see dumpprivkey), comma delimited or (array, optional) A json array of private keys</param>
        /// <param name="label">An optional label</param>
        /// <param name="rescan">(boolean or integer, optional, default=true) Rescan the wallet for transactions. If integer rescan from block, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ImportPrivKeyAsync(object priv_keys, [Optional] string label, [Optional] object rescan) =>
            ImportPrivKeyAsync(CliOptions.ChainName, priv_keys, label, rescan);

        /// <summary>
        /// 
        /// <para>Imports keys from a wallet dump file (see dumpwallet).</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="filename">The wallet file</param>
        /// <param name="rescan">(boolean or integer, optional, default=true) Rescan the wallet for transactions. If integer rescan from block, if negative - from the end.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ImportWalletAsync(string blockchainName, string filename, [Optional] bool rescan) =>
            TransactAsync<object>(blockchainName, WalletAction.ImportWalletMethod, new[] { filename, $"{rescan}" });

        /// <summary>
        /// 
        /// <para>Imports keys from a wallet dump file (see dumpwallet).</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="filename">The wallet file</param>
        /// <param name="rescan">(boolean or integer, optional, default=true) Rescan the wallet for transactions. If integer rescan from block, if negative - from the end.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ImportWalletAsync(string filename, [Optional] bool rescan) =>
            ImportWalletAsync(CliOptions.ChainName, filename, rescan);

        /// <summary>
        /// 
        /// <para>Issue a new AssetEntity model to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(AssetEntity type, required) A strongly typed object with asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string blockchainName,
                                                    string toAddress,
                                                    AssetEntity assetParams,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    Dictionary<string, string>? customFields = null)
        {
            customFields ??= new Dictionary<string, string>();
            return TransactAsync<string>(blockchainName, WalletAction.IssueMethod, new string[] { toAddress, assetParams.Serialize(), $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new AssetEntity model to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(AssetEntity type, required) A strongly typed object with asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string blockchainName,
                                                    string toAddress,
                                                    AssetEntity assetParams,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    object? customFields = null)
        {
            customFields ??= new { };
            return TransactAsync<string>(blockchainName, WalletAction.IssueMethod, new string[] { toAddress, assetParams.Serialize(), $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new Asset using object params to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(object type, required) A generic object that must include asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string blockchainName,
                                                    string toAddress,
                                                    object assetParams,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    Dictionary<string, string>? customFields = null)
        {
            customFields ??= new Dictionary<string, string>();
            return TransactAsync<string>(blockchainName, WalletAction.IssueMethod, new string[] { toAddress, assetParams.Serialize(), $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new Asset using object params to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(object type, required) A generic object that must include asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string blockchainName,
                                                    string toAddress,
                                                    object assetParams,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    object? customFields = null)
        {
            customFields ??= new { };
            return TransactAsync<string>(blockchainName, WalletAction.IssueMethod, new string[] { toAddress, assetParams.Serialize(), $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new Asset by name to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetName"> (string, required) Asset name, if not "" should be unique or (object, required) A json object of with asset params</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string blockchainName,
                                                    string toAddress,
                                                    string assetName,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    Dictionary<string, string>? customFields = null)
        {
            customFields ??= new Dictionary<string, string>();
            return TransactAsync<string>(blockchainName, WalletAction.IssueMethod, new string[] { toAddress, assetName, $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new Asset by name to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetName"> (string, required) Asset name, if not "" should be unique or (object, required) A json object of with asset params</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string blockchainName,
                                                    string toAddress,
                                                    string assetName,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    object? customFields = null)
        {
            customFields ??= new { };
            return TransactAsync<string>(blockchainName, WalletAction.IssueMethod, new string[] { toAddress, assetName, $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new AssetEntity model to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(AssetEntity type, required) A strongly typed object with asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string toAddress,
                                                    AssetEntity assetParams,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    Dictionary<string, string>? customFields = null) =>
            IssueAsync(CliOptions.ChainName, toAddress, assetParams, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new AssetEntity model to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(AssetEntity type, required) A strongly typed object with asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string toAddress,
                                                    AssetEntity assetParams,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    object? customFields = null) =>
            IssueAsync(CliOptions.ChainName, toAddress, assetParams, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new Asset using object params to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(object type, required) A generic object that must include asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string toAddress,
                                                    object assetParams,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    Dictionary<string, string>? customFields = null) =>
            IssueAsync(CliOptions.ChainName, toAddress, assetParams, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new Asset using object params to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(object type, required) A generic object that must include asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string toAddress,
                                                    object assetParams,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    object? customFields = null) =>
            IssueAsync(CliOptions.ChainName, toAddress, assetParams, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new Asset by name to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetname">(string, required) Asset name, if not "" should be unique</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string toAddress,
                                                    string assetname,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    Dictionary<string, string>? customFields = null) =>
            IssueAsync(CliOptions.ChainName, toAddress, assetname, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new Asset by name to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetname">(string, required) Asset name, if not "" should be unique</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueAsync(string toAddress,
                                                    string assetname,
                                                    int quantity,
                                                    double smallestUnit = 1,
                                                    decimal nativeCurrencyAmount = 0,
                                                    object? customFields = null) =>
            IssueAsync(CliOptions.ChainName, toAddress, assetname, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new AssetEntity model to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(AssetEntity type, required) A strongly typed object with asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string blockchainName,
                                                        string fromAddress,
                                                        string toAddress,
                                                        AssetEntity assetParams,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        Dictionary<string, string>? customFields = null)
        {
            customFields ??= new Dictionary<string, string>();
            return TransactAsync<string>(blockchainName, WalletAction.IssueFromMethod, new string[] { fromAddress, toAddress, assetParams.Serialize(), $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new AssetEntity model to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(AssetEntity type, required) A strongly typed object with asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string blockchainName,
                                                        string fromAddress,
                                                        string toAddress,
                                                        AssetEntity assetParams,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        object? customFields = null)
        {
            customFields ??= new { };
            return TransactAsync<string>(blockchainName, WalletAction.IssueFromMethod, new string[] { fromAddress, toAddress, assetParams.Serialize(), $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new Asset using object params to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(object type, required) A generic object that must include asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string blockchainName,
                                                        string fromAddress,
                                                        string toAddress,
                                                        object assetParams,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        Dictionary<string, string>? customFields = null)
        {
            customFields ??= new Dictionary<string, string>();
            return TransactAsync<string>(blockchainName, WalletAction.IssueFromMethod, new string[] { fromAddress, toAddress, assetParams.Serialize(), $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new Asset using object params to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(object type, required) A generic object that must include asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string blockchainName,
                                                        string fromAddress,
                                                        string toAddress,
                                                        object assetParams,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        object? customFields = null)
        {
            customFields ??= new { };
            return TransactAsync<string>(blockchainName, WalletAction.IssueFromMethod, new string[] { fromAddress, toAddress, assetParams.Serialize(), $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new Asset by name to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetName">(string, required) Asset name, if not "" should be unique</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string blockchainName,
                                                        string fromAddress,
                                                        string toAddress,
                                                        string assetName,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        Dictionary<string, string>? customFields = null)
        {
            customFields ??= new Dictionary<string, string>();
            return TransactAsync<string>(blockchainName, WalletAction.IssueFromMethod, new string[] { fromAddress, toAddress, assetName, $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new Asset by name to an address on the blockchain network.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetName">(string, required) Asset name, if not "" should be unique</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string blockchainName,
                                                        string fromAddress,
                                                        string toAddress,
                                                        string assetName,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        object? customFields = null)
        {
            customFields ??= new { };
            return TransactAsync<string>(blockchainName, WalletAction.IssueFromMethod, new string[] { fromAddress, toAddress, assetName, $"{quantity}", $"{smallestUnit}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Issue a new AssetEntity model to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(AssetEntity type, required) A strongly typed object with asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>                                                                                                                                                                                                                                                                            
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string fromAddress,
                                                        string toAddress,
                                                        AssetEntity assetParams,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        Dictionary<string, string>? customFields = null) =>
            IssueFromAsync(CliOptions.ChainName, fromAddress, toAddress, assetParams, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new AssetEntity model to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(AssetEntity type, required) A strongly typed object with asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>                                                                                                                                                                                                                                                                            
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string fromAddress,
                                                        string toAddress,
                                                        AssetEntity assetParams,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        object? customFields = null) =>
            IssueFromAsync(CliOptions.ChainName, fromAddress, toAddress, assetParams, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new Asset using object params to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(object type, required) A generic object that must include asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string fromAddress,
                                                        string toAddress,
                                                        object assetParams,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        Dictionary<string, string>? customFields = null) =>
            IssueFromAsync(CliOptions.ChainName, fromAddress, toAddress, assetParams, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new Asset using object params to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetParams">(object type, required) A generic object that must include asset params (string)name, (bool)open, and (string, comma delimited)restrict</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string fromAddress,
                                                        string toAddress,
                                                        object assetParams,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        object? customFields = null) =>
            IssueFromAsync(CliOptions.ChainName, fromAddress, toAddress, assetParams, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new Asset by name to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetName">(string, required) Asset name, if not "" should be unique</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string fromAddress,
                                                        string toAddress,
                                                        string assetName,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        Dictionary<string, string>? customFields = null) =>
            IssueFromAsync(CliOptions.ChainName, fromAddress, toAddress, assetName, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Issue a new Asset by name to an address on the blockchain network.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetName">(string, required) Asset name, if not "" should be unique</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="smallestUnit">Number of raw units in one displayed unit, eg 0.01 for cents. Default value is 1</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields. { "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value, ,... }</param>
        /// <returns></returns>
        public Task<CliResponse<string>> IssueFromAsync(string fromAddress,
                                                        string toAddress,
                                                        string assetName,
                                                        int quantity,
                                                        double smallestUnit = 1,
                                                        decimal nativeCurrencyAmount = 0,
                                                        object? customFields = null) =>
            IssueFromAsync(CliOptions.ChainName, fromAddress, toAddress, assetName, quantity, smallestUnit, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Create more units for asset</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetIdentifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields</param>
        /// <returns></returns>
        public Task<CliResponse<object>> IssueMoreAsync(string blockchainName,
                                                        string toAddress,
                                                        string assetIdentifier,
                                                        int quantity,
                                                        decimal nativeCurrencyAmount = 0,
                                                        Dictionary<string, string>? customFields = null)
        {
            customFields ??= new Dictionary<string, string>();
            return TransactAsync<object>(blockchainName, WalletAction.IssueMoreMethod, new[] { toAddress, assetIdentifier, $"{quantity}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Create more units for asset</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetIdentifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields</param>
        /// <returns></returns>
        public Task<CliResponse<object>> IssueMoreAsync(string blockchainName,
                                                        string toAddress,
                                                        string assetIdentifier,
                                                        int quantity,
                                                        decimal nativeCurrencyAmount = 0,
                                                        object? customFields = null)
        {
            customFields ??= new { };
            return TransactAsync<object>(blockchainName, WalletAction.IssueMoreMethod, new[] { toAddress, assetIdentifier, $"{quantity}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Create more units for asset</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetIdentifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields</param>
        /// <returns></returns>
        public Task<CliResponse<object>> IssueMoreAsync(string toAddress,
                                                        string assetIdentifier,
                                                        int quantity,
                                                        decimal nativeCurrencyAmount = 0,
                                                        Dictionary<string, string>? customFields = null) =>
            IssueMoreAsync(CliOptions.ChainName, toAddress, assetIdentifier, quantity, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Create more units for asset</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetIdentifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields</param>
        /// <returns></returns>
        public Task<CliResponse<object>> IssueMoreAsync(string toAddress,
                                                        string assetIdentifier,
                                                        int quantity,
                                                        decimal nativeCurrencyAmount = 0,
                                                        object? customFields = null) =>
            IssueMoreAsync(CliOptions.ChainName, toAddress, assetIdentifier, quantity, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Create more units for asset from specific address</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetIdentifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields</param>
        /// <returns></returns>
        public Task<CliResponse<object>> IssueMoreFromAsync(string blockchainName,
                                                            string fromAddress,
                                                            string toAddress,
                                                            string assetIdentifier,
                                                            int quantity,
                                                            decimal nativeCurrencyAmount = 0,
                                                            Dictionary<string, string>? customFields = null)
        {
            customFields ??= new Dictionary<string, string>();
            return TransactAsync<object>(blockchainName, WalletAction.IssueMoreFromMethod, new[] { fromAddress, toAddress, assetIdentifier, $"{quantity}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Create more units for asset from specific address</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetIdentifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields</param>
        /// <returns></returns>
        public Task<CliResponse<object>> IssueMoreFromAsync(string blockchainName,
                                                            string fromAddress,
                                                            string toAddress,
                                                            string assetIdentifier,
                                                            int quantity,
                                                            decimal nativeCurrencyAmount = 0,
                                                            object? customFields = null)
        {
            customFields ??= new { };
            return TransactAsync<object>(blockchainName, WalletAction.IssueMoreFromMethod, new[] { fromAddress, toAddress, assetIdentifier, $"{quantity}", $"{nativeCurrencyAmount}", customFields.Serialize() });
        }

        /// <summary>
        /// 
        /// <para>Create more units for asset from specific address</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetIdentifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields</param>
        /// <returns></returns>
        public Task<CliResponse<object>> IssueMoreFromAsync(string fromAddress,
                                                            string toAddress,
                                                            string assetIdentifier,
                                                            int quantity,
                                                            decimal nativeCurrencyAmount = 0,
                                                            Dictionary<string, string>? customFields = null) =>
            IssueMoreFromAsync(CliOptions.ChainName, fromAddress, toAddress, assetIdentifier, quantity, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Create more units for asset from specific address</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="fromAddress">Address used for issuing</param>
        /// <param name="toAddress">The address to send newly created asset to</param>
        /// <param name="assetIdentifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="quantity">The asset total amount in display units. eg. 1234.56</param>
        /// <param name="nativeCurrencyAmount">native currency amount to send. eg 0.1, Default: minimum-per-output.</param>
        /// <param name="customFields">a dictionary object with custom fields</param>
        /// <returns></returns>
        public Task<CliResponse<object>> IssueMoreFromAsync(string fromAddress,
                                                            string toAddress,
                                                            string assetIdentifier,
                                                            int quantity,
                                                            decimal nativeCurrencyAmount = 0,
                                                            object? customFields = null) =>
            IssueMoreFromAsync(CliOptions.ChainName, fromAddress, toAddress, assetIdentifier, quantity, nativeCurrencyAmount, customFields);

        /// <summary>
        /// 
        /// <para>Fills the keypool.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="new_size">The new keypool size</param>
        /// <returns></returns>
        public Task<CliResponse<object>> KeyPoolRefillAsync(string blockchainName, int new_size) =>
            TransactAsync<object>(blockchainName, WalletAction.KeyPoolRefillMethod, new[] { $"{new_size}" });

        /// <summary>
        /// 
        /// <para>Fills the keypool.</para>
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="new_size">The new keypool size</param>
        /// <returns></returns>
        public Task<CliResponse<object>> KeyPoolRefillAsync(int new_size) =>
            KeyPoolRefillAsync(CliOptions.ChainName, new_size);

        /// <summary>
        /// 
        /// <para>Returns Object that has account names as keys, account balances as values.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="min_conf"> Only include transactions with at least this many confirmations</param>
        /// <param name="include_watch_only">Include balances in watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListAccountsAsync(string blockchainName, int min_conf, bool include_watch_only) =>
            TransactAsync<object>(blockchainName, WalletAction.ListAccountsMethod, new[] { $"{min_conf}", $"{include_watch_only}" });

        /// <summary>
        /// 
        /// <para>Returns Object that has account names as keys, account balances as values.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="min_conf"> Only include transactions with at least this many confirmations</param>
        /// <param name="include_watch_only">Include balances in watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListAccountsAsync(int min_conf, bool include_watch_only) =>
            ListAccountsAsync(CliOptions.ChainName, min_conf, include_watch_only);

        /// <summary>
        /// 
        /// Returns asset balances for specified address
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="addresses">(string, optional, default *) Address(es) to return information for, comma delimited. Default - all or (array, optional) A json array of addresses to return information for</param>
        /// <param name="verbose">If true return more information about address.</param>
        /// <param name="count">The number of addresses to display</param>
        /// <param name="start">Start from specific address, 0 based, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<ListAddressesResult[]>> ListAddressesAsync(string blockchainName, [Optional] object addresses, [Optional] bool verbose, [Optional] int count, [Optional] int start) =>
            (addresses) switch
            {
                string s => TransactAsync<ListAddressesResult[]>(blockchainName, WalletAction.ListAddressesMethod, new[] { s, $"{verbose}".ToLower(), $"{count}", $"{start}" }),
                object o => TransactAsync<ListAddressesResult[]>(blockchainName, WalletAction.ListAddressesMethod, new[] { o.Serialize(), $"{verbose}".ToLower(), $"{count}", $"{start}" })
            };

        /// <summary>
        /// 
        /// Returns asset balances for specified address
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="addresses">(string, optional, default *) Address(es) to return information for, comma delimited. Default - all or (array, optional) A json array of addresses to return information for</param>
        /// <param name="verbose">If true return more information about address.</param>
        /// <param name="count">The number of addresses to display</param>
        /// <param name="start">Start from specific address, 0 based, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<ListAddressesResult[]>> ListAddressesAsync([Optional] object addresses, [Optional] bool verbose, [Optional] int count, [Optional] int start) =>
            ListAddressesAsync(CliOptions.ChainName, addresses, verbose, count, start);

        /// <summary>
        ///
        /// <para>Lists groups of addresses which have had their common ownership made public by common use as inputs or as the resulting change in past transactions</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListAddressGroupingsAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, WalletAction.ListAddressGroupingsMethod);

        /// <summary>
        ///
        /// <para>Lists groups of addresses which have had their common ownership made public by common use as inputs or as the resulting change in past transactions</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> ListAddressGroupingsAsync() =>
            ListAddressGroupingsAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// Lists information about the count most recent transactions related to address in this nodeΓÇÖs wallet.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">Address to list transactions for</param>
        /// <param name="count">The number of transactions to return</param>
        /// <param name="skip">The number of transactions to skip</param>
        /// <param name="verbose">If true, returns detailed array of inputs and outputs and raw hex of transactions</param>
        /// <returns></returns>
        public Task<CliResponse<ListAddressTransactionsResult[]>> ListAddressTransactionsAsync(string blockchainName, string address, int count, int skip, bool verbose) =>
            TransactAsync<ListAddressTransactionsResult[]>(blockchainName, WalletAction.ListAddressTransactionsMethod, new[] { address, $"{count}", $"{skip}", $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// Lists information about the count most recent transactions related to address in this nodeΓÇÖs wallet.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="address">Address to list transactions for</param>
        /// <param name="count">The number of transactions to return</param>
        /// <param name="skip">The number of transactions to skip</param>
        /// <param name="verbose">If true, returns detailed array of inputs and outputs and raw hex of transactions</param>
        /// <returns></returns>
        public Task<CliResponse<ListAddressTransactionsResult[]>> ListAddressTransactionsAsync(string address, int count, int skip, bool verbose) =>
            ListAddressTransactionsAsync(CliOptions.ChainName, address, count, skip, verbose);

        /// <summary>
        /// 
        /// Lists transactions involving asset.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="asset_identifier">One of the following: asset txid, asset reference, asset name</param>
        /// <param name="verbose">If true, returns information about transaction</param>
        /// <param name="count">The number of transactions to display</param>
        /// <param name="start">Start from specific transaction, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, transactions appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListAssetTransactionsResult[]>> ListAssetTransactionsAsync(string blockchainName, string asset_identifier, bool verbose, int count, int start, bool local_ordering) =>
            TransactAsync<ListAssetTransactionsResult[]>(blockchainName, WalletAction.ListAssetTransactionsMethod, new[] { asset_identifier, $"{verbose}".ToLower(), $"{count}", $"{start}", $"{local_ordering}".ToLower() });

        /// <summary>
        /// 
        /// Lists transactions involving asset.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="asset_identifier">One of the following: asset txid, asset reference, asset name</param>
        /// <param name="verbose">If true, returns information about transaction</param>
        /// <param name="count">The number of transactions to display</param>
        /// <param name="start">Start from specific transaction, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, transactions appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListAssetTransactionsResult[]>> ListAssetTransactionsAsync(string asset_identifier, bool verbose, int count, int start, bool local_ordering) =>
            ListAssetTransactionsAsync(CliOptions.ChainName, asset_identifier, verbose, count, start, local_ordering);

        /// <summary>
        /// 
        /// Returns list of temporarily unspendable outputs.
        /// <para>See the lockunspent call to lock and unlock transactions for spending.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListLockUnspentAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, WalletAction.ListLockUnspentMethod);

        /// <summary>
        /// 
        /// Returns list of temporarily unspendable outputs.
        /// <para>See the lockunspent call to lock and unlock transactions for spending.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> ListLockUnspentAsync() =>
            ListLockUnspentAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// List balances by account.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="min_conf">The minimum number of confirmations before payments are included</param>
        /// <param name="include_empty">Whether to include accounts that haven't received any payments</param>
        /// <param name="include_watch_only">Whether to include watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListReceivedByAccountAsync(string blockchainName, int min_conf, bool include_empty, bool include_watch_only) =>
            TransactAsync<object>(blockchainName, WalletAction.ListReceivedByAccountMethod, new[] { $"{min_conf}", $"{include_empty}", $"{include_watch_only}" });

        /// <summary>
        /// 
        /// List balances by account.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="min_conf">The minimum number of confirmations before payments are included</param>
        /// <param name="include_empty">Whether to include accounts that haven't received any payments</param>
        /// <param name="include_watch_only">Whether to include watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListReceivedByAccountAsync(int min_conf, bool include_empty, bool include_watch_only) =>
            ListReceivedByAccountAsync(CliOptions.ChainName, min_conf, include_empty, include_watch_only);

        /// <summary>
        /// 
        /// List balances by receiving address.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="min_conf">The minimum number of confirmations before payments are included</param>
        /// <param name="include_empty">Whether to include accounts that haven't received any payments</param>
        /// <param name="include_watch_only">Whether to include watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListReceivedByAddressAsync(string blockchainName, int min_conf, bool include_empty, bool include_watch_only) =>
            TransactAsync<object>(blockchainName, WalletAction.ListReceivedByAddressMethod, new[] { $"{min_conf}", $"{include_empty}", $"{include_watch_only}" });

        /// <summary>
        /// 
        /// List balances by receiving address.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="min_conf">The minimum number of confirmations before payments are included</param>
        /// <param name="include_empty">Whether to include accounts that haven't received any payments</param>
        /// <param name="include_watch_only">Whether to include watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListReceivedByAddressAsync(int min_conf, bool include_empty, bool include_watch_only) =>
            ListReceivedByAddressAsync(CliOptions.ChainName, min_conf, include_empty, include_watch_only);

        /// <summary>
        /// 
        /// Get all transactions in blocks since block [blockhash], or all transactions if omitted
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="block_hash">The block hash to list transactions since</param>
        /// <param name="target_confirmations">The confirmations required, must be 1 or more</param>
        /// <param name="include_watch_only">Include transactions to watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListSinceBlockAsync(string blockchainName, [Optional] string block_hash, [Optional] int target_confirmations, [Optional] bool include_watch_only) =>
            TransactAsync<object>(blockchainName, WalletAction.ListSinceBlockMethod, new[] { block_hash, $"{target_confirmations}", $"{include_watch_only}".ToLower() });

        /// <summary>
        /// 
        /// Get all transactions in blocks since block [blockhash], or all transactions if omitted
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="block_hash">The block hash to list transactions since</param>
        /// <param name="target_confirmations">The confirmations required, must be 1 or more</param>
        /// <param name="include_watch_only">Include transactions to watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListSinceBlockAsync([Optional] string block_hash, [Optional] int target_confirmations, [Optional] bool include_watch_only) =>
            ListSinceBlockAsync(CliOptions.ChainName, block_hash, target_confirmations, include_watch_only);

        /// <summary>
        /// 
        /// Returns stream items in certain block range.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">(string, required) Stream identifier - one of the following: stream txid, stream reference, stream name</param>
        /// <param name="block_set_identifier">(string, required) Comma delimited list of block identifiers or A json array of block identifiers or A json object with time range</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListStreamBlockItemsAsync(string blockchainName, string stream_identifier, object block_set_identifier, [Optional] bool verbose, [Optional] int count, [Optional] int start) =>
            (block_set_identifier) switch
            {
                string s => TransactAsync<object>(blockchainName, WalletAction.ListStreamBlockItemsMethod, new[] { stream_identifier, s, $"{verbose}".ToLower(), $"{count}", $"{start}" }),
                object o => TransactAsync<object>(blockchainName, WalletAction.ListStreamBlockItemsMethod, new[] { stream_identifier, o.Serialize(), $"{verbose}".ToLower(), $"{count}", $"{start}" })
            };

        /// <summary>
        /// 
        /// Returns stream items in certain block range.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="stream_identifier">(string, required) Stream identifier - one of the following: stream txid, stream reference, stream name</param>
        /// <param name="block_set_identifier">(string, required) Comma delimited list of block identifiers or A json array of block identifiers or A json object with time range</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListStreamBlockItemsAsync(string stream_identifier, object block_set_identifier, [Optional] bool verbose, [Optional] int count, [Optional] int start) =>
            ListStreamBlockItemsAsync(CliOptions.ChainName, stream_identifier, block_set_identifier, verbose, count, start);

        /// <summary>
        /// 
        /// Returns stream items.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamItemsResult[]>> ListStreamItemsAsync(string blockchainName, string stream_identifier, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            TransactAsync<ListStreamItemsResult[]>(blockchainName, WalletAction.ListStreamItemsMethod, new[] { stream_identifier, $"{verbose}".ToLower(), $"{count}", $"{start}", $"{local_ordering}".ToLower() });

        /// <summary>
        /// 
        /// Returns stream items.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamItemsResult[]>> ListStreamItemsAsync(string stream_identifier, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            ListStreamItemsAsync(CliOptions.ChainName, stream_identifier, verbose, count, start, local_ordering);

        /// <summary>
        /// 
        /// Returns stream items for specific key.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="key">Stream key</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamKeyItemsResult[]>> ListStreamKeyItemsAsync(string blockchainName, string stream_identifier, string key, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            TransactAsync<ListStreamKeyItemsResult[]>(blockchainName, WalletAction.ListStreamKeyItemsMethod, new[] { stream_identifier, key, $"{verbose}".ToLower(), $"{count}", $"{start}", $"{local_ordering}".ToLower() });

        /// <summary>
        /// 
        /// Returns stream items for specific key.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="key">Stream key</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamKeyItemsResult[]>> ListStreamKeyItemsAsync(string stream_identifier, string key, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            ListStreamKeyItemsAsync(CliOptions.ChainName, stream_identifier, key, verbose, count, start, local_ordering);

        /// <summary>
        /// 
        /// Returns stream keys.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="keys">Stream key or a json array of stream keys</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamKeysResult[]>> ListStreamKeysAsync(string blockchainName, string stream_identifier, object keys, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            TransactAsync<ListStreamKeysResult[]>(blockchainName, WalletAction.ListStreamKeysMethod, new[] { stream_identifier, keys.Serialize(), $"{verbose}".ToLower(), $"{count}", $"{start}", $"{local_ordering}".ToLower() });

        /// <summary>
        /// 
        /// Returns stream keys.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="keys">Stream key or a json array of stream keys</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamKeysResult[]>> ListStreamKeysAsync(string stream_identifier, object keys, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            ListStreamKeysAsync(CliOptions.ChainName, stream_identifier, keys, verbose, count, start, local_ordering);

        /// <summary>
        /// 
        /// Returns stream items for specific publisher.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifiers">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="address">Publisher address</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamPublisherItemsResult[]>> ListStreamPublisherItemsAsync(string blockchainName, string stream_identifiers, string address, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            TransactAsync<ListStreamPublisherItemsResult[]>(blockchainName, WalletAction.ListStreamPublisherItemsMethod, new[] { stream_identifiers, address, $"{verbose}".ToLower(), $"{count}", $"{start}", $"{local_ordering}".ToLower() });

        /// <summary>
        /// 
        /// Returns stream items for specific publisher.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifiers">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="address">Publisher address</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamPublisherItemsResult[]>> ListStreamPublisherItemsAsync(string stream_identifiers, string address, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            ListStreamPublisherItemsAsync(CliOptions.ChainName, stream_identifiers, address, verbose, count, start, local_ordering);

        /// <summary>
        /// 
        /// Returns stream publishers.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="addresses">Publisher addresses, comma delimited or a json array of publisher addresses</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamPublishersResult[]>> ListStreamPublishersAsync(string blockchainName, string stream_identifier, object addresses, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            (addresses) switch
            {
                string s => TransactAsync<ListStreamPublishersResult[]>(blockchainName, WalletAction.ListStreamPublishersMethod, new[] { stream_identifier, s, $"{verbose}".ToLower(), $"{count}", $"{start}", $"{local_ordering}".ToLower() }),
                object o => TransactAsync<ListStreamPublishersResult[]>(blockchainName, WalletAction.ListStreamPublishersMethod, new[] { stream_identifier, o.Serialize(), $"{verbose}".ToLower(), $"{count}", $"{start}", $"{local_ordering}".ToLower() })
            };

        /// <summary>
        /// 
        /// Returns stream publishers.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="addresses">Publisher addresses, comma delimited or a json array of publisher addresses</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <param name="count">The number of items to display</param>
        /// <param name="start">Start from specific item, 0 based, if negative - from the end</param>
        /// <param name="local_ordering">If true, items appear in the order they were processed by the wallet, if false - in the order they appear in blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<ListStreamPublishersResult[]>> ListStreamPublishersAsync(string stream_identifier, object addresses, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering) =>
            ListStreamPublishersAsync(CliOptions.ChainName, stream_identifier, addresses, verbose, count, start, local_ordering);

        /// <summary>
        /// 
        /// Returns stream items for specific query.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="query">Query { "key" : "key" (string, optional, default: "") Item key, or "keys" : keys (array, optional) Item keys, array of strings, and or  "publisher" : "publisher" (string, optional, default: "") Publisher or "publishers" : publishers (array, optional) Publishers, array of strings }</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListStreamQueryItemsAsync(string blockchainName, string stream_identifier, object query, bool verbose) =>
            TransactAsync<object>(blockchainName, WalletAction.ListStreamQueryItemsMethod, new[] { stream_identifier, query.Serialize(), $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// Returns stream items for specific query.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="query">Query { "key" : "key" (string, optional, default: "") Item key, or "keys" : keys (array, optional) Item keys, array of strings, and or  "publisher" : "publisher" (string, optional, default: "") Publisher or "publishers" : publishers (array, optional) Publishers, array of strings }</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListStreamQueryItemsAsync(string stream_identifier, object query, bool verbose) =>
            ListStreamQueryItemsAsync(CliOptions.ChainName, stream_identifier, query, verbose);

        /// <summary>
        /// 
        /// Returns stream items.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifiers">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="txids"> Transaction IDs, comma delimited or Array of transaction IDs</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListStreamTxItemsAsync(string blockchainName, string stream_identifiers, string txids, bool verbose) =>
            TransactAsync<object>(blockchainName, WalletAction.ListStreamTxItemsMethod, new[] { stream_identifiers, txids, $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// Returns stream items.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifiers">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="txids"> Transaction IDs, comma delimited or Array of transaction IDs</param>
        /// <param name="verbose">If true, returns information about item transaction</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ListStreamTxItemsAsync(string stream_identifiers, string txids, bool verbose) =>
            ListStreamTxItemsAsync(CliOptions.ChainName, stream_identifiers, txids, verbose);

        /// <summary>
        /// 
        /// Returns up to 'count' most recent transactions skipping the first 'from' transactions for account 'account'.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="account">The account name. If not included, it will list all transactions for all accounts. If "" is set, it will list transactions for the default account.</param>
        /// <param name="count">The number of transactions to return</param>
        /// <param name="from">The number of transactions to skip</param>
        /// <param name="include_watch_only">Include transactions to watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<ListTransactionsResult[]>> ListTransactionsAsync(string blockchainName, [Optional] string account, [Optional] int count, [Optional] int from, [Optional] bool include_watch_only) =>
            TransactAsync<ListTransactionsResult[]>(blockchainName, WalletAction.ListTransactionsMethod, new[] { account.Serialize(), $"{count}", $"{from}", $"{include_watch_only}" });

        /// <summary>
        /// 
        /// Returns up to 'count' most recent transactions skipping the first 'from' transactions for account 'account'.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="account">The account name. If not included, it will list all transactions for all accounts. If "" is set, it will list transactions for the default account.</param>
        /// <param name="count">The number of transactions to return</param>
        /// <param name="from">The number of transactions to skip</param>
        /// <param name="include_watch_only">Include transactions to watchonly addresses (see 'importaddress')</param>
        /// <returns></returns>
        public Task<CliResponse<ListTransactionsResult[]>> ListTransactionsAsync([Optional] string account, [Optional] int count, [Optional] int from, [Optional] bool include_watch_only) =>
            ListTransactionsAsync(CliOptions.ChainName, account, count, from, include_watch_only);

        /// <summary>
        /// 
        /// Returns array of unspent transaction outputs with between minconf and maxconf (inclusive) confirmations.
        /// 
        /// <para>Optionally filter to only include txouts paid to specified addresses.</para>
        /// <para>Results are an array of Objects, each of which has: {txid, vout, scriptPubKey, amount, confirmations}</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="min_conf">The minimum confirmations to filter</param>
        /// <param name="max_conf">The maximum confirmations to filter</param>
        /// <param name="addresses">A json array of addresses to filter</param>
        /// <returns></returns>
        public Task<CliResponse<ListUnspentResult[]>> ListUnspentAsync(string blockchainName, [Optional] int min_conf, [Optional] int max_conf, [Optional] object addresses) =>
            TransactAsync<ListUnspentResult[]>(blockchainName, WalletAction.ListUnspentMethod, new[] { $"{min_conf}", $"{max_conf}", addresses.Serialize() });

        /// <summary>
        /// 
        /// Returns array of unspent transaction outputs with between minconf and maxconf (inclusive) confirmations.
        /// 
        /// <para>Optionally filter to only include txouts paid to specified addresses.</para>
        /// <para>Results are an array of Objects, each of which has: {txid, vout, scriptPubKey, amount, confirmations}</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="min_conf">The minimum confirmations to filter</param>
        /// <param name="max_conf">The maximum confirmations to filter</param>
        /// <param name="addresses">A json array of addresses to filter</param>
        /// <returns></returns>
        public Task<CliResponse<ListUnspentResult[]>> ListUnspentAsync([Optional] int min_conf, [Optional] int max_conf, [Optional] object addresses) =>
            ListUnspentAsync(CliOptions.ChainName, min_conf, max_conf, addresses);

        /// <summary>
        /// 
        /// Lists information about the count most recent transactions in this nodeΓÇÖs wallet.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="count">The number of transactions to return</param>
        /// <param name="skip">The number of transactions to skip</param>
        /// <param name="include_watch_only">Include transactions to watchonly addresses (see 'importaddress')</param>
        /// <param name="verbose">If true, returns detailed array of inputs and outputs and raw hex of transactions</param>
        /// <returns></returns>
        public Task<CliResponse<ListWalletTransactionsResult[]>> ListWalletTransactionsAsync(string blockchainName, int count, int skip, bool include_watch_only, bool verbose) =>
            TransactAsync<ListWalletTransactionsResult[]>(blockchainName, WalletAction.ListWalletTransactionsMethod, new[] { $"{count}", $"{skip}", $"{include_watch_only}".ToLower(), $"{verbose}".ToLower() });

        /// <summary>
        /// 
        /// Lists information about the count most recent transactions in this nodeΓÇÖs wallet.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="count">The number of transactions to return</param>
        /// <param name="skip">The number of transactions to skip</param>
        /// <param name="include_watch_only">Include transactions to watchonly addresses (see 'importaddress')</param>
        /// <param name="verbose">If true, returns detailed array of inputs and outputs and raw hex of transactions</param>
        /// <returns></returns>
        public Task<CliResponse<ListWalletTransactionsResult[]>> ListWalletTransactionsAsync(int count, int skip, bool include_watch_only, bool verbose) =>
            ListWalletTransactionsAsync(CliOptions.ChainName, count, skip, include_watch_only, verbose);

        /// <summary>
        /// 
        /// Updates list of temporarily unspendable outputs.
        /// <para>Temporarily lock (unlock=false) or unlock (unlock=true) specified transaction outputs.</para>
        /// <para>A locked transaction output will not be chosen by automatic coin selection, when spending assetss.</para>
        /// Locks are stored in memory only. Nodes start with zero locked outputs, and the locked output list is always cleared (by virtue of process exit) when a node stops or fails.
        /// <para>Also see the listunspent call</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="unlock">Whether to unlock (true) or lock (false) the specified transactions</param>
        /// <param name="unspent">A json array of objects. Each object should have the the txid (string) vout (numeric)</param>
        /// <returns></returns>
        public Task<CliResponse<object>> LockUnspentAsync(string blockchainName, bool unlock, object[] unspent) =>
            TransactAsync<object>(blockchainName, WalletAction.LockUnspentMethod, new[] { $"{unlock}".ToLower(), unspent.Serialize() });

        /// <summary>
        /// 
        /// Updates list of temporarily unspendable outputs.
        /// <para>Temporarily lock (unlock=false) or unlock (unlock=true) specified transaction outputs.</para>
        /// <para>A locked transaction output will not be chosen by automatic coin selection, when spending assetss.</para>
        /// Locks are stored in memory only. Nodes start with zero locked outputs, and the locked output list is always cleared (by virtue of process exit) when a node stops or fails.
        /// <para>Also see the listunspent call</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="unlock">Whether to unlock (true) or lock (false) the specified transactions</param>
        /// <param name="unspent">A json array of objects. Each object should have the the txid (string) vout (numeric)</param>
        /// <returns></returns>
        public Task<CliResponse<object>> LockUnspentAsync(bool unlock, object[] unspent) =>
            LockUnspentAsync(CliOptions.ChainName, unlock, unspent);

        /// <summary>
        /// 
        /// Move a specified amount from one account in your wallet to another.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_account">The name of the account to move funds from. May be the default account using ""</param>
        /// <param name="to_account">The name of the account to move funds to. May be the default account using ""</param>
        /// <param name="amount">Number to move</param>
        /// <param name="min_conf">Only use funds with at least this many confirmations</param>
        /// <param name="comment">An optional comment, stored in the wallet only</param>
        /// <returns></returns>
        public Task<CliResponse<object>> MoveAsync(string blockchainName, string from_account, string to_account, object amount, [Optional] int min_conf, [Optional] string comment) =>
            TransactAsync<object>(blockchainName, WalletAction.MoveMethod, new[] { from_account, to_account, amount.Serialize(), $"{min_conf}", comment });

        /// <summary>
        /// 
        /// Move a specified amount from one account in your wallet to another.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="from_account">The name of the account to move funds from. May be the default account using ""</param>
        /// <param name="to_account">The name of the account to move funds to. May be the default account using ""</param>
        /// <param name="amount">Number to move</param>
        /// <param name="min_conf">Only use funds with at least this many confirmations</param>
        /// <param name="comment">An optional comment, stored in the wallet only</param>
        /// <returns></returns>
        public Task<CliResponse<object>> MoveAsync(string from_account, string to_account, object amount, [Optional] int min_conf, [Optional] string comment) =>
            MoveAsync(CliOptions.ChainName, from_account, to_account, amount, min_conf, comment);

        /// <summary>
        /// 
        /// Prepares exchange transaction output for createrawexchange, appendrawexchange
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///  
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="asset_quantities">A json object of assets to send</param>
        /// <param name="_lock">Lock prepared unspent output. Default is true</param>
        /// <returns></returns>
        public Task<CliResponse<PrepareLockUnspentResult>> PrepareLockUnspentAsync(string blockchainName, object asset_quantities, bool _lock = true) =>
            TransactAsync<PrepareLockUnspentResult>(blockchainName, WalletAction.PrepareLockUnspentMethod, new[] { asset_quantities.Serialize(), $"{_lock}".ToLower() });

        /// <summary>
        /// 
        /// Prepares exchange transaction output for createrawexchange, appendrawexchange
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///  
        /// </summary>
        /// <param name="asset_quantities">A json object of assets to send</param>
        /// <param name="_lock">Lock prepared unspent output. Default is true</param>
        /// <returns></returns>
        public Task<CliResponse<PrepareLockUnspentResult>> PrepareLockUnspentAsync(object asset_quantities, bool _lock = true) =>
            PrepareLockUnspentAsync(CliOptions.ChainName, asset_quantities, _lock);

        /// <summary>
        /// 
        /// Prepares exchange transaction output for createrawexchange, appendrawexchange using specific address
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address to send from</param>
        /// <param name="asset_quantities">A json object of assets to send</param>
        /// <param name="_lock">Lock prepared unspent output</param>
        /// <returns></returns>
        public Task<CliResponse<PrepareLockUnspentFromResult>> PrepareLockUnspentFromAsync(string blockchainName, string from_address, object asset_quantities, bool _lock) =>
            TransactAsync<PrepareLockUnspentFromResult>(blockchainName, WalletAction.PrepareLockUnspentFromMethod, new[] { from_address, asset_quantities.Serialize(), $"{_lock}".ToLower() });

        /// <summary>
        /// 
        /// Prepares exchange transaction output for createrawexchange, appendrawexchange using specific address
        /// <para>Requires wallet passphrase to be set with walletpassphrase call.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="from_address">Address to send from</param>
        /// <param name="asset_quantities">A json object of assets to send</param>
        /// <param name="_lock">Lock prepared unspent output</param>
        /// <returns></returns>
        public Task<CliResponse<PrepareLockUnspentFromResult>> PrepareLockUnspentFromAsync(string from_address, object asset_quantities, bool _lock) =>
            PrepareLockUnspentFromAsync(CliOptions.ChainName, from_address, asset_quantities, _lock);

        /// <summary>
        /// 
        /// Publishes stream item. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="keys">Item key or Array of item keys</param>
        /// <param name="data_hex_or_object">Data hex string or JSON data object or Text data object or Binary raw data created with appendbinarycache</param>
        /// <param name="options">Should be "offchain" or omitted</param>
        /// <returns></returns>
        public Task<CliResponse<string>> PublishAsync(string blockchainName, string stream_identifier, object keys, object data_hex_or_object, [Optional] string options) => data_hex_or_object switch
        {
            string s => TransactAsync<string>(blockchainName, WalletAction.PublishMethod, new[] { stream_identifier, keys.Serialize(), s, options }),

            object o => TransactAsync<string>(blockchainName, WalletAction.PublishMethod, new[] { stream_identifier, keys.Serialize(), o.Serialize(), options }),
        };

        /// <summary>
        /// 
        /// Publishes stream item. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="keys">Item key or Array of item keys</param>
        /// <param name="data_hex_or_object">Data hex string or JSON data object or Text data object or Binary raw data created with appendbinarycache</param>
        /// <param name="options">Should be "offchain" or omitted</param>
        /// <returns></returns>
        public Task<CliResponse<string>> PublishAsync(string stream_identifier, object keys, object data_hex_or_object, [Optional] string options) =>
            PublishAsync(CliOptions.ChainName, stream_identifier, keys, data_hex_or_object, options);

        /// <summary>
        /// 
        /// Publishes stream item from specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address used for issuing</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="keys">Item key or Array of item keys</param>
        /// <param name="data_hex_or_object">Data hex string or JSON data object or Text data object or Binary raw data created with appendbinarycache</param>
        /// <param name="options">Should be "offchain" or omitted</param>
        /// <returns></returns>
        public Task<CliResponse<string>> PublishFromAsync(string blockchainName, string from_address, string stream_identifier, object keys, object data_hex_or_object, string options = "") => 
            (keys, data_hex_or_object) switch
            {
                (string _keys, string _data) => TransactAsync<string>(blockchainName, WalletAction.PublishFromMethod, new[] { from_address, stream_identifier, _keys, _data, options }),
                (object _keys, string _data) => TransactAsync<string>(blockchainName, WalletAction.PublishFromMethod, new[] { from_address, stream_identifier, _keys.Serialize(), _data, options }),
                (object _keys, object _data) => TransactAsync<string>(blockchainName, WalletAction.PublishFromMethod, new[] { from_address, stream_identifier, _keys.Serialize(), _data.Serialize(), options })
            };

        /// <summary>
        /// 
        /// Publishes stream item from specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="from_address">Address used for issuing</param>
        /// <param name="stream_identifier">One of the following: stream txid, stream reference, stream name</param>
        /// <param name="keys">Item key or Array of item keys</param>
        /// <param name="data_hex_or_object">Data hex string or JSON data object or Text data object or Binary raw data created with appendbinarycache</param>
        /// <param name="options">Should be "offchain" or omitted</param>
        /// <returns></returns>
        public Task<CliResponse<string>> PublishFromAsync(string from_address, string stream_identifier, object keys, object data_hex_or_object, string options = "") =>
            PublishFromAsync(CliOptions.ChainName, from_address, stream_identifier, keys, data_hex_or_object, options);

        /// <summary>
        /// 
        /// Publishes several stream items.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="stream_identifier">One of: create txid, stream reference, stream name. Default for items if "for" field is omitted</param>
        /// <param name="items">Array of stream items.</param>
        /// <param name="options">Should be "offchain" or omitted. Default for items if "options" field is omitted</param>
        /// <returns></returns>
        public Task<CliResponse<string>> PublishMultiAsync(string blockchainName, string stream_identifier, object[] items, [Optional] string options) =>
            TransactAsync<string>(blockchainName, WalletAction.PublishMultiMethod, new string[] { stream_identifier, items.Serialize(), options });

        /// <summary>
        /// 
        /// Publishes several stream items.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="stream_identifier">One of: create txid, stream reference, stream name. Default for items if "for" field is omitted</param>
        /// <param name="items">Array of stream items.</param>
        /// <param name="options">Should be "offchain" or omitted. Default for items if "options" field is omitted</param>
        /// <returns></returns>
        public Task<CliResponse<string>> PublishMultiAsync(string stream_identifier, object[] items, [Optional] string options) =>
            PublishMultiAsync(CliOptions.ChainName, stream_identifier, items, options);

        /// <summary>
        /// 
        /// Publishes several stream items
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address"> Address used for publishing</param>
        /// <param name="stream_identifier">One of: create txid, stream reference, stream name. Default for items if "for" field is omitted</param>
        /// <param name="items">Array of stream items.</param>
        /// <param name="options">Should be "offchain" or omitted. Default for items if "options" field is omitted</param>
        /// <returns></returns>
        public Task<CliResponse<string>> PublishMultiFromAsync(string blockchainName, string from_address, string stream_identifier, object[] items, [Optional] string options) =>
            TransactAsync<string>(blockchainName, WalletAction.PublishMultiFromMethod, new string[] { from_address, stream_identifier, items.Serialize(), options });

        /// <summary>
        /// 
        /// Publishes several stream items
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="from_address"> Address used for publishing</param>
        /// <param name="stream_identifier">One of: create txid, stream reference, stream name. Default for items if "for" field is omitted</param>
        /// <param name="items">Array of stream items.</param>
        /// <param name="options">Should be "offchain" or omitted. Default for items if "options" field is omitted</param>
        /// <returns></returns>
        public Task<CliResponse<string>> PublishMultiFromAsync(string from_address, string stream_identifier, object[] items, [Optional] string options) =>
            PublishMultiFromAsync(CliOptions.ChainName, from_address, stream_identifier, items, options);

        /// <summary>
        /// 
        /// Resends wallet transactions
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ResendWalletTransactionsAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, WalletAction.ResendWalletTransactionsMethod);

        /// <summary>
        /// 
        /// Resends wallet transactions
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> ResendWalletTransactionsAsync() =>
            ResendWalletTransactionsAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// Revoke permission from a given address. The amount is a real. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="addresses">The addresses(es) to revoke permissions from</param>
        /// <param name="permissions">
        ///     <para>
        ///         Permission strings, comma delimited. Global: connect,send,receive,issue,mine,admin,activate,create or per-asset: asset-identifier.issue,admin,activate,send,receive or per-stream: stream-identifier.write,activate,admin
        ///     </para> 
        /// </param>
        /// <param name="native_amount">native currency amount to send. eg 0.1. Default - 0</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> RevokeAsync(string blockchainName, string addresses, string permissions, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to) =>
            TransactAsync<object>(blockchainName, WalletAction.RevokeMethod, new[] { addresses, permissions, $"{native_amount}", comment, comment_to });

        /// <summary>
        /// 
        /// Revoke permission from a given address. The amount is a real. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="addresses">The addresses(es) to revoke permissions from</param>
        /// <param name="permissions">
        ///     <para>
        ///         Permission strings, comma delimited. Global: connect,send,receive,issue,mine,admin,activate,create or per-asset: asset-identifier.issue,admin,activate,send,receive or per-stream: stream-identifier.write,activate,admin
        ///     </para> 
        /// </param>
        /// <param name="native_amount">native currency amount to send. eg 0.1. Default - 0</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> RevokeAsync(string addresses, string permissions, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to) =>
            RevokeAsync(CliOptions.ChainName, addresses, permissions, native_amount, comment, comment_to);

        /// <summary>
        /// 
        /// Revoke permissions using specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Addresses used for revoke.</param>
        /// <param name="to_addresses">The addresses(es) to revoke permissions from</param>
        /// <param name="permissions">
        ///     <para>
        ///         Permission strings, comma delimited. Global: connect,send,receive,issue,mine,admin,activate,create or per-asset: asset-identifier.issue,admin,activate,send,receive or per-stream: stream-identifier.write,activate,admin
        ///     </para> 
        /// </param>
        /// <param name="native_amount">native currency amount to send. eg 0.1. Default - 0</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> RevokeFromAsync(string blockchainName, string from_address, string to_addresses, string permissions, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to) =>
            TransactAsync<object>(blockchainName, WalletAction.RevokeFromMethod, new[] { from_address, to_addresses, permissions, $"{native_amount}", comment, comment_to });

        /// <summary>
        /// 
        /// Revoke permissions using specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="from_address">Addresses used for revoke.</param>
        /// <param name="to_addresses">The addresses(es) to revoke permissions from</param>
        /// <param name="permissions">
        ///     <para>
        ///         Permission strings, comma delimited. Global: connect,send,receive,issue,mine,admin,activate,create or per-asset: asset-identifier.issue,admin,activate,send,receive or per-stream: stream-identifier.write,activate,admin
        ///     </para> 
        /// </param>
        /// <param name="native_amount">native currency amount to send. eg 0.1. Default - 0</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> RevokeFromAsync(string from_address, string to_addresses, string permissions, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to) =>
            RevokeFromAsync(CliOptions.ChainName, from_address, to_addresses, permissions, native_amount, comment, comment_to);

        /// <summary>
        /// 
        /// Send asset amount to a given address. The amounts are real. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="asset_identifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="asset_quantity">Asset quantity to send. eg 0.1</param>
        /// <param name="native_amount">native currency amount to send. eg 0.1, Default: minimum-per-output</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendAssetAsync(string blockchainName, string to_address, string asset_identifier, int asset_quantity, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to) =>
            TransactAsync<object>(blockchainName, WalletAction.SendAssetMethod, new[] { to_address, asset_identifier, $"{asset_quantity}", $"{native_amount}", comment, comment_to });

        /// <summary>
        /// 
        /// Send asset amount to a given address. The amounts are real. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="to_address">The address to send to</param>
        /// <param name="asset_identifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="asset_quantity">Asset quantity to send. eg 0.1</param>
        /// <param name="native_amount">native currency amount to send. eg 0.1, Default: minimum-per-output</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendAssetAsync(string to_address, string asset_identifier, int asset_quantity, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to) =>
            SendAssetAsync(CliOptions.ChainName, to_address, asset_identifier, asset_quantity, native_amount, comment, comment_to);

        /// <summary>
        /// 
        /// Send an asset amount using specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address to send from</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="asset_identifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="asset_quantity">Asset quantity to send. eg 0.1</param>
        /// <param name="native_amount">native currency amount to send. eg 0.1, Default: minimum-per-output</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendAssetFromAsync(string blockchainName, string from_address, string to_address, string asset_identifier, int asset_quantity, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to) =>
            TransactAsync<object>(blockchainName, WalletAction.SendAssetFromMethod, new[] { from_address, to_address, asset_identifier, $"{asset_quantity}", $"{native_amount}", comment, comment_to });

        /// <summary>
        /// 
        /// Send an asset amount using specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="from_address">Address to send from</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="asset_identifier">One of the following: issue txid, asset reference, asset name</param>
        /// <param name="asset_quantity">Asset quantity to send. eg 0.1</param>
        /// <param name="native_amount">native currency amount to send. eg 0.1, Default: minimum-per-output</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendAssetFromAsync(string from_address, string to_address, string asset_identifier, int asset_quantity, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to) =>
            SendAssetFromAsync(CliOptions.ChainName, from_address, to_address, asset_identifier, asset_quantity, native_amount, comment, comment_to);

        /// <summary>
        /// 
        /// Send an amount (or several asset amounts) to a given address. The amount is a real and is rounded to the nearest 0.00000001. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="amount_or_asset_quantities">(numeric, required) The amount in native currency to send. eg 0.1 or (object, required) A json object of assets to send</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> SendAsync(string blockchainName, string to_address, object amount_or_asset_quantities, [Optional] string comment, [Optional] string comment_to) =>
            TransactAsync<string>(blockchainName, WalletAction.SendMethod, new[] { to_address, amount_or_asset_quantities.Serialize(), comment, comment_to });

        /// <summary>
        /// 
        /// Send an amount (or several asset amounts) to a given address. The amount is a real and is rounded to the nearest 0.00000001. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="to_address">The address to send to</param>
        /// <param name="amount_or_asset_quantities">(numeric, required) The amount in native currency to send. eg 0.1 or (object, required) A json object of assets to send</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<string>> SendAsync(string to_address, object amount_or_asset_quantities, [Optional] string comment, [Optional] string comment_to) =>
            SendAsync(CliOptions.ChainName, to_address, amount_or_asset_quantities, comment, comment_to);

        /// <summary>
        /// 
        /// Sent an amount from an account to a address. The amount is a real and is rounded to the nearest 0.00000001. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_account">The name of the account to send funds from. May be the default account using "".</param>
        /// <param name="to_address">The address to send funds to</param>
        /// <param name="amount">The amount in native currency. (transaction fee is added on top)</param>
        /// <param name="min_conf">Only use funds with at least this many confirmations</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendFromAccountAsync(string blockchainName, string from_account, string to_address, object amount, [Optional] int min_conf, [Optional] string comment, [Optional] string comment_to) =>
            TransactAsync<object>(blockchainName, WalletAction.SendFromAccountMethod, new[] { from_account, to_address, amount.Serialize(), $"{min_conf}", comment, comment_to });

        /// <summary>
        /// 
        /// Sent an amount from an account to a address. The amount is a real and is rounded to the nearest 0.00000001. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="from_account">The name of the account to send funds from. May be the default account using "".</param>
        /// <param name="to_address">The address to send funds to</param>
        /// <param name="amount">The amount in native currency. (transaction fee is added on top)</param>
        /// <param name="min_conf">Only use funds with at least this many confirmations</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendFromAccountAsync(string from_account, string to_address, object amount, [Optional] int min_conf, [Optional] string comment, [Optional] string comment_to) =>
            SendFromAccountAsync(CliOptions.ChainName, from_account, to_address, amount, min_conf, comment, comment_to);

        /// <summary>
        /// 
        /// Send an amount (or several asset amounts) using specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address to send from</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="amount_or_asset_quantities">(numeric, required) The amount in native currency to send. eg 0.1 or (object, required) A json object of assets to send</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendFromAsync(string blockchainName, string from_address, string to_address, object amount_or_asset_quantities, [Optional] string comment, [Optional] string comment_to) =>
            TransactAsync<object>(blockchainName, WalletAction.SendFromMethod, new[] { from_address, to_address, amount_or_asset_quantities.Serialize(), comment, comment_to });

        /// <summary>
        /// 
        /// Send an amount (or several asset amounts) using specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="from_address">Address to send from</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="amount_or_asset_quantities">(numeric, required) The amount in native currency to send. eg 0.1 or (object, required) A json object of assets to send</param>
        /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
        /// <param name="comment_to">A comment to store the name of the person or organization to which you're sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendFromAsync(string from_address, string to_address, object amount_or_asset_quantities, [Optional] string comment, [Optional] string comment_to) =>
            SendFromAsync(CliOptions.ChainName, from_address, to_address, amount_or_asset_quantities, comment, comment_to);

        /// <summary>
        /// 
        /// Send multiple times. Amounts are double-precision floating point numbers. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_account">The account to send the funds from, can be "" for the default account</param>
        /// <param name="amounts">A json object with addresses and amounts</param>
        /// <param name="min_conf">Only use the balance confirmed at least this many times</param>
        /// <param name="comment">A comment</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendManyAsync(string blockchainName, string from_account, object[] amounts, [Optional] int min_conf, [Optional] string comment) =>
            TransactAsync<object>(blockchainName, WalletAction.SendManyMethod, new[] { from_account, amounts.Serialize(), $"{min_conf}", comment });

        /// <summary>
        /// 
        /// Send multiple times. Amounts are double-precision floating point numbers. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="from_account">The account to send the funds from, can be "" for the default account</param>
        /// <param name="amounts">A json object with addresses and amounts</param>
        /// <param name="min_conf">Only use the balance confirmed at least this many times</param>
        /// <param name="comment">A comment</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendManyAsync(string from_account, object[] amounts, [Optional] int min_conf, [Optional] string comment) =>
            SendManyAsync(CliOptions.ChainName, from_account, amounts, min_conf, comment);

        /// <summary>
        /// 
        /// Send an amount (or several asset amounts) to a given address with appended metadata. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="amount_or_asset_quantities">The amount in native currency to send. eg 0.1 or a json object of assets to send</param>
        /// <param name="data_or_publish_new_stream_item">(string or object, required) Data, see help data-with for details.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendWithDataAsync(string blockchainName,
            string to_address, object amount_or_asset_quantities, object data_or_publish_new_stream_item) => 
            (amount_or_asset_quantities, data_or_publish_new_stream_item) switch
            {
                (int amount, string hex) => TransactAsync<object>(blockchainName, WalletAction.SendWithDataMethod, new[] { to_address, amount.ToString(), hex }),
                (decimal amount, string hex) => TransactAsync<object>(blockchainName, WalletAction.SendWithDataMethod, new[] { to_address, amount.ToString(), hex }),
                (object asset, object streamItem) => TransactAsync<object>(blockchainName, WalletAction.SendWithDataMethod, new[] { to_address, asset.Serialize(), streamItem.Serialize() }),
            };
            

        /// <summary>
        /// 
        /// Send an amount (or several asset amounts) to a given address with appended metadata. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="to_address">The address to send to</param>
        /// <param name="amount_or_asset_quantities">The amount in native currency to send. eg 0.1 or a json object of assets to send</param>
        /// <param name="data_or_publish_new_stream_item">(string or object, required) Data, see help data-with for details.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendWithDataAsync(string to_address, object amount_or_asset_quantities, object data_or_publish_new_stream_item) =>
            SendWithDataAsync(CliOptions.ChainName, to_address, amount_or_asset_quantities, data_or_publish_new_stream_item);

        /// <summary>
        /// 
        /// Send an amount (or several asset amounts) using specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="from_address">Address to send from.</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="amount_or_asset_quantities">The amount in native currency to send. eg 0.1 or a json object of assets to send</param>
        /// <param name="data_or_publish_new_stream_item">(string or object, required) Data, see help data-with for details.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendWithDataFromAsync(string blockchainName, string from_address,
            string to_address, object amount_or_asset_quantities, object data_or_publish_new_stream_item) =>
            (amount_or_asset_quantities, data_or_publish_new_stream_item) switch
            {
                (int amount, string hex) => TransactAsync<object>(blockchainName, WalletAction.SendWithDataFromMethod, new[] { from_address, to_address, amount.ToString(), hex }),
                (decimal amount, string hex) => TransactAsync<object>(blockchainName, WalletAction.SendWithDataFromMethod, new[] { from_address, to_address, amount.ToString(), hex }),
                (object asset, object streamItem) => TransactAsync<object>(blockchainName, WalletAction.SendWithDataFromMethod, new[] { from_address, to_address, asset.Serialize(), streamItem.Serialize() }),
            };

        /// <summary>
        /// 
        /// Send an amount (or several asset amounts) using specific address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="from_address">Address to send from.</param>
        /// <param name="to_address">The address to send to</param>
        /// <param name="amount_or_asset_quantities">The amount in native currency to send. eg 0.1 or a json object of assets to send</param>
        /// <param name="data_or_publish_new_stream_item">(string or object, required) Data, see help data-with for details.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SendWithDataFromAsync(string from_address, string to_address, object amount_or_asset_quantities, object data_or_publish_new_stream_item) =>
            SendWithDataFromAsync(CliOptions.ChainName, from_address, to_address, amount_or_asset_quantities, data_or_publish_new_stream_item);

        /// <summary>
        /// 
        /// Sets the account associated with the given address.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">The address to be associated with an account</param>
        /// <param name="account">The account to assign the address to</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SetAccountAsync(string blockchainName, string address, string account) =>
            TransactAsync<object>(blockchainName, WalletAction.SetAccountMethod, new[] { address, account });

        /// <summary>
        /// 
        /// Sets the account associated with the given address.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="address">The address to be associated with an account</param>
        /// <param name="account">The account to assign the address to</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SetAccountAsync(string address, string account) =>
            SetAccountAsync(CliOptions.ChainName, address, account);

        /// <summary>
        /// 
        /// Set the transaction fee per kB.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="amount">The transaction fee in native currency/kB rounded to the nearest 0.00000001</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SetTxFeeAsync(string blockchainName, double amount) =>
            TransactAsync<object>(blockchainName, WalletAction.SetTxFeeMethod, new[] { $"{amount}" });

        /// <summary>
        /// 
        /// Set the transaction fee per kB.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="amount">The transaction fee in native currency/kB rounded to the nearest 0.00000001</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SetTxFeeAsync(double amount) =>
            SetTxFeeAsync(CliOptions.ChainName, amount);

        /// <summary>
        /// 
        /// Sign a message with the private key of an address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address_privkey">The address to use for the private key or the private key (see dumpprivkey and createkeypairs)</param>
        /// <param name="message">The message to create a signature of</param>
        /// <returns></returns>
        public Task<CliResponse<string>> SignMessageAsync(string blockchainName, string address_privkey, string message) =>
            TransactAsync<string>(blockchainName, WalletAction.SignMessageMethod, new[] { address_privkey, message.Replace(' ', '_') });

        /// <summary>
        /// 
        /// Sign a message with the private key of an address. Requires wallet passphrase to be set with walletpassphrase call.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="address_privkey">The address to use for the private key or the private key (see dumpprivkey and createkeypairs)</param>
        /// <param name="message">The message to create a signature of</param>
        /// <returns></returns>
        public Task<CliResponse<string>> SignMessageAsync(string address_privkey, string message) =>
            SignMessageAsync(CliOptions.ChainName, address_privkey, message);

        /// <summary>
        /// 
        /// <para>Subscribes to a stream or asset.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="entity_identifiers">One of: create txid, stream reference, stream name or one of: issue txid, asset reference, asset name or A json array of stream or asset identifiers</param>
        /// <param name="rescan">Rescan the wallet for transactions. Default true</param>
        /// <param name="parameters">Available only in Enterprise Edition</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SubscribeAsync(string blockchainName, object entity_identifiers, bool rescan = true, string parameters = "")
        {
            string _entity_identifier;
            if (entity_identifiers.GetType().Equals(typeof(string)))
                _entity_identifier = entity_identifiers.ToString() ?? string.Empty;
            else
                _entity_identifier = entity_identifiers.Serialize();

            if (string.IsNullOrEmpty(parameters))
                return TransactAsync<object>(blockchainName, WalletAction.SubscribeMethod, new string[] { _entity_identifier, $"{rescan}".ToLower() });
            else
                return TransactAsync<object>(blockchainName, WalletAction.SubscribeMethod, new[] { _entity_identifier, $"{rescan}".ToLower(), parameters });
        }

        /// <summary>
        /// 
        /// <para>Subscribes to a stream or asset.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="entity_identifiers">One of: create txid, stream reference, stream name or one of: issue txid, asset reference, asset name or A json array of stream or asset identifiers</param>
        /// <param name="rescan">Rescan the wallet for transactions. Default true</param>
        /// <param name="parameters">Available only in Enterprise Edition</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SubscribeAsync(object entity_identifiers, bool rescan = true, string parameters = "") =>
            SubscribeAsync(CliOptions.ChainName, entity_identifiers, rescan, parameters);

        /// <summary>
        /// 
        /// Available only in Enterprise Edition. Removes indexes from subscriptions to the stream.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="streams">One of: create txid, stream reference, stream name or a json array of stream identifiers</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<CliResponse<object>> TrimSubscribeAsync(string blockchainName, object streams, string parameters) =>
            TransactAsync<object>(blockchainName, WalletAction.TrimSubscribeMethod, new[] { JsonConvert.SerializeObject(streams), parameters });

        /// <summary>
        /// 
        /// Available only in Enterprise Edition. Removes indexes from subscriptions to the stream.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="streams">One of: create txid, stream reference, stream name or a json array of stream identifiers</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<CliResponse<object>> TrimSubscribeAsync(object streams, string parameters) =>
            TrimSubscribeAsync(CliOptions.ChainName, streams, parameters);

        /// <summary>
        /// 
        /// Stores metadata of transaction output in binary cache.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="identifier">Binary cache item identifier</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="vout">vout value</param>
        /// <param name="count_bytes">Number of bytes to return</param>
        /// <param name="start_byte">start from specific byte</param>
        /// <returns></returns>
        public Task<CliResponse<double>> TxOutToBinaryCacheAsync(string blockchainName, string identifier, string txid, int vout, [Optional] int count_bytes, [Optional] int start_byte) =>
            TransactAsync<double>(blockchainName, WalletAction.TxOutToBinaryCacheMethod, new[] { identifier, txid, $"{vout}", $"{count_bytes}", $"{start_byte}" });

        /// <summary>
        /// 
        /// Stores metadata of transaction output in binary cache.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="identifier">Binary cache item identifier</param>
        /// <param name="txid">The transaction id</param>
        /// <param name="vout">vout value</param>
        /// <param name="count_bytes">Number of bytes to return</param>
        /// <param name="start_byte">start from specific byte</param>
        /// <returns></returns>
        public Task<CliResponse<double>> TxOutToBinaryCacheAsync(string identifier, string txid, int vout, [Optional] int count_bytes, [Optional] int start_byte) =>
            TxOutToBinaryCacheAsync(CliOptions.ChainName, identifier, txid, vout, count_bytes, start_byte);

        /// <summary>
        /// 
        /// Unsubscribes from the stream.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="entity_identifiers">Stream identifier - one of the following: stream txid, stream reference, stream name or Asset identifier - one of the following: asset txid, asset reference, asset name or a json array of stream or asset identifiers </param>
        /// <param name="purge"> Purge all offchain data for the stream</param>
        /// <returns></returns>
        public Task<CliResponse<object>> UnsubscribeAsync(string blockchainName, string entity_identifiers, bool purge) =>
            TransactAsync<object>(blockchainName, WalletAction.UnsubscribeMethod, new[] { entity_identifiers, $"{purge}".ToLower() });

        /// <summary>
        /// 
        /// Unsubscribes from the stream.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="entity_identifiers">Stream identifier - one of the following: stream txid, stream reference, stream name or Asset identifier - one of the following: asset txid, asset reference, asset name or a json array of stream or asset identifiers </param>
        /// <param name="purge"> Purge all offchain data for the stream</param>
        /// <returns></returns>
        public Task<CliResponse<object>> UnsubscribeAsync(string entity_identifiers, bool purge) =>
            UnsubscribeAsync(CliOptions.ChainName, entity_identifiers, purge);

        /// <summary>
        /// 
        /// Removes the wallet encryption key from memory, locking the wallet.
        /// <para>After calling this method, you will need to call walletpassphrase again before being able to call any methods which require the wallet to be unlocked.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> WalletLockAsync(string blockchainName) =>
            TransactAsync<object>(blockchainName, WalletAction.WalletLockMethod);

        /// <summary>
        /// 
        /// Removes the wallet encryption key from memory, locking the wallet.
        /// <para>After calling this method, you will need to call walletpassphrase again before being able to call any methods which require the wallet to be unlocked.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<object>> WalletLockAsync() => WalletLockAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// Stores the wallet decryption key in memory for 'timeout' seconds.
        /// <para>This is needed prior to performing transactions related to private keys such as sending assets</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="passphrase">The wallet passphrase</param>
        /// <param name="time_out">The time to keep the decryption key in seconds.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> WalletPassphraseAsync(string blockchainName, string passphrase, int time_out) =>
            TransactAsync<object>(blockchainName, WalletAction.WalletPassphraseMethod, new[] { passphrase, $"{time_out}" });

        /// <summary>
        /// 
        /// Stores the wallet decryption key in memory for 'timeout' seconds.
        /// <para>This is needed prior to performing transactions related to private keys such as sending assets</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="passphrase">The wallet passphrase</param>
        /// <param name="time_out">The time to keep the decryption key in seconds.</param>
        /// <returns></returns>
        public Task<CliResponse<object>> WalletPassphraseAsync(string passphrase, int time_out) =>
            WalletPassphraseAsync(CliOptions.ChainName, passphrase, time_out);

        /// <summary>
        /// 
        /// Changes the wallet passphrase from 'oldpassphrase' to 'newpassphrase'.
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="old_passphrase">The current passphrase</param>
        /// <param name="new_passphrase">The new passphrase</param>
        /// <returns></returns>
        public Task<CliResponse<object>> WalletPassphraseChangeAsync(string blockchainName, string old_passphrase, string new_passphrase) =>
            TransactAsync<object>(blockchainName, WalletAction.WalletPassphraseChangeMethod, new[] { old_passphrase, new_passphrase });

        /// <summary>
        /// 
        /// Changes the wallet passphrase from 'oldpassphrase' to 'newpassphrase'.
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="old_passphrase">The current passphrase</param>
        /// <param name="new_passphrase">The new passphrase</param>
        /// <returns></returns>
        public Task<CliResponse<object>> WalletPassphraseChangeAsync(string old_passphrase, string new_passphrase) =>
            WalletPassphraseChangeAsync(CliOptions.ChainName, old_passphrase, new_passphrase);
    }
}