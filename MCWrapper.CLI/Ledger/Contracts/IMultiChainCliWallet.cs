﻿using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Ledger.Contracts;
using MCWrapper.Data.Models.Wallet;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    ///
    /// <para>MutliChain Core methods established by the IMultiChainCliWallet contract</para>
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
    public interface IMultiChainCliWallet : IMultiChainCli
    {
        Task<CliResponse<object>> AddMultiSigAddressAsync(int n_required, string[] keys, [Optional] string account);
        Task<CliResponse<object>> AddMultiSigAddressAsync(string blockchainName, int n_required, string[] keys, [Optional] string account);
        Task<CliResponse<AppendRawExchangeResult>> AppendRawExchangeAsync(string hex, string txid, int vout, object ask_assets);
        Task<CliResponse<AppendRawExchangeResult>> AppendRawExchangeAsync(string blockchainName, string hex, string txid, int vout, object ask_assets);
        Task<CliResponse<object>> ApproveFromAsync(string from_address, string upgrade_identifier, object approve);
        Task<CliResponse<object>> ApproveFromAsync(string blockchainName, string from_address, string upgrade_identifier, object approve);
        Task<CliResponse<object>> BackupWalletAsync(string destination);
        Task<CliResponse<object>> BackupWalletAsync(string blockchainName, string destination);
        Task<CliResponse<object>> CombineUnspentAsync([Optional] string addresses, [Optional] int min_conf, [Optional] int max_combines, [Optional] int min_inputs, [Optional] int max_inputs, [Optional] int max_time);
        Task<CliResponse<object>> CombineUnspentAsync(string blockchainName, [Optional] string addresses, [Optional] int min_conf, [Optional] int max_combines, [Optional] int min_inputs, [Optional] int max_inputs, [Optional] int max_time);
        Task<CliResponse<object>> CompleteRawExchangeAsync(string hex, string txid, int vout, object ask_assets, [Optional] object data);
        Task<CliResponse<object>> CompleteRawExchangeAsync(string blockchainName, string hex, string txid, int vout, object ask_assets, [Optional] object data);
        Task<CliResponse<string>> CreateAsync(string entity_type, string entity_name, object restrictions_or_open, [Optional] object custom_fields);
        Task<CliResponse<string>> CreateAsync(string blockchainName, string entity_type, string entity_name, object restrictions_or_open, [Optional] object custom_fields);
        Task<CliResponse<string>> CreateFromAsync(string from_address, string entity_type, string entity_name, object restrictions_or_open, [Optional] object custom_fields);
        Task<CliResponse<string>> CreateFromAsync(string blockchainName, string from_address, string entity_type, string entity_name, object restrictions_or_open, [Optional] object custom_fields);
        Task<CliResponse<string>> CreateRawExchangeAsync(string txid, int vout, object ask_assets);
        Task<CliResponse<string>> CreateRawExchangeAsync(string blockchainName, string txid, int vout, object ask_assets);
        Task<CliResponse<object>> CreateRawSendFromAsync(string from_address, object addresses, [Optional] object[] data, [Optional] string action);
        Task<CliResponse<object>> CreateRawSendFromAsync(string blockchainName, string from_address, object addresses, [Optional] object[] data, [Optional] string action);
        Task<CliResponse<DecodeRawExchangeResult>> DecodeRawExchangeAsync(string tx_hex, bool verbose);
        Task<CliResponse<DecodeRawExchangeResult>> DecodeRawExchangeAsync(string blockchainName, string tx_hex, bool verbose);
        Task<CliResponse<object>> DisableRawTransactionAsync(string tx_hex);
        Task<CliResponse<object>> DisableRawTransactionAsync(string blockchainName, string tx_hex);
        Task<CliResponse<object>> DumpPrivKeyAsync(string address);
        Task<CliResponse<object>> DumpPrivKeyAsync(string blockchainName, string address);
        Task<CliResponse<object>> DumpWalletAsync(string filename);
        Task<CliResponse<object>> DumpWalletAsync(string blockchainName, string filename);
        Task<CliResponse<object>> EncryptWalletAsync(string passphrase);
        Task<CliResponse<object>> EncryptWalletAsync(string blockchainName, string passphrase);
        Task<CliResponse<object>> GetAccountAddressAsync(string account);
        Task<CliResponse<object>> GetAccountAddressAsync(string blockchainName, string account);
        Task<CliResponse<object>> GetAccountAsync(string address);
        Task<CliResponse<object>> GetAccountAsync(string blockchainName, string address);
        Task<CliResponse<GetAddressBalancesResult[]>> GetAddressBalancesAsync(string address, int min_conf = 1, bool include_locked = false);
        Task<CliResponse<GetAddressBalancesResult[]>> GetAddressBalancesAsync(string blockchainName, string address, int min_conf = 1, bool include_locked = false);
        Task<CliResponse<GetAddressesResult[]>> GetAddressesAsync(bool verbose);
        Task<CliResponse<GetAddressesResult[]>> GetAddressesAsync(string blockchainName, bool verbose);
        Task<CliResponse<object>> GetAddressesByAccountAsync(string account);
        Task<CliResponse<object>> GetAddressesByAccountAsync(string blockchainName, string account);
        Task<CliResponse<GetAddressTransactionResult>> GetAddressTransactionAsync(string address, string txid, bool verbose);
        Task<CliResponse<GetAddressTransactionResult>> GetAddressTransactionAsync(string blockchainName, string address, string txid, bool verbose);
        Task<CliResponse<object>> GetAssetBalancesAsync([Optional] string account, [Optional] int min_conf, [Optional] bool include_watch_only, [Optional] bool include_locked);
        Task<CliResponse<object>> GetAssetBalancesAsync(string blockchainName, [Optional] string account, [Optional] int min_conf, [Optional] bool include_watch_only, [Optional] bool include_locked);
        Task<CliResponse<GetAssetTransactionResult>> GetAssetTransactionAsync(string asset_identifier, string txid, bool verbose);
        Task<CliResponse<GetAssetTransactionResult>> GetAssetTransactionAsync(string blockchainName, string asset_identifier, string txid, bool verbose);
        Task<CliResponse<object>> GetBalanceAsync([Optional] string account, [Optional] int min_conf, [Optional] bool include_watch_only);
        Task<CliResponse<object>> GetBalanceAsync(string blockchainName, [Optional] string account, [Optional] int min_conf, [Optional] bool include_watch_only);
        Task<CliResponse<object>> GetMultiBalancesAsync([Optional] string addresses, [Optional] object[] assets, [Optional] int min_conf, [Optional] bool include_locked, [Optional] bool include_watch_only);
        Task<CliResponse<object>> GetMultiBalancesAsync(string blockchainName, [Optional] string addresses, [Optional] object[] assets, [Optional] int min_conf, [Optional] bool include_locked, [Optional] bool include_watch_only);
        Task<CliResponse<string>> GetNewAddressAsync([Optional] string account);
        Task<CliResponse<string>> GetNewAddressAsync(string blockchainName, [Optional] string account);
        Task<CliResponse<object>> GetRawChangeAddressAsync();
        Task<CliResponse<object>> GetRawChangeAddressAsync(string blockchainName);
        Task<CliResponse<object>> GetReceivedByAccountAsync(string account, int min_conf);
        Task<CliResponse<object>> GetReceivedByAccountAsync(string blockchainName, string account, int min_conf);
        Task<CliResponse<object>> GetReceivedByAddressAsync(string address, int min_conf);
        Task<CliResponse<object>> GetReceivedByAddressAsync(string blockchainName, string address, int min_conf);
        Task<CliResponse<GetStreamItemResult>> GetStreamItemAsync(string stream_identifier, string txid, bool verbose);
        Task<CliResponse<GetStreamItemResult>> GetStreamItemAsync(string blockchainName, string stream_identifier, string txid, bool verbose);
        Task<CliResponse<object>> GetStreamKeySummaryAsync(string stream_identifier, string key, string mode);
        Task<CliResponse<object>> GetStreamKeySummaryAsync(string blockchainName, string stream_identifier, string key, string mode);
        Task<CliResponse<object>> GetStreamPublisherSummaryAsync(string stream_identifier, string address, string mode);
        Task<CliResponse<object>> GetStreamPublisherSummaryAsync(string blockchainName, string stream_identifier, string address, string mode);
        Task<CliResponse<GetTotalBalancesResult[]>> GetTotalBalancesAsync(int min_conf, bool include_watch_only, bool include_locked);
        Task<CliResponse<GetTotalBalancesResult[]>> GetTotalBalancesAsync(string blockchainName, int min_conf, bool include_watch_only, bool include_locked);
        Task<CliResponse<GetTransactionResult>> GetTransactionAsync(string txid, bool include_watch_only);
        Task<CliResponse<GetTransactionResult>> GetTransactionAsync(string blockchainName, string txid, bool include_watch_only);
        Task<CliResponse<object>> GetTxOutDataAsync(string txid, int vout, [Optional] int count_bytes, [Optional] int start_byte);
        Task<CliResponse<object>> GetTxOutDataAsync(string blockchainName, string txid, int vout, [Optional] int count_bytes, [Optional] int start_byte);
        Task<CliResponse<object>> GetUnconfirmedBalanceAsync();
        Task<CliResponse<object>> GetUnconfirmedBalanceAsync(string blockchainName);
        Task<CliResponse<GetWalletInfoResult>> GetWalletInfoAsync();
        Task<CliResponse<GetWalletInfoResult>> GetWalletInfoAsync(string blockchainName);
        Task<CliResponse<GetWalletTransactionResult>> GetWalletTransactionAsync(string txid, bool include_watch_only, bool verbose);
        Task<CliResponse<GetWalletTransactionResult>> GetWalletTransactionAsync(string blockchainName, string txid, bool include_watch_only, bool verbose);
        Task<CliResponse<object>> GrantAsync(string addresses, string permissions, [Optional] decimal native_amount, [Optional] int start_block, [Optional] uint end_block, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> GrantAsync(string blockchainName, string addresses, string permissions, [Optional] decimal native_amount, [Optional] int start_block, [Optional] uint end_block, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> GrantFromAsync(string from_address, string addresses, string permissions, [Optional] decimal native_amount, [Optional] int start_block, [Optional] uint end_block, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> GrantFromAsync(string blockchainName, string from_address, string addresses, string permissions, [Optional] decimal native_amount, [Optional] int start_block, [Optional] uint end_block, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> GrantWithDataAsync(string addresses, string permissions, object object_or_hex, [Optional] decimal native_amount, [Optional] int start_block, [Optional] int end_block);
        Task<CliResponse<object>> GrantWithDataAsync(string blockchainName, string addresses, string permissions, object object_or_hex, [Optional] decimal native_amount, [Optional] int start_block, [Optional] int end_block);
        Task<CliResponse<object>> GrantWithDataFromAsync(string from_address, string to_addresses, string permissions, object object_or_hex, [Optional] decimal native_amount, [Optional] int start_block, [Optional] int end_block);
        Task<CliResponse<object>> GrantWithDataFromAsync(string blockchainName, string from_address, string to_addresses, string permissions, object object_or_hex, [Optional] decimal native_amount, [Optional] int start_block, [Optional] int end_block);
        Task<CliResponse<object>> ImportAddressAsync(object addresses, [Optional] string label, [Optional] object rescan);
        Task<CliResponse<object>> ImportAddressAsync(string blockchainName, object addresses, [Optional] string label, [Optional] object rescan);
        Task<CliResponse<object>> ImportPrivKeyAsync(object priv_keys, [Optional] string label, [Optional] object rescan);
        Task<CliResponse<object>> ImportPrivKeyAsync(string blockchainName, object priv_keys, [Optional] string label, [Optional] object rescan);
        Task<CliResponse<object>> ImportWalletAsync(string filename, [Optional] bool rescan);
        Task<CliResponse<object>> ImportWalletAsync(string blockchainName, string filename, [Optional] bool rescan);
        Task<CliResponse<string>> IssueAsync(string to_address, object asset_params, int quantity, [Optional] double smallest_unit, [Optional] decimal native_amount, [Optional] object custom_fields);
        Task<CliResponse<string>> IssueAsync(string blockchainName, string to_address, object asset_params, int quantity, [Optional] double smallest_unit, [Optional] decimal native_amount, [Optional] object custom_fields);
        Task<CliResponse<string>> IssueFromAsync(string from_address, string to_address, object asset_params, int quantity, [Optional] double smallest_unit, [Optional] decimal native_amount, [Optional] object custom_fields);
        Task<CliResponse<string>> IssueFromAsync(string blockchainName, string from_address, string to_address, object asset_params, int quantity, [Optional] double smallest_unit, [Optional] decimal native_amount, [Optional] object custom_fields);
        Task<CliResponse<object>> IssueMoreAsync(string to_address, string asset_identifier, int quantity, [Optional] decimal native_amount, [Optional] object custom_fields);
        Task<CliResponse<object>> IssueMoreAsync(string blockchainName, string to_address, string asset_identifier, int quantity, [Optional] decimal native_amount, [Optional] object custom_fields);
        Task<CliResponse<object>> IssueMoreFromAsync(string from_address, string to_address, string asset_identifier, int quantity, [Optional] decimal native_amount, [Optional] object custom_fields);
        Task<CliResponse<object>> IssueMoreFromAsync(string blockchainName, string from_address, string to_address, string asset_identifier, int quantity, [Optional] decimal native_amount, [Optional] object custom_fields);
        Task<CliResponse<object>> KeyPoolRefillAsync(int new_size);
        Task<CliResponse<object>> KeyPoolRefillAsync(string blockchainName, int new_size);
        Task<CliResponse<object>> ListAccountsAsync(int min_conf, bool include_watch_only);
        Task<CliResponse<object>> ListAccountsAsync(string blockchainName, int min_conf, bool include_watch_only);
        Task<CliResponse<ListAddressesResult[]>> ListAddressesAsync([Optional] object addresses, [Optional] bool verbose, [Optional] int count, [Optional] int start);
        Task<CliResponse<ListAddressesResult[]>> ListAddressesAsync(string blockchainName, [Optional] object addresses, [Optional] bool verbose, [Optional] int count, [Optional] int start);
        Task<CliResponse<object>> ListAddressGroupingsAsync();
        Task<CliResponse<object>> ListAddressGroupingsAsync(string blockchainName);
        Task<CliResponse<ListAddressTransactionsResult[]>> ListAddressTransactionsAsync(string address, int count, int skip, bool verbose);
        Task<CliResponse<ListAddressTransactionsResult[]>> ListAddressTransactionsAsync(string blockchainName, string address, int count, int skip, bool verbose);
        Task<CliResponse<ListAssetTransactionsResult[]>> ListAssetTransactionsAsync(string asset_identifier, bool verbose, int count, int start, bool local_ordering);
        Task<CliResponse<ListAssetTransactionsResult[]>> ListAssetTransactionsAsync(string blockchainName, string asset_identifier, bool verbose, int count, int start, bool local_ordering);
        Task<CliResponse<object>> ListLockUnspentAsync();
        Task<CliResponse<object>> ListLockUnspentAsync(string blockchainName);
        Task<CliResponse<object>> ListReceivedByAccountAsync(int min_conf, bool include_empty, bool include_watch_only);
        Task<CliResponse<object>> ListReceivedByAccountAsync(string blockchainName, int min_conf, bool include_empty, bool include_watch_only);
        Task<CliResponse<object>> ListReceivedByAddressAsync(int min_conf, bool include_empty, bool include_watch_only);
        Task<CliResponse<object>> ListReceivedByAddressAsync(string blockchainName, int min_conf, bool include_empty, bool include_watch_only);
        Task<CliResponse<object>> ListSinceBlockAsync([Optional] string block_hash, [Optional] int target_confirmations, [Optional] bool include_watch_only);
        Task<CliResponse<object>> ListSinceBlockAsync(string blockchainName, [Optional] string block_hash, [Optional] int target_confirmations, [Optional] bool include_watch_only);
        Task<CliResponse<object>> ListStreamBlockItemsAsync(string stream_identifier, object block_set_identifier, [Optional] bool verbose, [Optional] int count, [Optional] int start);
        Task<CliResponse<object>> ListStreamBlockItemsAsync(string blockchainName, string stream_identifier, object block_set_identifier, [Optional] bool verbose, [Optional] int count, [Optional] int start);
        Task<CliResponse<ListStreamItemsResult[]>> ListStreamItemsAsync(string stream_identifier, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamItemsResult[]>> ListStreamItemsAsync(string blockchainName, string stream_identifier, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamKeyItemsResult[]>> ListStreamKeyItemsAsync(string stream_identifier, string key, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamKeyItemsResult[]>> ListStreamKeyItemsAsync(string blockchainName, string stream_identifier, string key, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamKeysResult[]>> ListStreamKeysAsync(string stream_identifier, object keys, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamKeysResult[]>> ListStreamKeysAsync(string blockchainName, string stream_identifier, object keys, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamPublisherItemsResult[]>> ListStreamPublisherItemsAsync(string stream_identifiers, string address, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamPublisherItemsResult[]>> ListStreamPublisherItemsAsync(string blockchainName, string stream_identifiers, string address, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamPublishersResult[]>> ListStreamPublishersAsync(string stream_identifier, object addresses, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<ListStreamPublishersResult[]>> ListStreamPublishersAsync(string blockchainName, string stream_identifier, object addresses, [Optional] bool verbose, [Optional] int count, [Optional] int start, [Optional] bool local_ordering);
        Task<CliResponse<object>> ListStreamQueryItemsAsync(string stream_identifier, object query, bool verbose);
        Task<CliResponse<object>> ListStreamQueryItemsAsync(string blockchainName, string stream_identifier, object query, bool verbose);
        Task<CliResponse<object>> ListStreamTxItemsAsync(string stream_identifiers, string txids, bool verbose);
        Task<CliResponse<object>> ListStreamTxItemsAsync(string blockchainName, string stream_identifiers, string txids, bool verbose);
        Task<CliResponse<ListTransactionsResult[]>> ListTransactionsAsync([Optional] string account, [Optional] int count, [Optional] int from, [Optional] bool include_watch_only);
        Task<CliResponse<ListTransactionsResult[]>> ListTransactionsAsync(string blockchainName, [Optional] string account, [Optional] int count, [Optional] int from, [Optional] bool include_watch_only);
        Task<CliResponse<ListUnspentResult[]>> ListUnspentAsync([Optional] int min_conf, [Optional] int max_conf, [Optional] object addresses);
        Task<CliResponse<ListUnspentResult[]>> ListUnspentAsync(string blockchainName, [Optional] int min_conf, [Optional] int max_conf, [Optional] object addresses);
        Task<CliResponse<ListWalletTransactionsResult[]>> ListWalletTransactionsAsync(int count, int skip, bool include_watch_only, bool verbose);
        Task<CliResponse<ListWalletTransactionsResult[]>> ListWalletTransactionsAsync(string blockchainName, int count, int skip, bool include_watch_only, bool verbose);
        Task<CliResponse<object>> LockUnspentAsync(bool unlock, object[] unspent);
        Task<CliResponse<object>> LockUnspentAsync(string blockchainName, bool unlock, object[] unspent);
        Task<CliResponse<object>> MoveAsync(string from_account, string to_account, object amount, [Optional] int min_conf, [Optional] string comment);
        Task<CliResponse<object>> MoveAsync(string blockchainName, string from_account, string to_account, object amount, [Optional] int min_conf, [Optional] string comment);
        Task<CliResponse<PrepareLockUnspentResult>> PrepareLockUnspentAsync(object asset_quantities, bool _lock = true);
        Task<CliResponse<PrepareLockUnspentResult>> PrepareLockUnspentAsync(string blockchainName, object asset_quantities, bool _lock = true);
        Task<CliResponse<PrepareLockUnspentFromResult>> PrepareLockUnspentFromAsync(string from_address, object asset_quantities, bool _lock);
        Task<CliResponse<PrepareLockUnspentFromResult>> PrepareLockUnspentFromAsync(string blockchainName, string from_address, object asset_quantities, bool _lock);
        Task<CliResponse<string>> PublishAsync(string stream_identifier, object keys, object data_hex_or_object, [Optional] string options);
        Task<CliResponse<string>> PublishAsync(string blockchainName, string stream_identifier, object keys, object data_hex_or_object, [Optional] string options);
        Task<CliResponse<string>> PublishFromAsync(string from_address, string stream_identifier, object keys, object data_hex_or_object, [Optional] string options);
        Task<CliResponse<string>> PublishFromAsync(string blockchainName, string from_address, string stream_identifier, object keys, object data_hex_or_object, [Optional] string options);
        Task<CliResponse<string>> PublishMultiAsync(string stream_identifier, object[] items, [Optional] string options);
        Task<CliResponse<string>> PublishMultiAsync(string blockchainName, string stream_identifier, object[] items, [Optional] string options);
        Task<CliResponse<string>> PublishMultiFromAsync(string from_address, string stream_identifier, object[] items, [Optional] string options);
        Task<CliResponse<string>> PublishMultiFromAsync(string blockchainName, string from_address, string stream_identifier, object[] items, [Optional] string options);
        Task<CliResponse<object>> ResendWalletTransactionsAsync();
        Task<CliResponse<object>> ResendWalletTransactionsAsync(string blockchainName);
        Task<CliResponse<object>> RevokeAsync(string addresses, string permissions, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> RevokeAsync(string blockchainName, string addresses, string permissions, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> RevokeFromAsync(string from_address, string to_addresses, string permissions, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> RevokeFromAsync(string blockchainName, string from_address, string to_addresses, string permissions, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendAssetAsync(string to_address, string asset_identifier, int asset_quantity, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendAssetAsync(string blockchainName, string to_address, string asset_identifier, int asset_quantity, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendAssetFromAsync(string from_address, string to_address, string asset_identifier, int asset_quantity, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendAssetFromAsync(string blockchainName, string from_address, string to_address, string asset_identifier, int asset_quantity, [Optional] double native_amount, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<string>> SendAsync(string to_address, object amount_or_asset_quantities, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<string>> SendAsync(string blockchainName, string to_address, object amount_or_asset_quantities, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendFromAccountAsync(string from_account, string to_address, object amount, [Optional] int min_conf, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendFromAccountAsync(string blockchainName, string from_account, string to_address, object amount, [Optional] int min_conf, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendFromAsync(string from_address, string to_address, object amount_or_asset_quantities, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendFromAsync(string blockchainName, string from_address, string to_address, object amount_or_asset_quantities, [Optional] string comment, [Optional] string comment_to);
        Task<CliResponse<object>> SendManyAsync(string from_account, object[] amounts, [Optional] int min_conf, [Optional] string comment);
        Task<CliResponse<object>> SendManyAsync(string blockchainName, string from_account, object[] amounts, [Optional] int min_conf, [Optional] string comment);
        Task<CliResponse<object>> SendWithDataAsync(string to_address, object amount_or_asset_quantities, object data_or_publish_new_stream_item);
        Task<CliResponse<object>> SendWithDataAsync(string blockchainName, string to_address, object amount_or_asset_quantities, object data_or_publish_new_stream_item);
        Task<CliResponse<object>> SendWithDataFromAsync(string from_address, string to_address, object amount_or_asset_quantities, object data_or_publish_new_stream_item);
        Task<CliResponse<object>> SendWithDataFromAsync(string blockchainName, string from_address, string to_address, object amount_or_asset_quantities, object data_or_publish_new_stream_item);
        Task<CliResponse<object>> SetAccountAsync(string address, string account);
        Task<CliResponse<object>> SetAccountAsync(string blockchainName, string address, string account);
        Task<CliResponse<object>> SetTxFeeAsync(double amount);
        Task<CliResponse<object>> SetTxFeeAsync(string blockchainName, double amount);
        Task<CliResponse<string>> SignMessageAsync(string address_privkey, string message);
        Task<CliResponse<string>> SignMessageAsync(string blockchainName, string address_privkey, string message);
        Task<CliResponse<object>> SubscribeAsync(object entity_identifiers, bool rescan = true, string parameters = "");
        Task<CliResponse<object>> SubscribeAsync(string blockchainName, object entity_identifiers, bool rescan = true, string parameters = "");
        Task<CliResponse<object>> TrimSubscribeAsync(object streams, string parameters);
        Task<CliResponse<object>> TrimSubscribeAsync(string blockchainName, object streams, string parameters);
        Task<CliResponse<double>> TxOutToBinaryCacheAsync(string identifier, string txid, int vout, [Optional] int count_bytes, [Optional] int start_byte);
        Task<CliResponse<double>> TxOutToBinaryCacheAsync(string blockchainName, string identifier, string txid, int vout, [Optional] int count_bytes, [Optional] int start_byte);
        Task<CliResponse<object>> UnsubscribeAsync(string entity_identifiers, bool purge);
        Task<CliResponse<object>> UnsubscribeAsync(string blockchainName, string entity_identifiers, bool purge);
        Task<CliResponse<object>> WalletLockAsync();
        Task<CliResponse<object>> WalletLockAsync(string blockchainName);
        Task<CliResponse<object>> WalletPassphraseAsync(string passphrase, int time_out);
        Task<CliResponse<object>> WalletPassphraseAsync(string blockchainName, string passphrase, int time_out);
        Task<CliResponse<object>> WalletPassphraseChangeAsync(string old_passphrase, string new_passphrase);
        Task<CliResponse<object>> WalletPassphraseChangeAsync(string blockchainName, string old_passphrase, string new_passphrase);
    }
}