using Generic.Base.Handler.Map;
using Generic.Service.DTO.Abstract;
using Generic.Service.DTO.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Forms.Generic;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PMIS.Forms
{
    public partial class IndicatorIdCard : BaseStandardForm<PmisContext,Indicator,IndicatorAddRequestDto,IndicatorAddResponseDto,IndicatorEditRequestDto,IndicatorEditResponseDto,IndicatorDeleteRequestDto,IndicatorDeleteResponseDto,IndicatorSearchResponseDto, IndicatorColumnsDto, IIndicatorService>
    {
       
        public IndicatorIdCard(IIndicatorService _entityService, ILookUpValueService _lookUpValueService) : base(_entityService, _lookUpValueService , new IndicatorColumnsDto(_lookUpValueService))
        {
            //InitializeComponent();         
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchEntity();
        }

        

        private async void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                await EditEntity();
                await LogicalDeleteEntity();
                await PhysicalDeleteEntity();
                await RecycleEntity();
                await AddEntity();


                MessageBox.Show("تغییرات با موفقیت اعمال شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshVisuals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در اعمال تغییرات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void IndicatorIdCard_Load(object sender, EventArgs e)
        {

        }

        protected override IndicatorAddRequestDto AddMaping(DataGridViewRow row)
        {
            try
            {
                IndicatorAddRequestDto addRequest = new IndicatorAddRequestDto();
                try { addRequest.Code = row.Cells["Code"].Value?.ToString(); } catch (Exception ex) { }
                try { addRequest.Title = row.Cells["Title"].Value?.ToString(); } catch (Exception ex) { }
                try { addRequest.FkLkpFormId = int.Parse(row.Cells["FkLkpFormId"].Value?.ToString()); } catch (Exception ex) { }
                try { addRequest.FkLkpManualityId = int.Parse(row.Cells["FkLkpManualityId"].Value?.ToString()); } catch (Exception ex) { }
                try { addRequest.FkLkpUnitId = int.Parse(row.Cells["FkLkpUnitId"].Value?.ToString()); } catch (Exception ex) { }
                try { addRequest.FkLkpPeriodId = int.Parse(row.Cells["FkLkpPeriodId"].Value?.ToString()); } catch (Exception ex) { }
                try { addRequest.FkLkpMeasureId = int.Parse(row.Cells["FkLkpMeasureId"].Value?.ToString()); } catch (Exception ex) { }
                try { addRequest.FkLkpDesirabilityId = int.Parse(row.Cells["FkLkpDesirabilityId"].Value?.ToString()); } catch (Exception ex) { }
                try { addRequest.Formula = row.Cells["Formula"].Value?.ToString(); } catch (Exception ex) { }
                try { addRequest.Description = row.Cells["Description"].Value?.ToString(); } catch (Exception ex) { }
                return addRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected override IndicatorEditRequestDto EditMaping(DataGridViewRow row)
        {
            try
            {
                IndicatorEditRequestDto editRequest = new IndicatorEditRequestDto();
                try { editRequest.Id = int.Parse(row.Cells["Id"].Value?.ToString()); } catch (Exception ex) { }
                try { editRequest.Code = row.Cells["Code"].Value?.ToString(); } catch (Exception ex) { }
                try { editRequest.Title = row.Cells["Title"].Value?.ToString(); } catch (Exception ex) { }
                try { editRequest.FkLkpFormId = int.Parse(row.Cells["FkLkpFormId"].Value?.ToString()); } catch (Exception ex) { }
                try { editRequest.FkLkpManualityId = int.Parse(row.Cells["FkLkpManualityId"].Value?.ToString()); } catch (Exception ex) { }
                try { editRequest.FkLkpUnitId = int.Parse(row.Cells["FkLkpUnitId"].Value?.ToString()); } catch (Exception ex) { }
                try { editRequest.FkLkpPeriodId = int.Parse(row.Cells["FkLkpPeriodId"].Value?.ToString()); } catch (Exception ex) { }
                try { editRequest.FkLkpMeasureId = int.Parse(row.Cells["FkLkpMeasureId"].Value?.ToString()); } catch (Exception ex) { }
                try { editRequest.FkLkpDesirabilityId = int.Parse(row.Cells["FkLkpDesirabilityId"].Value?.ToString()); } catch (Exception ex) { }
                try { editRequest.Formula = row.Cells["Formula"].Value?.ToString(); } catch (Exception ex) { }
                try { editRequest.Description = row.Cells["Description"].Value?.ToString(); } catch (Exception ex) { }
                return editRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected override void CustomInitializeComponent()
        {
            InitializeComponent();
        }

       
       

        public override async Task<(DataGridView, DataGridView, CheckBox)> SetDataControls()
        {
            try
            {
                return await Task.FromResult((dgvFiltersList, dgvResultsList, chbRecycle));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
