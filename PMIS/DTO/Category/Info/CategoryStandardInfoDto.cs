using Generic.Base.Handler.Map;
using PMIS.DTO.IndicatorCategory.Info;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Category.Info
{
    public class CategoryStandardInfoDto : CategoryTinyInfoDto
    {
        public async Task<CategoryStandardInfoDto> extraMapFromBaseModel(PMIS.Models.Category baseModel)
        {
            if (baseModel == null)
                return null;
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkLkpCategoryTypeInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FklkpType);
            this.FkParentInfo = await (new CategoryShortInfoDto()).extraMapFromBaseModel(baseModel.FkParent);

            this.IndicatorCategoriesInfo = await Task.WhenAll(baseModel.IndicatorCategories.Select(v => (new IndicatorCategoryShortInfoDto()).extraMapFromBaseModel(v)).ToList());
            this.InverseFkParentInfo = await Task.WhenAll(baseModel.InverseFkParent.Select(v => (new CategoryShortInfoDto()).extraMapFromBaseModel(v)).ToList());

            return this;
        }
        public virtual CategoryShortInfoDto? FkParentInfo { get; set; }

        public virtual LookUpValueShortInfoDto FkLkpCategoryTypeInfo { get; set; } = null!;

        public virtual ICollection<IndicatorCategoryShortInfoDto> IndicatorCategoriesInfo { get; set; } = new List<IndicatorCategoryShortInfoDto>();

        public virtual ICollection<CategoryShortInfoDto> InverseFkParentInfo { get; set; } = new List<CategoryShortInfoDto>();

    }
}
