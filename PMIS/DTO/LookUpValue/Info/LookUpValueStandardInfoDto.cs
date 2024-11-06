using Generic.Base.Handler.Map;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpValue.Info
{
    public class LookUpValueStandardInfoDto
    {
        public LookUpValueStandardInfoDto extraMapFromBaseModel(PMIS.Models.LookUpValue baseModel)
        {
            LookUpValueStandardInfoDto temp = GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map<PMIS.Models.LookUpValue, LookUpValueStandardInfoDto>(baseModel, this);

            //this.FkLookUpInfo = (new LookUpSearchResponseDto()).extraMapFromBaseModel(baseModel.FkLookUp);
            temp.LookUpDestinationsInfo = baseModel.FkLookUp.LookUpDestinations.Select(v => (new LookUpDestinationShortInfoDto()).extraMapFromBaseModel(v)).ToList();
            temp.FkLookUpInfo = (new LookUpShortInfoDto()).extraMapFromBaseModel(baseModel.FkLookUp);
            return temp;
        }
        public int Id { get; set; }

        //public int FkLookUpId { get; set; }

        public string Value { get; set; } = null!;

        public string Display { get; set; } = null!;

        public int OrderNum { get; set; }

        public string? Description { get; set; }

        // public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        //  public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

        public virtual LookUpShortInfoDto FkLookUpInfo { get; set; } = null!;
        public virtual ICollection<LookUpDestinationShortInfoDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationShortInfoDto>();

    }
}
