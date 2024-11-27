﻿using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.ClaimUserOnSystem;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface IClaimOnSystemService : IGenericNormalService<ClaimUserOnSystem, ClaimOnSystemAddRequestDto, ClaimOnSystemAddResponseDto, ClaimOnSystemEditRequestDto, ClaimOnSystemEditResponseDto, ClaimOnSystemDeleteRequestDto, ClaimOnSystemDeleteResponseDto, ClaimOnSystemSearchResponseDto>
    {
        Task<IEnumerable<ClaimOnSystemSearchResponseDto>> GetCurrentUserClaims();
    }
}
