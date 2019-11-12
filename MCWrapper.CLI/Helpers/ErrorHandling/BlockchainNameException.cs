using System;

namespace MCWrapper.CLI.Helpers.ErrorHandling
{
    /// <summary>
    /// Custom blockchain name exception for handling the absence of a blockchain name parameter in the CliClient class
    /// </summary>
    public class BlockchainNameException : Exception
    {
        /// <summary>
        /// Custom message
        /// </summary>
        private const string _message = "There is no blockchain name detected with your request. 1.) Explicitly pass a blockchain name. 2.) Add 'ChainName' key/value to Environment Variable Store or 3.) Add 'ChainName' key/value to appsettings.json. Options 2 and 3 allow for use of inferred blockchain name methods by auto detecting the 'ChainName' key/value.";

        /// <summary>
        /// Parameterless constructor uses private field value to set message
        /// </summary>
        public BlockchainNameException() 
            : base(_message) { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="message"></param>
        public BlockchainNameException(string message) 
            : base(message) { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BlockchainNameException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
