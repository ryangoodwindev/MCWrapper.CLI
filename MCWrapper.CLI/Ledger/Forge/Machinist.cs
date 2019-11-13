using MCWrapper.CLI.Constants;
using MCWrapper.CLI.Helpers;
using MCWrapper.CLI.Options;
using MCWrapper.Ledger.Entities.ErrorHandling;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCWrapper.CLI.Ledger.Clients
{
    /// <summary>
    /// Machinist interacts with MultiChain binary files directly and monitors their reponses while reacting accordingly
    /// </summary>
    public class Machinist
    {
        /// <summary>
        /// Create a new Machinist instance
        /// </summary>
        public Machinist(IOptions<CliOptions> cliOptions) => 
            CliOptions = cliOptions.Value;

        /// <summary>
        /// Command Line Interface option values
        /// </summary>
        public CliOptions CliOptions { get; }

        /// <summary>
        /// Create a new MultiChain blockchain ledger according to blockchain name
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> CreateBlockchainAsync(string blockchainName) => 
            Task.Run(() => CreateBlockChain(blockchainName));

        /// <summary>
        /// Create a new MultiChain blockchain ledger according to blockchain name
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        private ForgeResponse CreateBlockChain(string blockchainName)
        {
            // disposable Process
            using var process = new Process();

            // set filename to multichain-util
            process.StartInfo.FileName = MultiChainPathHelper.GetMultiChainUtilExePath(CliOptions.ChainBinaryLocation);

            // populate argument list
            process.StartInfo.ArgumentList.Add(RuntimeCommand.Create);
            process.StartInfo.ArgumentList.Add(blockchainName);

            // configure Process behavior
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            // storage for sdtout and stderr
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            // set data received delegate
            process.ErrorDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                stderr.Append(cast.Data);
            };

            // set data received delegate
            process.OutputDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                stdout.Append(cast.Data);
            };

            // kick-off Process
            process.Start();

            // read err and out
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            // ensure Process has completed
            process.WaitForExit();

            // set response
            var response = new ForgeResponse(stdout.ToString(), stderr.ToString());

            // determine success
            // set errors if necessary
            if (ResponseIsSuccess())
                response.Success = true;
            else if (ResponseIsFailure())
                response.Errors.Add("Response_Failed_Reason_Exists", 
                    $"Sorry, it seems that a blockchain with this name might already exists: {blockchainName}");
            else
                response.Errors.Add("Response_Failed_Reason_None", 
                    "Sorry, we can't determine if this attempt failed or not so we assume it did fail.");

            return response;

            // determine response success
            bool ResponseIsSuccess() => response.StandardOutput.Contains("Blockchain parameter set was successfully generated.", StringComparison.OrdinalIgnoreCase)
                && response.StandardOutput.Contains("To generate blockchain please run", StringComparison.OrdinalIgnoreCase)
                && response.StandardOutput.Contains("-daemon", StringComparison.OrdinalIgnoreCase);

            // determine response failure
            bool ResponseIsFailure() => response.StandardError.Equals("ERROR: Blockchain parameter set was not generated.", StringComparison.OrdinalIgnoreCase)
                && response.StandardOutput.Contains("Cannot create chain parameter set, file", StringComparison.OrdinalIgnoreCase)
                && response.StandardOutput.Contains("params.dat already exists", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Start an existing blockchain according to name
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="useSsl"></param>
        /// <param name="runtimeCommands"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StartBlockchainAsync(string blockchainName, bool useSsl, string runtimeCommands) =>
            Task.Run(() => StartBlockchain(blockchainName, useSsl, runtimeCommands));

        /// <summary>
        /// Start an existing blockchain according to name
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="useSsl"></param>
        /// <param name="runtimeCommands"></param>
        /// <returns></returns>
        private ForgeResponse StartBlockchain(string blockchainName, bool useSsl, string runtimeCommands)
        {
            // disposable Process
            using var process = new Process();

            // set file name to multichaind
            process.StartInfo.FileName = MultiChainPathHelper.GetMultiChainDExePath(CliOptions.ChainBinaryLocation);

            // populate argument list
            process.StartInfo.ArgumentList.Add(blockchainName);
            if (useSsl)
                process.StartInfo.ArgumentList.Add(RuntimeCommand.UseRpcSSL);
            process.StartInfo.ArgumentList.Add(runtimeCommands);
            process.StartInfo.ArgumentList.Add(RuntimeCommand.Daemon);

            // configure Process behavior
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            // stdout & stderr StringBuilders are accessed by multiple threads
            // lock objects to prevent exception and cross threading.
            object stdoutLock = new object();
            object stderrLock = new object();

            // storage for sdtout and stderr
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            // set data received delegate
            process.ErrorDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                AddStdErr(cast.Data);
            };

            // set data received delegate
            process.OutputDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                AddStdOut(cast.Data);
            };

            // kick-off Process
            process.Start();

            // read err and out
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            // configure timer and ticker
            var maxCounter = 40;
            var timerState = new GeneralTimerState();
            var waitForStart = new Timer(
                callback: WaitForStartCallback,
                state: timerState,
                dueTime: 0,
                period: 250);

            // initialize a new response
            var response = new ForgeResponse();

            // timer handles Process stdout & sdterr content since multichaind doesn't exit in windows
            // we detect and handle failure or success based on the stdout & stderr
            while (!timerState.TimerJobSuccess && timerState.Counter <= maxCounter)
            {
                if (ResponseIsSuccess())
                {
                    response.Success = true;
                    timerState.TimerJobSuccess = true;
                }
                else if (ResponseIsFailure())
                {
                    timerState.TimerJobSuccess = true;
                    response.Errors.Add("Response_Failed_Reason_Exists",
                        $"Sorry, it seems that a blockchain with this name might already be running in this environment: {blockchainName}");
                }
            }

            // force Process exit after 1 millisecond
            process.WaitForExit(1);

            // set response properties
            response.StandardError = GetStdErr();
            response.StandardOutput = GetStdOut();

            // determine if no result is detected
            if (timerState.Counter >= maxCounter && response.Errors.Count == 0 && !response.Success)
                response.Errors.Add("Response_Failed_Reason_Unknown", $"Sorry, it seems that something went wrong while starting blockchain: {blockchainName}");

            return response;

            // lock for multiple thread access
            string GetStdOut()
            {
                lock (stdoutLock)
                    return stdout.ToString();
            }

            // lock for multiple thread access
            string GetStdErr()
            {
                lock (stderrLock)
                    return stderr.ToString();
            }

            // lock for multiple thread access
            void AddStdOut(string phrase)
            {
                lock (stdoutLock)
                    stdout.Append(phrase);
            }

            // lock for multiple thread access
            void AddStdErr(string phrase)
            {
                lock (stderrLock)
                    stderr.Append(phrase);
            }

            // determine response success
            bool ResponseIsSuccess()
            {
                var output = GetStdOut();
                return output.Contains("Other nodes can connect to this node using:", StringComparison.OrdinalIgnoreCase)
                    && output.Contains("multichaind", StringComparison.OrdinalIgnoreCase)
                    && output.Contains("Listening for API requests on port", StringComparison.OrdinalIgnoreCase)
                    && output.Contains("Node ready.", StringComparison.OrdinalIgnoreCase);
            }

            // determine response failure
            bool ResponseIsFailure()
            {
                var error = GetStdErr();
                return error.Equals("ERROR: Couldn't initialize permission database for blockchain EntropyChain. Probably multichaind for this blockchain is already running. Exiting...", StringComparison.OrdinalIgnoreCase);
            }

            // Timer callback
            static void WaitForStartCallback(object? state)
            {
                var timerState = state as GeneralTimerState
                    ?? throw new ArgumentNullException("GeneralTimerState is null");

                Interlocked.Increment(ref timerState.Counter);
            }
        }

        /// <summary>
        /// Start a blockchain cold node
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StartColdNodeAsync(string blockchainName) =>
            Task.Run(() => StartColdNode(blockchainName));

        /// <summary>
        /// Start a blockchain cold node
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        private ForgeResponse StartColdNode(string blockchainName)
        {
            if (!Directory.Exists(MultiChainPathHelper.GetColdWalletPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName)))
                throw new ServiceException("Sorry, we can't find the MultiChainCold folder. Please be sure to run CreateColdNode first or create the folder manually.");

            if (!File.Exists(MultiChainPathHelper.GetColdWalletParamsDatPath(CliOptions.ChainDefaultColdNodeLocation, blockchainName)))
                throw new ServiceException($"Sorry, it seems there is no params.dat file found in the Cold Node wallet you are trying to use for blockchain {blockchainName}");

            // disposable Process
            using var process = new Process();

            // set file name to multichaind-cold
            process.StartInfo.FileName = MultiChainPathHelper.GetMultiChainDColdExePath(CliOptions.ChainBinaryLocation);

            // populate argument list
            process.StartInfo.ArgumentList.Add(blockchainName);
            process.StartInfo.ArgumentList.Add(RuntimeCommand.Daemon);

            // configure Process behavior
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            // stdout & stderr StringBuilders are accessed by multiple threads
            // lock objects to prevent exception and cross threading.
            object stdoutLock = new object();
            object stderrLock = new object();

            // storage for sdtout and stderr
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            // set data received delegate
            process.ErrorDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                AddStdErr(cast.Data);
            };

            // set data received delegate
            process.OutputDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                AddStdOut(cast.Data);
            };

            // kick-off Process
            process.Start();

            // read err and out
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            // configure timer and ticker
            var maxCounter = 40;
            var timerState = new GeneralTimerState();
            var waitForStart = new Timer(
                callback: WaitForStartCallback,
                state: timerState,
                dueTime: 0,
                period: 250);

            // initialize a new response
            var response = new ForgeResponse();

            // timer handles Process stdout & sdterr content since multichaind doesn't exit in windows
            // we detect and handle failure or success based on the stdout & stderr
            while (!timerState.TimerJobSuccess && timerState.Counter <= maxCounter)
            {
                if (ResponseIsSuccess())
                {
                    response.Success = true;
                    timerState.TimerJobSuccess = true;
                }
                else if (ResponseIsFailure())
                {
                    timerState.TimerJobSuccess = true;
                    response.Errors.Add("Response_Failed_Reason_Exists",
                        $"Sorry, it seems that a blockchain with this name might already be running in this environment: {blockchainName}");
                }
            }

            // ensure Process has completed
            process.WaitForExit(1);

            // set response properties
            response.StandardError = GetStdErr();
            response.StandardOutput = GetStdOut();

            // determine if no result is detected
            if (timerState.Counter >= maxCounter && response.Errors.Count == 0 && !response.Success)
                response.Errors.Add("Response_Failed_Reason_Unknown", $"Sorry, it seems that something went wrong while starting blockchain cold node: {blockchainName}");

            return response;

            // lock for multiple thread access
            string GetStdOut()
            {
                lock (stdoutLock)
                    return stdout.ToString();
            }

            // lock for multiple thread access
            string GetStdErr()
            {
                lock (stderrLock)
                    return stderr.ToString();
            }

            // lock for multiple thread access
            void AddStdOut(string phrase)
            {
                lock (stdoutLock)
                    stdout.Append(phrase);
            }

            // lock for multiple thread access
            void AddStdErr(string phrase)
            {
                lock (stderrLock)
                    stderr.Append(phrase);
            }

            // determine response success
            bool ResponseIsSuccess()
            {
                var output = GetStdOut();
                return output.Contains("Listening for API requests on port", StringComparison.OrdinalIgnoreCase) 
                    && output.Contains("Node ready.", StringComparison.OrdinalIgnoreCase);
            }

            // determine response failure
            bool ResponseIsFailure()
            {
                var error = GetStdErr();
                return error.Contains("Couldn't initialize permission database for blockchain EntropyChain. Probably multichaind for this blockchain is already running. Exiting...", StringComparison.OrdinalIgnoreCase);
            }

            // Timer callback
            static void WaitForStartCallback(object? state)
            {
                var timerState = state as GeneralTimerState
                    ?? throw new ArgumentNullException("GeneralTimerState is null");

                Interlocked.Increment(ref timerState.Counter);
            }
        }

        /// <summary>
        /// Connect to a remote MultiChain ledger
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <param name="useSSL"></param>
        /// <returns></returns>
        public Task<ForgeResponse> ConnectToRemoteNodeAsync(string blockchainName, string ipAddress, string port, bool useSSL) =>
            Task.Run(() => ConnectToRemoteNode(blockchainName, ipAddress, port, useSSL));

        /// <summary>
        /// Connect to a remote MultiChain ledger
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <param name="useSSL"></param>
        /// <returns></returns>
        private ForgeResponse ConnectToRemoteNode(string blockchainName, string ipAddress, string port, bool useSSL)
        {
            // disposable Process
            using var process = new Process();

            // set file name to multichaind
            process.StartInfo.FileName = MultiChainPathHelper.GetMultiChainDExePath(CliOptions.ChainBinaryLocation);

            // populate argument list
            process.StartInfo.ArgumentList.Add($"{blockchainName}@{ipAddress}:{port}");
            if (useSSL)
                process.StartInfo.ArgumentList.Add(RuntimeCommand.UseRpcSSL);
            process.StartInfo.ArgumentList.Add(RuntimeCommand.Daemon);

            // configure Process behavior
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            // storage for sdtout and stderr
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            // set data received delegate
            process.ErrorDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                stderr.Append(cast.Data);
            };

            // set data received delegate
            process.OutputDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                stdout.Append(cast.Data);
            };

            // kick-off Process
            process.Start();

            // read err and out
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            // ensure Process has completed
            process.WaitForExit();

            // set response
            var response = new ForgeResponse(stdout.ToString(), stderr.ToString());

            // determine success
            // set errors if necessary
            if (ResponseIsSuccess())
                response.Success = true;
            else if (ResponseIsFailure())
                response.Errors.Add("Response_Failed_Reason_Exists",
                    "Couldn't connect to the seed node please check multichaind is running at that address and that your firewall settings allow incoming connections");
            else
                response.Errors.Add("Response_Failed_Reason_None", 
                    "Sorry, for some reason the remote host could not be contacted or the connection was refused by the host.");

            return response;

            // determine response success
            bool ResponseIsSuccess() => 
                !response.StandardError.Contains("Error: Couldn't connect to the seed node", StringComparison.OrdinalIgnoreCase) 
                && !response.StandardError.Contains("please check multichaind is running at that address and that your firewall settings allow incoming connections.", StringComparison.OrdinalIgnoreCase);

            // determine response success
            bool ResponseIsFailure() => 
                response.StandardError.Contains("Error: Couldn't connect to the seed node", StringComparison.OrdinalIgnoreCase)
                && response.StandardError.Contains("please check multichaind is running at that address and that your firewall settings allow incoming connections.", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Stop blockchain according to name
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StopBlockchainAsync(string blockchainName) =>
            Task.Run(() => StopBlockchain(blockchainName));

        /// <summary>
        /// Stop blockchain according to name
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        private ForgeResponse StopBlockchain(string blockchainName)
        {
            // disposable Process
            using var process = new Process();

            // set file name to multichaind
            process.StartInfo.FileName = MultiChainPathHelper.GetMultiChainCliExePath(CliOptions.ChainBinaryLocation);

            // populate argument list
            process.StartInfo.ArgumentList.Add(blockchainName);
            process.StartInfo.ArgumentList.Add(RuntimeCommand.Stop);

            // configure Process behavior
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            // storage for sdtout and stderr
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            // set data received delegate
            process.ErrorDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                stderr.Append(cast.Data);
            };

            // set data received delegate
            process.OutputDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                stdout.Append(cast.Data);
            };

            // kick-off Process
            process.Start();

            // read err and out
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            // ensure Process has completed
            process.WaitForExit();

            // set response
            var response = new ForgeResponse(stdout.ToString(), stderr.ToString());

            // determine success
            // set errors if necessary
            if (response.StandardOutput.Contains("MultiChain server stopping", StringComparison.OrdinalIgnoreCase))
                response.Success = true;
            else if (response.StandardError.Contains("couldn't connect to server", StringComparison.OrdinalIgnoreCase))
                response.Errors.Add("Response_Failed_Reason_Exists", $"Sorry, we couldn't connect to the server for: {blockchainName}");
            else
                response.Errors.Add("Response_Failed_Reason_None", $"Sorry, something went wrong while trying to stop the server for: {blockchainName}");

            return response;
        }

        /// <summary>
        /// Stop a cold node according to blockchain name value
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        public Task<ForgeResponse> StopColdNodeAsync(string blockchainName) =>
            Task.Run(() => StopColdNode(blockchainName));

        /// <summary>
        /// Stop a cold node according to blockchain name value
        /// </summary>
        /// <param name="blockchainName"></param>
        /// <returns></returns>
        private ForgeResponse StopColdNode(string blockchainName)
        {
            // disposable Process
            using var process = new Process();

            // set file name to multichaind
            process.StartInfo.FileName = MultiChainPathHelper.GetMultiChainCliExePath(CliOptions.ChainBinaryLocation);

            // populate argument list
            process.StartInfo.ArgumentList.Add(RuntimeCommand.Cold);
            process.StartInfo.ArgumentList.Add(blockchainName);
            process.StartInfo.ArgumentList.Add(RuntimeCommand.Stop);

            // configure Process behavior
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            // storage for sdtout and stderr
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            // set data received delegate
            process.ErrorDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                stderr.Append(cast.Data);
            };

            // set data received delegate
            process.OutputDataReceived += (sender, args) =>
            {
                var cast = args as DataReceivedEventArgs;
                stdout.Append(cast.Data);
            };

            // kick-off Process
            process.Start();

            // read err and out
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            // ensure Process has completed
            process.WaitForExit();

            // set response
            var response = new ForgeResponse(stdout.ToString(), stderr.ToString());

            // determine success
            // set errors if necessary
            if (response.StandardOutput.Contains("MultiChain server stopping", StringComparison.OrdinalIgnoreCase))
                response.Success = true;
            else if (response.StandardError.Contains("couldn't connect to server", StringComparison.OrdinalIgnoreCase))
                response.Errors.Add("Response_Failed_Reason_Exists", $"Sorry, we couldn't connect to the server for: {blockchainName}");
            else
                response.Errors.Add("Response_Failed_Reason_None", $"Sorry, something went wrong while trying to stop the server for: {blockchainName}");

            return response;
        }
    }
}