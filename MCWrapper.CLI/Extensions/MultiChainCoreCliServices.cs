﻿using MCWrapper.CLI.Ledger.Clients;
using MCWrapper.CLI.Options;
using MCWrapper.Ledger.Entities.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Runtime.InteropServices;

namespace MCWrapper.CLI.Extensions
{
    /// <summary>
    /// Extension methods offering several different options for adding MultiChain Core Command Line Interface services to your .NET Core application.
    /// </summary>
    public static class MultiChainCoreCliServices
    {
        /// <summary>
        /// <para> **** Please be aware! MCWrapper.CLI has only been tested and configured to function in a Windows environment. ****</para>
        /// <para> **** We are working on a Linux client for version 2.0.0, current version is 1.0.0 ****</para>
        /// 
        /// Use this method to add MultiChain Command Line Interface (CLI) services to an application's service container.
        /// 
        /// <para>
        ///     Be aware a MultiChain blockchain network must be installed and configured externally from this application.
        ///     However, consumers using the MCWrapper.CLI ForgeClient are able to create, start, and stop Hot and Cold node 
        ///     wallet types directly from an application.
        /// </para>
        /// <para>
        ///     The MultiChain library and installation instructions are availabale at https://multichain.com for consumers not using the MCWrapper.CLI ForgeClient
        /// </para>
        /// <para>
        ///     This method automatically loads the CliOptions and RuntimeParamOptions from the
        ///     local environment's variable store. CliOptions and RuntimeParamOptions are implemented in such a way
        ///     that during instantiation the local environment's variable store 
        ///     is automatically parsed for available parameter values. In our opinion this is the best and most secure
        ///     method to implement MCWrapper within an application.
        ///     Set each environment's variables and let MCWrapper lazy load all the values. Easy peasy!
        /// </para>
        /// </summary>
        /// <param name="services">Service container</param>
        /// <returns></returns>
        public static IServiceCollection AddMultiChainCoreCliServices(this IServiceCollection services)
        {
            var cliOptions = new CliOptions(true);
            var runtimeOptions = new RuntimeParamOptions(true);

            // detect misconfiguration early in pipeline
            if (string.IsNullOrEmpty(cliOptions.ChainAdminAddress)) throw new ArgumentNullException($"{nameof(cliOptions.ChainAdminAddress)} is required and cannot be empty or null");
            if (string.IsNullOrEmpty(cliOptions.ChainBurnAddress)) throw new ArgumentNullException($"{nameof(cliOptions.ChainBurnAddress)} is required and cannot be empty or null");
            if (string.IsNullOrEmpty(cliOptions.ChainName)) throw new ArgumentNullException($"{nameof(cliOptions.ChainName)} is required and cannot be empty or null");

            // load Options from the local environment variable store
            services.Configure<CliOptions>(config =>
            {
                config.ChainDefaultColdNodeLocation = cliOptions.ChainDefaultColdNodeLocation;
                config.ChainDefaultLocation = cliOptions.ChainDefaultLocation;
                config.ChainBinaryLocation = cliOptions.ChainBinaryLocation;
                config.ChainAdminAddress = cliOptions.ChainAdminAddress;
                config.ChainBurnAddress = cliOptions.ChainBurnAddress;
                config.ChainName = cliOptions.ChainName;
            })
                .Configure<RuntimeParamOptions>(config =>
                {
                    config.MiningRequiresPeers = runtimeOptions.MiningRequiresPeers;
                    config.LockAdminMineRounds = runtimeOptions.LockAdminMineRounds;
                    config.MaxQueryScanItems = runtimeOptions.MaxQueryScanItems;
                    config.HideKnownOpDrops = runtimeOptions.HideKnownOpDrops;
                    config.MineEmptyRounds = runtimeOptions.MineEmptyRounds;
                    config.MiningTurnOver = runtimeOptions.MiningTurnOver;
                    config.HandshakeLocal = runtimeOptions.HandshakeLocal;
                    config.AutoSubscribe = runtimeOptions.AutoSubscribe;
                    config.MaxShownData = runtimeOptions.MaxShownData;
                    config.LockBlock = runtimeOptions.LockBlock;
                    config.BanTx = runtimeOptions.BanTx;
                });

            // command line interface clients and client factory
            services.AddTransient<IMultiChainCliGeneral, MultiChainCliGeneralClient>()
                .AddTransient<IMultiChainCliGenerate, MultiChainCliGenerateClient>()
                .AddTransient<IMultiChainCliOffChain, MultiChainCliOffChainClient>()
                .AddTransient<IMultiChainCliControl, MultiChainCliControlClient>()
                .AddTransient<IMultiChainCliNetwork, MultiChainCliNetworkClient>()
                .AddTransient<IMultiChainCliUtility, MultiChainCliUtilityClient>()
                .AddTransient<IMultiChainCliWallet, MultiChainCliWalletClient>()
                .AddTransient<IMultiChainCliMining, MultiChainCliMiningClient>()
                .AddTransient<IMultiChainCliRaw, MultiChainCliRawClient>()
                .AddTransient<IMultiChainCliForge, MultiChainCliForgeClient>()
                .AddTransient<IMultiChainCliClientFactory, MultiChainCliClientFactory>();

            return services;
        }

