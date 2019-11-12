﻿using System;

namespace MCWrapper.CLI.Helpers.ErrorHandling
{
    public class BlockchainNameException : Exception
    {
        private const string _message = "There is no blockchain name detected with your request. 1.) Explicitly pass a blockchain name. 2.) Add 'ChainName' key/value to Environment Variable Store or 3.) Add 'ChainName' key/value to appsettings.json. Options 2 and 3 allow for use of inferred blockchain name methods by auto detecting the 'ChainName' key/value.";

        public BlockchainNameException() 
            : base(_message) { }

        public BlockchainNameException(string message) : base(message)
        {
        }

        public BlockchainNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}