using Generic.DTO.Base.Handler.Log;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.DTO.Base.Handler.SystemLog.Serilog
{
    public class GenericConfigureLogWithSerilogInFileRequestDto 
    {
        public string filePath { get; set; }

    }
}
