using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public interface ILogger
    {
        void Error(Exception e, string message, params object?[]? args);
        void Error(string message, params object?[]? args);
        void Warning(Exception e, string message, params object?[]? args);
        void Warning(string message, params object?[]? args);
        void Info(string message, params object?[]? args);
        void Info(Exception e, string message, params object?[]? args);
        void Debug(string message, params object?[]? args);
        void Debug(Exception e, string message, params object?[]? args);
    }
}
