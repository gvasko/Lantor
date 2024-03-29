using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public static class LoggerService
    {
        public static ILogger Logger { get; set; } = new NullLogger();
    }
}
