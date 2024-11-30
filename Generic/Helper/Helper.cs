using Newtonsoft.Json;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Helper
{
    public static class Helper
    {
        public static class Convert
        {
            public static string CapitalizeFirstLetter(string input)
            {
                if (string.IsNullOrEmpty(input))
                    return input;

                return char.ToUpper(input[0]) + input.Substring(1).ToLower();
            }
            public static string ConvertObjectToJson(object? _obj, ReferenceLoopHandling _referenceLoop = ReferenceLoopHandling.Serialize, PreserveReferencesHandling _PreserveReferencesHandling = PreserveReferencesHandling.Objects, int _maxDepth = 1)
            {
                try
                {
                    if (_obj == null)
                        return null;

                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    };

                    var entityJson = JsonConvert.SerializeObject(_obj, settings);
                    return entityJson;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            public static string ConvertGregorianToShamsi(DateTime? DateTime, string Format = null)

            {
                PersianCalendar PersianCalendar = new PersianCalendar();
                string Result = "";
                if (DateTime is not null)
                {
                    if (Format is null)
                    {
                        Result = $"{PersianCalendar.GetYear((DateTime)DateTime)}/{PersianCalendar.GetMonth((DateTime)DateTime).ToString("00")}/{PersianCalendar.GetDayOfMonth((DateTime)DateTime).ToString("00")} در ساعت {PersianCalendar.GetHour((DateTime)DateTime).ToString("00")}:{PersianCalendar.GetMinute((DateTime)DateTime).ToString("00")}";
                    }
                    else
                    {
                        if (Format.Contains("RRRR"))
                        {
                            Result += $"{PersianCalendar.GetYear((DateTime)DateTime)}";
                        }
                        if (Format.Count() > 4 && !char.IsLetterOrDigit(Format[4]))
                        {
                            Result += Format[4];
                        }
                        if (Format.Contains("MM"))
                        {
                            Result += $"{PersianCalendar.GetMonth((DateTime)DateTime).ToString("00")}";
                        }
                        if (Format.Count() > 7 && !char.IsLetterOrDigit(Format[7]))
                        {
                            Result += Format[7];
                        }
                        if (Format.Contains("DD"))
                        {
                            Result += $"{PersianCalendar.GetDayOfMonth((DateTime)DateTime).ToString("00")}";
                        }
                        if (Format.Count() > 10 && !char.IsLetterOrDigit(Format[10]))
                        {
                            Result += Format[10];
                        }
                        if (Format.Contains("HH24"))
                        {
                            Result += $"{PersianCalendar.GetHour((DateTime)DateTime).ToString("00")}";
                        }
                        if (Format.Count() > 13 && !char.IsLetterOrDigit(Format[13]))
                        {
                            Result += Format[13];
                        }
                        if (Format.Contains("MI"))
                        {
                            Result += $"{PersianCalendar.GetMinute((DateTime)DateTime).ToString("00")}";
                        }
                        if (Format.Count() > 16 && !char.IsLetterOrDigit(Format[16]))
                        {
                            Result += Format[16];
                        }
                        if (Format.Contains("SS"))
                        {
                            Result += $"{PersianCalendar.GetSecond((DateTime)DateTime).ToString("00")}";
                        }
                        if (Format.Count() > 19 && !char.IsLetterOrDigit(Format[19]))
                        {
                            Result += Format[19];
                        }
                        if (Format.Contains("MS"))
                        {
                            Result += $"{PersianCalendar.GetMilliseconds((DateTime)DateTime).ToString("00")}";
                        }
                    }
                    if (Result == "")
                    {
                        Result = DateTime.ToString();
                    }
                }
                return Result;
            }

            public static DateTime? ConvertShamsiToGregorian(string persianDate)
            {
                try
                {
                    var persianCalendar = new PersianCalendar();

                    var parts = persianDate.Split('/');
                    var year = int.Parse(parts[0]);
                    var month = int.Parse(parts[1]);
                    var day = int.Parse(parts[2]);

                    DateTime dateTime = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

                    return dateTime;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static List<DateTime> GetDatesBetween(DateTime start, DateTime end)
            {
                List<DateTime> dates = new List<DateTime>();
                DateTime currentDate = start;

                while (currentDate <= end)
                {
                    dates.Add(currentDate);
                    currentDate = currentDate.AddDays(1);
                }

                return dates;
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

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

    public class Wrapper<T>
    {
        public T Value { get; set; }

        public Wrapper(T value)
        {
            Value = value;
        }
    }
}
