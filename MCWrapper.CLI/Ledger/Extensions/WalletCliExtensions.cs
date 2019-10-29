using MCWrapper.CLI.Connection;
using MCWrapper.Ledger.Entities;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// Extension methods derived from the WalletCLIClient contract and WalletCLIClient implementation
    /// </summary>
    public static class WalletCliExtensions
    {
        // *** Create Stream extension methods

        /// <summary>
        /// Create stream; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateStream(this WalletCliClient client, StreamEntity streamEntity) => 
            client.CreateAsync(streamEntity.EntityType, streamEntity.Name, streamEntity.Restrictions, streamEntity.CustomFields);

        /// <summary>
        /// Create stream; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateStream(this WalletCliClient client, string blockchainName, StreamEntity streamEntity) => 
            client.CreateAsync(blockchainName, streamEntity.EntityType, streamEntity.Name, streamEntity.Restrictions, streamEntity.CustomFields);

        /// <summary>
        /// Create stream from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateStreamFrom(this WalletCliClient client, string fromAddress, StreamEntity streamEntity) => 
            client.CreateFromAsync(fromAddress, streamEntity.EntityType, streamEntity.Name, streamEntity.Restrictions, streamEntity.CustomFields);

        /// <summary>
        /// Create stream from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateStreamFrom(this WalletCliClient client, string blockchainName, string fromAddress, StreamEntity streamEntity) => 
            client.CreateFromAsync(blockchainName, fromAddress, streamEntity.EntityType, streamEntity.Name, streamEntity.Restrictions, streamEntity.CustomFields);


        // *** Create Upgrade extension methods

        /// <summary>
        /// Create upgrade; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="upgradeEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateUpgrade(this WalletCliClient client, UpgradeEntity upgradeEntity) =>
            client.CreateAsync(upgradeEntity.EntityType, upgradeEntity.Name, upgradeEntity.Open, upgradeEntity.CustomFields);

        /// <summary>
        /// Create upgrade; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="upgradeEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateUpgrade(this WalletCliClient client, string blockchainName, UpgradeEntity upgradeEntity) =>
            client.CreateAsync(blockchainName, upgradeEntity.EntityType, upgradeEntity.Name, upgradeEntity.Open, upgradeEntity.CustomFields);

        /// <summary>
        /// Create upgrade from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="upgradeEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateUpgradeFrom(this WalletCliClient client, string fromAddress, UpgradeEntity upgradeEntity) =>
            client.CreateFromAsync(fromAddress, upgradeEntity.EntityType, upgradeEntity.Name, upgradeEntity.Open, upgradeEntity.CustomFields);

        /// <summary>
        /// Create upgrade from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="upgradeEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateUpgradeFrom(this WalletCliClient client, string blockchainName, string fromAddress, UpgradeEntity upgradeEntity) => 
            client.CreateFromAsync(blockchainName, fromAddress, upgradeEntity.EntityType, upgradeEntity.Name, upgradeEntity.Open, upgradeEntity.CustomFields);


        // *** Create Stream Filter extension methods

        /// <summary>
        /// Create stream filter; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamFilterEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateStreamFilter(this WalletCliClient client, StreamFilterEntity streamFilterEntity) => 
            client.CreateAsync(streamFilterEntity.EntityType, streamFilterEntity.Name, streamFilterEntity.Restrictions, streamFilterEntity.JavaScriptCode);

        /// <summary>
        /// Create stream filter; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamFilterEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateStreamFilter(this WalletCliClient client, string blockchainName, StreamFilterEntity streamFilterEntity) => 
            client.CreateAsync(blockchainName, streamFilterEntity.EntityType, streamFilterEntity.Name, streamFilterEntity.Restrictions, streamFilterEntity.JavaScriptCode);

        /// <summary>
        /// Create stream filter from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamFilterEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateStreamFilterFrom(this WalletCliClient client, string fromAddress, StreamFilterEntity streamFilterEntity) => 
            client.CreateFromAsync(fromAddress, streamFilterEntity.EntityType, streamFilterEntity.Name, streamFilterEntity.Restrictions, streamFilterEntity.JavaScriptCode);

        /// <summary>
        /// Create stream filter from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamFilterEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateStreamFilterFrom(this WalletCliClient client, string blockchainName, string fromAddress, StreamFilterEntity streamFilterEntity) =>
            client.CreateFromAsync(blockchainName, fromAddress, streamFilterEntity.EntityType, streamFilterEntity.Name, streamFilterEntity.Restrictions, streamFilterEntity.JavaScriptCode);


        // *** Create Tx Filter extension methods

        /// <summary>
        /// Create transaction filter; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="txFilterEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateTxFilter(this WalletCliClient client, TxFilterEntity txFilterEntity) =>
            client.CreateAsync(txFilterEntity.EntityType, txFilterEntity.Name, txFilterEntity.Restrictions, txFilterEntity.JavaScriptCode);

        /// <summary>
        /// Create transaction filter; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="txFilterEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateTxFilter(this WalletCliClient client, string blockchainName, TxFilterEntity txFilterEntity) => 
            client.CreateAsync(blockchainName, txFilterEntity.EntityType, txFilterEntity.Name, txFilterEntity.Restrictions, txFilterEntity.JavaScriptCode);

        /// <summary>
        /// Create transaction filter from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="txFilterEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateTxFilterFrom(this WalletCliClient client, string fromAddress, TxFilterEntity txFilterEntity) =>
            client.CreateFromAsync(fromAddress, txFilterEntity.EntityType, txFilterEntity.Name, txFilterEntity.Restrictions, txFilterEntity.JavaScriptCode);

        /// <summary>
        /// Create transaction filter from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="txFilterEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> CreateTxFilterFrom(this WalletCliClient client, string blockchainName, string fromAddress, TxFilterEntity txFilterEntity) => 
            client.CreateFromAsync(blockchainName, fromAddress, txFilterEntity.EntityType, txFilterEntity.Name, txFilterEntity.Restrictions, txFilterEntity.JavaScriptCode);


        // *** PublishStreamItem using an inferred blockchain name

        /// <summary>
        /// Create stream item; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKey(this WalletCliClient client, PublishEntity streamItemEntity) => 
            client.PublishAsync(streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKey(this WalletCliClient client, PublishEntity<DataCached> streamItemEntity) => 
            client.PublishAsync(streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKey(this WalletCliClient client, PublishEntity<DataJson> streamItemEntity) =>
            client.PublishAsync(streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKey(this WalletCliClient client, PublishEntity<DataText> streamItemEntity) => 
            client.PublishAsync(streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);


        // *** PublishStreamItems using an inferred blockchain name

        /// <summary>
        /// Create stream item; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeys(this WalletCliClient client, PublishEntity streamItemEntity) => 
            client.PublishAsync(streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeys(this WalletCliClient client, PublishEntity<DataCached> streamItemEntity) =>
            client.PublishAsync(streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeys(this WalletCliClient client, PublishEntity<DataJson> streamItemEntity) => 
            client.PublishAsync(streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeys(this WalletCliClient client, PublishEntity<DataText> streamItemEntity) =>
            client.PublishAsync(streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);


        // *** PublishStreamItem using an explicit blockchain name

        /// <summary>
        /// Create stream item; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKey(this WalletCliClient client, string blockchainName, PublishEntity streamItemEntity) => 
            client.PublishAsync(blockchainName, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKey(this WalletCliClient client, string blockchainName, PublishEntity<DataCached> streamItemEntity) => 
            client.PublishAsync(blockchainName, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKey(this WalletCliClient client, string blockchainName, PublishEntity<DataJson> streamItemEntity) =>
            client.PublishAsync(blockchainName, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKey(this WalletCliClient client, string blockchainName, PublishEntity<DataText> streamItemEntity) =>
            client.PublishAsync(blockchainName, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);


        // *** PublishStreamItems using an explicit blockchain name

        /// <summary>
        /// Create stream item; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeys(this WalletCliClient client, string blockchainName, PublishEntity streamItemEntity) =>
            client.PublishAsync(blockchainName, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeys(this WalletCliClient client, string blockchainName, PublishEntity<DataCached> streamItemEntity) =>
            client.PublishAsync(blockchainName, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeys(this WalletCliClient client, string blockchainName, PublishEntity<DataJson> streamItemEntity) => 
            client.PublishAsync(blockchainName, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeys(this WalletCliClient client, string blockchainName, PublishEntity<DataText> streamItemEntity) => 
            client.PublishAsync(blockchainName, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);


        // *** PublishStreamItemFrom using an inferred blockchain name

        /// <summary>
        /// Create stream item from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeyFrom(this WalletCliClient client, string fromAddress, PublishEntity streamItemEntity) => 
            client.PublishFromAsync(fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeyFrom(this WalletCliClient client, string fromAddress, PublishEntity<DataCached> streamItemEntity) => 
            client.PublishFromAsync(fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeyFrom(this WalletCliClient client, string fromAddress, PublishEntity<DataJson> streamItemEntity) =>
            client.PublishFromAsync(fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeyFrom(this WalletCliClient client, string fromAddress, PublishEntity<DataText> streamItemEntity) =>
            client.PublishFromAsync(fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);


        // *** PublishStreamItemsFrom using an inferred blockchain name

        /// <summary>
        /// Create stream item from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeysFrom(this WalletCliClient client, string fromAddress, PublishEntity streamItemEntity) => 
            client.PublishFromAsync(fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeysFrom(this WalletCliClient client, string fromAddress, PublishEntity<DataCached> streamItemEntity) => 
            client.PublishFromAsync(fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeysFrom(this WalletCliClient client, string fromAddress, PublishEntity<DataJson> streamItemEntity) => 
            client.PublishFromAsync(fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeysFrom(this WalletCliClient client, string fromAddress, PublishEntity<DataText> streamItemEntity) =>
            client.PublishFromAsync(fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);


        // *** PublishStreamItemFrom using an explicit blockchain name

        /// <summary>
        /// Create stream item from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeyFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishEntity streamItemEntity) =>
            client.PublishFromAsync(blockchainName, fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeyFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishEntity<DataCached> streamItemEntity) =>
            client.PublishFromAsync(blockchainName, fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeyFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishEntity<DataJson> streamItemEntity) => 
            client.PublishFromAsync(blockchainName, fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeyFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishEntity<DataText> streamItemEntity) =>
            client.PublishFromAsync(blockchainName, fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Key, streamItemEntity.Data, streamItemEntity.Options);


        // *** PublishStreamItemsFrom using an explicit blockchain name

        /// <summary>
        /// Create stream item from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeysFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishEntity streamItemEntity) => 
            client.PublishFromAsync(blockchainName, fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeysFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishEntity<DataCached> streamItemEntity) => 
            client.PublishFromAsync(blockchainName, fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeysFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishEntity<DataJson> streamItemEntity) => 
            client.PublishFromAsync(blockchainName, fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);

        /// <summary>
        /// Create stream item from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="streamItemEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishStreamItemKeysFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishEntity<DataText> streamItemEntity) =>
            client.PublishFromAsync(blockchainName, fromAddress, streamItemEntity.StreamIdentifer, streamItemEntity.Keys, streamItemEntity.Data, streamItemEntity.Options);


        // *** PublishMultiStreamItems using an inferred blockchain name

        /// <summary>
        /// Publish multiple stream items; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="publishMultiEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishMultiStreamItems(this WalletCliClient client, PublishMultiEntity publishMultiEntity) => 
            client.PublishMultiAsync(publishMultiEntity.StreamIdentifier, publishMultiEntity.Items, publishMultiEntity.Options);

        // *** PublishMultiStreamItems using an explicit blockchain name

        /// <summary>
        /// Publish multiple stream items; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="publishMultiEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishMultiStreamItems(this WalletCliClient client, string blockchainName, PublishMultiEntity publishMultiEntity) => 
            client.PublishMultiAsync(blockchainName, publishMultiEntity.StreamIdentifier, publishMultiEntity.Items, publishMultiEntity.Options);

        // *** PublishMultiStreamItemsFrom using an inferred blockchain name

        /// <summary>
        /// Publish multiple stream items from an address; Blockchain name is inferred
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fromAddress"></param>
        /// <param name="publishMultiEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishMultiStreamItemsFrom(this WalletCliClient client, string fromAddress, PublishMultiEntity publishMultiEntity) => 
            client.PublishMultiFromAsync(fromAddress, publishMultiEntity.StreamIdentifier, publishMultiEntity.Items, publishMultiEntity.Options);

        // *** PublishMultiStreamItemsFrom using an explicit blockchain name

        /// <summary>
        /// Publish multiple stream items from an address; Blockchain name is explicit
        /// </summary>
        /// <param name="client"></param>
        /// <param name="blockchainName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="publishMultiEntity"></param>
        /// <returns></returns>
        public static Task<CliResponse<string>> PublishMultiStreamItemsFrom(this WalletCliClient client, string blockchainName, string fromAddress, PublishMultiEntity publishMultiEntity) => 
            client.PublishMultiFromAsync(blockchainName, fromAddress, publishMultiEntity.StreamIdentifier, publishMultiEntity.Items, publishMultiEntity.Options);
    }
}
