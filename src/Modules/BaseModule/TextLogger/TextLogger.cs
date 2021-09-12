using Microsoft.Extensions.Logging;
using System;

namespace ProjectManagementModule
{
    public class TextLogger : ILogger
    {
        private readonly Func<TextLoggerConfiguration> _getCurrentConfig;

        public TextLogger(Func<TextLoggerConfiguration> getCurrentConfig)
        {
            _getCurrentConfig = getCurrentConfig;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        public event EventHandler<TextLogEventArgs> LogAdded;

        public bool IsEnabled(LogLevel logLevel) => _getCurrentConfig().LogLevels.ContainsKey(logLevel);

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                string message = string.Empty;
                if (formatter != null)
                {
                    message += formatter(state, exception);
                }

                var resultMessage = $"{DateTime.Now:dd.MM.yyyy HH.mm.ss} {logLevel} - {message}";
                LogAdded?.Invoke(this, new TextLogEventArgs(resultMessage, config.LogLevels[logLevel]));
            }
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
