
using Serilog;

namespace Lantor.Server.Infrastructure
{
    public class SeriLogger : Lantor.DomainModel.ILogger
    {
        public void Debug(string message, params object?[]? args)
        {
            Log.Debug(message, args);
        }

        public void Debug(Exception e, string message, params object?[]? args)
        {
            Log.Debug(e, message, args);
        }

        public void Error(Exception e, string message, params object?[]? args)
        {
            Log.Error(e, message, args);
        }

        public void Error(string message, params object?[]? args)
        {
            Log.Error(message, args);
        }

        public void Info(string message, params object?[]? args)
        {
            Log.Information(message, args);
        }

        public void Info(Exception e, string message, params object?[]? args)
        {
            Log.Information(e, message, args);
        }

        public void Warning(Exception e, string message, params object?[]? args)
        {
            Log.Warning(e, message, args);
        }

        public void Warning(string message, params object?[]? args)
        {
            Log.Warning(message, args);
        }
    }
}
