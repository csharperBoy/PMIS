using Generic.Base.Handler.SystemLog.Abstract;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.SystemLog.Concrete
{
    public class GenericLogInSqlServerHandler : AbstractGenericLogHandler
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public GenericLogInSqlServerHandler(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        public override ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.MSSqlServer(
                    connectionString: _connectionString,
                    tableName: _tableName,
                    autoCreateSqlTable: true
                )
                .CreateLogger();
        }
    }
}
