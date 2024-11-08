using Generic.Base.Handler.Map;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination.Info
{
    public class LookUpDestinationTinyInfoDto
    {
        public LookUpDestinationTinyInfoDto extraMapFromBaseModel(PMIS.Models.LookUpDestination baseModel)
        {
           // LookUpDestinationTinyInfoDto temp = GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map<PMIS.Models.LookUpDestination, LookUpDestinationTinyInfoDto>(baseModel).Result;

            return this;
        }
        public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string TableName { get; set; } = null!;

        public string ColumnName { get; set; } = null!;

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        // public bool? FlgLogicalDelete { get; set; }

       
    }
}
