using MCWrapper.CLI.Constants;
using MCWrapper.CLI.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MCWrapper.CLI.Ledger.Forge
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

        public CliOptions CliOptions { get; }

        public ForgeResponse CreateBlockchain(string blockchainName)
        {
            using var process = new Process();

            process.StartInfo.FileName = MultiChainPaths.GetMultiChainUtilExePath(CliOptions.ChainBinaryLocation);
            process.StartInfo.Arguments = $"{RuntimeCommand.Create} {blockchainName}";
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

            return new ValueTuple<string, string>(stdout.ToString(), stderr.ToString());
        }

        public ForgeResponse StartBlockchain(string blockchainName, bool useSsl, string runtimeCommands)
        {
            using var process = new Process();

            process.StartInfo.FileName = MultiChainPaths.GetMultiChainDExePath(CliOptions.ChainBinaryLocation);
            process.StartInfo.Arguments = @$"{blockchainName} {(useSsl ? RuntimeCommand.UseRpcSSL : string.Empty)} {runtimeCommands} {RuntimeCommand.Daemon}";
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

            //process.WaitForExit();

            return new ValueTuple<string, string>(stdout.ToString(), stderr.ToString());
        }

        public ForgeResponse StartColdNode(string blockchainName)
        {
            using var process = new Process();

            process.StartInfo.FileName = MultiChainPaths.GetMultiChainDColdExePath(CliOptions.ChainBinaryLocation);
            process.StartInfo.Arguments = $"{blockchainName} {RuntimeCommand.Daemon}";
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

            return new ValueTuple<string, string>(stdout.ToString(), stderr.ToString());
        }

        public ForgeResponse ConnectToRemoteNode(string blockchainName, string ipAddress, string port, bool useSSL)
        {
            using var process = new Process();

            process.StartInfo.FileName = MultiChainPaths.GetMultiChainDExePath(CliOptions.ChainBinaryLocation);
            process.StartInfo.Arguments = $"{blockchainName}@{ipAddress}:{port} {(useSSL ? RuntimeCommand.UseRpcSSL : string.Empty)} {RuntimeCommand.Daemon}";
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

            return new ValueTuple<string, string>(stdout.ToString(), stderr.ToString());
        }

        public ForgeResponse StopBlockchain(string blockchainName)
        {
            using var process = new Process();

            process.StartInfo.FileName = MultiChainPaths.GetMultiChainCliExePath(CliOptions.ChainBinaryLocation);
            process.StartInfo.Arguments = $"{blockchainName} {RuntimeCommand.Stop}";
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

            return new ValueTuple<string, string>(stdout.ToString(), stderr.ToString());
        }

        public ForgeResponse StopColdNode(string blockchainName)
        {
            using var process = new Process();

            process.StartInfo.FileName = MultiChainPaths.GetMultiChainCliExePath(CliOptions.ChainBinaryLocation);
            process.StartInfo.Arguments = $"{RuntimeCommand.Cold} {blockchainName} {RuntimeCommand.Stop}";
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

            return new ValueTuple<string, string>(stdout.ToString(), stderr.ToString());
        }

        public void VerifyNewHotWallet(object? timerState)
        {
            var state = timerState as GeneralTimerState ?? throw new NullReferenceException("TimerState is null");

            if (!File.Exists(state.ParamsDatPath) || !File.Exists(state.MultiChainConfPath))
                state.TimerJobSuccess = true;

            state.Counter++;

            Interlocked.Exchange(ref state, state);
        }
    }
}
