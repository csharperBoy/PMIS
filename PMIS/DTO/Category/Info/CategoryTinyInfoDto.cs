using Generic.Base.Handler.Map;
using PMIS.DTO.Category.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Category.Info
{
    public class CategoryTinyInfoDto
    {
        public async Task<CategoryTinyInfoDto> extraMapFromBaseModel(PMIS.Models.Category baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CategoryCode { get; set; }
        public byte? OrderCategory { get; set; }
        public string? Description { get; set; }
    }
}