        /// <summary>
        /// <para> **** Please be aware! MCWrapper.CLI has only been tested and configured to function in a Windows environment. ****</para>
        /// <para> **** We are working on a Linux client for version 2.0.0, current version is 1.0.0 ****</para>
        /// 
        /// Use this method to add MultiChain Command Line Interface (CLI) services to an application's service container.
        /// 
        /// <para>
        ///     Be aware a MultiChain blockchain network must be installed and configured externally from this application.
        ///     However, consumers using the MCWrapper.CLI ForgeClient are able to create, start, and stop Hot and Cold node 
        ///     wallet types directly from an application.
        /// </para>
        /// <para>
        ///     The MultiChain library and installation instructions are availabale at https://multichain.com for consumers not using the MCWrapper.CLI ForgeClient
        /// </para>
        /// <para>
        ///     This method automatically loads the CliOptions and RuntimeParamOptions from the
        ///     IConfiguration interface (appsettings.json file usually). Mildly secure method, our second preference if environment variables are not an option.
        /// </para>
        /// </summary>
        /// <param name="services">Service container</param>
        /// <param name="configuration">Configuration pipeline</param>
        /// <param name="useSecrets">true = use Secrets Manager / false (default) = use appsettings.json</param>
        /// <returns></returns>
        public static IServiceCollection AddMultiChainCoreCliServices(this IServiceCollection services, IConfiguration configuration, bool useSecrets = false)
        {
            if (!useSecrets)
            {
                // load Options from the IConfiguration interface (appsettings.json file usually)
                services.Configure<RuntimeParamOptions>(configuration)
                    .Configure<CliOptions>(configuration);
            }
            else
            {
                services.Configure<RuntimeParamOptions>(configuration)
                    .Configure<CliOptions>(config =>
                    {
                        config.ChainDefaultColdNodeLocation = configuration["MULTICHAIN__DEFAULTCOLDNODELOCATION"];
                        config.ChainDefaultLocation = configuration["MULTICHAIN__DEFAULTLOCATION"];
                        config.ChainBinaryLocation = configuration["MULTICHAIN__BINARYLOCATION"];
                        config.ChainAdminAddress = configuration["MULTICHAIN__ADMINADDRESS"];
                        config.ChainBurnAddress = configuration["MULTICHAIN__BURNADDRESS"];
                        config.ChainName = configuration["MULTICHAIN__NAME"];
                    });
            }

            var provider = services.BuildServiceProvider();
            var cliOptions = provider.GetRequiredService<IOptions<CliOptions>>().Value;

            // detect misconfiguration early in pipeline
            if (string.IsNullOrEmpty(cliOptions.ChainAdminAddress)) throw new ArgumentNullException($"{nameof(cliOptions.ChainAdminAddress)} is required and cannot be empty or null");
            if (string.IsNullOrEmpty(cliOptions.ChainBurnAddress)) throw new ArgumentNullException($"{nameof(cliOptions.ChainBurnAddress)} is required and cannot be empty or null");
            if (string.IsNullOrEmpty(cliOptions.ChainName)) throw new ArgumentNullException($"{nameof(cliOptions.ChainName)} is required and cannot be empty or null");

            // command line interface clients and client factory
            services.AddTransient<IMultiChainCliGeneral, MultiChainCliGeneralClient>()
               .AddTransient<IMultiChainCliGenerate, MultiChainCliGenerateClient>()
               .AddTransient<IMultiChainCliOffChain, MultiChainCliOffChainClient>()
               .AddTransient<IMultiChainCliControl, MultiChainCliControlClient>()
               .AddTransient<IMultiChainCliNetwork, MultiChainCliNetworkClient>()
               .AddTransient<IMultiChainCliUtility, MultiChainCliUtilityClient>()
               .AddTransient<IMultiChainCliWallet, MultiChainCliWalletClient>()
               .AddTransient<IMultiChainCliMining, MultiChainCliMiningClient>()
               .AddTransient<IMultiChainCliRaw, MultiChainCliRawClient>()
               .AddTransient<IMultiChainCliForge, MultiChainCliForgeClient>()
               .AddTransient<IMultiChainCliClientFactory, MultiChainCliClientFactory>();

            return services;
        }

