using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.DTO;
using Generic.Base.Handler.Map.Concrete;
using Microsoft.Extensions.DependencyInjection;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace Generic.Base.Handler.SystemLog.WithSerilog
{

    public static class GenericLogWithSerilogHandlerFactory
    {
        private static AbstractGenericLogWithSerilogHandler fileLogger;
        private static AbstractGenericLogWithSerilogHandler sqlSeverLogger;

        public enum LogHandlerType
        {
            File,
            Database
        }

        public static AbstractGenericLogWithSerilogHandler GetLogHandler(GenericConfigureLogWithSerilogRequestDto req)
        {
            switch (req.logHandlerType)
            {
                case LogHandlerType.File:
                    if (fileLogger == null)
                    {
                        fileLogger = new GenericLogWithSerilogInFileHandler(req);
                    }
                    return fileLogger;
                case LogHandlerType.Database:
                    if (sqlSeverLogger == null)
                    {
                        sqlSeverLogger = new GenericLogWithSerilogInSqlServerHandler(req);
                    }
                    return sqlSeverLogger;
                default:
                    throw new ArgumentException("Invalid log handler type");
            }
        }
        public static AbstractGenericLogWithSerilogHandler GetLogHandler(IServiceProvider serviceProvider, GenericConfigureLogWithSerilogRequestDto req)
        {
            switch (req.logHandlerType)
            {
                case LogHandlerType.File:
                    return serviceProvider.GetService<GenericLogWithSerilogInFileHandler>();
                case LogHandlerType.Database:
                    return serviceProvider.GetService<GenericLogWithSerilogInSqlServerHandler>();
                default:
                    throw new ArgumentException("Invalid log handler  mode");
            }
        }
    }

}
