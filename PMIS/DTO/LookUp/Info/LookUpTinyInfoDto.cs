using Generic.Base.Handler.Map;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUp.Info
{
    public class LookUpTinyInfoDto
    {
        public LookUpTinyInfoDto extraMapFromBaseModel(PMIS.Models.LookUp baseModel)
        {
            LookUpTinyInfoDto temp = GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map< PMIS.Models.LookUp, LookUpTinyInfoDto >(baseModel).Result;
            return temp;
        }
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        // public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

      
    }
}

