using PMIS.DTO.Indicator;
using PMIS.Services;
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
        public MainForm()
        {
            InitializeComponent();
        }
        IndicatorService indicatorService = new IndicatorService();
        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private async void button1_Click(object sender, EventArgs e)
        {
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
            }
            };

            IEnumerable<IndicatorAddResponseDto> response = new List<IndicatorAddResponseDto>();
            bool IsSuccess;
            //(IsSuccess, response) = await indicatorService.AddGroup(lstReq);
        }
    }
}
