using Generic.Service;
using Generic.Service.Normal.Composition.Contract;
using Microsoft.EntityFrameworkCore;
using PMIS.DTO.Indicator;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface IIndicatorService : IGenericNormalService<Indicator,IndicatorAddRequestDto,IndicatorAddResponseDto,IndicatorEditRequestDto,IndicatorEditResponseDto,IndicatorDeleteRequestDto,IndicatorDeleteResponseDto,IndicatorSearchResponseDto>
    {
         
    }
}
