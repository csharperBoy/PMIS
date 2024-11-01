using Generic.Base.Handler.SystemLog.Concrete;
using Generic.Base.Handler.SystemLog.Contract;
using Generic.Base.Handler.SystemLog.Abstract;
using Generic.DTO.Base.Handler.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.SystemLog
{

    public static class GenericLogHandlerFactory
    {
        public enum LogHandlerType
        {
            File,
            Database
        }

        public static AbstractGenericLogHandler GetLogHandler(GenericConfigureLogRequestDto req)
        {
            switch (logHandlerType)
            {
                case LogHandlerType.File:
                    return new GenericLogInFileHandler(req.path);
                case LogHandlerType.Database:
                    return new GenericLogInSqlServerHandler(req.connectionString, req.tableName);
                default:
                    throw new ArgumentException("Invalid log handler type");
            }
        }
    }

}
