using JackNTFS.src.environment;
using log4net;
using log4net.Core;
using log4net.Repository;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static log4net.Core.Level;

namespace JackNTFS.src.userinterface.exports
{
    internal class JackLogger : ILogger
    {
        private Process DEFAULT_PROCESS;
        private StreamReader DEFAULT_STREAM_READER;
        private StreamWriter DEFAULT_STREAM_WRITER;
        private static ILog globalLogger = LogManager.GetLogger(typeof(JackLogger));

        private ILog mLocalLogger;

        public JackLogger()
        {
            this.mLocalLogger = globalLogger;
            DEFAULT_PROCESS = new Process();
            DEFAULT_PROCESS.Start();
            DEFAULT_STREAM_READER = DEFAULT_PROCESS.StandardOutput;
            DEFAULT_STREAM_WRITER = DEFAULT_PROCESS.StandardInput;
        }

        public JackLogger(ILog localLogger)
        {
            this.mLocalLogger = localLogger;
            DEFAULT_PROCESS = new Process();
            DEFAULT_PROCESS.Start();
            DEFAULT_STREAM_READER = DEFAULT_PROCESS.StandardOutput;
            DEFAULT_STREAM_WRITER = DEFAULT_PROCESS.StandardInput;
        }

        public string Name => throw new NotImplementedException();

        public ILoggerRepository Repository => throw new NotImplementedException();

        public bool IsEnabledFor(Level level)
        {
            /* Enable all */
            return true;
        }

        public void Log(Type callerStackBoundaryDeclaringType, Level level, object? message, Exception? exception)
        {
            /* FIXME: callerStackBoundaryDeclaringType : Type is not used */
            CombinedLog(level, message, exception);
        }

        public void Log(Type callerStackBoundaryDeclaringType, Level level, object? message, Exception? exception, StreamWriter outputStream)
        {
            /* FIXME: callerStackBoundaryDeclaringType : Type is not used */
            CombinedLog(level, message, exception, outputStream);
        }

        public void Log(LoggingEvent logEvent)
        {
            CombinedLog(logEvent.Level, logEvent.MessageObject, logEvent.ExceptionObject);
        }

        public void Log(LoggingEvent logEvent, StreamWriter outputStream)
        {
            CombinedLog(logEvent.Level, logEvent.MessageObject, logEvent.ExceptionObject, outputStream);
        }

        public void Log(Level level, object? message, Exception? exception)
        {
            CombinedLog(level, message, exception);
        }

        public void Log(Level level, object? message, Exception? exception, StreamWriter outputStream)
        {
            CombinedLog(level, message, exception, outputStream);
        }

        public void Info(object message)
        {
            CombinedLog(Level.Info, message, null);
        }

        public void Info(object message, StreamWriter outputStream)
        {
            CombinedLog(Level.Info, message, null, outputStream);
        }

        public void Warn(object message)
        {
            CombinedLog(Level.Warn, message, null);
        }

        public void Warn(object message, StreamWriter outputStream)
        {
            CombinedLog(Level.Warn, message, null, outputStream);
        }

        public void Error(object message)
        {
            CombinedLog(Level.Error, message, null);
        }

        public void Error(object message, StreamWriter outputStream)
        {
            CombinedLog(Level.Error, message, null, outputStream);
        }

        public void Fatal(object message)
        {
            CombinedLog(Level.Fatal, message, null);
        }

        public void Fatal(object message, StreamWriter outputStream)
        {
            CombinedLog(Level.Fatal, message, null, outputStream);
        }

        public void Debug(object message)
        {
            CombinedLog(Level.Debug, message, null);
        }

        public void Debug(object message, StreamWriter outputStream)
        {
            CombinedLog(Level.Debug, message, null, outputStream);
        }

        public void SetLocalLogger(ILog localLogger)
        {
            /* 避免流异常中断 */
            DEFAULT_STREAM_WRITER.Close(); // ------------------------ 3
            DEFAULT_STREAM_READER.Close(); // ------------------------ 2
            DEFAULT_PROCESS.Close(); // ------------------------------ 1
            /* 重设定日志器 */
            mLocalLogger = localLogger;
            /* 重启流 */
            DEFAULT_PROCESS.Start(); // ------------------------------ 1
            DEFAULT_STREAM_READER = DEFAULT_PROCESS.StandardOutput; // 2
            DEFAULT_STREAM_WRITER = DEFAULT_PROCESS.StandardInput; //  3
        }

        private void CombinedLog(Level? level, object? message, Exception? exception)
        {
            CombinedLog(level, message, exception, DEFAULT_STREAM_WRITER);
        }

        private void CombinedLog(Level? level, object? message, Exception? exception, StreamWriter outputStream)
        {
            if (level == null) { level = Level.Debug; }
            if (exception == null) { exception = new Exception(); }
            if (message == null) { message = ""; }

            if (level.Value == Level.Fatal.Value)
            {
                globalLogger.Fatal(message + $"\n{exception.Message}");
                outputStream.WriteLine(message + $"\n{exception.Message}");
            }
            else if (level.Value == Level.Error.Value)
            {
                globalLogger.Error(message + $"\n{exception.Message}");
                outputStream.WriteLine(message + $"\n{exception.Message}");
            }
            else if (level.Value == Level.Warn.Value)
            {
                globalLogger.Warn(message + $"\n{exception.Message}");
                outputStream.WriteLine(message + $"\n{exception.Message}");
            }
            else if (level.Value == Level.Info.Value)
            {
                globalLogger.Info(message + $"\n{exception.Message}");
                outputStream.WriteLine(message + $"\n{exception.Message}");
            }
            else /* (null || Debug): Level Debug output */
            {
                globalLogger.Debug(message + $"\n{exception.Message}");
                outputStream.WriteLine(message + $"\n{exception.Message}");
            }
        }

    }
}
