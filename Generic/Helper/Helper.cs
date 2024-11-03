using Newtonsoft.Json;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    //public static class PredicateBuilder
    //{
    //    public static Expression<Func<T, bool>> True<T>() { return f => true; }
    //    public static Expression<Func<T, bool>> False<T>() { return f => false; }

    //    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    //    {
    //        var parameter = expr1.Parameters[0];
    //        var leftVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
    //        var left = leftVisitor.Visit(expr1.Body);
    //        var rightVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
    //        var right = rightVisitor.Visit(expr2.Body);
    //        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
    //    }

    //    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    //    {
    //        var parameter = expr1.Parameters[0];
    //        var leftVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
    //        var left = leftVisitor.Visit(expr1.Body);
    //        var rightVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
    //        var right = rightVisitor.Visit(expr2.Body);
    //        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
    //    }

    //    private class ReplaceExpressionVisitor : ExpressionVisitor
    //    {
    //        private readonly Expression _oldValue;
    //        private readonly Expression _newValue;

    //        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
    //        {
    //            _oldValue = oldValue;
    //            _newValue = newValue;
    //        }

    //        public override Expression Visit(Expression node)
    //        {
    //            if (node == _oldValue)
    //                return _newValue;
    //            return base.Visit(node);
    //        }
    //    }
    //}
}
