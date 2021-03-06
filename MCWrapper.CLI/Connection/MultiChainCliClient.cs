﻿using MCWrapper.CLI.Helpers;
using MCWrapper.CLI.Helpers.ErrorHandling;
using MCWrapper.CLI.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Connection
{
    /// <summary>
    /// Defines Command Line Interface Process client
    /// </summary>
    public abstract class MultiChainCliClient
    {
        /// <summary>
        /// Create a new CliClient instance;
        /// Inject arguments to multichain-cli.exe and receive a string response
        /// </summary>
        /// <param name="cliOptions"></param>
        protected MultiChainCliClient(IOptions<CliOptions> cliOptions) =>
            CliOptions = cliOptions.Value;

        /// <summary>
        /// MultiChain CLI options
        /// </summary>
        public CliOptions CliOptions { get; set; }

        /// <summary>
        /// Send commands to MultiChain Core
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="cliOptions"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<CliResponse> TransactAsync(string blockchainName, string methodName, string[]? parameters = null, CliArgumentHelper? cliOptions = null) =>
            Task.Run(() => Transact(blockchainName, methodName, parameters, cliOptions));

        /// <summary>
        /// Private member that handles work
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <param name="cliArguments"></param>
        /// <returns></returns>
        private CliResponse Transact(string blockchainName, string methodName, string[]? parameters, CliArgumentHelper? cliArguments)
        {
            // throw exception on no blockchain name
            if (string.IsNullOrEmpty(blockchainName)) throw new BlockchainNameException();

            cliArguments ??= new CliArgumentHelper();
            parameters ??= Array.Empty<string>();

            try
            {
                using var process = new Process();

                // populate blockchain name and any additional CLI arguments before the methodName is added
                foreach (var argument in cliArguments.ToList(blockchainName))
                    process.StartInfo.ArgumentList.Add(argument);

                // add methodName to argument list
                process.StartInfo.ArgumentList.Add(methodName);

                // finish up argument list with any arguments we want to pass to the remote methodName
                foreach (var parameter in parameters.ToList())
                {
                    if (parameter != null)
                        process.StartInfo.ArgumentList.Add(parameter);
                }

                // An empty or null ChainBinaryLocation occurs under the following conditions:
                //  1. No "ChainBinaryLocation" environment variable detected in the local environment.
                //  2. No "ChainBinaryLocation" value present on the appsettings.json file.
                //  3. No "ChainBinaryLocation" value explicitly passed during 'AddMultiChainCoreCliServices' or 'AddMultiChainCoreServices' configuration
                //
                //  If any of the above scenarios are occurring then we will try to use the default locations according to the Operating System in use.
                //      - Windows..: C:\ or C:\multichain
                //      - Linux....: /usr/local/bin
                process.StartInfo.FileName = MultiChainPathHelper.GetMultiChainCliExePath(CliOptions.ChainBinaryLocation);

                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;

                var stderr = new StringBuilder();
                var stdout = new StringBuilder();

                process.ErrorDataReceived += (sender, args) =>
                {
                    var cast = args as DataReceivedEventArgs;
                    stderr.Append(cast.Data);
                };

                process.OutputDataReceived += (sender, args) =>
                {
                    var cast = args as DataReceivedEventArgs;
                    stdout.Append(cast.Data);
                };

                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                process.WaitForExit();

                var _stderr = stderr.ToString();
                var _stdout = stdout.ToString().TrimEnd();

                // multichain-cli.exe response model
                var clientResponse = new CliResponse();

                // detect error occurrence
                if (_stderr.Contains("error"))
                {
                    // assign CliResponse Error property value to the CLIClientResponse Error property
                    clientResponse.Error = _stderr;
                }
                else
                {
                    // deserialize information about the request we sent to multichain-cli.exe
                    clientResponse.Request = JsonConvert.DeserializeObject<CLIRequest>(_stderr);

                    try
                    {
                        // if any exception occurs we assume that cliResponse.ResponseObject is a plain string
                        // in catch phrase we will try to dynamically serialize the plain string and then will
                        // attempt to deserialize the dynamic object as Type T
                        clientResponse.Result = JsonConvert.DeserializeObject(_stdout) ?? new { };
                    }
                    catch (Exception)
                    {
                        // switch on string Type
                        switch (_stdout)
                        {
                            case string s:
                                // this is a necessary workaround since multichain-cli.exe passes back plain string values versus JSON formatted strings
                                // we serialize the plain string to JSON if we detect a String Type and then deserialize that value to a JSON object
                                var parsedJson = JsonConvert.SerializeObject(s);
                                clientResponse.Result = JsonConvert.DeserializeObject(parsedJson) ?? new { };
                                break;

                            // if we default then a 'response' is returned with no values having been set for the ResponseObject. 
                            // This permits the subscriber to view any errors returned by multichain-cli.exe and present in the Response portion of the request
                            default: break;
                        }
                    }
                }

                return clientResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Send commands to MultiChain Core
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="cliOptions"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<CliResponse<T>> TransactAsync<T>(string blockchainName, string methodName, string[]? parameters = null, CliArgumentHelper? cliOptions = null) =>
            Task.Run(() => Transact<T>(blockchainName, methodName, parameters, cliOptions));

        /// <summary>
        /// Private member that handles work
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="blockchainName"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <param name="cliArguments"></param>
        /// <returns></returns>
        private CliResponse<T> Transact<T>(string blockchainName, string methodName, string[]? parameters, CliArgumentHelper? cliArguments)
        {
            // throw exception on no blockchain name
            if (string.IsNullOrEmpty(blockchainName)) throw new BlockchainNameException();

            cliArguments ??= new CliArgumentHelper();
            parameters ??= Array.Empty<string>();

            try
            {
                using var process = new Process();

                // populate blockchain name and any additional CLI arguments before the methodName is added
                foreach (var argument in cliArguments.ToList(blockchainName))
                    process.StartInfo.ArgumentList.Add(argument);

                // add methodName to argument list
                process.StartInfo.ArgumentList.Add(methodName);

                // finish up argument list with any arguments we want to pass to the remote methodName
                foreach (var parameter in parameters.ToList())
                {
                    if (parameter != null)
                        process.StartInfo.ArgumentList.Add(parameter);
                }

                // An empty or null ChainBinaryLocation occurs under the following conditions:
                //  1. No "ChainBinaryLocation" environment variable detected in the local environment.
                //  2. No "ChainBinaryLocation" value present on the appsettings.json file.
                //  3. No "ChainBinaryLocation" value explicitly passed during 'AddMultiChainCoreCliServices' or 'AddMultiChainCoreServices' configuration
                //
                //  If any of the above scenarios are occurring then we will try to use the default locations according to the Operating System in use.
                //      - Windows..: C:\ or C:\multichain
                //      - Linux....: /usr/local/bin
                process.StartInfo.FileName = MultiChainPathHelper.GetMultiChainCliExePath(CliOptions.ChainBinaryLocation);

                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;

                var stderr = new StringBuilder();
                var stdout = new StringBuilder();

                process.ErrorDataReceived += (sender, args) =>
                {
                    var cast = args as DataReceivedEventArgs;
                    stderr.Append(cast.Data);
                };

                process.OutputDataReceived += (sender, args) =>
                {
                    var cast = args as DataReceivedEventArgs;
                    stdout.Append(cast.Data);
                };

                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                process.WaitForExit();

                var _stderr = stderr.ToString();
                var _stdout = stdout.ToString().TrimEnd();

                // multichain-cli.exe response model
                var clientResponse = new CliResponse<T>();

                // detect error occurrence
                if (_stderr.Contains("error"))
                {
                    // assign CliResponse Error property value to the CLIClientResponse Error property
                    clientResponse.Error = _stderr;
                }
                else
                {
                    // deserialize information about the request we sent to multichain-cli.exe
                    clientResponse.Request = JsonConvert.DeserializeObject<CLIRequest>(_stderr);

                    try
                    {
                        // if any exception occurs we assume that cliResponse.ResponseObject is a plain string
                        // in catch phrase we will try to dynamically serialize the plain string and then will
                        // attempt to deserialize the dynamic object as Type T
                        clientResponse.Result = JsonConvert.DeserializeObject<T>(_stdout);
                    }
                    catch (Exception)
                    {
                        // switch on string Type
                        switch (_stdout)
                        {
                            case string s:
                                // this is a necessary workaround since multichain-cli.exe passes back plain string values versus JSON formatted strings
                                // we serialize the plain string to JSON if we detect a String Type and then deserialize that value to a JSON object
                                var parsedJson = JsonConvert.SerializeObject(s);
                                clientResponse.Result = JsonConvert.DeserializeObject<T>(parsedJson);
                                break;

                            // if we default then a 'response' is returned with no values having been set for the ResponseObject. 
                            // This permits the subscriber to view any errors returned by multichain-cli.exe and present in the Response portion of the request
                            default: break;
                        }
                    }
                }

                return clientResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}