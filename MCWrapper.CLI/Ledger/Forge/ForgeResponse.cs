using System;

namespace MCWrapper.CLI.Ledger.Forge
{
    /// <summary>
    /// Forge response handles responses from the ForgeClient
    /// </summary>
    public class ForgeResponse
    {
        /// <summary>
        /// stdout value from MultiChain Core binary
        /// </summary>
        public string Output;

        /// <summary>
        /// stderr value from MultiChain Core binary
        /// </summary>
        public string Error;

        /// <summary>
        /// Create a new ForgeResponse instance with parameters
        /// </summary>
        /// <param name="output"></param>
        /// <param name="error"></param>
        /// <param name="success"></param>
        public ForgeResponse(string output, string error)
        {
            this.Output = output;
            this.Error = error;
        }

        /// <summary>
        /// Equality override
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is ForgeResponse other &&
                   Output == other.Output &&
                   Error == other.Error;
        }

        /// <summary>
        /// Hash code override
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Output, Error);
        }

        /// <summary>
        /// Deconstruct to ValueTuple(string, string, bool)
        /// </summary>
        /// <param name="output"></param>
        /// <param name="error"></param>
        /// <param name="success"></param>
        public void Deconstruct(out string output, out string error)
        {
            output = this.Output;
            error = this.Error;
        }

        /// <summary>
        /// Implicitly convert ForgeResponse struct to ValueTuple(string, string, bool)
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator (string output, string error)(ForgeResponse value)
        {
            return (value.Output, value.Error);
        }

        /// <summary>
        /// Implicitly convert ValueTuple(string, string, bool) to ForgeResponse struct instance
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ForgeResponse((string output, string error) value)
        {
            return new ForgeResponse(value.output, value.error);
        }
    }
}
