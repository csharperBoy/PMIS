using Microsoft.Extensions.Logging;

namespace Generic.Base.Handler.SystemLog.Contract
{
    public interface IGenericLogHandler //:Serilog.ILogger//  ILogger
    { 
        Serilog.ILogger CreateLogger();
    }
}
