using System.Collections.Generic;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// Forge response handles responses from the ForgeClient
    /// </summary>
    public class ForgeResponse
    {
        /// <summary>
        /// Create a new ForgeResponse instance
        /// </summary>
        /// <param name="output"></param>
        /// <param name="error"></param>
        /// <param name="success"></param>
        public ForgeResponse() { }

        /// <summary>
        /// Create a new ForgeResponse instance with parameters
        /// </summary>
        /// <param name="output"></param>
        public ForgeResponse(string output) => StandardOutput = output;

        /// <summary>
        /// Create a new ForgeResponse instance with parameters
        /// </summary>
        /// <param name="output"></param>
        /// <param name="error"></param>
        public ForgeResponse(string output, string error)
            : this(output) => StandardError = error;

        /// <summary>
        /// Create a new ForgeResponse instance with parameters
        /// </summary>
        /// <param name="output"></param>
        /// <param name="error"></param>
        /// <param name="success"></param>
        public ForgeResponse(string output, string error, bool success)
            : this(output, error) => Success = success;

        /// <summary>
        /// stdout value from MultiChain Core binary
        /// </summary>
        public string StandardOutput 
        {
            get => output; 
            set => output = value; 
        }
        private string output = string.Empty;

        /// <summary>
        /// stderr value from MultiChain Core binary
        /// </summary>
        public string StandardError
        {
            get => error;
            set => error = value;
        }
        private string error = string.Empty;

        /// <summary>
        /// Was action successful
        /// </summary>
        public bool Success 
        { 
            get => success; 
            set => success = value; 
        }
        private bool success = false;
     
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Errors 
        { 
            get => errors; 
            set => errors = value;
        }
        private Dictionary<string, string> errors = new Dictionary<string, string>();
    }
}
