using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnSystem.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimUserOnSystem.Info
{
    public class ClaimUserOnSystemTinyInfoDto
    {
        public async Task<ClaimUserOnSystemTinyInfoDto> extraMapFromBaseModel(PMIS.Models.ClaimUserOnSystem baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public int Id { get; set; }

       
        public string? Description { get; set; }

        public int FkLkpClaimOnSystemId { get; set; }

        public int FkUserId { get; set; }


    }
}
