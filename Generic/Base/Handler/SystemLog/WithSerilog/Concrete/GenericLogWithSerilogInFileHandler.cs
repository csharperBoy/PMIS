using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.DTO;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.SystemLog.WithSerilog.Concrete
{
    public class GenericLogWithSerilogInFileHandler : AbstractGenericLogWithSerilogHandler
    {

        private GenericConfigureLogWithSerilogRequestDto req;
        public GenericLogWithSerilogInFileHandler(GenericConfigureLogWithSerilogRequestDto _req)
        {
            req = _req;
        }

        public override ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Is(req.minimumLevel)
                .WriteTo.File(req.inFileConfig.filePath, rollingInterval: req.rollingInterval)
                .CreateLogger();
        }
    }
}
