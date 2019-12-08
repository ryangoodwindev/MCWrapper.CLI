using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCWrapper.CLI.Helpers
{
    /// <summary>
    /// Command line options that can be passed as arguments to multichain-cli.exe
    /// </summary>
    public class CliArgumentHelper
    {
        /// <summary>
        /// Specify configuration file (default: multichain.conf)
        /// </summary>
        [Display(Name = "-conf=")]
        public string Conf { get; set; } = string.Empty;

        /// <summary>
        /// Specify data directory
        /// </summary>
        [Display(Name = "-datadir=")]
        public string DataDir { get; set; } = string.Empty;

        /// <summary>
        /// Send request to stderr, stdout or null (not print it at all), default stderr
        /// </summary>
        [Display(Name = "-requestout=")]
        public string RequestOut { get; set; } = string.Empty;

        /// <summary>
        /// If n=0 multichain-cli history is not saved, default 1
        /// </summary>
        [Display(Name = "-saveclilog=")]
        public string SaveCliLog { get; set; } = string.Empty;

        /// <summary>
        /// Send commands to node running on IP (default: 127.0.0.1)
        /// </summary>
        [Display(Name = "-rpcconnect=")]
        public string RpcConnect { get; set; } = string.Empty;

        /// <summary>
        /// Connect to JSON-RPC on -port-
        /// </summary>
        [Display(Name = "-rpcport=")]
        public string RpcPort { get; set; } = string.Empty;

        /// <summary>
        /// Username for JSON-RPC connections
        /// </summary>
        [Display(Name = "-rpcuser=")]
        public string RpcUser { get; set; } = string.Empty;

        /// <summary>
        /// Password for JSON-RPC connections
        /// </summary>
        [Display(Name = "-rpcpassword=")]
        public string RpcPassword { get; set; } = string.Empty;

        /// <summary>
        /// Indicates to CliClient whether this is a cold node or not
        /// </summary>
        public bool IsColdNode { get; set; }

        /// <summary>
        /// Indicates to CliClient whether to wait for RPC server to start or not
        /// </summary>
        public bool UseRpcWait { get; set; }

        /// <summary>
        /// Indicates to CliClient whether or not to use OpenSSL (https) for JSON-RPC connections
        /// </summary>
        public bool UseRpcssl { get; set; }

        /// <summary>
        /// Returns a string that represents the current object as a properly formatted string of command line switches and arguments
        /// usable for passing to multichain-cli.exe
        /// </summary>
        /// <returns></returns>
        internal string ToString(string blockchainName)
        {
            var formatted = new StringBuilder();

            if (IsColdNode)
                formatted.Append($"{ColdNodeSwitch} ");

            if (UseRpcssl)
                formatted.Append($"{RpcSslSwitch} ");

            if (UseRpcWait)
                formatted.Append($"{RpcWaitSwitch} ");

            if (!string.IsNullOrEmpty(Conf))
                formatted.Append($"{nameof(Conf)}{Conf} ");

            if (!string.IsNullOrEmpty(DataDir))
                formatted.Append($"{nameof(DataDir)}{DataDir} ");

            if (!string.IsNullOrEmpty(RequestOut))
                formatted.Append($"{nameof(RequestOut)}{RequestOut} ");

            if (!string.IsNullOrEmpty(SaveCliLog))
                formatted.Append($"{nameof(SaveCliLog)}{SaveCliLog} ");

            if (!string.IsNullOrEmpty(RpcConnect))
                formatted.Append($"{nameof(RpcConnect)}{RpcConnect} ");

            if (!string.IsNullOrEmpty(RpcPort))
                formatted.Append($"{nameof(RpcPort)}{RpcPort} ");

            if (!string.IsNullOrEmpty(RpcUser))
                formatted.Append($"{nameof(RpcUser)}{RpcUser} ");

            if (!string.IsNullOrEmpty(RpcPassword))
                formatted.Append($"{nameof(RpcPassword)}{RpcPassword} ");

            formatted.Append($"{blockchainName} ");

            return formatted.ToString();
        }

        /// <summary>
        /// This is a new collection property that we are using witht he ArgumentList option when starting a new Process.
        /// Ideally this will help with supporting both Linux and Windows environments in a more robust/reliable fashion.
        /// </summary>
        /// <returns></returns>
        internal List<string> ToList(string blockchainName)
        {
            var argumentList = new List<string>();

            if (IsColdNode)
                argumentList.Add(ColdNodeSwitch);

            if (UseRpcssl)
                argumentList.Add(RpcSslSwitch);

            if (UseRpcWait)
                argumentList.Add(RpcWaitSwitch);

            if (!string.IsNullOrEmpty(Conf))
                argumentList.Add($"{nameof(Conf)}{Conf}");

            if (!string.IsNullOrEmpty(DataDir))
                argumentList.Add($"{nameof(DataDir)}{DataDir}");

            if (!string.IsNullOrEmpty(RequestOut))
                argumentList.Add($"{nameof(RequestOut)}{RequestOut}");

            if (!string.IsNullOrEmpty(SaveCliLog))
                argumentList.Add($"{nameof(SaveCliLog)}{SaveCliLog}");

            if (!string.IsNullOrEmpty(RpcConnect))
                argumentList.Add($"{nameof(RpcConnect)}{RpcConnect}");

            if (!string.IsNullOrEmpty(RpcPort))
                argumentList.Add($"{nameof(RpcPort)}{RpcPort}");

            if (!string.IsNullOrEmpty(RpcUser))
                argumentList.Add($"{nameof(RpcUser)}{RpcUser}");

            if (!string.IsNullOrEmpty(RpcPassword))
                argumentList.Add($"{nameof(RpcPassword)}{RpcPassword}");

            argumentList.Add(blockchainName);

            return argumentList;
        }

        /// <summary>
        /// Return Help CLI swtich
        /// </summary>
        /// <returns></returns>
        public string HelpArgument => Help;
        private const string Help = "help";

        /// <summary>
        /// Return Cold Node CLI switch
        /// </summary>
        /// <returns></returns>
        public string ColdNodeSwitch => Cold;
        private const string Cold = "-cold";

        /// <summary>
        /// Return RpcSsl CLI switch
        /// </summary>
        /// <returns></returns>
        public string RpcSslSwitch => RpcSsl;
        private const string RpcSsl = "-rpcssl";

        /// <summary>
        /// Return RpcWait CLI switch
        /// </summary>
        /// <returns></returns>
        public string RpcWaitSwitch => RpcWait;
        private const string RpcWait = "-rpcwait";
    }
}
