using Generic.Base.Handler.Map;
using Generic.Service.DTO.Abstract;
using Generic.Service.DTO.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Helper;
using PMIS.Models;
using PMIS.Services;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PMIS.Forms
{
    public partial class IndicatorIdCard : Form
    {
        BaseStandardForm<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchResponseDto, IndicatorColumnsDto, IIndicatorService> standard;

        public IndicatorIdCard(IIndicatorService _indicatorService, ILookUpValueService _lookUpValueService)
        {
            InitializeComponent();
            CustomInitialize();
            standard = new BaseStandardForm<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchResponseDto, IndicatorColumnsDto, IIndicatorService>(_indicatorService, _lookUpValueService, new BaseStandardFormElements() { dgvFiltersList = dgvFiltersList, dgvResultsList = dgvResultsList, chbRecycle = chbRecycle});
        }

        private void CustomInitialize()
        {
           
        }

        private void IndicatorIdCard_Load(object sender, EventArgs e)
        {

        }

        public void dgvResultsList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            standard.RowPostPaint(e.RowIndex);
        }

        protected void dgvResultsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            standard.CellContentClick(e.RowIndex, e.ColumnIndex);
        }

        protected void dgvResultsList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        protected void dgvResultsList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        protected void dgvResultsList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (standard.CellBeginEdit(e.RowIndex))
            {
                e.Cancel = true;
            }
        }

        protected void dgvResultsList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            standard.RowLeave(e.RowIndex);
        }

        protected void dgvResultsList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            standard.RowEnter(e.RowIndex);
        }
        private async void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                await standard.EditEntity();
                await standard.LogicalDeleteEntity();
                await standard.PhysicalDeleteEntity();
                await standard.RecycleEntity();
                await standard.AddEntity();


                MessageBox.Show("تغییرات با موفقیت اعمال شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                standard.RefreshVisuals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در اعمال تغییرات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await standard.SearchEntity();
        }
    }
}
