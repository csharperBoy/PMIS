﻿using Generic.Base.Handler.Map;
using Generic.Service.DTO.Abstract;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Helper;
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
        private List<IndicatorAddRequestDto> lstAddRequest;
        private List<IndicatorEditRequestDto> lstEditRequest;
        private List<IndicatorDeleteRequestDto> lstLogicalDeleteRequest;
        private List<IndicatorDeleteRequestDto> lstPhysicalDeleteRequest;
        private List<IndicatorDeleteRequestDto> lstRecycleRequest;
        private IIndicatorService indicatorService;
        private ILookUpValueService lookUpValueService;
        private IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination;

        private bool isLoaded = false;
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
            lstLogicalDeleteRequest = new List<IndicatorDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<IndicatorDeleteRequestDto>();
            lstRecycleRequest = new List<IndicatorDeleteRequestDto>();
            lstLookUpDestination = await lookUpValueService.GetList("Indicator");

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
                IndeterminateValue = false
            });
            dgvIndicatorList.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                HeaderText = "ویرایش شده",
                Name = "FlgEdited",
                ReadOnly = false,
                Visible = false,
                IndeterminateValue = false
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
                Name = "LogicalDelete",
                Text = "حذف موقت",
                ReadOnly = false,
                Visible = true,
                UseColumnTextForButtonValue = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "",
                Name = "Recycle",
                Text = "بازیابی",
                ReadOnly = false,
                Visible = false,
                UseColumnTextForButtonValue = true,

            });
            dgvIndicatorList.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "",
                Name = "PhysicalDelete",
                Text = "حذف",
                ReadOnly = false,
                Visible = false,
                UseColumnTextForButtonValue = true,
            });
            FiltersInitialize();
            SearchIndicator();
        }
        private async void FiltersInitialize()
        {
            List<LookUpValueShortInfoDto> lstForm = new List<LookUpValueShortInfoDto>() {
                new LookUpValueShortInfoDto()             {
                Id = 0,
                Display = "همه"
            } };
            lstForm.AddRange(await lookUpValueService.GetList(lstLookUpDestination, "FkLkpFormID", "LkpForm"));
            cbLkpForm.DataSource = lstForm;
            cbLkpForm.DisplayMember = "Display";
            cbLkpForm.ValueMember = "Id";
            cbLkpForm.SelectedIndex = 0;


        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchIndicator();
        }

        private async Task SearchIndicator()
        {
            isLoaded = false;
            lstAddRequest = new List<IndicatorAddRequestDto>();
            lstEditRequest = new List<IndicatorEditRequestDto>();
            lstLogicalDeleteRequest = new List<IndicatorDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<IndicatorDeleteRequestDto>();
            lstRecycleRequest = new List<IndicatorDeleteRequestDto>();
            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto()
            {
                filters = new List<GenericSearchFilterDto>()
            {
                new GenericSearchFilterDto()
                {
                    columnName = "Code",
                    value=txtCode.Text,
                    LogicalOperator = LogicalOperator.Begin,
                    operation = FilterOperator.Contains,
                    type = PhraseType.Condition,
                },
                new GenericSearchFilterDto()
                {
                    columnName = "Title",
                    value=txtTitle.Text,
                    LogicalOperator = LogicalOperator.And,
                    operation = FilterOperator.Contains,
                    type = PhraseType.Condition,
                }
                ,
                new GenericSearchFilterDto()
                {
                    columnName = "FlgLogicalDelete",
                    value=chbRecycle.Checked.ToString(),
                    LogicalOperator = LogicalOperator.And,
                    operation = FilterOperator.Equals,
                    type = PhraseType.Condition,
                }
            }
            };
            if (cbLkpForm.SelectedValue.ToString() != "0")
            {

                searchRequest.filters.Add(new GenericSearchFilterDto()
                {
                    columnName = "FkLkpFormId",
                    value = cbLkpForm.SelectedValue.ToString(),
                    LogicalOperator = LogicalOperator.And,
                    operation = FilterOperator.Equals,
                    type = PhraseType.Condition,
                });
            }
            (bool isSuccess, IEnumerable<IndicatorSearchResponseDto> list) = await indicatorService.Search(searchRequest);
            if (isSuccess)
            {
                if (list.Count() == 0)
                {
                    dgvIndicatorList.DataSource = null;
                    MessageBox.Show("موردی یافت نشد!!!");

                }
                else
                {
                    dgvIndicatorList.DataSource = new BindingList<IndicatorSearchResponseDto>(list.ToList());

                }
            }
            else
            {
                MessageBox.Show("عملیات موفقیت‌آمیز نبود!!!");
            }
            RefreshVisuals();
            isLoaded = true;
        }

        private void RefreshVisuals()
        {
            try
            {
                dgvIndicatorList.Columns["Edit"].Visible = !chbRecycle.Checked;
                dgvIndicatorList.Columns["LogicalDelete"].Visible = !chbRecycle.Checked;
                dgvIndicatorList.Columns["Recycle"].Visible = chbRecycle.Checked;
                dgvIndicatorList.Columns["PhysicalDelete"].Visible = chbRecycle.Checked;
                dgvIndicatorList.AllowUserToAddRows = !chbRecycle.Checked;

                foreach (DataGridViewRow row in dgvIndicatorList.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                    row.Cells["FlgEdited"].Value = false;
                    
                }
                if (dgvIndicatorList.Rows.Count > 0) 
                { 
                    dgvIndicatorList.CurrentCell = dgvIndicatorList.Rows[0].Cells[0]; 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                await EditIndicator();
                await LogicalDeleteIndicator();
                await PhysicalDeleteIndicator();
                await RecycleIndicator();
                await AddIndicator();


                MessageBox.Show("تغییرات با موفقیت اعمال شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshVisuals();
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
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                }
                else
                {
                    //string errorMessage = String.Join("\n", list.Select((x, index) => new
                    //{
                    //    ErrorMessage = (index + 1) + " " + x.ErrorMessage,
                    //    IsSuccess = x.IsSuccess
                    //})
                    //.Where(h => h.IsSuccess == false).Select(m => m.ErrorMessage));
                    MessageBox.Show("عملیات افزودن موفقیت‌آمیز نبود: \n" /*+ errorMessage*/);
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
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                }
                else
                {
                    MessageBox.Show("عملیات ویرایش موفقیت آمیز نبود");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task LogicalDeleteIndicator()
        {
            try
            {

                //(bool isSuccess, IEnumerable<IndicatorDeleteResponseDto> list) = await indicatorService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorService.LogicalDeleteRange(lstLogicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstLogicalDeleteRequest = new List<IndicatorDeleteRequestDto>();
                }
                else
                {
                    MessageBox.Show("عملیات حذف موفقیت آمیز نبود");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task PhysicalDeleteIndicator()
        {
            try
            {

                //(bool isSuccess, IEnumerable<IndicatorDeleteResponseDto> list) = await indicatorService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorService.PhysicalDeleteRange(lstPhysicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstPhysicalDeleteRequest = new List<IndicatorDeleteRequestDto>();
                }
                else
                {
                    MessageBox.Show("عملیات حذف موفقیت آمیز نبود");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task RecycleIndicator()
        {
            try
            {

                //(bool isSuccess, IEnumerable<IndicatorDeleteResponseDto> list) = await indicatorService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorService.RecycleRange(lstRecycleRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstRecycleRequest = new List<IndicatorDeleteRequestDto>();
                }
                else
                {
                    MessageBox.Show("عملیات حذف موفقیت آمیز نبود");
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
            if (e.RowIndex == -1)
                return;
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
            }
            else if (dgvIndicatorList.Columns[e.ColumnIndex].Name == "LogicalDelete" && e.RowIndex >= 0)
            {
                var row = dgvIndicatorList.Rows[e.RowIndex];
                if (dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    lstLogicalDeleteRequest.Add(new IndicatorDeleteRequestDto() { Id = int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) });
                    dgvIndicatorList.Rows.RemoveAt(e.RowIndex);
                }
            }

            else if (dgvIndicatorList.Columns[e.ColumnIndex].Name == "PhysicalDelete" && e.RowIndex >= 0)
            {
                var row = dgvIndicatorList.Rows[e.RowIndex];
                if (dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    lstPhysicalDeleteRequest.Add(new IndicatorDeleteRequestDto() { Id = int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) });
                    dgvIndicatorList.Rows.RemoveAt(e.RowIndex);
                }
            }
            else if (dgvIndicatorList.Columns[e.ColumnIndex].Name == "Recycle" && e.RowIndex >= 0)
            {
                var row = dgvIndicatorList.Rows[e.RowIndex];
                if (dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    lstRecycleRequest.Add(new IndicatorDeleteRequestDto() { Id = int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) });
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

        private void dgvIndicatorList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow previousRow = dgvIndicatorList.Rows[e.RowIndex];
            if (dgvIndicatorList.Rows[e.RowIndex].Cells["FlgEdited"].Value != null && bool.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["FlgEdited"].Value.ToString()))
            {
                previousRow.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvIndicatorList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) == 0)
            {
                previousRow.DefaultCellStyle.BackColor = Color.Honeydew;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;

            }
            else
            {
                previousRow.DefaultCellStyle.BackColor = Color.White;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dgvIndicatorList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoaded)
            {
                DataGridViewRow selectedRow = dgvIndicatorList.Rows[e.RowIndex];
                selectedRow.DefaultCellStyle.BackColor = Color.LightBlue;
                selectedRow.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void IndicatorIdCard_Load(object sender, EventArgs e)
        {

        }
    }
}