using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimUserOnIndicator.Info
{
    public class ClaimUserOnIndicatorTinyInfoDto
    {
        public async Task<ClaimUserOnIndicatorTinyInfoDto> extraMapFromBaseModel(PMIS.Models.ClaimUserOnIndicator baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public int Id { get; set; }

        public string? Description { get; set; }

    }
}
