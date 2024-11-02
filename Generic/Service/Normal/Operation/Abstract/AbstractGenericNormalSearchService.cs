using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.DTO.Service;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Abstract
{
    public class AbstractGenericNormalSearchService<TContext, TEntity, TEntitySearchResponseDto>
        : IGenericSearchService<TEntity, TEntitySearchResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntitySearchResponseDto : class, new()
    {
        private AbstractGenericRepository<TEntity, TContext> repository;
        private AbstractGenericMapHandler mapper;
        private AbstractGenericExceptionHandler exceptionHandler;
        private Serilog.ILogger logHandler;
        protected AbstractGenericNormalSearchService(
            AbstractGenericRepository<TEntity, TContext> _repository,
            AbstractGenericMapHandler _mapper,
            AbstractGenericExceptionHandler _exceptionHandler,
            AbstractGenericLogWithSerilogHandler _logHandler
            )
        {
            repository = _repository;
            mapper = _mapper;
            exceptionHandler = _exceptionHandler;
            logHandler = _logHandler.CreateLogger();
        }


        public async Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Search(GenericSearchRequestDto requestInput)
        {
            try
            {
                var filterExpression = BuildFilterExpression(requestInput.filters);

                // مرتب‌سازی
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = query => ApplySorting(query, requestInput.sorts);

                // فراخوانی GetPagingAsync برای دریافت نتایج
                 (IEnumerable<TEntity> entities,int totalCount) = await repository.GetPagingAsync(
                    filter: filterExpression,
                    orderBy: orderBy,
                    pageNumber: requestInput.pageNumber,
                    recordCount: requestInput.recordCount
                );

                // تبدیل نتیجه به DTO
                List<TEntitySearchResponseDto> results = entities.Select(entity => mapper.Map<TEntity, TEntitySearchResponseDto>(entity).Result).ToList();

                return (true, results);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Helper.Helper.ServiceLog.FinallyAction(logHandler);
            }
        }
        private Expression<Func<TEntity, bool>> BuildFilterExpression(List<GenericSearchFilterDto> filters)
        {
            Expression<Func<TEntity, bool>> filterExpression = e => true;
            foreach (var filter in filters)
            {
                filterExpression = CombineExpressions(filterExpression, BuildSingleFilterExpression(filter), filter.NextLogicalOperator);
            }
            return filterExpression;
        }
        private Expression<Func<TEntity, bool>> BuildSingleFilterExpression(GenericSearchFilterDto filter)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "entity");
            var member = Expression.Property(parameter, filter.columnName);
            var constant = Expression.Constant(Convert.ChangeType(filter.value, member.Type));

            Expression body = filter.operation switch
            {
                FilterOperator.Equals => Expression.Equal(member, constant),
                FilterOperator.NotEquals => Expression.NotEqual(member, constant),
                FilterOperator.GreaterThan => Expression.GreaterThan(member, constant),
                FilterOperator.LessThan => Expression.LessThan(member, constant),
                FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
                FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
                FilterOperator.Contains => Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant),
                FilterOperator.StartsWith => Expression.Call(member, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant),
                FilterOperator.EndsWith => Expression.Call(member, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), constant),
                _ => throw new NotSupportedException($"Filter operation '{filter.operation}' is not supported.")
            };

            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }
        private Expression<Func<TEntity, bool>> CombineExpressions(Expression<Func<TEntity, bool>> expr1, Expression<Func<TEntity, bool>> expr2, LogicalOperator logicalOperator)
        {
            switch (logicalOperator)
            {
                case LogicalOperator.And:
                    return Expression.Lambda<Func<TEntity, bool>>(
                        Expression.AndAlso(expr1.Body, expr2.Body),
                        expr1.Parameters.Single()
                    );
                case LogicalOperator.Or:
                    return Expression.Lambda<Func<TEntity, bool>>(
                        Expression.OrElse(expr1.Body, expr2.Body),
                        expr1.Parameters.Single()
                    );
                case LogicalOperator.End:
                    return expr1;
                default:
                    throw new NotSupportedException($"Logical operator '{logicalOperator}' is not supported.");
            }
        }
        private IOrderedQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, List<GenericSearchSortDto> sorts)
        {
            IOrderedQueryable<TEntity> orderedQuery = null;

            for (int i = 0; i < sorts.Count; i++)
            {
                var sort = sorts[i];
                var parameter = Expression.Parameter(typeof(TEntity), "entity");
                var member = Expression.Property(parameter, sort.columnName);
                var keySelector = Expression.Lambda(member, parameter);
                if (i == 0)
                {
                    orderedQuery = sort.direction == SortDirection.Ascending
                        ? query.OrderBy((dynamic)keySelector)
                        : query.OrderByDescending((dynamic)keySelector);
                }
                else
                {
                    orderedQuery = sort.direction == SortDirection.Ascending
                        ? orderedQuery.ThenBy((dynamic)keySelector)
                        : orderedQuery.ThenByDescending((dynamic)keySelector);
                }
            }
            return orderedQuery ?? (IOrderedQueryable<TEntity>)query;
        }
}
