using Generic.Base.Handler.Map;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination.Info
{
    public class LookUpDestinationShortInfoDto : LookUpDestinationTinyInfoDto
    {
        public async Task<LookUpDestinationShortInfoDto> extraMapFromBaseModel(PMIS.Models.LookUpDestination baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            this.LookUpValuesInfo = await Task.WhenAll(baseModel.FkLookUp.LookUpValues.Select(v => (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.FkLookUpInfo = this.FkLookUpInfo = await (new LookUpTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLookUp);

            return this;
        }
        public virtual LookUpTinyInfoDto FkLookUpInfo { get; set; } = null!;
        public virtual ICollection<LookUpValueTinyInfoDto> LookUpValuesInfo { get; set; } = new List<LookUpValueTinyInfoDto>();
    }
}
