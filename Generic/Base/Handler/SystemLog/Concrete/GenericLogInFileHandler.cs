using Generic.Base.Handler.SystemLog.Abstract;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.SystemLog.Concrete
{
    public class GenericLogInFileHandler : AbstractGenericLogHandler
    {
        private readonly string filePath;
        private Serilog.Events.LogEventLevel minimumLevel;
        private RollingInterval rollingInterval;
        public GenericLogInFileHandler(string _filePath, Serilog.Events.LogEventLevel _minimumLevel,RollingInterval _rollingInterval)
        {
            filePath = _filePath;
            minimumLevel = _minimumLevel;
            rollingInterval = _rollingInterval;
        }

        public override ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Is(minimumLevel)
                .WriteTo.File(filePath, rollingInterval: rollingInterval)
                .CreateLogger();
        }
    }
}
