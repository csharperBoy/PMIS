using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.DTO.Base.Handler.SystemLog.Serilog;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.SystemLog.WithSerilog.Concrete
{
    public class GenericLogWithSerilogInSqlServerHandler : AbstractGenericLogWithSerilogHandler
    {
        private GenericConfigureLogWithSerilogRequestDto req;
        public GenericLogWithSerilogInSqlServerHandler(GenericConfigureLogWithSerilogRequestDto _req)
        {
            req = _req;
        }

        public override ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                 .MinimumLevel.Is(req.minimumLevel)
                .WriteTo.MSSqlServer(
                    connectionString: req.inSqlServerConfig.connectionString,
                    tableName: req.inSqlServerConfig.tableName,
                    autoCreateSqlTable: true
                )
                .CreateLogger();
        }
    }
}
