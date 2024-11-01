using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.SystemLog.GenericLogHandlerFactory;

namespace Generic.DTO.Base.Handler.Log
{
    public class GenericConfigureLogRequestDto
    {
        public LogHandlerType logHandlerType { get; set; }
        
    }
}
