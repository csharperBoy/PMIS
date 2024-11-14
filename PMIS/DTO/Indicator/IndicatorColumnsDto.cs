using PMIS.DTO.LookUpDestination;
using PMIS.Services;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Indicator
{
    public class IndicatorColumnsDto : GenericColumnsDto
    {
        public override async Task Initialize(ILookUpValueService lookUpValueService)
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination = await lookUpValueService.GetList("Indicator");
            FilterColumns.AddRange(new List<DataGridViewColumn>()
            {

               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "کد",
                   Name = "Code",
                   DataPropertyName = "Code",
                   ReadOnly = false,
                   Visible = true,
                   Frozen = true,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "عنوان",
                   Name = "Title",
                   DataPropertyName = "Title",
                   ReadOnly = false,
                   Visible = true,
                   Frozen = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "فرم مربوطه",
                   Name = "FkLkpFormId",
                   DataPropertyName = "FkLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpFormID", "LkpForm"),
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دستی/اتوماتیک",
                   Name = "FkLkpManualityId",
                   DataPropertyName = "FkLkpManualityId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpManualityID", "LkpManuality"),
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "واحد عملیاتی",
                   Name = "FkLkpUnitId",
                   DataPropertyName = "FkLkpUnitId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpUnitID", "LkpUnit"),
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دوره زمانی",
                   Name = "FkLkpPeriodId",
                   DataPropertyName = "FkLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpPeriodID", "LkpPeriod"),
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "واحد اندازه‌گیری",
                   Name = "FkLkpMeasureId",
                   DataPropertyName = "FkLkpMeasureId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpMeasureID", "LkpMeasure"),
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "مطلوبیت",
                   Name = "FkLkpDesirabilityId",
                   DataPropertyName = "FkLkpDesirabilityId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpDesirabilityID", "LkpDesirability"),
                   ReadOnly = false,
                   Visible = true,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "فرمول",
                   Name = "Formula",
                   DataPropertyName = "Formula",
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
                   HeaderText = "کد",
                   Name = "Code",
                   DataPropertyName = "Code",
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "عنوان",
                   Name = "Title",
                   DataPropertyName = "Title",
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "فرم مربوطه",
                   Name = "FkLkpFormId",
                   DataPropertyName = "FkLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpFormID", "LkpForm"),
                   ReadOnly = true,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دستی/اتوماتیک",
                   Name = "FkLkpManualityId",
                   DataPropertyName = "FkLkpManualityId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpManualityID", "LkpManuality"),
                   ReadOnly = true,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "واحد عملیاتی",
                   Name = "FkLkpUnitId",
                   DataPropertyName = "FkLkpUnitId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpUnitID", "LkpUnit"),
                   ReadOnly = true,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "دوره زمانی",
                   Name = "FkLkpPeriodId",
                   DataPropertyName = "FkLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpPeriodID", "LkpPeriod"),
                   ReadOnly = true,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "واحد اندازه‌گیری",
                   Name = "FkLkpMeasureId",
                   DataPropertyName = "FkLkpMeasureId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpMeasureID", "LkpMeasure"),
                   ReadOnly = true,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "مطلوبیت",
                   Name = "FkLkpDesirabilityId",
                   DataPropertyName = "FkLkpDesirabilityId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpDesirabilityID", "LkpDesirability"),
                   ReadOnly = true,
                   Visible = true,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "فرمول",
                   Name = "Formula",
                   DataPropertyName = "Formula",
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
