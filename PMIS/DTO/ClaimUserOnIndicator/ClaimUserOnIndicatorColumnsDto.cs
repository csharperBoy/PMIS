﻿using Microsoft.AspNetCore.Hosting.Server;
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
        public async Task Initialize(ILookUpValueService lookUpValueService, IIndicatorService indicatorService, IUserService userService, int fkId)
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination = await lookUpValueService.GetList("ClaimUserOnIndicator");
            (bool isSuccessInd, IEnumerable<IndicatorSearchResponseDto> lstIndicator) = await indicatorService.Search(new Generic.Service.DTO.Concrete.GenericSearchRequestDto()
            {
                filters = new List<Generic.Service.DTO.Concrete.GenericSearchFilterDto>()
                {
                    new Generic.Service.DTO.Concrete.GenericSearchFilterDto()
                    {
                        columnName = "ID",
                        LogicalOperator = Generic.Service.DTO.Concrete.LogicalOperator.Begin,
                        operation = Generic.Service.DTO.Concrete.FilterOperator.Equals,
                        type = Generic.Service.DTO.Concrete.PhraseType.Condition,
                        value = fkId.ToString()
                    }
                }
            });
            (bool isSuccessUser, IEnumerable<UserSearchResponseDto> lstUser) = await userService.Search(new Generic.Service.DTO.Concrete.GenericSearchRequestDto());

            FilterColumns.AddRange(new List<DataGridViewColumn>()
            {


               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "نوع ادعا",
                   Name = "FkLkpClaimUserOnIndicatorId",
                   DataPropertyName = "FkLkpClaimUserOnIndicatorId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpClaimUserOnIndicatorID", "LkpClaimUserOnIndicator"),
                   ReadOnly = false,
                   Visible = true,

               },
                new DataGridViewComboBoxColumn()
               {
                   HeaderText = "کاربر",
                   Name = "FkUserId",
                   DataPropertyName = "FkUserId",
                   DisplayMember = "Username",
                   ValueMember = "Id",
                   DataSource = lstUser,
                   ReadOnly = false,
                   Visible = true,
               },new DataGridViewComboBoxColumn()
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

               new DataGridViewComboBoxColumn()
               {
                   HeaderText = "نوع ادعا",
                   Name = "FkLkpClaimUserOnIndicatorId",
                   DataPropertyName = "FkLkpClaimUserOnIndicatorId",
                   DisplayMember = "Display",
                   ValueMember = "Id",
                   DataSource =  await  lookUpValueService.GetList(lstLookUpDestination, "FkLkpClaimUserOnIndicatorID", "LkpClaimUserOnIndicator"),
                   ReadOnly = true,
                   Visible = true,

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
               },new DataGridViewComboBoxColumn()
               {
                   HeaderText = "شاخص",
                   Name = "FkIndicatorId",
                   DataPropertyName = "FkIndicatorId",
                   DisplayMember = "Title",
                   ValueMember = "Id",
                   DataSource = lstIndicator,
                   ReadOnly = true,
                   Visible = false,
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