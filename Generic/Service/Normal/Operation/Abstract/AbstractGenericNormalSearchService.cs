using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Helper;
using Generic.Repository.Abstract;
using Generic.Service.DTO;
using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
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
                var includeProperties = "";
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy;
                if (requestInput.sorts == null)
                    orderBy = null;
                else
                    orderBy = query => ApplySorting(query, requestInput.sorts);

                (IEnumerable<TEntity> entities, int totalCount) = await repository.GetPagingAsync(
                   filter: filterExpression,
                   orderBy: orderBy,
                   includeProperties: includeProperties,
                   pageNumber: requestInput.pageNumber,
                   recordCount: requestInput.recordCount
               );
                
                List<TEntitySearchResponseDto> results = entities.Select(entity => mapper.Map<TEntity, TEntitySearchResponseDto>(entity).Result).ToList();
                foreach (var entity in entities)
                {
                    repository.SetEntityStateAsync<TEntity>(entity, EntityState.Detached);
                }
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
                if (filter.type == PhraseType.Condition)
                    filterExpression = CombineExpressions(filterExpression, BuildSingleFilterExpression(filter), filter.LogicalOperator);
                if (filter.InternalFilters != null && filter.InternalFilters.Any())
                    filterExpression = CombineExpressions(filterExpression, BuildFilterExpression(filter.InternalFilters), filter.LogicalOperator);
            }
            return filterExpression;
        }
        private Expression<Func<TEntity, bool>> BuildSingleFilterExpression(GenericSearchFilterDto filter)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "entity");
            var member = Expression.Property(parameter, filter.columnName);
            //var constant = Expression.Constant(Convert.ChangeType(filter.value, member.Type));
            object convertedValue;
            if (Nullable.GetUnderlyingType(member.Type) != null)
            {
                convertedValue = string.IsNullOrEmpty(filter.value) ? null : Convert.ChangeType(filter.value, Nullable.GetUnderlyingType(member.Type));
            }
            else
            {
                convertedValue = Convert.ChangeType(filter.value, member.Type);
            }

            var constant = Expression.Constant(convertedValue, member.Type);

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
                    return expr1.And(expr2);
                case LogicalOperator.Or:
                    return expr1.Or(expr2);
                case LogicalOperator.Begin:
                    return expr2;
                default:
                    throw new NotSupportedException($"Logical operator '{logicalOperator}' is not supported.");
            }
        }



        private IOrderedQueryable<TEntity>? ApplySorting(IQueryable<TEntity> query, List<GenericSearchSortDto> sorts)
        {
            if (sorts == null)
                return null;
            IOrderedQueryable<TEntity> orderedQuery = null;
            for (int i = 0; i < sorts.Count; i++)
            {
                var sort = sorts[i];
                var parameter = Expression.Parameter(typeof(TEntity), "entity");
                var member = Expression.Property(parameter, sort.columnName);
                var keySelector = Expression.Lambda(member, parameter);

                var methodName = (i == 0) ?
                    (sort.direction == SortDirection.Ascending ? "OrderBy" : "OrderByDescending") :
                    (sort.direction == SortDirection.Ascending ? "ThenBy" : "ThenByDescending");

                var method = typeof(Queryable).GetMethods()
                    .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .Single()
                    .MakeGenericMethod(typeof(TEntity), member.Type);

                orderedQuery = (IOrderedQueryable<TEntity>)method.Invoke(null, new object[] { (i == 0 ? query : orderedQuery), keySelector });
            }

            return orderedQuery ?? (IOrderedQueryable<TEntity>)query;
        }

    }
}
