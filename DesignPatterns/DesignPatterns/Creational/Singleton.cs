// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Creational
{
    public class SingletonDemo
    {
        public static void Run()
        {
            DoHardWork();
        }

        public static void DoHardWork()
        {
            LoggerSingleton logger = LoggerSingleton.GetInstance();
            HardProcessor processor = new HardProcessor(1);
            logger.Log("Hard work started...");
            processor.ProcessTo(5);
            logger.Log("Hard work finished...");
        }
    }

    public class HardProcessor
    {
        private int _start;
        public HardProcessor(int start)
        {
            _start = start;
            LoggerSingleton.GetInstance().Log("Processor just created.");
        }
        public int ProcessTo(int end)
        {
            int sum = 0;
            for (int i = _start; i <= end; ++i)
            {
                sum += i;
            }
            LoggerSingleton.GetInstance().Log("Processor just calculated some value: " + sum);
            return sum;
        }
    }

    public class LoggerSingleton
    {
        private LoggerSingleton() { }
        private int _logCount = 0;
        private static LoggerSingleton _loggerSingletonInstance = null;
        public static LoggerSingleton GetInstance()
        {
            if (_loggerSingletonInstance == null)
            {
                _loggerSingletonInstance = new LoggerSingleton();
            }
            return _loggerSingletonInstance;
        }

        public void Log(String message)
        {
            Console.WriteLine(_logCount + ": " + message);
            _logCount++;
        }
    }

public class ThreadSafeLoggerSingleton
{
    private ThreadSafeLoggerSingleton()
    {
        // Читаємо дані із якогось файлу і дістаємо номер останнього запису
        // _logCount = вичитане значення
    }
    private int _logCount = 0;
    private static ThreadSafeLoggerSingleton _loggerInstance;
    private static readonly object locker = new object();

    public static ThreadSafeLoggerSingleton GetInstance()
    {
        lock (locker)
        {
            if (_loggerInstance == null)
            {
                _loggerInstance = new ThreadSafeLoggerSingleton();
            }
        }
        return _loggerInstance;
    }

    public void Log(String message)
    {
        Console.WriteLine(_logCount + ": " + message);
        _logCount++;
    }
}

    public class AlmostThreadSafeLoggerSingletonWithDoubleLocking
    {
        private AlmostThreadSafeLoggerSingletonWithDoubleLocking()
        {
            // Читаємо дані із якогось файлу і дістаємо номер останнього запису
            // _logCount = вичитане значення
        }
        private int _logCount = 0;
        private static AlmostThreadSafeLoggerSingletonWithDoubleLocking _loggerInstance;
        public static AlmostThreadSafeLoggerSingletonWithDoubleLocking GetInstance()
        {
            if (_loggerInstance == null)
            {
                lock (_loggerInstance)
                {
                    if (_loggerInstance == null)
                    {
                        _loggerInstance = new AlmostThreadSafeLoggerSingletonWithDoubleLocking();
                    }
                }
            }
            return _loggerInstance;
        }

        public void Log(String message)
        {
            Console.WriteLine(_logCount + ": " + message);
            _logCount++;
        }
    }
}