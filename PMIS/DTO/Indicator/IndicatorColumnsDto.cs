using PMIS.DTO.LookUpDestination;
using PMIS.Forms;
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
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "عنوان",
                   Name = "Title",
                   DataPropertyName = "Title",
                   ReadOnly = false,
                   Visible = true,
                   Frozen = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpForm")).Single().Title,
                   Name = "FkLkpFormId",
                   DataPropertyName = "FkLkpFormId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpFormID", "LkpForm"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpManuality")).Single().Title,
                   Name = "FkLkpManualityId",
                   DataPropertyName = "FkLkpManualityId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpManualityID", "LkpManuality"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpUnit")).Single().Title,
                   Name = "FkLkpUnitId",
                   DataPropertyName = "FkLkpUnitId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpUnitID", "LkpUnit"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpPeriod")).Single().Title,
                   Name = "FkLkpPeriodId",
                   DataPropertyName = "FkLkpPeriodId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpPeriodID", "LkpPeriod"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpMeasure")).Single().Title,
                   Name = "FkLkpMeasureId",
                   DataPropertyName = "FkLkpMeasureId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpMeasureID", "LkpMeasure"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpDesirability")).Single().Title,
                   Name = "FkLkpDesirabilityId",
                   DataPropertyName = "FkLkpDesirabilityId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpDesirabilityID", "LkpDesirability"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "فرمول",
                   Name = "Formula",
                   DataPropertyName = "Formula",
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
                    MinimumWidth = 150,
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpForm")).Single().Title,
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpManuality")).Single().Title,
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpUnit")).Single().Title,
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpPeriod")).Single().Title,
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpMeasure")).Single().Title,
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpDesirability")).Single().Title,
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
               },
               new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "Claims",
                    Text = "ادعاها",
                    ReadOnly = false,
                    Visible = true,
                    UseColumnTextForButtonValue = true,
                }
        });
        }

        public List<DataGridViewColumn> FilterColumns { get; set; } = new List<DataGridViewColumn>();
        public List<DataGridViewColumn> ResultColumns { get; set; } = new List<DataGridViewColumn>();

    }
}
