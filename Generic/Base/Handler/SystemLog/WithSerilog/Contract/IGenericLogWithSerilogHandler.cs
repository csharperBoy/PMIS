using Microsoft.Extensions.Logging;

namespace Generic.Base.Handler.SystemLog.WithSerilog.Contract
{
    public interface IGenericLogWithSerilogHandler //:Serilog.ILogger//  ILogger
    {
        Serilog.ILogger CreateLogger();
    }
}
