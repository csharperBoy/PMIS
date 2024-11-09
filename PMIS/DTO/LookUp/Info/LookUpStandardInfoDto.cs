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
    public class LookUpStandardInfoDto:LookUpTinyInfoDto
    {
        public async Task<LookUpStandardInfoDto> extraMapFromBaseModel(PMIS.Models.LookUp baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            this.LookUpDestinationsInfo = await Task.WhenAll(baseModel.LookUpDestinations.Select(d => (new LookUpDestinationShortInfoDto()).extraMapFromBaseModel(d)).ToList());
            this.LookUpValuesInfo = await Task.WhenAll(baseModel.LookUpValues.Select(v => (new LookUpValueShortInfoDto()).extraMapFromBaseModel(v)).ToList());
            return this;
        }
       

        public virtual ICollection<LookUpDestinationShortInfoDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationShortInfoDto>();

        public virtual ICollection<LookUpValueShortInfoDto> LookUpValuesInfo { get; set; } = new List<LookUpValueShortInfoDto>();
    }
}
