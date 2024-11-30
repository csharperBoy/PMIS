using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.ClaimUserOnSystem;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface IClaimOnSystemService : IGenericNormalService<ClaimUserOnSystem, ClaimUserOnSystemAddRequestDto, ClaimUserOnSystemAddResponseDto, ClaimUserOnSystemEditRequestDto, ClaimUserOnSystemEditResponseDto, ClaimUserOnSystemDeleteRequestDto, ClaimUserOnSystemDeleteResponseDto, ClaimUserOnSystemSearchResponseDto>
    {
        Task<IEnumerable<ClaimUserOnSystemSearchResponseDto>> GetCurrentUserClaims();
    }
}
