using AutoMapper;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemException.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.Map
{
    public static class GenericExceptionHandlerFactory
    {
        private static AbstractGenericExceptionHandler genericExceptionHandler;

        public enum ExceptionHandler
        {
            Generic
        }

        public static AbstractGenericExceptionHandler GetExceptionHandler(ExceptionHandler exceptionHandler)
        {
            switch (exceptionHandler)
            {
                case ExceptionHandler.Generic:
                    if (genericExceptionHandler == null)
                    {
                        genericExceptionHandler = new GenericExceptionHandler();
                    }
                    return genericExceptionHandler;

                default:
                    throw new ArgumentException("Invalid mapping mode");
            }
        }
        public static AbstractGenericExceptionHandler GetExceptionHandler(IServiceProvider serviceProvider, ExceptionHandler exceptionHandler)
        {
            switch (exceptionHandler)
            {
                case ExceptionHandler.Generic:
                    return serviceProvider.GetService<GenericExceptionHandler>();
                default:
                    throw new ArgumentException("Invalid exception handler");
            }
        }
    }
}
