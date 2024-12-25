using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.Category.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.IndicatorCategory.Info;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.ClaimUserOnIndicator.Info;

namespace PMIS.DTO.Category.Info
{
    public class CategoryShortInfoDto : CategoryTinyInfoDto
    {

        public async Task<CategoryShortInfoDto> extraMapFromBaseModel(PMIS.Models.Category baseModel)
        {
            if (baseModel == null)
                return null;
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FklkpTypeInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FklkpType);
            this.FkParentInfo = await (new CategoryTinyInfoDto()).extraMapFromBaseModel(baseModel.FkParent);
            
            this.IndicatorCategoriesInfo = await Task.WhenAll(baseModel.IndicatorCategories.Select(v => (new IndicatorCategoryTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.InverseFkParentInfo = await Task.WhenAll(baseModel.InverseFkParent.Select(v => (new CategoryTinyInfoDto()).extraMapFromBaseModel(v)).ToList());

            return this;
        }
        public virtual CategoryTinyInfoDto? FkParentInfo { get; set; }

        public virtual LookUpValueTinyInfoDto FklkpTypeInfo { get; set; } = null!;

        public virtual ICollection<IndicatorCategoryTinyInfoDto> IndicatorCategoriesInfo { get; set; } = new List<IndicatorCategoryTinyInfoDto>();

        public virtual ICollection<CategoryTinyInfoDto> InverseFkParentInfo { get; set; } = new List<CategoryTinyInfoDto>();

       
    }
}
