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
            lstIndicator = (await indicatorService.SearchByExternaFilter(lstIndicator, GlobalVariable.userId)).ToArray();
            // lstIndicator.AddRange(lstIndicator1);
            var lstLkpShift = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "LkpShift")).Single();
            var lstLkpValuesShift = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "FkLkpShiftID", "LkpShift")).ToArray();
            
            var lstLkpForm = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "LkpForm")).Single();
            var lstLkpValuesForm = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "FkLkpFormID", "LkpForm")).ToArray();
            
            var lstLkpPeriod = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "LkpPeriod")).Single();
            var lstLkpValuesPeriod = (await lookUpValueService.GetList(lstLookUpDestinationIndicator, "FkLkpPeriodID", "LkpPeriod")).ToArray();
            
            var lstLkpValueType = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "LkpValueType")).Single();
            var lstLkpValuesValueType = (await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "FkLkpValueTypeID", "LkpValueType")).ToArray();


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
                   HeaderText = lstLkpShift.Title,
                   Name = "FkLkpShiftId",
                   DataPropertyName = "FkLkpShiftId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpValuesShift,
                   ReadOnly = false,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 5
               },new DataGridViewComboBoxColumn()
               {
                   HeaderText = lstLkpForm.Title,
                   Name = "VrtLkpForm",
                   DataPropertyName = "VrtLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpValuesForm,
                   ReadOnly = false,
                   Visible = true,
                  MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = lstLkpPeriod.Title,
                   Name = "VrtLkpPeriod",
                   DataPropertyName = "VrtLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpValuesPeriod,
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
                   DataSource = lstIndicator,
                   ReadOnly = false,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = lstLkpValueType.Title,
                   Name = "FkLkpValueTypeId",
                   DataPropertyName = "FkLkpValueTypeId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpValuesValueType,
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
                   HeaderText = lstLkpShift.Title,
                   Name = "FkLkpShiftId",
                   DataPropertyName = "FkLkpShiftId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpValuesShift,
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = lstLkpForm.Title,
                   Name = "VrtLkpForm",
                   DataPropertyName = "VrtLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpValuesForm,
                   ReadOnly = true,
                   Visible = false,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = lstLkpPeriod.Title,
                   Name = "VrtLkpPeriod",
                   DataPropertyName = "VrtLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpValuesPeriod,
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
                   HeaderText = lstLkpValueType.Title,
                   Name = "FkLkpValueTypeId",
                   DataPropertyName = "FkLkpValueTypeId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  lstLkpValuesValueType,
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
               }, new DataGridViewTextBoxColumn()
               {
                   HeaderText = "مقدار تجمعی",
                   Name = "ValueCumulative",
                   DataPropertyName = "ValueCumulative",
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

