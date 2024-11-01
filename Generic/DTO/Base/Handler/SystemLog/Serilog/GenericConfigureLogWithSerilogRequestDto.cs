using Generic.DTO.Base.Handler.Log;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.DTO.Base.Handler.SystemLog.Serilog
{
    public class GenericConfigureLogWithSerilogRequestDto : GenericConfigureLogRequestDto
    {
        public Serilog.Events.LogEventLevel minimumLevel { get; set; }
        public RollingInterval rollingInterval { get; set; }
    }
}
