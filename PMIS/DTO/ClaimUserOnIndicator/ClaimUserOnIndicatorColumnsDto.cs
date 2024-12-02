using Microsoft.AspNetCore.Hosting.Server;
using PMIS.Bases;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.User;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimUserOnIndicator
{
    public class ClaimUserOnIndicatorColumnsDto //: GenericColumnsDto
    {
        public async Task Initialize(ILookUpValueService lookUpValueService, IUserService userService, IIndicatorService indicatorService, int fkUserId, int fkIndicatorId)
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination = await lookUpValueService.GetList("ClaimUserOnIndicator");

           // List<UserSearchResponseDto> lstUser = new List<UserSearchResponseDto>() { new UserSearchResponseDto() { Id = 0, UserName = "همه" } };
            (bool isSuccessUser, IEnumerable<UserSearchResponseDto> lstUser) = await userService.Search(new Generic.Service.DTO.Concrete.GenericSearchRequestDto()
            {
                filters = new List<Generic.Service.DTO.Concrete.GenericSearchFilterDto>()
                {
                    new Generic.Service.DTO.Concrete.GenericSearchFilterDto()
                    {
                        columnName = "ID",
                        LogicalOperator = Generic.Service.DTO.Concrete.LogicalOperator.Begin,
                        operation = fkUserId != 0 ? Generic.Service.DTO.Concrete.FilterOperator.Equals : Generic.Service.DTO.Concrete.FilterOperator.NotEquals,
                        type = Generic.Service.DTO.Concrete.PhraseType.Condition,
                        value = fkUserId.ToString()
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
          //  lstUser.AddRange(lstUser1);

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

            FilterColumns.AddRange(new List<DataGridViewColumn>()
            {
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = "کاربر",
                   Name = "FkUserId",
                   DataPropertyName = "FkUserId",
                   DisplayMember = "Username",
                   ValueMember = "Id",
                   DataSource = lstUser.ToArray(),
                   ReadOnly = false,
                   Visible = true,
                   MinimumWidth = 150,
                   DividerWidth = 5
               },new DataGridViewComboBoxColumn()
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpClaimUserOnIndicator")).Single().Title,
                   Name = "FkLkpClaimUserOnIndicatorId",
                   DataPropertyName = "FkLkpClaimUserOnIndicatorId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  (await lookUpValueService.GetList(lstLookUpDestination, "FkLkpClaimUserOnIndicatorID", "LkpClaimUserOnIndicator")).ToArray(),
                   ReadOnly = false,
                   Visible = true,
                   MinimumWidth = 200,
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
                   HeaderText = "کاربر",
                   Name = "FkUserId",
                   DataPropertyName = "FkUserId",
                   DisplayMember = "Username",
                   ValueMember = "Id",
                   DataSource = lstUser,
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
                   HeaderText = (await lookUpValueService.GetList(lstLookUpDestination, "LkpClaimUserOnIndicator")).Single().Title,
                   Name = "FkLkpClaimUserOnIndicatorId",
                   DataPropertyName = "FkLkpClaimUserOnIndicatorId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  (await lookUpValueService.GetList(lstLookUpDestination, "FkLkpClaimUserOnIndicatorID", "LkpClaimUserOnIndicator")).ToArray(),
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