        /// <summary>
        /// <para> **** Please be aware! MCWrapper.CLI has only been tested and configured to function in a Windows environment. ****</para>
        /// <para> **** We are working on a Linux client for version 2.0.0, current version is 1.0.0 ****</para>
        /// 
        /// Use this method to add MultiChain Command Line Interface (CLI) services to an application's service container.
        /// 
        /// <para>
        ///     Be aware a MultiChain blockchain network must be installed and configured externally from this application.
        ///     However, consumers using the MCWrapper.CLI ForgeClient are able to create, start, and stop Hot and Cold node 
        ///     wallet types directly from an application.
        /// </para>
        /// <para>
        ///     The MultiChain library and installation instructions are availabale at https://multichain.com for consumers not using the MCWrapper.CLI ForgeClient
        /// </para>
        /// <para>
        ///     This method requires the CliOptions and RuntimeParamOptions property values to be
        ///     explicitly passed via the <paramref name="runtimeParamOptions"/> and <paramref name="cliOptions"/> 
        ///     Action parameters when added to the DI pipeline. Least secure method and should only be used during testing or in sandbox environments.
        /// </para>
        /// </summary>
        /// <param name="services">Service container</param>
        /// <param name="cliOptions">Blockchain profile configuration (Information the app will use to connect to a MultiChain ledger)</param>
        /// <param name="runtimeParamOptions">Runtime parameter configuration (How a MultiChain ledger should behave)</param>
        /// <returns></returns>
        public static IServiceCollection AddMultiChainCoreCliServices(this IServiceCollection services, Action<CliOptions> cliOptions, [Optional] Action<RuntimeParamOptions> runtimeParamOptions)
        {
           
            var _cliOptions = new CliOptions();
            cliOptions?.Invoke(_cliOptions);

            // detect misconfiguration early in pipeline
            if (string.IsNullOrEmpty(_cliOptions.ChainAdminAddress)) throw new ArgumentNullException($"{nameof(_cliOptions.ChainAdminAddress)} is required and cannot be empty or null");
            if (string.IsNullOrEmpty(_cliOptions.ChainBurnAddress)) throw new ArgumentNullException($"{nameof(_cliOptions.ChainBurnAddress)} is required and cannot be empty or null");
            if (string.IsNullOrEmpty(_cliOptions.ChainName)) throw new ArgumentNullException($"{nameof(_cliOptions.ChainName)} is required and cannot be empty or null");

            // configure Options
            services.Configure<CliOptions>(config =>
            {
                config.ChainDefaultColdNodeLocation = _cliOptions.ChainDefaultColdNodeLocation;
                config.ChainDefaultLocation = _cliOptions.ChainDefaultLocation;
                config.ChainBinaryLocation = _cliOptions.ChainBinaryLocation;
                config.ChainAdminAddress = _cliOptions.ChainAdminAddress;
                config.ChainBurnAddress = _cliOptions.ChainBurnAddress;
                config.ChainName = _cliOptions.ChainName;
            });

            if (runtimeParamOptions != null)
            {
                var _runtimeParamOptions = new RuntimeParamOptions();
                runtimeParamOptions?.Invoke(_runtimeParamOptions);

                services.Configure<RuntimeParamOptions>(config =>
                {
                    config.MiningRequiresPeers = _runtimeParamOptions.MiningRequiresPeers;
                    config.LockAdminMineRounds = _runtimeParamOptions.LockAdminMineRounds;
                    config.MaxQueryScanItems = _runtimeParamOptions.MaxQueryScanItems;
                    config.HideKnownOpDrops = _runtimeParamOptions.HideKnownOpDrops;
                    config.MineEmptyRounds = _runtimeParamOptions.MineEmptyRounds;
                    config.MiningTurnOver = _runtimeParamOptions.MiningTurnOver;
                    config.HandshakeLocal = _runtimeParamOptions.HandshakeLocal;
                    config.AutoSubscribe = _runtimeParamOptions.AutoSubscribe;
                    config.MaxShownData = _runtimeParamOptions.MaxShownData;
                    config.LockBlock = _runtimeParamOptions.LockBlock;
                    config.BanTx = _runtimeParamOptions.BanTx;
                });
            }

            // command line interface clients and client factory
            services.AddTransient<IMultiChainCliGeneral, MultiChainCliGeneralClient>()
               .AddTransient<IMultiChainCliGenerate, MultiChainCliGenerateClient>()
               .AddTransient<IMultiChainCliOffChain, MultiChainCliOffChainClient>()
               .AddTransient<IMultiChainCliControl, MultiChainCliControlClient>()
               .AddTransient<IMultiChainCliNetwork, MultiChainCliNetworkClient>()
               .AddTransient<IMultiChainCliUtility, MultiChainCliUtilityClient>()
               .AddTransient<IMultiChainCliWallet, MultiChainCliWalletClient>()
               .AddTransient<IMultiChainCliMining, MultiChainCliMiningClient>()
               .AddTransient<IMultiChainCliRaw, MultiChainCliRawClient>()
               .AddTransient<IMultiChainCliForge, MultiChainCliForgeClient>()
               .AddTransient<IMultiChainCliClientFactory, MultiChainCliClientFactory>();

            return services;
        }


