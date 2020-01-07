namespace MCWrapper.CLI.Connection
{
    /// <summary>
    /// This is the CLI client Response object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CliResponse<T>
    {
        #pragma warning disable CS8618 // Non-nullable field is uninitialized.
        /// <summary>
        /// This is the CLI client Response object
        /// </summary>
        public CliResponse()
        #pragma warning restore CS8618 // Non-nullable field is uninitialized.
        {
            Error = string.Empty;
            Request = new CLIRequest();
        }

        /// <summary>
        /// This is the CLI client Response object
        /// </summary>
        /// <param name="error"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public CliResponse(string error, CLIRequest request, T response)
        {
            Error = error;
            Request = request;
            Result = response;
        }

        /// <summary>
        /// If error is detected in stdout the message is rerouted to the Error property
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// stdout output from multichain-cli.exe
        /// </summary>
        public CLIRequest Request { get; set; }

        /// <summary>
        /// stderr output from multichain-cli.exe
        /// </summary>
        public T Result { get; set; }
    }

    /// <summary>
    /// This is the CLI client Response object
    /// </summary>
    public class CliResponse
    {
        /// <summary>
        /// This is the CLI client Response object
        /// </summary>
        public CliResponse()
        {
            Error = string.Empty;
            Request = new CLIRequest();
            Result = new { };
        }

        /// <summary>
        /// This is the CLI client Response object
        /// </summary>
        /// <param name="error"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public CliResponse(string error, CLIRequest request, object response)
        {
            Error = error;
            Request = request;
            Result = response;
        }

        /// <summary>
        /// If error is detected in stdout the message is rerouted to the Error property
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// stdout output from multichain-cli.exe
        /// </summary>
        public CLIRequest Request { get; set; }

        /// <summary>
        /// stderr output from multichain-cli.exe
        /// </summary>
        public object Result { get; set; }
    }
}
