using Generic.Base.Handler.Map;
using Generic.Service.DTO;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Services;
using PMIS.Services.Contract;
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
    public partial class IndicatorIdCard : Form
    {
        IIndicatorService indicatorService;
        ILookUpValueService lookUpValueService;
        public IndicatorIdCard(IIndicatorService _indicatorService, ILookUpValueService _lookUpValueService)
        {
            InitializeComponent();
            indicatorService = _indicatorService;
            this.lookUpValueService = _lookUpValueService;

           

            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto();
            //Task.Run(async () =>
            //{
            //    IEnumerable<LookUpDestinationSearchResponseDto> lstLookupsDestination = await lookUpValueService.GetList("Indicator");
            //    IEnumerable<LookUpValueShortInfoDto> lstLkpForm = await lookUpValueService.GetList(lstLookupsDestination, "FkLkpFormID", "LkpForm");
            //    dgvcbLkpForm.DataSource = lstLkpForm;
            //}).ContinueWith(task =>
            //{
            //    if (task.Exception != null)
            //    {
            //        // مدیریت استثنا
            //        MessageBox.Show(task.Exception.Message);
            //    }
            //}, TaskScheduler.FromCurrentSynchronizationContext());


        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookupsDestination = await lookUpValueService.GetList("Indicator");

            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto();
            IEnumerable<LookUpValueShortInfoDto> lstLkpForm = await lookUpValueService.GetList(lstLookupsDestination, "FkLkpFormID", "LkpForm");
            IEnumerable<LookUpValueShortInfoDto> lstLkpManuality = await lookUpValueService.GetList(lstLookupsDestination, "FkLkpManualityID", "LkpManuality");
            IEnumerable<LookUpValueShortInfoDto> lstLkpUnit = await lookUpValueService.GetList(lstLookupsDestination, "FkLkpUnitID", "LkpUnit");
            IEnumerable<LookUpValueShortInfoDto> lstLkpPeriod = await lookUpValueService.GetList(lstLookupsDestination, "FkLkpPeriodID", "LkpPeriod");
            IEnumerable<LookUpValueShortInfoDto> lstLkpMeasure = await lookUpValueService.GetList(lstLookupsDestination, "FkLkpMeasureID", "LkpMeasure");
            IEnumerable<LookUpValueShortInfoDto> lstLkpDesirability = await lookUpValueService.GetList(lstLookupsDestination, "FkLkpDesirabilityID", "LkpDesirability");
            dgvcbLkpForm.DataSource = lstLkpForm;
            dgvcbLkpManuality.DataSource = lstLkpManuality;
            dgvcbLkpUnit.DataSource = lstLkpUnit;
            dgvcbLkpPeriod.DataSource = lstLkpPeriod;
            dgvcbLkpMeasure.DataSource = lstLkpMeasure;
            dgvcbLkpDesirability.DataSource = lstLkpDesirability;

            (bool isSuccesst, IEnumerable<IndicatorSearchResponseDto> list) = await indicatorService.Search(searchRequest);
            if (isSuccesst)
            {
                if (list.Count() == 0)
                { MessageBox.Show("موردی یافت نشد!!!"); }
                else
                {
                    dgvIndicatorList.DataSource = list;
                }
            }
            else
            {
                MessageBox.Show("عملیات ناموفق بود!!!");
            }
        }
    }
}
