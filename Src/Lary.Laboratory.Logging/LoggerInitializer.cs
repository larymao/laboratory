using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lary.Laboratory.Logging
{
    /// <summary>
    /// Serilog logger initializer.
    /// </summary>
    public static class LoggerInitializer
    {
        private static readonly Assembly _assembly = Assembly.GetAssembly(typeof(LoggerInitializer));

        /// <summary>
        /// Initializes serilog logger with default configurations.
        /// </summary>
        public static void InitSerilogger(LoggingOption? option = null)
        {
            var configFilePath = $"{_assembly.GetName().Name}.appsettings.log.json";
            var defaultConfigSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(configFilePath);
            var configuration = new ConfigurationBuilder().AddJsonStream(defaultConfigSource).Build();

            InitLogByConfiguration(configuration, option);
        }

        /// <summary>
        /// init log by path and option config
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="filePath"></param>
        /// <param name="option"></param>
        public static void InitSerilogger(string basePath, string filePath, LoggingOption? option = null)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(filePath)
                .Build();

            InitLogByConfiguration(configuration, option);
        }

        private static void InitLogByConfiguration(IConfigurationRoot configuration, LoggingOption? loggingOption)
        {
            loggingOption ??= new LoggingOption();
            var preLogger = new LoggerConfiguration().ReadFrom.Configuration(configuration);

            #region congi enrich

            var defaultEnriches = LoadDefaultEnrichConfig();

            foreach (var item in defaultEnriches)
            {
                preLogger = preLogger.Enrich.WithProperty(item.Key, item.Value);
            }

            foreach (var item in loggingOption.Enriches)
            {
                preLogger = preLogger.Enrich.WithProperty(item.Key, item.Value);
            }

            #endregion

            Log.Logger = preLogger
                .CreateLogger();
        }

        /// <summary>
        /// load default config items / environment ,platform 
        /// </summary>
        private static Dictionary<string, string> LoadDefaultEnrichConfig()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "NONE";
            var platform = Environment.GetEnvironmentVariable("PlatformName") ?? "NONE";
            var ApplicationName = Assembly.GetEntryAssembly().GetName().Name;

            var defaultConfig = new Dictionary<string, string>
            {
                { "PlatformEnv", environment },
                { "PlatformName", platform },
                { "ApplicationName", ApplicationName }
            };

            return defaultConfig;
        }
    }
}
