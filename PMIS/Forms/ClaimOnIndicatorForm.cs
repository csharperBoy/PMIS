using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.ClaimUserOnIndicator;
using PMIS.DTO.Indicator;
using PMIS.DTO.User;
using PMIS.Forms.Generic;
using PMIS.Models;
using PMIS.Services.Contract;

namespace PMIS.Forms
{
    public partial class ClaimOnIndicatorForm : Form
    {
        private AbstractBaseStandardForm standard;
        public readonly ClaimOnIndicatorFormElements claimOnIndicatorFormElements;

        public ClaimOnIndicatorForm(IIndicatorService _indicatorService, ILookUpValueService _lookUpValueService)
        {
            InitializeComponent();
            claimOnIndicatorFormElements = new ClaimOnIndicatorFormElements(dgvFiltersList, dgvResultsList, chbRecycle);
            standard = new BaseDetaildForm<IIndicatorService, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchResponseDto, IndicatorColumnsDto>(_indicatorService, _lookUpValueService, claimOnIndicatorFormElements);
        }
        public ClaimOnIndicatorForm(IClaimUserOnIndicatorService _claimUserOnIndicatorService, ILookUpValueService _lookUpValueService)
        {
            InitializeComponent();
            claimOnIndicatorFormElements = new ClaimOnIndicatorFormElements(dgvFiltersList, dgvResultsList, chbRecycle);
            standard = new BaseDetaildForm<IClaimUserOnIndicatorService, ClaimUserOnIndicator, ClaimUserOnIndicatorAddRequestDto, ClaimUserOnIndicatorAddResponseDto, ClaimUserOnIndicatorEditRequestDto, ClaimUserOnIndicatorEditResponseDto, ClaimUserOnIndicatorDeleteRequestDto, ClaimUserOnIndicatorDeleteResponseDto, ClaimUserOnIndicatorSearchResponseDto, ClaimUserOnIndicatorColumnsDto>(_claimUserOnIndicatorService, _lookUpValueService, claimOnIndicatorFormElements);
        }

        public ClaimOnIndicatorForm(IUserService _userService, ILookUpValueService _lookUpValueService)
        {
            InitializeComponent();
            claimOnIndicatorFormElements = new ClaimOnIndicatorFormElements(dgvFiltersList, dgvResultsList, chbRecycle);
            standard = new BaseDetaildForm<IUserService, User, UserAddRequestDto, UserAddResponseDto, UserEditRequestDto, UserEditResponseDto, UserDeleteRequestDto, UserDeleteResponseDto, UserSearchResponseDto, UserColumnsDto>(_userService, _lookUpValueService, claimOnIndicatorFormElements);
        }

        private void NormalForm_Load(object sender, EventArgs e)
        {

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await standard.SearchEntity();
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
                standard.RefreshVisuals();

                MessageBox.Show("تغییرات با موفقیت اعمال شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در اعمال تغییرات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvResultsList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            standard.RowEnter(e.RowIndex);
        }

        private void dgvResultsList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            standard.RowLeave(e.RowIndex);
        }

        private void dgvResultsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            standard.CellContentClick(e.RowIndex, e.ColumnIndex);
        }

        private void dgvResultsList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (standard.CellBeginEdit(e.RowIndex))
            {
                e.Cancel = true;
            }
        }

        private void dgvResultsList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            standard.RowPostPaint(e.RowIndex);
        }

        private void dgvResultsList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dgvResultsList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

       
    }

    public class ClaimOnIndicatorFormElements : AbstractFormElements
    {
        public ClaimOnIndicatorFormElements(DataGridView _dgvFiltersList, DataGridView _dgvResultsList, CheckBox _chbRecycle) : base(_dgvFiltersList, _dgvResultsList, _chbRecycle)
        {
        }
    }
}
