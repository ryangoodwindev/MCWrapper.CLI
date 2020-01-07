using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Options;
using MCWrapper.Data.Models.Utility;
using MCWrapper.Ledger.Actions;
using MCWrapper.Ledger.Entities.Extensions;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods implemented by the MultiChainCliUtilityClient concrete class</para>
    ///
    /// appendbinarycache, createbinarycache, createkeypairs,
    /// createmultisig, deletebinarycache, estimatefee, estimatepriority,
    /// validateaddress, verifymessage
    /// 
    /// </summary>
    public class MultiChainCliUtilityClient : MultiChainCliClient, IMultiChainCliUtility
    {
        /// <summary>
        /// Create a new UtilityCLIClient instance with parameters
        /// 
        /// <para>
        ///     MutliChain methods implemented:
        ///     appendbinarycache, createbinarycache, createkeypairs, 
        ///     createmultisig, deletebinarycache, estimatefee, estimatepriority,
        ///     validateaddress, verifymessage
        /// </para>
        /// </summary>
        /// <param name="options"></param>
        public MultiChainCliUtilityClient(IOptions<CliOptions> options)
            : base(options) { }

        /// <summary>
        /// 
        /// <para>Appends data to binary cache.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="identifier">Binary cache item identifier</param>
        /// <param name="data_hex">The hex string to be added to binary cache item</param>
        /// <returns></returns>
        public Task<CliResponse<int>> AppendBinaryCacheAsync(string blockchainName, string identifier, string data_hex) =>
            TransactAsync<int>(blockchainName, UtilityAction.AppendBinaryCacheMethod, new[] { identifier, data_hex });

        /// <summary>
        /// 
        /// <para>Appends data to binary cache.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="identifier">Binary cache item identifier</param>
        /// <param name="data_hex">The hex string to be added to binary cache item</param>
        public Task<CliResponse<int>> AppendBinaryCacheAsync(string identifier, string data_hex) =>
            AppendBinaryCacheAsync(CliOptions.ChainName, identifier, data_hex);

        /// <summary>
        /// 
        /// <para>Returns random string, which can be used as binary cache item identifier</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<string>> CreateBinaryCacheAsync(string blockchainName) =>
            TransactAsync<string>(blockchainName, UtilityAction.CreateBinaryCacheMethod);

        /// <summary>
        /// 
        /// <para>Returns random string, which can be used as binary cache item identifier</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<string>> CreateBinaryCacheAsync() =>
            CreateBinaryCacheAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Creates public/private key pairs. These key pairs are not stored in the wallet.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="count">Number of key pairs to generate</param>
        /// <returns></returns>
        public Task<CliResponse<CreateKeyPairsResult[]>> CreateKeyPairsAsync(string blockchainName, int count = 1) =>
            TransactAsync<CreateKeyPairsResult[]>(blockchainName, UtilityAction.CreateKeyPairsMethod, new[] { $"{count}" });

        /// <summary>
        /// 
        /// <para>Creates public/private key pairs. These key pairs are not stored in the wallet.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="count">Number of key pairs to generate</param>
        /// <returns></returns>
        public Task<CliResponse<CreateKeyPairsResult[]>> CreateKeyPairsAsync(int count = 1) =>
            CreateKeyPairsAsync(CliOptions.ChainName, count);

        /// <summary>
        /// 
        /// <para>Creates a multi-signature address with n signature of m keys required.</para>
        /// <para>It returns a json object with the address and redeemScript.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="n_required">The number of required signatures out of the n keys or addresses</param>
        /// <param name="keys">A json array of keys which are addresses or hex-encoded public keys</param>
        /// <returns></returns>
        public Task<CliResponse<CreateMultiSigResult>> CreateMultiSigAsync(string blockchainName, int n_required, string[] keys) =>
            TransactAsync<CreateMultiSigResult>(blockchainName, UtilityAction.CreateMultiSigMethod, new[] { $"{n_required}", keys.Serialize() });

        /// <summary>
        /// 
        /// <para>Creates a multi-signature address with n signature of m keys required.</para>
        /// <para>It returns a json object with the address and redeemScript.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="n_required">The number of required signatures out of the n keys or addresses</param>
        /// <param name="keys">A json array of keys which are addresses or hex-encoded public keys</param>
        /// <returns></returns>
        public Task<CliResponse<CreateMultiSigResult>> CreateMultiSigAsync(int n_required, string[] keys) =>
            CreateMultiSigAsync(CliOptions.ChainName, n_required, keys);

        /// <summary>
        /// 
        /// <para>Clear binary cache item</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="identifier">Binary cache item identifier, "*" - to clear all items</param>
        /// <returns></returns>
        public Task<CliResponse> DeleteBinaryCacheAsync(string blockchainName, string identifier) =>
            TransactAsync(blockchainName, UtilityAction.DeleteBinaryCacheMethod, new[] { identifier });

        /// <summary>
        /// 
        /// <para>Clear binary cache item</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="identifier">Binary cache item identifier, "*" - to clear all items</param>
        /// <returns></returns>
        public Task<CliResponse> DeleteBinaryCacheAsync(string identifier) =>
            DeleteBinaryCacheAsync(CliOptions.ChainName, identifier);

        /// <summary>
        /// 
        /// <para>Estimates the approximate fee per kilobyte needed for a transaction to begin confirmation within nblocks blocks.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="n_blocks">Number of blocks to estimate fee for</param>
        /// <returns></returns>
        public Task<CliResponse<long>> EstimateFeeAsync(string blockchainName, int n_blocks) =>
            TransactAsync<long>(blockchainName, UtilityAction.EstimateFeeMethod, new[] { $"{n_blocks}" });

        /// <summary>
        /// 
        /// <para>Estimates the approximate fee per kilobyte needed for a transaction to begin confirmation within nblocks blocks.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="n_blocks">Number of blocks to estimate fee for</param>
        /// <returns></returns>
        public Task<CliResponse<long>> EstimateFeeAsync(int n_blocks) =>
            EstimateFeeAsync(CliOptions.ChainName, n_blocks);

        /// <summary>
        /// 
        /// <para>Estimates the approximate priority  a zero-fee transaction needs to begin confirmation within nblocks blocks.</para>
        /// <para>-1.0 is returned if not enough transactions and blocks have been observed to make an estimate.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="n_blocks">Number of blocks to estimate priority for</param>
        /// <returns></returns>
        public Task<CliResponse<float>> EstimatePriorityAsync(string blockchainName, int n_blocks) =>
            TransactAsync<float>(blockchainName, UtilityAction.EstimatePriorityMethod, new[] { $"{n_blocks}" });

        /// <summary>
        /// 
        /// <para>Estimates the approximate priority  a zero-fee transaction needs to begin confirmation within nblocks blocks.</para>
        /// <para>-1.0 is returned if not enough transactions and blocks have been observed to make an estimate.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="n_blocks">Number of blocks to estimate priority for</param>
        /// <returns></returns>
        public Task<CliResponse<float>> EstimatePriorityAsync(int n_blocks) =>
            EstimatePriorityAsync(CliOptions.ChainName, n_blocks);

        /// <summary>
        /// 
        /// <para>Return information about the given address or public key or private key.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address_pubkey_privkey">
        ///     <para>1. "address" (string, required) The address to validate</para>
        ///     <para>or</para>
        ///     <para>1. "pubkey" (string, required) The public key (hexadecimal) to validate</para>
        ///     <para>or</para>
        ///     <para>1. "privkey" (string, required) The private key (see dumpprivkey) to validate</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<ValidateAddressResult>> ValidateAddressAsync(string blockchainName, string address_pubkey_privkey) =>
            TransactAsync<ValidateAddressResult>(blockchainName, UtilityAction.ValidateAddressMethod, new[] { address_pubkey_privkey });

        /// <summary>
        /// 
        /// <para>Return information about the given address or public key or private key.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="address_pubkey_privkey">
        ///     <para>1. "address" (string, required) The address to validate</para>
        ///     <para>or</para>
        ///     <para>1. "pubkey" (string, required) The public key (hexadecimal) to validate</para>
        ///     <para>or</para>
        ///     <para>1. "privkey" (string, required) The private key (see dumpprivkey) to validate</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<ValidateAddressResult>> ValidateAddressAsync(string address_pubkey_privkey) =>
            ValidateAddressAsync(CliOptions.ChainName, address_pubkey_privkey);

        /// <summary>
        /// 
        /// <para>Verify a signed message</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="address">The address to use for the signature</param>
        /// <param name="signature">The signature provided by the signer in base 64 encoding (see signmessage)</param>
        /// <param name="message">The message that was signed</param>
        /// <returns></returns>
        public Task<CliResponse<bool>> VerifyMessageAsync(string blockchainName, string address, string signature, string message) =>
            TransactAsync<bool>(blockchainName, UtilityAction.VerifyMessageMethod, new[] { address, signature, message.Replace(' ', '_') });

        /// <summary>
        /// 
        /// <para>Verify a signed message</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="address">The address to use for the signature</param>
        /// <param name="signature">The signature provided by the signer in base 64 encoding (see signmessage)</param>
        /// <param name="message">The message that was signed</param>
        /// <returns></returns>
        public Task<CliResponse<bool>> VerifyMessageAsync(string address, string signature, string message) =>
            VerifyMessageAsync(CliOptions.ChainName, address, signature, message);
    }
}
