using Generic.Service.DTO;
using PMIS.DTO.Indicator;
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
        public IndicatorIdCard(IIndicatorService _indicatorService)
        {
            InitializeComponent();
            indicatorService = _indicatorService;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto();
            (bool isSuccesst , IEnumerable<IndicatorSearchResponseDto> list) =await indicatorService.Search(searchRequest);
            if (isSuccesst)
            {
                if (list.Count() == 0)
                    MessageBox.Show("موردی یافت نشد!!!");
                else
                    dgvIndicatorList.DataSource = list;
            }
            else
            {
                MessageBox.Show("عملیات ناموفق بود!!!");
            }
        }
    }
}
