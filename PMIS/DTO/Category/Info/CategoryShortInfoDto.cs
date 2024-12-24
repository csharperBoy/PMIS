using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.Category.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Category.Info
{
    public class CategoryShortInfoDto : CategoryTinyInfoDto
    {

        public async Task<CategoryShortInfoDto> extraMapFromBaseModel(PMIS.Models.Category baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkIndicatorInfo = await (new IndicatorTinyInfoDto()).extraMapFromBaseModel(baseModel.FkIndicator);
            this.FkCategoryInfo = await (new CategoryTinyInfoDto()).extraMapFromBaseModel(baseModel.FkCategory);
            return this;
        }
        public virtual Category? FkParent { get; set; }

        public virtual LookUpValue FklkpType { get; set; } = null!;

        public virtual ICollection<IndicatorCategory> IndicatorCategories { get; set; } = new List<IndicatorCategory>();

        public virtual ICollection<Category> InverseFkParent { get; set; } = new List<Category>();


    }
}
