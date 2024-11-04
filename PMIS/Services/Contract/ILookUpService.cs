using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.LookUp;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface ILookUpService : IGenericNormalService<LookUp, LookUpAddRequestDto, LookUpAddResponseDto, LookUpEditRequestDto, LookUpEditResponseDto, LookUpDeleteRequestDto, LookUpDeleteResponseDto, LookUpSearchResponseDto>
    {
    }
}
