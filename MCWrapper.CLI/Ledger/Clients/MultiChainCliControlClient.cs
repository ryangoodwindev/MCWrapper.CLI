using MCWrapper.CLI.Connection;
using MCWrapper.CLI.Options;
using MCWrapper.Data.Models.Control;
using MCWrapper.Ledger.Actions;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// 
    /// <para>MutliChain Core methods implemented by the MultiChainCliControlClient concrete class</para>
    ///
    /// clearmempool, getblockchainparams, getinfo, getruntimeparams,
    /// help, pause, resume, setlastblock, setruntimeparam, stop
    /// 
    /// </summary>
    public class MultiChainCliControlClient : MultiChainCliClient, IMultiChainCliControl
    {
        /// <summary>
        /// Create a new MultiChainCliControlClient instance
        /// </summary>
        /// <param name="options"></param>
        public MultiChainCliControlClient(IOptions<CliOptions> options)
            : base(options) { }

        /// <summary>
        /// 
        /// <para>Removes all transactions from the TX memory pool.</para>
        /// <para>Local mining and the processing of incoming transactions and blocks should be paused.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<string>> ClearMemPoolAsync(string blockchainName) =>
            TransactAsync<string>(blockchainName, ControlAction.ClearMemPoolMethod);

        /// <summary>
        /// 
        /// <para>Removes all transactions from the TX memory pool.</para>
        /// <para>Local mining and the processing of incoming transactions and blocks should be paused.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<string>> ClearMemPoolAsync() =>
            ClearMemPoolAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns a list of values of this blockchain's parameters.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="display_names">Use display names instead of internal</param>
        /// <param name="with_upgrades">Take upgrades into account</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockchainParamsResult>> GetBlockchainParamsAsync(string blockchainName, bool display_names = false, bool with_upgrades = false) =>
            TransactAsync<GetBlockchainParamsResult>(blockchainName, ControlAction.GetBlockchainParamsMethod, new[] { $"{display_names}".ToLower(), $"{with_upgrades}".ToLower() });

        /// <summary>
        /// 
        /// <para>Returns a list of values of this blockchain's parameters.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="display_names">Use display names instead of internal</param>
        /// <param name="with_upgrades">Take upgrades into account</param>
        /// <returns></returns>
        public Task<CliResponse<GetBlockchainParamsResult>> GetBlockchainParamsAsync(bool display_names = false, bool with_upgrades = false) =>
            GetBlockchainParamsAsync(CliOptions.ChainName, display_names, with_upgrades);

        /// <summary>
        /// 
        /// <para>Returns general information about this node and blockchain.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<GetInfoResult>> GetInfoAsync(string blockchainName) =>
            TransactAsync<GetInfoResult>(blockchainName, ControlAction.GetInfoMethod);

        /// <summary>
        /// 
        /// <para>Returns general information about this node and blockchain.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<GetInfoResult>> GetInfoAsync() =>
            GetInfoAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>Returns a selection of this node's runtime parameters.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<GetRuntimeParamsResult>> GetRuntimeParamsAsync(string blockchainName) =>
            TransactAsync<GetRuntimeParamsResult>(blockchainName, ControlAction.GetRuntimeParamsMethod);

        /// <summary>
        /// 
        /// <para>Returns a selection of this node's runtime parameters.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<GetRuntimeParamsResult>> GetRuntimeParamsAsync() =>
            GetRuntimeParamsAsync(CliOptions.ChainName);

        /// <summary>
        /// 
        /// <para>List all commands, or get help for a specified command.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="command">The command to get help on</param>
        /// <returns></returns>
        public Task<CliResponse<object>> HelpAsync(string blockchainName, string command = "getinfo") =>
            TransactAsync<object>(blockchainName, ControlAction.HelpMethod, new[] { command });

        /// <summary>
        /// 
        /// <para>List all commands, or get help for a specified command.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="command">The command to get help on</param>
        /// <returns></returns>
        public Task<CliResponse<object>> HelpAsync(string command = "getinfo") =>
            HelpAsync(CliOptions.ChainName, command);

        /// <summary>
        /// 
        /// <para>Pauses local mining or the processing of incoming transactions and blocks.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tasks">Task(s) to be paused. Possible values: incoming,mining,offchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> PauseAsync(string blockchainName, string tasks = "incoming,mining") =>
            TransactAsync<object>(blockchainName, ControlAction.PauseMethod, new[] { tasks });

        /// <summary>
        /// 
        /// <para>Pauses local mining or the processing of incoming transactions and blocks.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <param name="tasks">Task(s) to be paused. Possible values: incoming,mining,offchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> PauseAsync(string tasks = "incoming,mining") =>
            PauseAsync(CliOptions.ChainName, tasks);

        /// <summary>
        /// 
        /// <para>Resumes local mining or the processing of incoming transactions and blocks</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="tasks">Task(s) to be resumed. Possible values: incoming,mining,offchain</param>
        /// <returns></returns>
        public Task<CliResponse<object>> ResumeAsync(string blockchainName, string tasks = "incoming,mining") =>
            TransactAsync<object>(blockchainName, ControlAction.ResumeMethod, new[] { tasks });

        /// <summary>
        /// 
        /// <para>Resumes local mining or the processing of incoming transactions and blocks</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="tasks">Task(s) to be resumed. Possible values: incoming,mining,offchain</param>
        public Task<CliResponse<object>> ResumeAsync(string tasks = "incoming,mining") =>
            ResumeAsync(CliOptions.ChainName, tasks);

        /// <summary>
        /// 
        /// <para>Sets last block in the chain.</para>
        /// <para>Local mining and the processing of incoming transactions and blocks should be paused.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="hash_or_height">
        ///     <para>(string, optional) The block hash, if omitted - best chain is activated</para>
        ///     <para>or</para>
        ///     <para>(numeric, optional) The block height in active chain or height before current tip (if negative)</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> SetLastBlockAsync(string blockchainName, [Optional] object hash_or_height) =>
            TransactAsync<object>(blockchainName, ControlAction.SetLastBlockMethod, new[] { $"{hash_or_height}" });

        /// <summary>
        /// 
        /// <para>Sets last block in the chain.</para>
        /// <para>Local mining and the processing of incoming transactions and blocks should be paused.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="hash_or_height">
        ///     <para>(string, optional) The block hash, if omitted - best chain is activated</para>
        ///     <para>or</para>
        ///     <para>(numeric, optional) The block height in active chain or height before current tip (if negative)</para>
        /// </param>
        /// <returns></returns>
        public Task<CliResponse<object>> SetLastBlockAsync([Optional] object hash_or_height) =>
            SetLastBlockAsync(CliOptions.ChainName, hash_or_height);

        /// <summary>
        /// 
        /// <para>Sets value for runtime parameter</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        ///
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <param name="parameter_name">Parameter name, one of the following: miningrequirespeers,mineemptyrounds,miningturnover,lockadminminerounds,maxshowndata,maxqueryscanitems,bantx,lockblock,autosubscribe,handshakelocal,hideknownopdrops</param>
        /// <param name="parameter_value">parameter value</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SetRuntimeParamAsync(string blockchainName, string parameter_name, object parameter_value) =>
            TransactAsync<object>(blockchainName, ControlAction.SetRuntimeParamMethod, new[] { parameter_name, $"{parameter_value}" });

        /// <summary>
        /// 
        /// <para>Sets value for runtime parameter</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        ///
        /// </summary>
        /// <param name="parameter_name">Parameter name, one of the following: miningrequirespeers,mineemptyrounds,miningturnover,lockadminminerounds,maxshowndata,maxqueryscanitems,bantx,lockblock,autosubscribe,handshakelocal,hideknownopdrops</param>
        /// <param name="parameter_value">parameter value</param>
        /// <returns></returns>
        public Task<CliResponse<object>> SetRuntimeParamAsync(string parameter_name, object parameter_value) =>
            SetRuntimeParamAsync(CliOptions.ChainName, parameter_name, parameter_value);

        /// <summary>
        /// 
        /// <para>Shuts down the this blockchain node. Sends stop signal to MultiChain server.</para>
        /// <para>Blockchain name is explicitly passed as parameter.</para>
        /// 
        /// </summary>
        /// <param name="blockchainName">Name of target blockchain</param>
        /// <returns></returns>
        public Task<CliResponse<string>> StopAsync(string blockchainName) =>
            TransactAsync<string>(blockchainName, ControlAction.StopMethod);

        /// <summary>
        /// 
        /// <para>Shuts down the this blockchain node. Sends stop signal to MultiChain server.</para>
        /// <para>Blockchain name is inferred from CliOptions properties.</para>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CliResponse<string>> StopAsync() =>
            StopAsync(CliOptions.ChainName);
    }
}
