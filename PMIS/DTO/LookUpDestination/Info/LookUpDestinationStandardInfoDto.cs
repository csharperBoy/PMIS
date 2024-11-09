using Generic.Base.Handler.Map;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination.Info
{
    public class LookUpDestinationStandardInfoDto
    {
        public async Task<LookUpDestinationStandardInfoDto> extraMapFromBaseModel(PMIS.Models.LookUpDestination baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map<PMIS.Models.LookUpDestination, LookUpDestinationStandardInfoDto>(baseModel,this);            
            this.LookUpValuesInfo = await Task.WhenAll(baseModel.FkLookUp.LookUpValues.Select(v => (new LookUpValueShortInfoDto()).extraMapFromBaseModel(v)).ToList());

            return this;
        }
        public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string TableName { get; set; } = null!;

        public string ColumnName { get; set; } = null!;

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        // public bool? FlgLogicalDelete { get; set; }

        public virtual LookUpShortInfoDto FkLookUpInfo { get; set; } = null!;
        public virtual ICollection<LookUpValueShortInfoDto> LookUpValuesInfo { get; set; } = new List<LookUpValueShortInfoDto>();
    }
}
