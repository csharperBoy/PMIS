using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimOnSystem.Info;
using PMIS.DTO.ClaimUserOnIndicator.Info;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.Models;
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
            this.ClaimOnSystemsInfo = await Task.WhenAll(baseModel.ClaimOnSystems.Select(v => (new ClaimOnSystemTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.ClaimUserOnIndicatorsInfo = await Task.WhenAll(baseModel.ClaimUserOnIndicators.Select(v => (new ClaimUserOnIndicatorTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.LookUpDestinationsInfo = await Task.WhenAll(baseModel.FkLookUp.LookUpDestinations.Select(v => (new LookUpDestinationTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.FkLookUpInfo = await (new LookUpTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLookUp);
            return this;
        }

        public virtual ICollection<ClaimOnSystemTinyInfoDto> ClaimOnSystemsInfo { get; set; } = new List<ClaimOnSystemTinyInfoDto>();

        
        public virtual ICollection<ClaimUserOnIndicatorTinyInfoDto> ClaimUserOnIndicatorsInfo { get; set; } = new List<ClaimUserOnIndicatorTinyInfoDto>();

        public virtual LookUpTinyInfoDto FkLookUpInfo { get; set; } = null!;
        public virtual ICollection<LookUpDestinationTinyInfoDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationTinyInfoDto>();

    }
}
