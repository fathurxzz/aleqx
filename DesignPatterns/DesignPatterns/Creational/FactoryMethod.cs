// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Creational
{
    public enum LoggingProviders
    {
        Enterprise,
        Log4Net
    }

    public interface ILogger
    {
        void LogMessage(string message);
        void LogError(string message);
        void LogVerboseInformation(string message);
    }

    class EnterpriseLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(string.Format("{0}: {1}", "Enterprise", message));
        }
        public void LogError(string message)
        {
            throw new NotImplementedException();
        }
        public void LogVerboseInformation(string message)
        {
            throw new NotImplementedException();
        }
    }

    class Log4NetLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(string.Format("{0}: {1}", "Log4Net", message));
            // Вивід: [Log4Net: Hello Factory Method Design Pattern]
        }
        public void LogError(string message)
        {
            throw new NotImplementedException();
        }
        public void LogVerboseInformation(string message)
        {
            throw new NotImplementedException();
        }
    }

    public class LoggerProviderFactory
    {
        public static ILogger GetLoggingProvider(LoggingProviders logProviders)
        {
            switch (logProviders)
            {
                case LoggingProviders.Enterprise:
                    return new EnterpriseLogger();
                case LoggingProviders.Log4Net:
                    return new Log4NetLogger();
                default:
                    return new EnterpriseLogger();
            }
        }
    }

    public class FactoryMethodDemo
    {
        public static void Run()
        {
            var providerType = GetTypeOfLoggingProviderFromConfigFile();
            ILogger logger = LoggerProviderFactory.GetLoggingProvider(providerType);
            logger.LogMessage("Hello Factory Method Design Pattern.");
        }
        private static LoggingProviders GetTypeOfLoggingProviderFromConfigFile()
        {
            return LoggingProviders.Log4Net;
        }
    }
}