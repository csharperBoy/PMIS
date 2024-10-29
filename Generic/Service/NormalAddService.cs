using Generic.Service.Abstract;
using Generic.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service
{
    internal class NormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto> : GenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
    {
        public override Task<TDestination> Map<TSource, TDestination>(TSource source)
        {
            return base.Map<TSource, TDestination>(source);
        }

        protected override Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            return base.ExtraMap(source, destination);
        }
    }
}
