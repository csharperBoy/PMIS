using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMIS.DTO.LookUp;
using PMIS.Models;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationSearchResponseDto
    {
        public LookUpDestinationSearchResponseDto extraMapFromBaseModel(PMIS.Models.LookUpDestination baseModel)
        {
            this.FkLookUpInfo = (new LookUpSearchResponseDto()).extraMapFromBaseModel(baseModel.FkLookUp);
            return this;
        }
        public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string TableName { get; set; } = null!;

        public string ColumnName { get; set; } = null!;

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

       // public bool? FlgLogicalDelete { get; set; }

        public virtual LookUpSearchResponseDto FkLookUpInfo { get; set; } = null!;
    }

  
}
