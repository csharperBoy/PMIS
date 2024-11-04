using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpValue;
using PMIS.Models;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationSearchResponseDto
    {
        public LookUpDestinationSearchResponseDto extraMapFromBaseModel(PMIS.Models.LookUpDestination sourceModel , LookUpDestinationSearchResponseDto destinationModel )
        {
            //this.FkLookUpInfo = (new LookUpSearchResponseDto()).extraMapFromBaseModel(baseModel.FkLookUp);
            destinationModel.LookUpValuesInfo = sourceModel.FkLookUp.LookUpValues.Select(v => (new LookUpValueSearchResponseDto(v)).extraMapFromBaseModel(v)).ToList();

            return destinationModel;
        }
        public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string TableName { get; set; } = null!;

        public string ColumnName { get; set; } = null!;

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

       // public bool? FlgLogicalDelete { get; set; }

        public virtual LookUpSearchResponseDto FkLookUpInfo { get; set; } = null!;
        public virtual ICollection<LookUpValueSearchResponseDto> LookUpValuesInfo { get; set; } = new List<LookUpValueSearchResponseDto>();

    }


}
