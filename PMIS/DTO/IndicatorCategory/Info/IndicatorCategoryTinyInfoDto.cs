using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorCategory.Info
{
    public class IndicatorCategoryTinyInfoDto
    {
        public async Task<IndicatorCategoryTinyInfoDto> extraMapFromBaseModel(PMIS.Models.IndicatorCategory baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public int Id { get; set; }
      
        public int OrderNum { get; set; }

        public string? Description { get; set; }

        
    }
}
