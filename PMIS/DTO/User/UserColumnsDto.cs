using PMIS.DTO.LookUpDestination;
using PMIS.Services;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User
{
    public class UserColumnsDto : GenericColumnsDto
    {
        public override async Task Initialize(ILookUpValueService lookUpValueService)
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination = await lookUpValueService.GetList("User");
            FilterColumns.AddRange(new List<DataGridViewColumn>()
            {
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "نام کاربری",
                   Name = "UserName",
                   DataPropertyName = "UserName",
                   ReadOnly = false,
                   Visible = true,
                   Frozen = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "نام کامل",
                   Name = "FullName",
                   DataPropertyName = "FullName",
                   ReadOnly = false,
                   Visible = true,
                   Frozen = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpWorkCalendar")).Single().Title,
                   Name = "FkLkpWorkCalendarId",
                   DataPropertyName = "FkLkpWorkCalendarId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpWorkCalendarID", "LkpWorkCalendar"),
                   ReadOnly = false,
                   Visible = true,
                    MinimumWidth = 150,
                   DividerWidth = 5
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "تلفن",
                   Name = "Phone",
                   DataPropertyName = "Phone",
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
                   HeaderText = "نام کاربری",
                   Name = "UserName",
                   DataPropertyName = "UserName",
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "نام کامل",
                   Name = "FullName",
                   DataPropertyName = "FullName",
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "گذرواژه",
                   Name = "PasswordHashTemp1",
                   DataPropertyName = "PasswordHashTemp1",
                   ReadOnly = true,
                   Visible = true,
                   Frozen = true,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "گذرواژه",
                   Name = "PasswordHashTemp2",
                   DataPropertyName = "PasswordHashTemp2",
                   ReadOnly = true,
                   Visible = false,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "گذرواژه",
                   Name = "PasswordHash",
                   DataPropertyName = "PasswordHash",
                   ReadOnly = true,
                   Visible = false,
               },
               new DataGridViewTextBoxColumn()
               {
                   HeaderText = "تلفن",
                   Name = "Phone",
                   DataPropertyName = "Phone",
                   ReadOnly = true,
                   Visible = true,
               },
               new DataGridViewComboBoxColumn()
               {
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpWorkCalendar")).Single().Title,
                   Name = "FkLkpWorkCalendarId",
                   DataPropertyName = "FkLkpWorkCalendarId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await lookUpValueService.GetList(lstLookUpDestination, "FkLkpWorkCalendarID", "LkpWorkCalendar"),
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
