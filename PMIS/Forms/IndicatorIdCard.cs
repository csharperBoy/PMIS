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
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchIndicator();
        }

        private async Task SearchIndicator()
        {
            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto();
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

        private async void IndicatorIdCard_Load(object sender, EventArgs e)
        {
            await InitializeLookUps();
        }

        private async Task InitializeLookUps()
        {
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination = await lookUpValueService.GetList("Indicator");

            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto();
            dgvcbLkpForm.DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpFormID", "LkpForm");
            dgvcbLkpManuality.DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpManualityID", "LkpManuality");
            dgvcbLkpUnit.DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpUnitID", "LkpUnit");
            dgvcbLkpPeriod.DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpPeriodID", "LkpPeriod");
            dgvcbLkpMeasure.DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpMeasureID", "LkpMeasure");
            dgvcbLkpDesirability.DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpDesirabilityID", "LkpDesirability");
        }
    }
}
