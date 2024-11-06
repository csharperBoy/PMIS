using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.SystemLog.WithSerilog.DTO
{
    public class GenericConfigureLogWithSerilogRequestDto : GenericConfigureLogRequestDto
    {
        public GenericConfigureLogWithSerilogInFileRequestDto inFileConfig { get; set; }
        public GenericConfigureLogWithSerilogInSqlServerRequestDto inSqlServerConfig { get; set; }
        public LogEventLevel minimumLevel { get; set; }
        public RollingInterval rollingInterval { get; set; }
    }
}
