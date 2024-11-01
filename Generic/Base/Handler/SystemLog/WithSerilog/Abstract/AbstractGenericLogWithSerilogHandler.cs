using Microsoft.Extensions.Logging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Generic.Base.Handler.SystemLog.WithSerilog.Contract;
namespace Generic.Base.Handler.SystemLog.WithSerilog.Abstract
{
    public abstract class AbstractGenericLogWithSerilogHandler : IGenericLogWithSerilogHandler
    {
        //public abstract IDisposable? BeginScope<TState>(TState state) 
        //    where TState : notnull ;

        //public abstract bool IsEnabled(LogLevel logLevel);

        //public abstract void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter);
        public abstract Serilog.ILogger CreateLogger();
    }
}
