using Generic.DTO.Base.Handler.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.DTO.Base.Handler.SystemLog.Serilog;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;

namespace Generic.Base.Handler.SystemLog.WithSerilog
{

    public static class GenericLogWithSerilogHandlerFactory
    {
        public enum LogHandlerType
        {
            File,
            Database
        }

        public static AbstractGenericLogWithSerilogHandler GetLogHandler(GenericConfigureLogWithSerilogRequestDto _req)
        {
            switch (_req.logHandlerType)
            {
                case LogHandlerType.File:
                    return new GenericLogWithSerilogInFileHandler(_req);
                case LogHandlerType.Database:
                    return new GenericLogWithSerilogInSqlServerHandler(_req);
                default:
                    throw new ArgumentException("Invalid log handler type");
            }
        }
    }

}
