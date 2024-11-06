using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Concrete;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUp.Info
{
    public class LookUpStandardInfoDto
    {
        public LookUpStandardInfoDto extraMapFromBaseModel(PMIS.Models.LookUp baseModel)
        {
            LookUpStandardInfoDto temp = GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map<PMIS.Models.LookUp, LookUpStandardInfoDto>(baseModel).Result;
            temp.LookUpDestinationsInfo = baseModel.LookUpDestinations.Select(d => (new LookUpDestinationShortInfoDto()).extraMapFromBaseModel(d)).ToList();
            temp.LookUpValuesInfo = baseModel.LookUpValues.Select(v => (new LookUpValueShortInfoDto()).extraMapFromBaseModel(v)).ToList();
            return temp;
        }
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        // public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        public virtual ICollection<LookUpDestinationShortInfoDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationShortInfoDto>();

        public virtual ICollection<LookUpValueShortInfoDto> LookUpValuesInfo { get; set; } = new List<LookUpValueShortInfoDto>();
    }
}
