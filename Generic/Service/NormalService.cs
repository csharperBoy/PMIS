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
    public class NormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto> 
         : GenericNormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto>          
        where TContext : DbContext
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
    {
         
    }
}