        /// <summary>
        /// Add MultiChain Command Line Interface (CLI) services to an application's service container.
        /// 
        /// <para>
        ///     Be aware a MultiChain blockchain network must be installed and configured externally from this application.
        /// </para>
        /// 
        /// <para>
        ///     The MultiChain library and installation instructions are availabale at https://multichain.com
        /// </para>
        /// 
        /// <para>
        ///     This method requires the CliOptions and RuntimeParamOptions property values to be
        ///     explicitly passed via the individual parameters for this method and the optional,
        ///     <paramref name="runtimeParamOptions"/> Action parameter when added to the DI pipeline.
        /// </para>
        /// <para>
        ///     See our documentation on Github, https://ryangoodwindev.github.io/MCWrapper/, 
        ///     for default locations used for <paramref name="chainBinaryLocation"/>,
        ///     <paramref name="chainDefaultLocation"/>, and <paramref name="chainDefaultColdNodeLocation"/>
        /// </para>
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="chainName">MultiChain Core blockchain node name</param>
        /// <param name="burnAddress">MultiChain Core blockchain Burn address</param>
        /// <param name="adminAddress">MultiChain Core blockchain Admin address</param>
        /// <param name="chainBinaryLocation">MultiChain Core executable location (i.e multichaind, multichain-cli, multichain-util, and multichaind-cold)</param>
        /// <param name="chainDefaultLocation">MulitChain Core hot node storage location</param>
        /// <param name="chainDefaultColdNodeLocation">MultiChain Core cold node storage location</param>
        /// <param name="runtimeParamOptions">MultiChain Core runtime parameters</param>
        /// <returns></returns>
        public static IServiceCollection AddMultiChainCoreRpcServices(this IServiceCollection services,
            string chainName,
            string burnAddress,
            string adminAddress,
            [Optional] string chainBinaryLocation,
            [Optional] string chainDefaultLocation,
            [Optional] string chainDefaultColdNodeLocation,
            [Optional] Action<RuntimeParamOptions> runtimeParamOptions)
        {
            var cliOptions = new CliOptions(chainName,
                burnAddress,
                adminAddress,
                chainBinaryLocation,
                chainDefaultLocation,
                chainDefaultColdNodeLocation);

            // configure Options
            services.Configure<CliOptions>(config =>
            {
                config.ChainDefaultColdNodeLocation = cliOptions.ChainDefaultColdNodeLocation;
                config.ChainDefaultLocation = cliOptions.ChainDefaultLocation;
                config.ChainBinaryLocation = cliOptions.ChainBinaryLocation;
                config.ChainAdminAddress = cliOptions.ChainAdminAddress;
                config.ChainBurnAddress = cliOptions.ChainBurnAddress;
                config.ChainName = cliOptions.ChainName;
            });

            if (runtimeParamOptions != null)
            {
                var _runtimeParamOptions = new RuntimeParamOptions();
                runtimeParamOptions?.Invoke(_runtimeParamOptions);

                services.Configure<RuntimeParamOptions>(config =>
                {
                    config.MiningRequiresPeers = _runtimeParamOptions.MiningRequiresPeers;
                    config.LockAdminMineRounds = _runtimeParamOptions.LockAdminMineRounds;
                    config.MaxQueryScanItems = _runtimeParamOptions.MaxQueryScanItems;
                    config.HideKnownOpDrops = _runtimeParamOptions.HideKnownOpDrops;
                    config.MineEmptyRounds = _runtimeParamOptions.MineEmptyRounds;
                    config.MiningTurnOver = _runtimeParamOptions.MiningTurnOver;
                    config.HandshakeLocal = _runtimeParamOptions.HandshakeLocal;
                    config.AutoSubscribe = _runtimeParamOptions.AutoSubscribe;
                    config.MaxShownData = _runtimeParamOptions.MaxShownData;
                    config.LockBlock = _runtimeParamOptions.LockBlock;
                    config.BanTx = _runtimeParamOptions.BanTx;
                });
            }

            // command line interface clients and client factory
            services.AddTransient<IMultiChainCliGeneral, MultiChainCliGeneralClient>()
               .AddTransient<IMultiChainCliGenerate, MultiChainCliGenerateClient>()
               .AddTransient<IMultiChainCliOffChain, MultiChainCliOffChainClient>()
               .AddTransient<IMultiChainCliControl, MultiChainCliControlClient>()
               .AddTransient<IMultiChainCliNetwork, MultiChainCliNetworkClient>()
               .AddTransient<IMultiChainCliUtility, MultiChainCliUtilityClient>()
               .AddTransient<IMultiChainCliWallet, MultiChainCliWalletClient>()
               .AddTransient<IMultiChainCliMining, MultiChainCliMiningClient>()
               .AddTransient<IMultiChainCliRaw, MultiChainCliRawClient>()
               .AddTransient<IMultiChainCliForge, MultiChainCliForgeClient>()
               .AddTransient<IMultiChainCliClientFactory, MultiChainCliClientFactory>();

            return services;
        }
    }
}