using Generic.Base.Handler.Map;
using Generic.Service.DTO.Abstract;
using Generic.Service.DTO.Concrete;
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
using System.Windows.Forms.VisualStyles;

namespace PMIS.Forms
{
    public partial class IndicatorIdCard : Form
    {
        List<IndicatorAddRequestDto> lstAddRequest;
        List<IndicatorEditRequestDto> lstEditRequest;
        List<IndicatorDeleteRequestDto> lstDeleteRequest;
        List<IndicatorDeleteRequestDto> lstRecycleRequest;
        IIndicatorService indicatorService;
        ILookUpValueService lookUpValueService;

        public IndicatorIdCard(IIndicatorService _indicatorService, ILookUpValueService _lookUpValueService)
        {
            InitializeComponent();
            indicatorService = _indicatorService;
            this.lookUpValueService = _lookUpValueService;

            CustomInitialize();
        }

        private async void CustomInitialize()
        {
            dgvIndicatorList.AutoGenerateColumns = false;
            lstDeleteRequest = new List<IndicatorDeleteRequestDto>();
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination = await lookUpValueService.GetList("Indicator");

            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ردیف",
                Name = "RowNumber",
                ReadOnly = true,
                Visible = true,
                Frozen = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "شناسه",
                Name = "Id",
                DataPropertyName = "Id",
                ReadOnly = true,
                Visible = false,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "کد",
                Name = "Code",
                DataPropertyName = "Code",
                ReadOnly = true,
                Visible = true,
                Frozen = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "عنوان",
                Name = "Title",
                DataPropertyName = "Title",
                ReadOnly = true,
                Visible = true,
                Frozen = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewComboBoxColumn()
            {
                HeaderText = "فرم مربوطه",
                Name = "FkLkpFormId",
                DataPropertyName = "FkLkpFormId",
                DisplayMember = "Display",
                ValueMember = "Id",
                DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpFormID", "LkpForm"),
                ReadOnly = true,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewComboBoxColumn()
            {
                HeaderText = "دستی/اتوماتیک",
                Name = "FkLkpManualityId",
                DataPropertyName = "FkLkpManualityId",
                DisplayMember = "Display",
                ValueMember = "Id",
                DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpManualityID", "LkpManuality"),
                ReadOnly = true,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewComboBoxColumn()
            {
                HeaderText = "واحد عملیاتی",
                Name = "FkLkpUnitId",
                DataPropertyName = "FkLkpUnitId",
                DisplayMember = "Display",
                ValueMember = "Id",
                DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpUnitID", "LkpUnit"),
                ReadOnly = true,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewComboBoxColumn()
            {
                HeaderText = "دوره زمانی",
                Name = "FkLkpPeriodId",
                DataPropertyName = "FkLkpPeriodId",
                DisplayMember = "Display",
                ValueMember = "Id",
                DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpPeriodID", "LkpPeriod"),
                ReadOnly = true,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewComboBoxColumn()
            {
                HeaderText = "واحد اندازه‌گیری",
                Name = "FkLkpMeasureId",
                DataPropertyName = "FkLkpMeasureId",
                DisplayMember = "Display",
                ValueMember = "Id",
                DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpMeasureID", "LkpMeasure"),
                ReadOnly = true,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewComboBoxColumn()
            {
                HeaderText = "مطلوبیت",
                Name = "FkLkpDesirabilityId",
                DataPropertyName = "FkLkpDesirabilityId",
                DisplayMember = "Display",
                ValueMember = "Id",
                DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpDesirabilityID", "LkpDesirability"),
                ReadOnly = true,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "فرمول",
                Name = "Formula",
                DataPropertyName = "Formula",
                ReadOnly = true,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "توضیحات",
                Name = "Description",
                DataPropertyName = "Description",
                ReadOnly = true,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                HeaderText = "حذف شده",
                Name = "FlgLogicalDelete",
                DataPropertyName = "FlgLogicalDelete",
                ReadOnly = false,
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                HeaderText = "ویرایش شده",
                Name = "FlgEdited",
                ReadOnly = false,
                Visible = false,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "",
                Name = "Edit",
                Text = "ویرایش",
                ReadOnly = false,
                Visible = true,
                UseColumnTextForButtonValue = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "",
                Name = "Delete",
                Text = "حذف",
                ReadOnly = false,
                Visible = true,
                UseColumnTextForButtonValue = true,
            });
            SearchIndicator();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchIndicator();
        }

        private async Task SearchIndicator()
        {
            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto();
            (bool isSuccess, IEnumerable<IndicatorSearchResponseDto> list) = await indicatorService.Search(searchRequest);
            if (isSuccess)
            {
                if (list.Count() == 0)
                { MessageBox.Show("موردی یافت نشد!!!"); }
                else
                {
                    dgvIndicatorList.DataSource = new BindingList<IndicatorSearchResponseDto>(list.ToList());
                }
            }
            else
            {
                MessageBox.Show("عملیات موفقیت‌آمیز نبود!!!");
            }
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                await EditIndicator();
                await DeleteIndicator();
                await AddIndicator();               
                

                MessageBox.Show("تغییرات با موفقیت اعمال شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در اعمال تغییرات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AddIndicator()
        {
            try
            {
                lstAddRequest = new List<IndicatorAddRequestDto>();

                foreach (DataGridViewRow row in dgvIndicatorList.Rows)
                {
                    try
                    {
                        if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) == 0)
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
                            lstAddRequest.Add(addRequest);
                        }
                    }
                    catch (Exception) { }
                }

                //(bool isSuccess, IEnumerable<IndicatorAddResponseDto> list) = await indicatorService.AddGroup(lstAddRequest);
                bool isSuccess = await indicatorService.AddRange(lstAddRequest);

                if (isSuccess)
                {
                    MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                }
                else
                {
                    //string errorMessage = String.Join("\n", list.Select((x, index) => new
                    //{
                    //    ErrorMessage = (index + 1) + " " + x.ErrorMessage,
                    //    IsSuccess = x.IsSuccess
                    //})
                    //.Where(h => h.IsSuccess == false).Select(m => m.ErrorMessage));
                    MessageBox.Show("عملیات برای ردیف های زیر موفقیت‌آمیز نبود: \n" /*+ errorMessage*/);
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task EditIndicator()
        {
            try
            {
                lstEditRequest = new List<IndicatorEditRequestDto>();

                foreach (DataGridViewRow row in dgvIndicatorList.Rows)
                {
                    try
                    {
                        if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0 && bool.Parse((row.Cells["FlgEdited"].Value ?? false).ToString()) == true)
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
                            lstEditRequest.Add(editRequest);
                        }
                    }
                    catch (Exception) { }
                }

                //(bool isSuccess, IEnumerable<IndicatorEditResponseDto> list) = await indicatorService.EditGroup(lstEditRequest);
                bool isSuccess = await indicatorService.EditRange(lstEditRequest);
                if (isSuccess)
                {
                    MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DeleteIndicator()
        {
            try
            {
               
                //(bool isSuccess, IEnumerable<IndicatorDeleteResponseDto> list) = await indicatorService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorService.LogicalDeleteRange(lstDeleteRequest);                
                if (isSuccess)
                {
                    MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstDeleteRequest = new List<IndicatorDeleteRequestDto>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgvIndicatorList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvIndicatorList.Rows[e.RowIndex].Cells["RowNumber"].Value = (e.RowIndex + 1).ToString();

            if (dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value == null)
            {
                foreach (DataGridViewCell cell in dgvIndicatorList.Rows[e.RowIndex].Cells)
                {
                    cell.ReadOnly = false;
                }
            }
            dgvIndicatorList.Rows[e.RowIndex].Cells["RowNumber"].ReadOnly = true;
        }

        private void dgvIndicatorList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvIndicatorList.Rows[e.RowIndex].IsNewRow)
                return;
            if (dgvIndicatorList.Columns[e.ColumnIndex].Name == "Edit" && e.RowIndex >= 0)
            {
                var row = dgvIndicatorList.Rows[e.RowIndex];
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dgvIndicatorList.Rows[e.RowIndex].Cells["FlgEdited"].Value = true;
                    cell.ReadOnly = false;
                }
            }else if(dgvIndicatorList.Columns[e.ColumnIndex].Name == "Delete" && e.RowIndex >= 0)
            {
                var row = dgvIndicatorList.Rows[e.RowIndex];
                if (dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    lstDeleteRequest.Add(new IndicatorDeleteRequestDto() { Id = int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) });
                    dgvIndicatorList.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void dgvIndicatorList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dgvIndicatorList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvIndicatorList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if ((dgvIndicatorList.Rows[e.RowIndex].Cells["FlgEdited"].Value == null || bool.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["FlgEdited"].Value.ToString()) == false) && (dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0))
            {
                e.Cancel = true;
            }
        }
    }
}
