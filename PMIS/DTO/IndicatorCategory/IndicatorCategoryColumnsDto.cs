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
        public async Task Initialize(ILookUpValueService lookUpValueService, IIndicatorService indicatorService, int fkIndicatorId)
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination = await lookUpValueService.GetList("IndicatorCategory");

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
                    }
                }
            });
            // lstIndicator.AddRange(lstIndicator1);

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
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpCategoryType")).Single().Title,
                   Name = "FkLkpCategoryTypeId",
                   DataPropertyName = "FkLkpCategoryTypeId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpCategoryTypeID", "LkpCategoryType"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               }, new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpCategoryMaster")).Single().Title,
                   Name = "FkLkpCategoryMasterId",
                   DataPropertyName = "FkLkpCategoryMasterId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpCategoryMasterID", "LkpCategoryMaster"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               }, new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpCategoryDetail")).Single().Title,
                   Name = "FkLkpCategoryDetailId",
                   DataPropertyName = "FkLkpCategoryDetailId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpCategoryDetailID", "LkpCategoryDetail"),
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
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpCategoryType")).Single().Title,
                   Name = "FkLkpCategoryTypeId",
                   DataPropertyName = "FkLkpCategoryTypeId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpCategoryTypeID", "LkpCategoryType"),
                   ReadOnly = true,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               }, new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpCategoryMaster")).Single().Title,
                   Name = "FkLkpCategoryMasterId",
                   DataPropertyName = "FkLkpCategoryMasterId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpCategoryMasterID", "LkpCategoryMaster"),
                   ReadOnly = true,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               }, new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpCategoryDetail")).Single().Title,
                   Name = "FkLkpCategoryDetailId",
                   DataPropertyName = "FkLkpCategoryDetailId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpCategoryDetailID", "LkpCategoryDetail"),
                   ReadOnly = true,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "ترتیب",
                   Name = "OrderNum",
                   DataPropertyName = "OrderNum",
                   ReadOnly = true,
                   Visible = true,
                   MinimumWidth = 50,
                   DividerWidth = 5
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "توضیحات",
                   Name = "Description",
                   DataPropertyName = "Description",
                   ReadOnly = true,
                   Visible = true,
                   MinimumWidth = 200,
                   DividerWidth = 5
               }
        });
        }

        public List<DataGridViewColumn> FilterColumns { get; set; } = new List<DataGridViewColumn>();
        public List<DataGridViewColumn> ResultColumns { get; set; } = new List<DataGridViewColumn>();
    }
}
