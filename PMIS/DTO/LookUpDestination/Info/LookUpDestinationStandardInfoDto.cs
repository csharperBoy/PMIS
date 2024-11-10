using Generic.Base.Handler.Map;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination.Info
{
    public class LookUpDestinationStandardInfoDto: LookUpDestinationTinyInfoDto
    {
        public async Task<LookUpDestinationStandardInfoDto> extraMapFromBaseModel(PMIS.Models.LookUpDestination baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            this.LookUpValuesInfo = await Task.WhenAll(baseModel.FkLookUp.LookUpValues.Select(v => (new LookUpValueShortInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.FkLookUpInfo = this.FkLookUpInfo = await (new LookUpShortInfoDto()).extraMapFromBaseModel(baseModel.FkLookUp);

            return this;
        }
        public async Task<Models.LookUpDestination> extraMapToBaseModel(LookUpDestinationStandardInfoDto Model)
        {
            Models.LookUpDestination baseModel = new Models.LookUpDestination();
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(Model, baseModel);
            //this.LookUpValuesInfo = await Task.WhenAll(baseModel.FkLookUp.LookUpValues.Select(v => (new LookUpValueShortInfoDto()).extraMapFromBaseModel(v)).ToList());
            //this.FkLookUpInfo = this.FkLookUpInfo = await (new LookUpShortInfoDto()).extraMapFromBaseModel(baseModel.FkLookUp);

            baseModel.FkLookUp = await (new LookUpShortInfoDto()).extraMapToBaseModel(Model.FkLookUpInfo);
            return baseModel;
        }
        public virtual LookUpShortInfoDto FkLookUpInfo { get; set; } = null!;
        public virtual ICollection<LookUpValueShortInfoDto> LookUpValuesInfo { get; set; } = new List<LookUpValueShortInfoDto>();
    }
}
