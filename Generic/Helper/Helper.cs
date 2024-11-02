using Newtonsoft.Json;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Helper
{
    public static class Helper
    {
        public static class Convert
        {
            public static string ConvertObjectToJson(object? obj)
            {
                try
                {
                    if (obj == null)
                        return null;
                    var entityJson = JsonConvert.SerializeObject(obj);
                    return entityJson;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public static class ServiceLog
        {
            // private Serilog.ILogger logHandler;

            public static void FinallyAction(
                Serilog.ILogger logHandler,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0
            )
            {
                try
                {
                    var calledPath = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
                    logHandler.Information("Calld in  {@calledPath} From {@sourceFilePath} and member name is {@memberName} and line is {@sourceLineNumber}."
                        , calledPath, memberName, sourceFilePath, sourceLineNumber);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
