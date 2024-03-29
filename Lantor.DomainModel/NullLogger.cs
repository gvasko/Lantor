using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    internal class NullLogger : ILogger
    {
        public void Debug(string message, params object?[]? args)
        {
            // Do nothing
        }

        public void Debug(Exception e, string message, params object?[]? args)
        {
            // Do nothing
        }

        public void Error(Exception e, string message, params object?[]? args)
        {
            // Do nothing
        }

        public void Error(string message, params object?[]? args)
        {
            // Do nothing
        }

        public void Info(string message, params object?[]? args)
        {
            // Do nothing
        }

        public void Info(Exception e, string message, params object?[]? args)
        {
            // Do nothing
        }

        public void Warning(Exception e, string message, params object?[]? args)
        {
            // Do nothing
        }

        public void Warning(string message, params object?[]? args)
        {
            // Do nothing
        }
    }
}
