using PMIS.Bases;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUpDestination;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorValue
{

    public class IndicatorValueColumnsDto //: GenericColumnsDto
    {
        public async Task Initialize(ILookUpValueService lookUpValueService, IIndicatorService indicatorService)
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestinationIndicatorValues = await lookUpValueService.GetList("IndicatorValue");
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestinationIndicator = await lookUpValueService.GetList("Indicator");
            (bool isSuccessInd, IEnumerable<IndicatorSearchResponseDto> lstIndicator) = await indicatorService.Search(new Generic.Service.DTO.Concrete.GenericSearchRequestDto());
            lstIndicator = await indicatorService.SearchByExternaFilter(lstIndicator, GlobalVariable.userId);
            FilterColumns.AddRange(new List<DataGridViewColumn>()
            {

               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "از تاریخ",
                   Name = "DateTimeFrom",
                   DataPropertyName = "DateTimeFrom",
                   ReadOnly = false,
                   Visible = true, 
               },
                new DataGridViewTextBoxColumn()
               {
                   HeaderText = "تا تاریخ",
                   Name = "DateTimeTo",
                   DataPropertyName = "DateTimeTo",
                   ReadOnly = false,
                   Visible = true,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = "شیفت",
                   Name = "FkLkpShiftId",
                   DataPropertyName = "FkLkpShiftId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "FkLkpShiftID", "LkpShift"),
                   ReadOnly = false,
                   Visible = true,
               },new DataGridViewComboBoxColumn()
               {
                   HeaderText = "فرم",
                   Name = "VrtLkpForm",
                   DataPropertyName = "VrtLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestinationIndicator, "FkLkpFormID", "LkpForm"),
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دوره زمانی",
                   Name = "VrtLkpPeriod",
                   DataPropertyName = "VrtLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestinationIndicator, "FkLkpPeriodID", "LkpPeriod"),
                   ReadOnly = false,
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
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "نوع مقدار",
                   Name = "FkLkpValueTypeId",
                   DataPropertyName = "FkLkpValueTypeId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "FkLkpValueTypeID", "LkpValueType"),
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "توضیحات",
                   Name = "Description",
                   DataPropertyName = "Description",
                   ReadOnly = false,
                   Visible = true,
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
                   Name = "DateTime",
                   DataPropertyName = "shamsiDateTime",
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
              new DataGridViewComboBoxColumn()
               {
                   HeaderText = "شیفت",
                   Name = "FkLkpShiftId",
                   DataPropertyName = "FkLkpShiftId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "FkLkpShiftID", "LkpShift"),
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
              new DataGridViewComboBoxColumn()
               {
                   HeaderText = "فرم",
                   Name = "VrtLkpForm",
                   DataPropertyName = "VrtLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestinationIndicator, "FkLkpFormID", "LkpForm"),
                   ReadOnly = true,
                   Visible = false,
               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دوره زمانی",
                   Name = "VrtLkpPeriod",
                   DataPropertyName = "VrtLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestinationIndicator, "FkLkpPeriodID", "LkpPeriod"),
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
                   HeaderText = "نوع مقدار",
                   Name = "FkLkpValueTypeId",
                   DataPropertyName = "FkLkpValueTypeId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestinationIndicatorValues, "FkLkpValueTypeID", "LkpValueType"),
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

