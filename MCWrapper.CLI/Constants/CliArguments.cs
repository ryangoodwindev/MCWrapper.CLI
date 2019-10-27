using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCWrapper.CLI.Constants
{
    /// <summary>
    /// Command line options that can be passed as arguments to multichain-cli.exe
    /// </summary>
    public class CliArguments
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
        /// Return Help CLI swtich
        /// </summary>
        /// <returns></returns>
        public string HelpArgument => Help;

        /// <summary>
        /// Return Cold Node CLI switch
        /// </summary>
        /// <returns></returns>
        public string ColdNodeSwitch => Cold;

        /// <summary>
        /// Return RpcSsl CLI switch
        /// </summary>
        /// <returns></returns>
        public string RpcSslSwitch => RpcSsl;

        /// <summary>
        /// Return RpcWait CLI switch
        /// </summary>
        /// <returns></returns>
        public string RpcWaitSwitch => RpcWait;

        // ! Helpers

        /// <summary>
        /// Generate help menu
        /// </summary>
        private const string Help = "help";

        /// <summary>
        /// Connect to multichaind-cold: use multichaind-cold default directory if -datadir is not set
        /// </summary>
        private const string Cold = "-cold";

        /// <summary>
        /// Use OpenSSL (https) for JSON-RPC connections
        /// </summary>
        private const string RpcSsl = "-rpcssl";

        /// <summary>
        /// Wait for RPC server to start
        /// </summary>
        private const string RpcWait = "-rpcwait";
    }
}
