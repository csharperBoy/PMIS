using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Composition;
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
    public class IndicatorService
        : GenericNormalService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto>
    {

        //protected override async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //{

        //    if (source is Indicator src && destination is IndicatorAddResponseDto dest)
        //    {
        //        //dest.ErrorMessage = $"{src.FirstName} {src.LastName}";

        //    }
        //    return destination;

        //}
        public IndicatorService(AbstractGenericRepository<Indicator, PmisContext> _repository, AbstractGenericMapHandler _mapper, AbstractGenericExceptionHandler _exceptionHandler) : base(_repository, _mapper, _exceptionHandler)
        {
        }
    }
}
