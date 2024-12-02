using PMIS.Bases;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PMIS.DTO.IndicatorValue
{

    public class IndicatorValueColumnsDto //: GenericColumnsDto
    {
        public async Task Initialize(ILookUpValueService lookUpValueService, IIndicatorService indicatorService)
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestinationIndicatorValues = await lookUpValueService.GetList("IndicatorValue");
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestinationIndicator = await lookUpValueService.GetList("Indicator");
            //List<IndicatorSearchResponseDto> lstIndicator = new List<IndicatorSearchResponseDto>() { new IndicatorSearchResponseDto() { Id = 0 , Title = "همه"} };
            (bool isSuccessInd, IEnumerable<IndicatorSearchResponseDto> lstIndicator) = await indicatorService.Search(new Generic.Service.DTO.Concrete.GenericSearchRequestDto()
            {
                filters = new List<Generic.Service.DTO.Concrete.GenericSearchFilterDto>(){
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
            lstIndicator = (await indicatorService.SearchByExternaFilter(lstIndicator, GlobalVariable.userId));
            // lstIndicator.AddRange(lstIndicator1);
            var lstLkpShift = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "FkLkpShiftID", "LkpShift"));
            var lstLkpForm = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "FkLkpFormID", "LkpForm"));
            var LkpPeriod = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "FkLkpPeriodID", "LkpPeriod"));
            var lstLkpValueType = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "FkLkpValueTypeID", "LkpValueType"));


            FilterColumns.AddRange(new List<DataGridViewColumn>()
            {

               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "از تاریخ",
                   Name = "DateTimeFrom",
                   DataPropertyName = "DateTimeFrom",
                   ReadOnly = false,
                   Visible = true,
                   MinimumWidth = 100,
                    DividerWidth = 5
               },
                new DataGridViewTextBoxColumn()
               {
                   HeaderText = "تا تاریخ",
                   Name = "DateTimeTo",
                   DataPropertyName = "DateTimeTo",
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 100,
                   DividerWidth = 5
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "LkpShift")).Single().Title,
                   Name = "FkLkpShiftId",
                   DataPropertyName = "FkLkpShiftId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpShift,
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "LkpForm")).Single().Title,
                   Name = "VrtLkpForm",
                   DataPropertyName = "VrtLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpForm,
                   ReadOnly = false,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "LkpPeriod")).Single().Title,
                   Name = "VrtLkpPeriod",
                   DataPropertyName = "VrtLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  LkpPeriod,
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "LkpValueType")).Single().Title,
                   Name = "FkLkpValueTypeId",
                   DataPropertyName = "FkLkpValueTypeId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource = lstLkpValueType,
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
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
                new DataGridViewTextBoxColumn()
               {
                   HeaderText = "تاریخ",
                   Name = "shamsiDateTime",
                   DataPropertyName = "shamsiDateTime",
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "LkpShift")).Single().Title,
                   Name = "FkLkpShiftId",
                   DataPropertyName = "FkLkpShiftId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource = lstLkpShift,
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "LkpForm")).Single().Title,
                   Name = "VrtLkpForm",
                   DataPropertyName = "VrtLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpForm,
                   ReadOnly = true,
                   Visible = false,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "LkpPeriod")).Single().Title,
                   Name = "VrtLkpPeriod",
                   DataPropertyName = "VrtLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource = LkpPeriod,
                   ReadOnly = true,
                   Visible = true,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = "شاخص",
                   Name = "FkIndicatorId",
                   DataPropertyName = "FkIndicatorId",
                   DisplayMember = "Title",
                   ValueMember = "Id",
                   DataSource = lstIndicator,
                   ReadOnly = true,
                   Visible = true,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "LkpValueType")).Single().Title,
                   Name = "FkLkpValueTypeId",
                   DataPropertyName = "FkLkpValueTypeId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource = lstLkpValueType,
                   ReadOnly = true,
                   Visible = true,
               },
                new DataGridViewTextBoxColumn()
               {
                   HeaderText = "مقدار",
                   Name = "Value",
                   DataPropertyName = "Value",
                   ReadOnly = true,
                   Visible = true,
               },
                new DataGridViewTextBoxColumn()
               {
                   HeaderText = "توضیحات",
                   Name = "Description",
                   DataPropertyName = "Description",
                   ReadOnly = true,
                   Visible = true,
               }
        });


        }

        public List<DataGridViewColumn> FilterColumns { get; set; } = new List<DataGridViewColumn>();
        public List<DataGridViewColumn> ResultColumns { get; set; } = new List<DataGridViewColumn>();

    }
}

