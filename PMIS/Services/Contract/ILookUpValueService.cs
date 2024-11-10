﻿using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface ILookUpValueService : IGenericNormalService<LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>
    {
        Task<List<LookUpValueShortInfoDto>> GetList(string _tableName, string _columnName, string _code);
    }
}
