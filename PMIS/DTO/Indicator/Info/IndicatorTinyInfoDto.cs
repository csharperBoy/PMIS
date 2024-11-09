using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.IndicatorCategory.Info;
using PMIS.DTO.IndicatorValue.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Indicator.Info
{
    public class IndicatorTinyInfoDto
    {
        public async Task<IndicatorTinyInfoDto> extraMapFromBaseModel(PMIS.Models.Indicator baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;       

        public string? Formula { get; set; }

        public string? Description { get; set; }

        public string? SystemInfo { get; set; }

        public bool? FlgLogicalDelete { get; set; }

       
    }
}
