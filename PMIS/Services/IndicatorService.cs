using Generic.Service;
using Generic.Service.Abstract;
using Microsoft.EntityFrameworkCore;
using PMIS.DTO.Indicator;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class IndicatorService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto>        
    : NormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
    {
         
        //protected override async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //{
           
        //    if (source is Indicator src && destination is IndicatorAddResponseDto dest)
        //    {
        //        //dest.ErrorMessage = $"{src.FirstName} {src.LastName}";
                
        //    }
        //    return destination;

        //}


    }
}
