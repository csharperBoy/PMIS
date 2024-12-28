using PMIS.DTO.Category;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.User;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorCategory
{
    public class IndicatorCategoryColumnsDto
    {
       public IEnumerable<CategorySearchResponseDto> lstMasterCategory;
        public IEnumerable<CategorySearchResponseDto> lstDetailCategory;
        public async Task Initialize(ICategoryService categoryService, IIndicatorService indicatorService, int fkIndicatorId, int fkCategoryId)
        {

            // List<IndicatorSearchResponseDto> lstIndicator = new List<IndicatorSearchResponseDto>() { new IndicatorSearchResponseDto() { Id = 0, Title = "همه" } };
            (bool isSuccessIndicator, IEnumerable<IndicatorSearchResponseDto> lstIndicator) = await indicatorService.Search(new Generic.Service.DTO.Concrete.GenericSearchRequestDto()
            {
                filters = new List<Generic.Service.DTO.Concrete.GenericSearchFilterDto>()
                {
                    new Generic.Service.DTO.Concrete.GenericSearchFilterDto()
                    {
                        columnName = "ID",
                        LogicalOperator = Generic.Service.DTO.Concrete.LogicalOperator.Begin,
                        operation = fkIndicatorId != 0 ? Generic.Service.DTO.Concrete.FilterOperator.Equals : Generic.Service.DTO.Concrete.FilterOperator.NotEquals,
                        type = Generic.Service.DTO.Concrete.PhraseType.Condition,
                        value = fkIndicatorId.ToString()
                    },
                    new Generic.Service.DTO.Concrete.GenericSearchFilterDto()
                    {
                        columnName = "FlgLogicalDelete",
                        LogicalOperator = Generic.Service.DTO.Concrete.LogicalOperator.And,
                        operation = Generic.Service.DTO.Concrete.FilterOperator.Equals,
                        type = Generic.Service.DTO.Concrete.PhraseType.Condition,
                        value = false.ToString()
                    }
                }
            });
            // lstIndicator.AddRange(lstIndicator1);

            (bool isSuccessCategory, IEnumerable<CategorySearchResponseDto> lstCategory) = await categoryService.Search(new Generic.Service.DTO.Concrete.GenericSearchRequestDto()
            {
                filters = new List<Generic.Service.DTO.Concrete.GenericSearchFilterDto>()
                {
                    new Generic.Service.DTO.Concrete.GenericSearchFilterDto()
                    {
                        columnName = "FlgLogicalDelete",
                        LogicalOperator = Generic.Service.DTO.Concrete.LogicalOperator.And,
                        operation = Generic.Service.DTO.Concrete.FilterOperator.Equals,
                        type = Generic.Service.DTO.Concrete.PhraseType.Condition,
                        value = false.ToString()
                    }
                }
            });
             lstMasterCategory = lstCategory.Where(c => c.FkParentInfo == null).ToList();
          lstDetailCategory = lstCategory.Where(c => c.FkParentInfo != null).ToList();
            if (fkCategoryId != 0)
            {
                lstDetailCategory = lstDetailCategory.Where(c => c.Id == fkCategoryId).ToList();
                lstMasterCategory = lstMasterCategory.Where(c => c.Id == lstDetailCategory.First().FkParentInfo.Id);
            }


            FilterColumns.AddRange(new List<DataGridViewColumn>()
            {
             new DataGridViewComboBoxColumn()
               {
                   HeaderText = "شاخص",
                   Name = "FkIndicatorId",
                   DataPropertyName = "FkIndicatorId",
                   DisplayMember = "Title",
                   ValueMember = "Id",
                   DataSource = lstIndicator.ToArray(),
                   ReadOnly = false,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 5
               },
             //new DataGridViewComboBoxColumn()
             //  {
             //      HeaderText = "دسته بندی اصلی",
             //      Name = "VrtParentCategory",
             //      DataPropertyName = "VrtParentCategoryId",
             //      DisplayMember = "Title",
             //      ValueMember = "Id",
             //      DataSource = lstMasterCategory.ToArray(),
             //      ReadOnly = false,
             //      Visible = true,
             //      MinimumWidth = 150,
             //      DividerWidth = 5
             //  },
             new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دسته بندی",
                   Name = "FkCategoryId",
                   DataPropertyName = "FkCategoryId",
                   DisplayMember = "Title",
                   ValueMember = "Id",
                   DataSource = lstDetailCategory.ToArray(),
                   ReadOnly = false,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "ترتیب",
                   Name = "OrderNum",
                   DataPropertyName = "OrderNum",
                   ReadOnly = false,
                   Visible = true,
                  MinimumWidth = 50,
                   DividerWidth = 5
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "توضیحات",
                   Name = "Description",
                   DataPropertyName = "Description",
                   ReadOnly = false,
                   Visible = true,
                  MinimumWidth = 200,
                   DividerWidth = 5
               }
        });

            ResultColumns.AddRange(new List<DataGridViewColumn>()
            {
                new DataGridViewTextBoxColumn()
                {
                    HeaderText = "شناسه",
                    Name = "Id",
                    DataPropertyName = "Id",
                    ReadOnly = true,
                    Visible = false,
                },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "شاخص",
                   Name = "FkIndicatorId",
                   DataPropertyName = "FkIndicatorId",
                   DisplayMember = "Title",
                   ValueMember = "Id",
                   DataSource = lstIndicator.ToArray(),
                   ReadOnly = true,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 0
               }, new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دسته بندی اصلی",
                   Name = "VrtParentCategory",
                   DataPropertyName = "VrtParentCategoryId",
                   DisplayMember = "Title",
                   ValueMember = "Id",
                   DataSource = lstMasterCategory.ToArray(),
                   ReadOnly = true,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 0
               }, new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دسته بندی",
                   Name = "FkCategoryId",
                   DataPropertyName = "FkCategoryId",
                   DisplayMember = "Title",
                   ValueMember = "Id",
                   DataSource = lstDetailCategory.ToArray(),
                   ReadOnly = true,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 0
               }   ,
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "ترتیب",
                   Name = "OrderNum",
                   DataPropertyName = "OrderNum",
                   ReadOnly = true,
                   Visible = true,
                  MinimumWidth = 50,
                   DividerWidth = 0
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "توضیحات",
                   Name = "Description",
                   DataPropertyName = "Description",
                   ReadOnly = true,
                   Visible = true,
                  MinimumWidth = 200,
                   DividerWidth = 0
               }
        });
        }

        public List<DataGridViewColumn> FilterColumns { get; set; } = new List<DataGridViewColumn>();
        public List<DataGridViewColumn> ResultColumns { get; set; } = new List<DataGridViewColumn>();
    }
}
