using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnSystem.Info;
using PMIS.DTO.ClaimUserOnIndicator.Info;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpValue.Info
{
    public class LookUpValueStandardInfoDto: LookUpValueTinyInfoDto
    {
        public async Task<LookUpValueStandardInfoDto> extraMapFromBaseModel(PMIS.Models.LookUpValue baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            this.ClaimOnSystemsInfo = await Task.WhenAll(baseModel.ClaimOnSystems.Select(v => (new ClaimUserOnSystemShortInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.ClaimUserOnIndicatorsInfo = await Task.WhenAll(baseModel.ClaimUserOnIndicators.Select(v => (new ClaimUserOnIndicatorShortInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.LookUpDestinationsInfo = await Task.WhenAll(baseModel.FkLookUp?.LookUpDestinations.Select(v => (new LookUpDestinationShortInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.FkLookUpInfo =await (new LookUpShortInfoDto()).extraMapFromBaseModel(baseModel.FkLookUp);
            return this;
        }

        public virtual ICollection<ClaimUserOnSystemShortInfoDto> ClaimOnSystemsInfo { get; set; } = new List<ClaimUserOnSystemShortInfoDto>();
        public virtual ICollection<ClaimUserOnIndicatorShortInfoDto> ClaimUserOnIndicatorsInfo { get; set; } = new List<ClaimUserOnIndicatorShortInfoDto>();

        public virtual LookUpShortInfoDto FkLookUpInfo { get; set; } = null!;
        public virtual ICollection<LookUpDestinationShortInfoDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationShortInfoDto>();

    }
}
