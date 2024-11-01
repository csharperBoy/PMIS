using Generic.Base.Handler.Log.Abstract;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.Log.Concrete
{
    public class GenericLogInFileHandler : AbstractGenericLogHandler
    {
        private readonly string _filePath;

        public GenericLogInFileHandler(string filePath)
        {
            _filePath = filePath;
        }

        public override ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File(_filePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
