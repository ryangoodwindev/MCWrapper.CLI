using MCWrapper.CLI.Ledger.Clients;
using MCWrapper.CLI.Ledger.Factory;
using MCWrapper.CLI.Ledger.Forge;
using MCWrapper.CLI.Options;
using MCWrapper.Ledger.Entities.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.InteropServices;

namespace MCWrapper.CLI.Extensions
{
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
            // load Options from the local environment variable store
            // 
            services.Configure<CliOptions>(config => new CliOptions())
                .Configure<RuntimeParamOptions>(config => new RuntimeParamOptions());

            // command line interface clients and client factory
            services.AddTransient<BlockchainCliClient>()
                .AddTransient<GenerateCliClient>()
                .AddTransient<OffChainCliClient>()
                .AddTransient<ControlCliClient>()
                .AddTransient<NetworkCliClient>()
                .AddTransient<UtilityCliClient>()
                .AddTransient<WalletCliClient>()
                .AddTransient<MiningCliClient>()
                .AddTransient<RawCliClient>()
                .AddTransient<ForgeClient>()
                .AddTransient<CliClientFactory>();

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
        /// <returns></returns>
        public static IServiceCollection AddMultiChainCoreCliServices(this IServiceCollection services, IConfiguration configuration)
        {
            // load Options from the IConfiguration interface (appsettings.json file usually)
            services.Configure<CliOptions>(configuration)
                .Configure<RuntimeParamOptions>(configuration);

            // command line interface clients and client factory
            services.AddTransient<BlockchainCliClient>()
                .AddTransient<GenerateCliClient>()
                .AddTransient<OffChainCliClient>()
                .AddTransient<ControlCliClient>()
                .AddTransient<NetworkCliClient>()
                .AddTransient<UtilityCliClient>()
                .AddTransient<WalletCliClient>()
                .AddTransient<MiningCliClient>()
                .AddTransient<RawCliClient>()
                .AddTransient<ForgeClient>()
                .AddTransient<CliClientFactory>();

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
        ///     explicitly passed via the <paramref name="runtimeConfig"/> and <paramref name="profileConfig"/> 
        ///     Action parameters when added to the DI pipeline. Least secure method and should only be used during testing or in sandbox environments.
        /// </para>
        /// </summary>
        /// <param name="services">Service container</param>
        /// <param name="profileConfig">Blockchain profile configuration (Information the app will use to connect to a MultiChain ledger)</param>
        /// <param name="runtimeConfig">Runtime parameter configuration (How a MultiChain ledger should behave)</param>
        /// <returns></returns>
        public static IServiceCollection AddMultiChainCoreCliServices(this IServiceCollection services, Action<CliOptions> profileConfig, [Optional] Action<RuntimeParamOptions> runtimeConfig)
        {
            // configure Options by invoking Actions
            services.Configure((Action<CliOptions>)(config => profileConfig?.Invoke(new CliOptions())))
                .Configure((Action<RuntimeParamOptions>)(config => runtimeConfig?.Invoke(new RuntimeParamOptions())));

            // command line interface clients and client factory
            services.AddTransient<BlockchainCliClient>()
                .AddTransient<GenerateCliClient>()
                .AddTransient<OffChainCliClient>()
                .AddTransient<ControlCliClient>()
                .AddTransient<NetworkCliClient>()
                .AddTransient<UtilityCliClient>()
                .AddTransient<WalletCliClient>()
                .AddTransient<MiningCliClient>()
                .AddTransient<RawCliClient>()
                .AddTransient<ForgeClient>()
                .AddTransient<CliClientFactory>();

            return services;
        }
    }
}