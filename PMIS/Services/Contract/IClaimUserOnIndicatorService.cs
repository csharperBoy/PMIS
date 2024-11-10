using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.ClaimUserOnIndicator;
using PMIS.DTO.LookUpDestination;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface IClaimUserOnIndicatorService : IGenericNormalService<ClaimUserOnIndicator, ClaimUserOnIndicatorAddRequestDto, ClaimUserOnIndicatorAddResponseDto, ClaimUserOnIndicatorEditRequestDto, ClaimUserOnIndicatorEditResponseDto, ClaimUserOnIndicatorDeleteRequestDto, ClaimUserOnIndicatorDeleteResponseDto, ClaimUserOnIndicatorSearchResponseDto>
    {
    }
}
