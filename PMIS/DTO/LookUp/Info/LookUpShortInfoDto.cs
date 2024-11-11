using Generic.Base.Handler.Map;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUp.Info
{
    public class LookUpShortInfoDto: LookUpTinyInfoDto
    {
        public async Task<LookUpShortInfoDto> extraMapFromBaseModel(PMIS.Models.LookUp baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.LookUpDestinationsInfo = await Task.WhenAll(baseModel.LookUpDestinations.Select(d => (new LookUpDestinationTinyInfoDto()).extraMapFromBaseModel(d)).ToList());
            this.LookUpValuesInfo = await Task.WhenAll(baseModel.LookUpValues.Select(v => (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            return this;
        }
        public async Task<PMIS.Models.LookUp> extraMapToBaseModel( LookUpShortInfoDto Model)
        {

            Models.LookUp baseModel = new Models.LookUp();
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(Model, baseModel);

            //baseModel.LookUpDestinations = await Task.WhenAll(Model.LookUpDestinationsInfo.Select(d => (new LookUpDestinationTinyInfoDto()).extraMapToBaseModel(d)).ToList());
            //baseModel.LookUpValues = await Task.WhenAll(Model.LookUpValuesInfo.Select(v => (new LookUpValueTinyInfoDto()).extraMapToBaseModel(v)).ToList());
            return baseModel;
        }

        public virtual ICollection<LookUpDestinationTinyInfoDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationTinyInfoDto>();

        public virtual ICollection<LookUpValueTinyInfoDto> LookUpValuesInfo { get; set; } = new List<LookUpValueTinyInfoDto>();
    }
}
