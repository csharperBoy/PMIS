using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.SystemLog.WithSerilog.DTO
{
    public class GenericConfigureLogWithSerilogInSqlServerRequestDto
    {
        public string connectionString { get; set; }
        public string tableName { get; set; }
    }
}