using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimOnSystem.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimOnSystem.Info
{
    public class ClaimOnSystemTinyInfoDto
    {
        public async Task<ClaimOnSystemTinyInfoDto> extraMapFromBaseModel(PMIS.Models.ClaimOnSystem baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public int Id { get; set; }

       
        public string? Description { get; set; }

        
    }
}
