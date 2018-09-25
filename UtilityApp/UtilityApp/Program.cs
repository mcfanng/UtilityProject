using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using UtilityApp.FileUtility;
using UtilityApp.Interfaces;

namespace UtilityApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection: serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var utilityApp = serviceProvider.GetService<UtilityApp>();
            utilityApp.Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json")
                .Build();

            var serilogName =  "UtilityApp" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration: configuration)
                .WriteTo.File(path: serilogName, rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
            });
            serviceCollection.AddSingleton<UtilityApp>();

            //serviceCollection.AddSingleton<IDevicePaths>(factory => new DevicePaths(
            //    defaultProtocol: configuration.GetSection(key: "DevicePaths:DefaultProtocol").Value,
            //    logFolderName: configuration.GetSection(key: "DevicePaths:LogFolderName").Value,
            //    ftpRoot: configuration.GetSection(key: "DevicePaths:FTPRoot").Value,
            //    stateFileName: configuration.GetSection(key: "DevicePaths:StateFileName").Value
            //));

          //  serviceCollection.AddScoped<ISqlConnectionManager, SqlConnectionManager>();

            serviceCollection.AddScoped<IFileUtil, FileUtil>();
            //serviceCollection.AddTransient<ILoggingRepository, Infrastructure.Dapper.Repositories.LoggingRepository>();
            //serviceCollection.AddTransient<ISimpleProcessRepository, SimpleProcessRepository>();
            

            serviceCollection.AddSingleton(implementationInstance: configuration);
        }
    }
}
