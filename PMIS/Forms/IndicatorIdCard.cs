using Generic.Base.Handler.Map;
using Generic.Service.DTO;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUp.Info;
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
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {

            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto();
            List<LookUpValueShortInfoDto> lstLkpForm = await lookUpValueService.GetList("Indicator", "FkLkpFormId", "LkpForm");
            List<LookUpValueShortInfoDto> lstLkpManuality = await lookUpValueService.GetList("Indicator", "FkLkpManualityId", "LkpManuality");
            List<LookUpValueShortInfoDto> lstLkpUnit = await lookUpValueService.GetList("Indicator", "FkLkpUnitId", "LkpUnit");
            List<LookUpValueShortInfoDto> lstLkpPeriod = await lookUpValueService.GetList("Indicator", "FkLkpPeriodId", "LkpPeriod");
            List<LookUpValueShortInfoDto> lstLkpMeasure = await lookUpValueService.GetList("Indicator", "FkLkpMeasureId", "LkpMeasure");
            List<LookUpValueShortInfoDto> lstLkpDesirability = await lookUpValueService.GetList("Indicator", "FkLkpDesirabilityId", "LkpDesirability");
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
