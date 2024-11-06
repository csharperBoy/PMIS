using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Models;

namespace PMIS.DTO.LookUp
{
    public class LookUpSearchResponseDto: LookUpStandardInfoDto
    {
        //public LookUpSearchResponseDto extraMapFromBaseModel(PMIS.Models.LookUp baseModel)
       // {
       //     this.LookUpDestinationsInfo = baseModel.LookUpDestinations.Select(d => (new LookUpDestinationSearchResponseDto()).extraMapFromBaseModel(d)).ToList();
       //     this.LookUpValuesInfo = baseModel.LookUpValues.Select(v => (new LookUpValueSearchResponseDto()).extraMapFromBaseModel(v)).ToList();
       //     return this;
       // }
       // public int Id { get; set; }

       // public string Code { get; set; } = null!;

       // public string Title { get; set; } = null!;

       // public string? Description { get; set; }

       //// public string? SystemInfo { get; set; }

       // //public bool? FlgLogicalDelete { get; set; }

       // public virtual ICollection<LookUpDestinationSearchResponseDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationSearchResponseDto>();

       // public virtual ICollection<LookUpValueSearchResponseDto> LookUpValuesInfo { get; set; } = new List<LookUpValueSearchResponseDto>();
    }

  
}
