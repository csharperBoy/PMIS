using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.Claim;
using PMIS.DTO.LookUpDestination;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface IClaimService : IGenericNormalService<Claim, ClaimAddRequestDto, ClaimAddResponseDto, ClaimEditRequestDto, ClaimEditResponseDto, ClaimDeleteRequestDto, ClaimDeleteResponseDto, ClaimSearchResponseDto>
    {
    }
}
