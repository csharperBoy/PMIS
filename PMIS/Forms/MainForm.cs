using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using Generic.Base.Handler.SystemException;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.DTO.Service;
using Generic.Repository;
using Generic.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using PMIS.DTO.Indicator;
using PMIS.Models;
using PMIS.Repository;
using PMIS.Services;
using PMIS.Services.Contract;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMIS.Forms
{
    public partial class MainForm : Form
    {
        IIndicatorService indicatorService;
        private Serilog.ILogger logHandler;
        public MainForm(IIndicatorService _indicatorService, AbstractGenericLogWithSerilogHandler _logHandler)
        {

            InitializeComponent();
            this.indicatorService = _indicatorService;
            this.logHandler = _logHandler.CreateLogger();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //int a = 1;
                //int b = 0;
                //int aa = a / b;

                List<IndicatorAddRequestDto> lstReq = new List<IndicatorAddRequestDto>() {
                new IndicatorAddRequestDto() {
                Code = textBox1.Text,
                Description = textBox2.Text,
                Formula = textBox3.Text,
                Title = textBox4.Text,
                FkLkpDesirabilityId = 500,
                FkLkpFormId = 500,
                FkLkpManualityId = 500,
                FkLkpMeasureId = 500,
                FkLkpPeriodId = 500,
                FkLkpUnitId = 500,
                SystemInfo = "{test : test}"
                },
                new IndicatorAddRequestDto() {
                Code = textBox1.Text,
                Description = textBox2.Text,
                Formula = textBox3.Text,
                Title = textBox4.Text,
                FkLkpDesirabilityId = 550,
                FkLkpFormId = 550,
                FkLkpManualityId = 550,
                FkLkpMeasureId = 550,
                FkLkpPeriodId = 550,
                FkLkpUnitId = 550,
                SystemInfo = "{test : test2}"
                }};

                IEnumerable<IndicatorAddResponseDto> response = new List<IndicatorAddResponseDto>();
                bool IsSuccess;
                //(IsSuccess, response) = await indicatorService.AddGroup(lstReq);
                //MessageBox.Show(IsSuccess.ToString() + response.FirstOrDefault().ErrorMessage);
                IsSuccess = await indicatorService.AddRange(lstReq);
                MessageBox.Show(IsSuccess.ToString());



                List<IndicatorEditRequestDto> lstEditReq = new List<IndicatorEditRequestDto>() {
                new IndicatorEditRequestDto() {
                    Id = 92,
                Code = textBox1.Text,
                Description = textBox2.Text,
                Formula = textBox3.Text,
                Title = textBox4.Text,
                FkLkpDesirabilityId = 501,
                FkLkpFormId = 501,
                FkLkpManualityId = 501,
                FkLkpMeasureId = 500,
                FkLkpPeriodId = 500,
                FkLkpUnitId = 500,
                SystemInfo = "{test : test}"
                },
                new IndicatorEditRequestDto() {
                    Id=93,
                Code = textBox1.Text,
                Description = textBox2.Text,
                Formula = textBox3.Text,
                Title = textBox4.Text,
                FkLkpDesirabilityId = 551,
                FkLkpFormId = 551,
                FkLkpManualityId = 550,
                FkLkpMeasureId = 550,
                FkLkpPeriodId = 550,
                FkLkpUnitId = 551,
                SystemInfo = "{test : test2}"
                }};
                IEnumerable<IndicatorEditResponseDto> response2 = new List<IndicatorEditResponseDto>();
                bool IsSuccess2;
                (IsSuccess2, response2) = await indicatorService.EditGroup(lstEditReq);
                MessageBox.Show(IsSuccess2.ToString() + response2.FirstOrDefault().ErrorMessage);
                //IsSuccess2 = await indicatorService.EditRange(lstEditReq);
                //MessageBox.Show(IsSuccess2.ToString());
            }
            catch (Exception ex)
            {
                //logHandler.Error(ex, "error log");
                throw;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                GenericSearchRequestDto requestDto = new GenericSearchRequestDto() 
                {
                    filters =new List<GenericSearchFilterDto>()
                    { 
                        //new GenericSearchFilterDto() 
                        //{ 
                        //    columnName = "Title",
                        //    LogicalOperator = LogicalOperator.begin,
                        //    operation = FilterOperator.Contains,
                        //    type = "string",
                        //    value = "test"
                        //}, 
                        new GenericSearchFilterDto() 
                        {
                            columnName = "FkLkpFormID",
                            LogicalOperator = LogicalOperator.begin,
                            operation = FilterOperator.Equals,
                            type = "int",
                            value = "501"
                        } ,
                        new GenericSearchFilterDto()
                        {
                            columnName = "FkLkpFormID",
                            LogicalOperator = LogicalOperator.Or,
                            operation = FilterOperator.Equals,
                            type = "int",
                            value = "500"
                        }
                    },
                    pageNumber = 1,
                    recordCount = 10,
                    sorts = new List<GenericSearchSortDto>() { new GenericSearchSortDto() { columnName="ID" , direction = SortDirection.Ascending } }
                };
                (bool IsSuccess , IEnumerable<IndicatorSearchResponseDto> res) = await indicatorService.Search(requestDto);
                MessageBox.Show(IsSuccess.ToString());
                
            }
            catch (Exception ex)
            {
                //logHandler.Error(ex, "error log");
                throw;
            }
        }
    }
}
