using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpDestination.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpValue.Info
{
    public class LookUpValueShortInfoDto
    {
        public async Task<LookUpValueShortInfoDto> extraMapFromBaseModel(PMIS.Models.LookUpValue baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            this.ClaimsInfo = await Task.WhenAll(baseModel.Claims.Select(v => (new ClaimTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.LookUpDestinationsInfo = await Task.WhenAll(baseModel.FkLookUp.LookUpDestinations.Select(v => (new LookUpDestinationTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.FkLookUpInfo = await (new LookUpTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLookUp);
            return this;
        }


        public virtual ICollection<ClaimTinyInfoDto> ClaimsInfo { get; set; } = new List<ClaimTinyInfoDto>();

        public virtual LookUpTinyInfoDto FkLookUpInfo { get; set; } = null!;
        public virtual ICollection<LookUpDestinationTinyInfoDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationTinyInfoDto>();

    }
}
