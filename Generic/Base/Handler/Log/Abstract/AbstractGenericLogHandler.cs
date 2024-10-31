using Generic.Base.Handler.Log.Contract;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.Log.Abstract
{
    public abstract class AbstractGenericLogHandler : IGenericLogHandler
    {
        //public abstract IDisposable? BeginScope<TState>(TState state) 
        //    where TState : notnull ;

        //public abstract bool IsEnabled(LogLevel logLevel);

        //public abstract void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter);
        public abstract Serilog.ILogger CreateLogger();
    }
}
