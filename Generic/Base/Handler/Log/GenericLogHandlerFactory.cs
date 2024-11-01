using Generic.Base.Handler.Log.Abstract;
using Generic.Base.Handler.Log.Concrete;
using Generic.Base.Handler.Log.Contract;
using Generic.DTO.Base.Handler.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.Log
{

    public static class GenericLogHandlerFactory
    {
        public enum LogHandlerType
        {
            File,
            Database
        }

        public static AbstractGenericLogHandler GetLogHandler(ConfigureGenericLogRequestDto req)
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
