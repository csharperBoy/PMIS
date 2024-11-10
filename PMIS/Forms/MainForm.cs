using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using Generic.Base.Handler.SystemException;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Service.DTO;
using Generic.Repository;
using Generic.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
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
        ILookUpService lookUpService;
        ILookUpValueService lookUpValueService;
        ILookUpDestinationService lookUpDestinationService;
        private Serilog.ILogger logHandler;
        public MainForm(IIndicatorService _indicatorService, AbstractGenericLogWithSerilogHandler _logHandler, ILookUpService _lookUpService, ILookUpValueService _lookUpValueService, ILookUpDestinationService _lookUpDestinationService)
        {

            InitializeComponent();
            this.indicatorService = _indicatorService;
            this.logHandler = _logHandler.CreateLogger();
            this.lookUpService = _lookUpService;
            this.lookUpDestinationService = _lookUpDestinationService;
            this.lookUpValueService = _lookUpValueService;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //List<LookUpDestinationAddRequestDto> lstReq = new List<LookUpDestinationAddRequestDto>() {
                //new LookUpDestinationAddRequestDto() {
                //FkLookUpId=60,
                //ColumnName=textBox1.Text,
                //Description=textBox3.Text,
                //FlgLogicalDelete = false,
                //SystemInfo="",
                //TableName = textBox2.Text
                //}};

                //IEnumerable<LookUpDestinationAddResponseDto> response = new List<LookUpDestinationAddResponseDto>();
                //bool IsSuccess;
                //(IsSuccess, response) = await lookUpDestinationService.AddGroup(lstReq);
                //MessageBox.Show(IsSuccess.ToString() + response.FirstOrDefault().ErrorMessage);
                //int a = 1;
                //int b = 0;
                //int aa = a / b;

                //  List<IndicatorAddRequestDto> lstReq = new List<IndicatorAddRequestDto>() {
                //  new IndicatorAddRequestDto() {
                //  Code = textBox1.Text,
                //  Description = textBox2.Text,
                //  Formula = textBox3.Text,
                //  Title = textBox4.Text,
                //  FkLkpDesirabilityId = 500,
                //  FkLkpFormId = 500,
                //  FkLkpManualityId = 500,
                //  FkLkpMeasureId = 500,
                //  FkLkpPeriodId = 500,
                //  FkLkpUnitId = 500,
                //  SystemInfo = "{test : test}"
                //  },
                //  new IndicatorAddRequestDto() {
                //  Code = textBox1.Text,
                //  Description = textBox2.Text,
                //  Formula = textBox3.Text,
                //  Title = textBox4.Text,
                //  FkLkpDesirabilityId = 550,
                //  FkLkpFormId = 550,
                //  FkLkpManualityId = 550,
                //  FkLkpMeasureId = 550,
                //  FkLkpPeriodId = 550,
                //  FkLkpUnitId = 550,
                //  SystemInfo = "{test : test2}"
                //  }};

                //  IEnumerable<IndicatorAddResponseDto> response = new List<IndicatorAddResponseDto>();
                //  bool IsSuccess;
                //  (IsSuccess, response) = await indicatorService.AddGroup(lstReq);
                //  MessageBox.Show(IsSuccess.ToString() + response.FirstOrDefault().ErrorMessage);
                // // IsSuccess = await indicatorService.AddRange(lstReq);
                ////  MessageBox.Show(IsSuccess.ToString());



                //  List<IndicatorEditRequestDto> lstEditReq = new List<IndicatorEditRequestDto>() {
                //  new IndicatorEditRequestDto() {
                //      Id = 92,
                //  Code = textBox1.Text,
                //  Description = textBox2.Text,
                //  Formula = textBox3.Text,
                //  Title = textBox4.Text,
                //  FkLkpDesirabilityId = 501,
                //  FkLkpFormId = 501,
                //  FkLkpManualityId = 501,
                //  FkLkpMeasureId = 500,
                //  FkLkpPeriodId = 500,
                //  FkLkpUnitId = 500,
                //  SystemInfo = "{test : test}"
                //  },
                //  new IndicatorEditRequestDto() {
                //      Id=93,
                //  Code = textBox1.Text,
                //  Description = textBox2.Text,
                //  Formula = textBox3.Text,
                //  Title = textBox4.Text,
                //  FkLkpDesirabilityId = 551,
                //  FkLkpFormId = 551,
                //  FkLkpManualityId = 550,
                //  FkLkpMeasureId = 550,
                //  FkLkpPeriodId = 550,
                //  FkLkpUnitId = 551,
                //  SystemInfo = "{test : test2}"
                //  }};
                //  IEnumerable<IndicatorEditResponseDto> response2 = new List<IndicatorEditResponseDto>();
                //  bool IsSuccess2;
                //  (IsSuccess2, response2) = await indicatorService.EditGroup(lstEditReq);
                //   MessageBox.Show(IsSuccess2.ToString() + response2.FirstOrDefault().ErrorMessage);
                //IsSuccess2 = await indicatorService.EditRange(lstEditReq);
                //MessageBox.Show(IsSuccess2.ToString());
            }
            catch (Exception ex)
            {
                //logHandler.Error(ex, "error log");
                throw;
            }
        }



        private async void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                //GenericSearchRequestDto requestDto = new GenericSearchRequestDto()
                //{
                //    filters = new List<GenericSearchFilterDto>()
                //    {
                //        new GenericSearchFilterDto()
                //        {
                //            columnName = "FlgLogicalDelete",
                //            LogicalOperator = LogicalOperator.begin,
                //            operation = FilterOperator.Equals,
                //            type = PharseType.Condition,
                //            value = "False"
                //        } ,
                //        new GenericSearchFilterDto()
                //        {
                //            columnName = "ID",
                //            LogicalOperator = LogicalOperator.And,
                //            operation = FilterOperator.Equals,
                //            type = PharseType.Condition,
                //            value = "501"
                //        }
                //    },
                //    pageNumber = null,
                //    recordCount = null,
                //    sorts = null
                //};
                //GenericSearchRequestDto requestDto = new GenericSearchRequestDto()
                //{
                //    filters = new List<GenericSearchFilterDto>()
                //    {
                //        new GenericSearchFilterDto()
                //        {
                //            columnName = "FlgLogicalDelete",
                //            LogicalOperator = LogicalOperator.Begin,
                //            operation = FilterOperator.Equals,
                //            type = PhraseType.Condition,
                //            value = "False"
                //        } ,
                //        new GenericSearchFilterDto()
                //        {
                //            columnName = "TableName",
                //            LogicalOperator = LogicalOperator.And,
                //            operation = FilterOperator.Equals,
                //            type = PhraseType.Condition,
                //            value = "IndicatorCategory"
                //        },
                //        new GenericSearchFilterDto()
                //        {
                //            columnName = "ColumnName",
                //            LogicalOperator = LogicalOperator.And,
                //            operation = FilterOperator.Equals,
                //            type = PhraseType.Condition,
                //            value = "FkLkpCategoryTypeID"
                //        }
                //    },
                //    pageNumber = null,
                //    recordCount = null,
                //    sorts = null
                //};
                //(bool IsSuccess, IEnumerable<LookUpDestinationSearchResponseDto> res) = await lookUpDestinationService.Search(requestDto);
                //List<LookUpValueShortInfoDto> lst = new List<LookUpValueShortInfoDto>();
                //foreach (var item in res)
                //{
                //    lst.AddRange(item.LookUpValuesInfo.ToList());
                //}
                //dataGridView1.DataSource = lst;
                //MessageBox.Show(IsSuccess.ToString());


            }
            catch (Exception ex)
            {
                //logHandler.Error(ex, "error log");
                throw;
            }
        }
        private void براساسفرمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnForm, new IndicatorValueDataEntryOnForm());
        }

        private void براساستاریخToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnDate, new IndicatorValueDataEntryOnDate());
        }

        private void براساسشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnIndicator, new IndicatorValueDataEntryOnIndicator());
        }

        private void تعریفکاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void دسترسیهایکاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void شناسنامهشاخصهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصدستهبندیبهشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصشاخصبهدستهبندیToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصکاربربهشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصشاخصبهکاربرToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void AddNewTabPage(TabPage tabPage, Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            if (tabControlMain.TabPages.Contains(tabPage))
            {
                tabPage.Controls.Clear();
            }
            else
            {
                tabControlMain.Controls.Add(tabPage);
            }
            Panel panel = new Panel();
            panel.Controls.Add(form);
            panel.Dock = DockStyle.Fill;
            tabPage.Controls.Add(panel);
            form.Show();
        }
    }
}
