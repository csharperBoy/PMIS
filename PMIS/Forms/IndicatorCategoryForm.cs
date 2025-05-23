﻿using Generic.Helper;
using Generic.Service.DTO.Concrete;
using Generic.Service.Normal.Composition.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PMIS.DTO.ClaimUserOnIndicator;
using PMIS.DTO.IndicatorCategory;
using PMIS.DTO.Indicator;
using PMIS.DTO.IndicatorValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User;
using PMIS.Forms.Generic;
using PMIS.Models;
using PMIS.Services;
using PMIS.Services.Contract;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using WSM.WindowsServices.FileManager;
using PMIS.DTO.ClaimUserOnSystem;
using PMIS.DTO.Category;

namespace PMIS.Forms
{
    public partial class IndicatorCategoryForm : Form
    {

        #region Variables
        private List<IndicatorCategoryAddRequestDto> lstAddRequest;
        private List<IndicatorCategoryEditRequestDto> lstEditRequest;
        private List<IndicatorCategoryDeleteRequestDto> lstLogicalDeleteRequest;
        private List<IndicatorCategoryDeleteRequestDto> lstPhysicalDeleteRequest;
        private List<IndicatorCategoryDeleteRequestDto> lstRecycleRequest;
        private IEnumerable<IndicatorCategorySearchResponseDto> lstSearchResponse;
        private BindingList<IndicatorCategorySearchResponseDto> lstBinding;
        private IndicatorCategoryColumnsDto columns;
        private ICategoryService categoryService;
        // private IUserService userService;
        private IIndicatorService indicatorService;
        private IIndicatorCategoryService indicatorCategoryService;
        private IClaimUserOnSystemService claimUserOnSystemService;
        private int fkIndicatorId;
        private int fkCategoryId;

        private bool isLoaded = false;
        private TabControl tabControl;
        #endregion

        public IndicatorCategoryForm(IIndicatorCategoryService _indicatorCategoryService, IClaimUserOnSystemService _claimUserOnSystemService, IIndicatorService _indicatorService, ICategoryService _categoryService, int _fkIndicatorId, int _fkCategoryId, TabControl _tabControl)
        {

            InitializeComponent();
            indicatorCategoryService = _indicatorCategoryService;
            //userService = _userService;
            indicatorService = _indicatorService;
            categoryService = _categoryService;
            fkIndicatorId = _fkIndicatorId;
            fkCategoryId = _fkCategoryId;
            tabControl = _tabControl;
            claimUserOnSystemService = _claimUserOnSystemService;
            CustomInitialize();

        }
        private async void CustomInitialize()
        {
            int selectedIndex = tabControl.SelectedIndex;
            AddNewTabPage(tabControl, this);
            if (await CheckSystemClaimsRequired())
            {
                // InitializeComponent();
                columns = new IndicatorCategoryColumnsDto();
                await columns.Initialize(categoryService, indicatorService, fkIndicatorId, fkCategoryId);
                lstLogicalDeleteRequest = new List<IndicatorCategoryDeleteRequestDto>();
                lstPhysicalDeleteRequest = new List<IndicatorCategoryDeleteRequestDto>();
                lstRecycleRequest = new List<IndicatorCategoryDeleteRequestDto>();
                GenerateDgvFilterColumnsInitialize();
                GenerateDgvResultColumnsInitialize();
                FiltersInitialize();
                SearchEntity();
            }
            else
            {

                tabControl.Controls.RemoveAt(tabControl.Controls.Count - 1);
                tabControl.SelectedIndex = selectedIndex;
                MessageBox.Show("باعرض پوزش شما دسترسی به این قسمت را ندارید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private async Task<bool> CheckSystemClaimsRequired()
        {
            try
            {
                IEnumerable<ClaimUserOnSystemSearchResponseDto> claims = await claimUserOnSystemService.GetCurrentUserClaims();
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "IndicatorCategoryForm"))
                {
                    this.Close();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void AddNewTabPage(TabControl tabControl, Form form)
        {
            TabPage tabPage = new TabPage();
            tabPage.Location = new Point(4, 24);
            tabPage.Name = "tabPageIndicatorCategoryForm";
            tabPage.Padding = new Padding(3);
            tabPage.Size = new Size(192, 0);
            tabPage.TabIndex = 0;
            tabPage.Text = "دسته بندی شاخص ها";
            tabPage.UseVisualStyleBackColor = true;

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            if (tabControl.TabPages.Contains(tabPage))
            {
                tabPage.Controls.Clear();
            }
            else
            {
                tabControl.Controls.Add(tabPage);
            }
            Panel panel = new Panel();
            panel.Controls.Add(form);
            panel.Dock = DockStyle.Fill;
            tabPage.Controls.Add(panel);
            form.Show();
            tabControl.DoubleClick += CloseTabPage;
            tabControl.SelectedTab = tabPage;
        }

        private void NormalForm_Load(object sender, EventArgs e)
        {

        }

        private async void NormalForm_Leave(object sender, EventArgs e)
        {
            await ShouldChangesBeSaved();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchEntity();
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            await Apply();
        }

        private void dgvResultsList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e.RowIndex);
        }

        private void dgvResultsList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            RowLeave(e.RowIndex);
        }

        private void dgvResultsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CellContentClick(e.RowIndex, e.ColumnIndex);
        }

        private void dgvResultsList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (CellBeginEdit(e.RowIndex))
            {
                e.Cancel = true;
            }
        }

        private void dgvResultsList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            RowPostPaint(e.RowIndex);
        }

        private void dgvResultsList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dgvResultsList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            Upload();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Download();
        }

        private void GenerateDgvFilterColumnsInitialize()
        {
            try
            {
                dgvFiltersList.AllowUserToAddRows = false;
                AddColumnsToGridView(dgvFiltersList, "FilterColumns");
                dgvFiltersList.Rows.Add();
                if (fkCategoryId != 0)
                {
                    ((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkCategoryId"]).Value = ((CategorySearchResponseDto)((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkCategoryId"]).Items[0]).Id;
                }
                if (fkIndicatorId != 0)
                {
                    ((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkIndicatorId"]).Value = ((IndicatorSearchResponseDto)((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkIndicatorId"]).Items[0]).Id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GenerateDgvResultColumnsInitialize()
        {
            try
            {
                dgvResultsList.AutoGenerateColumns = false;
                dgvResultsList.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "ردیف",
                    Name = "RowNumber",
                    ReadOnly = true,
                    Visible = true,
                    Frozen = true,
                });
                AddColumnsToGridView(dgvResultsList, "ResultColumns");
                dgvResultsList.Columns.Add(new DataGridViewCheckBoxColumn()
                {
                    HeaderText = "حذف شده",
                    Name = "FlgLogicalDelete",
                    DataPropertyName = "FlgLogicalDelete",
                    ReadOnly = false,
                    Visible = false,
                    IndeterminateValue = false
                });
                dgvResultsList.Columns.Add(new DataGridViewCheckBoxColumn()
                {
                    HeaderText = "ویرایش شده",
                    Name = "FlgEdited",
                    ReadOnly = false,
                    Visible = false,
                    IndeterminateValue = false
                });
                dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "Edit",
                    Text = "ویرایش",
                    ReadOnly = false,
                    Visible = true,
                    UseColumnTextForButtonValue = true,
                });
                dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "LogicalDelete",
                    Text = "حذف موقت",
                    ReadOnly = false,
                    Visible = true,
                    UseColumnTextForButtonValue = true,
                });
                dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "Recycle",
                    Text = "بازیابی",
                    ReadOnly = false,
                    Visible = false,
                    UseColumnTextForButtonValue = true,

                });
                dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "PhysicalDelete",
                    Text = "حذف",
                    ReadOnly = false,
                    Visible = false,
                    UseColumnTextForButtonValue = true,
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AddColumnsToGridView(DataGridView dgv, string propName)
        {
            var entityFields = columns.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Contains(propName));
            if (fieldInfo != null)
            {
                var tempColumns = (List<DataGridViewColumn>)fieldInfo.GetValue(columns);
                if (tempColumns != null)
                {
                    dgv.Columns.AddRange(tempColumns.ToArray());
                }
            }
        }

        private void FiltersInitialize()
        {
            foreach (DataGridViewColumn column in dgvFiltersList.Columns)
            {
                if (column is DataGridViewComboBoxColumn comboBoxColumn)
                {
                    if (comboBoxColumn.DataSource is LookUpValueShortInfoDto[] array)
                    {
                        List<LookUpValueShortInfoDto> lstSourse = array.ToList();
                        lstSourse.Insert(0, new LookUpValueShortInfoDto() { Id = 0, Display = "همه", });
                        comboBoxColumn.DataSource = lstSourse;
                        comboBoxColumn.DisplayMember = "Display";
                        comboBoxColumn.ValueMember = "Id";
                        // comboBoxColumn.SelectedIndex = 0;
                        dgvFiltersList.Rows[0].Cells[column.Name].Value = 0;
                    }
                    if (comboBoxColumn.DataSource is IndicatorSearchResponseDto[] arrayInd)
                    {

                        if (fkIndicatorId != 0)
                        {
                            ((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkIndicatorId"]).Value = ((IndicatorSearchResponseDto)((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkIndicatorId"]).Items[0]).Id;
                            dgvFiltersList.Rows[0].Cells[column.Name].Value = ((IndicatorSearchResponseDto)comboBoxColumn.Items[0]).Id;
                        }
                        else
                        {
                            List<IndicatorSearchResponseDto> lstSourse = arrayInd.ToList();
                            lstSourse.Insert(0, new IndicatorSearchResponseDto() { Id = 0, Title = "همه", });
                            comboBoxColumn.DataSource = lstSourse;
                            comboBoxColumn.DisplayMember = "Title";
                            comboBoxColumn.ValueMember = "Id";
                            // comboBoxColumn.SelectedIndex = 0;
                            dgvFiltersList.Rows[0].Cells[column.Name].Value = 0;
                        }
                    }
                    if (comboBoxColumn.DataSource is CategorySearchResponseDto[] arrayCtg)
                    {

                        if (fkCategoryId != 0)
                        {
                            ((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkCategoryId"]).Value = ((CategorySearchResponseDto)((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkCategoryId"]).Items[0]).Id;
                            dgvFiltersList.Rows[0].Cells[column.Name].Value = ((CategorySearchResponseDto)comboBoxColumn.Items[0]).Id;
                        }
                        else
                        {
                            List<CategorySearchResponseDto> lstSourse = arrayCtg.ToList();
                            lstSourse.Insert(0, new CategorySearchResponseDto() { Id = 0, Title = "همه", });
                            comboBoxColumn.DataSource = lstSourse;
                            comboBoxColumn.DisplayMember = "Title";
                            comboBoxColumn.ValueMember = "Id";
                            // comboBoxColumn.SelectedIndex = 0;
                            dgvFiltersList.Rows[0].Cells[column.Name].Value = 0;
                        }

                    }
                    // dgvFiltersList.Rows[0].Cells[column.Name].Value = 0;
                }
            }
        }
        public void RefreshVisuals()
        {
            try
            {
                dgvResultsList.Columns["Edit"].Visible = !chbRecycle.Checked;
                dgvResultsList.Columns["LogicalDelete"].Visible = !chbRecycle.Checked;
                dgvResultsList.Columns["Recycle"].Visible = chbRecycle.Checked;
                dgvResultsList.Columns["PhysicalDelete"].Visible = chbRecycle.Checked;
                dgvResultsList.AllowUserToAddRows = !chbRecycle.Checked;

                foreach (DataGridViewRow row in dgvResultsList.Rows)
                {
                    if (row.Cells["FlgEdited"].Value != null && bool.Parse(row.Cells["FlgEdited"].Value.ToString()))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Honeydew;
                        row.DefaultCellStyle.ForeColor = Color.Black;

                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }


                    //   row.Cells["FlgEdited"].Value = false;

                }
                if (dgvResultsList.Rows.Count > 0)
                {
                    dgvResultsList.CurrentCell = dgvResultsList.Rows[0].Cells[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private async void CloseTabPage(object? sender, EventArgs e)
        {
            if (tabControl.Controls[tabControl.SelectedIndex].Controls[0].Controls[0] == this)
            {
                await ShouldChangesBeSaved();
                tabControl.DoubleClick -= CloseTabPage;
            }
        }

        private async Task ShouldChangesBeSaved()
        {
            if (lstSearchResponse != null && HasChangeResults())
            {
                var dialogResult = MessageBox.Show("آیا مایل به اعمال تغییرات هستید؟", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    await Apply();
                }
            }
        }

        private bool HasChangeResults()
        {
            if (dgvResultsList.Rows.Cast<DataGridViewRow>().Count(row => row.Cells["Id"].Value is Int64 id && id == 0) > 0 || // lstAddRequest.Count != 0 ||
                dgvResultsList.Rows.Cast<DataGridViewRow>().Count(row => row.Cells["FlgEdited"].Value is bool flgEdited && flgEdited) > 0 || // lstEditRequest.Count != 0 ||
                lstLogicalDeleteRequest.Count != 0 ||
                lstPhysicalDeleteRequest.Count != 0 ||
                lstRecycleRequest.Count != 0
                )
            {
                return true;
            }
            return false;
        }

        public async Task SearchEntity()
        {
            isLoaded = false;
            await ShouldChangesBeSaved();
            lstAddRequest = new List<IndicatorCategoryAddRequestDto>();
            lstEditRequest = new List<IndicatorCategoryEditRequestDto>();
            lstLogicalDeleteRequest = new List<IndicatorCategoryDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<IndicatorCategoryDeleteRequestDto>();
            lstRecycleRequest = new List<IndicatorCategoryDeleteRequestDto>();

            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto()
            {
                filters = new List<GenericSearchFilterDto>(),
            };
            searchRequest.filters.Add(new GenericSearchFilterDto()
            {
                columnName = "FlgLogicalDelete",
                value = chbRecycle.Checked.ToString(),
                LogicalOperator = LogicalOperator.Begin,
                operation = FilterOperator.Equals,
                type = PhraseType.Condition,
            });
            GenericSearchFilterDto filter = new GenericSearchFilterDto()
            {
                InternalFilters = new List<GenericSearchFilterDto>(),
                LogicalOperator = LogicalOperator.And,
                type = PhraseType.Parentheses,
            };
            foreach (DataGridViewRow row in dgvFiltersList.Rows)
            {
                GenericSearchFilterDto tempFilter = new GenericSearchFilterDto()
                {
                    InternalFilters = new List<GenericSearchFilterDto>(),
                    LogicalOperator = row.Index == 0 ? LogicalOperator.Begin : LogicalOperator.Or,
                    type = PhraseType.Parentheses,
                };
                foreach (DataGridViewColumn column in dgvFiltersList.Columns)
                {
                    var cellValue = row.Cells[column.Name].Value == null ? "" : row.Cells[column.Name].Value.ToString();
                    if ((column is not DataGridViewComboBoxColumn && !cellValue.IsNullOrEmpty()) || (column is DataGridViewComboBoxColumn && cellValue != "" && cellValue != "0"))
                    {
                        tempFilter.InternalFilters.Add(new GenericSearchFilterDto()
                        {
                            columnName = column.Name,
                            value = cellValue,
                            LogicalOperator = column.Index == 0 ? LogicalOperator.Begin : LogicalOperator.And,
                            operation = column is DataGridViewTextBoxColumn ? FilterOperator.Contains : FilterOperator.Equals,
                            type = PhraseType.Condition,
                        });
                    }
                }
                filter.InternalFilters.Add(tempFilter);
            }
            searchRequest.filters.Add(filter);


            (bool isSuccess, lstSearchResponse) = await indicatorCategoryService.Search(searchRequest);
            lstBinding = new BindingList<IndicatorCategorySearchResponseDto>(lstSearchResponse.ToList());
            dgvResultsList.DataSource = lstBinding;

            if (isSuccess)
            {
                if (lstSearchResponse.Count() == 0)
                {
                    MessageBox.Show("موردی یافت نشد!!!", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("عملیات موفقیت‌آمیز نبود!!!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RefreshVisuals();
            isLoaded = true;
        }

        private async Task Apply()
        {
            try
            {
                await EditEntity();
                await LogicalDeleteEntity();
                await PhysicalDeleteEntity();
                await RecycleEntity();
                await AddEntity();
                RefreshVisuals();

                MessageBox.Show("عملیات موفقیت‌آمیز بود!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"عملیات موفقیت‌آمیز نبود: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task AddEntity()
        {
            try
            {
                lstAddRequest = new List<IndicatorCategoryAddRequestDto>();

                foreach (DataGridViewRow row in dgvResultsList.Rows)
                {
                    try
                    {

                        if ((row.Cells["Id"].Value == null && row.Index + 1 < dgvResultsList.Rows.Count) || (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) == 0))
                        {
                            IndicatorCategoryAddRequestDto addRequest = new IndicatorCategoryAddRequestDto();

                            addRequest = AddMaping(row);
                            lstAddRequest.Add(addRequest);
                        }
                    }
                    catch (Exception) { }
                }
                if (lstAddRequest.Count > 0)
                {
                    (bool isSuccess, IEnumerable<IndicatorCategoryAddResponseDto> list) = await indicatorCategoryService.AddGroup(lstAddRequest);
                    //bool isSuccess = await indicatorCategoryService.AddRange(lstAddRequest);

                    if (isSuccess)
                    {
                        var listResponse = list.ToList();
                        // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        foreach (DataGridViewRow row in dgvResultsList.Rows)
                        {
                            try
                            {
                                if ((row.Cells["Id"].Value == null && row.Index + 1 < dgvResultsList.Rows.Count) || (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) == 0))
                                {
                                    row.Cells["Id"].Value = listResponse.FirstOrDefault().Id;
                                    listResponse.RemoveAt(0);
                                }
                            }
                            catch (Exception) { }
                        }
                        lstAddRequest = new List<IndicatorCategoryAddRequestDto>();
                    }
                    else
                    {
                        //string errorMessage = String.Join("\n", list.Select((x, index) => new
                        //{
                        //    ErrorMessage = (index + 1) + " " + x.ErrorMessage,
                        //    IsSuccess = x.IsSuccess
                        //})
                        //.Where(h => h.IsSuccess == false).Select(m => m.ErrorMessage));
                        MessageBox.Show("عملیات افزودن موفقیت‌آمیز نبود: \n" /*+ errorMessage*/, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EditEntity()
        {
            try
            {
                lstEditRequest = new List<IndicatorCategoryEditRequestDto>();

                foreach (DataGridViewRow row in dgvResultsList.Rows)
                {
                    try
                    {
                        if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0 && bool.Parse((row.Cells["FlgEdited"].Value ?? false).ToString()) == true)
                        {
                            IndicatorCategoryEditRequestDto editRequest = new IndicatorCategoryEditRequestDto();

                            editRequest = EditMaping(row);
                            lstEditRequest.Add(editRequest);
                        }
                    }
                    catch (Exception) { }
                }
                if (lstEditRequest.Count > 0)
                {
                    (bool isSuccess, IEnumerable<IndicatorCategoryEditResponseDto> list) = await indicatorCategoryService.EditGroup(lstEditRequest);
                    //bool isSuccess = await indicatorCategoryService.EditRange(lstEditRequest);
                    if (isSuccess)
                    {
                        var listResponse = list.ToList();
                        // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        foreach (DataGridViewRow row in dgvResultsList.Rows)
                        {
                            try
                            {
                                if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0 && bool.Parse((row.Cells["FlgEdited"].Value ?? false).ToString()) == true)
                                {
                                    if (listResponse.FirstOrDefault().IsSuccess)
                                        row.Cells["FlgEdited"].Value = false;
                                    listResponse.RemoveAt(0);
                                }
                            }
                            catch (Exception) { }
                        }
                        lstEditRequest = new List<IndicatorCategoryEditRequestDto>();
                    }
                    else
                    {
                        MessageBox.Show("عملیات ویرایش موفقیت آمیز نبود", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LogicalDeleteEntity()
        {
            try
            {

                //(bool isSuccess, IEnumerable<IndicatorCategoryDeleteResponseDto> list) = await IndicatorCategoryService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorCategoryService.LogicalDeleteRange(lstLogicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstLogicalDeleteRequest = new List<IndicatorCategoryDeleteRequestDto>();
                }
                else
                {
                    MessageBox.Show("عملیات حذف موقت موفقیت آمیز نبود", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RecycleEntity()
        {
            try
            {

                //(bool isSuccess, IEnumerable<IndicatorCategoryDeleteResponseDto> list) = await IndicatorCategoryService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorCategoryService.RecycleRange(lstRecycleRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstRecycleRequest = new List<IndicatorCategoryDeleteRequestDto>();
                }
                else
                {
                    MessageBox.Show("عملیات بازیابی موفقیت آمیز نبود", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PhysicalDeleteEntity()
        {
            try
            {

                //(bool isSuccess, IEnumerable<IndicatorCategoryDeleteResponseDto> list) = await IndicatorCategoryService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorCategoryService.PhysicalDeleteRange(lstPhysicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstPhysicalDeleteRequest = new List<IndicatorCategoryDeleteRequestDto>();
                }
                else
                {
                    MessageBox.Show("عملیات حذف موفقیت آمیز نبود", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RowEnter(int rowIndex)
        {
            if (isLoaded)
            {
                DataGridViewRow selectedRow = dgvResultsList.Rows[rowIndex];
                selectedRow.DefaultCellStyle.BackColor = Color.LightBlue;
                selectedRow.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        public void RowLeave(int rowIndex)
        {
            DataGridViewRow previousRow = dgvResultsList.Rows[rowIndex];
            if (dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value != null && bool.Parse(dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value.ToString()))
            {
                previousRow.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) == 0)
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

        public void CellContentClick(int rowIndex, int columnIndex)
        {
            if (rowIndex == -1)
                return;
            if (dgvResultsList.Rows[rowIndex].IsNewRow)
                return;
            var row = dgvResultsList.Rows[rowIndex];
            if (dgvResultsList.Columns[columnIndex].Name == "Edit" && rowIndex >= 0)
            {

                foreach (DataGridViewCell cell in row.Cells)
                {
                    dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value = true;
                    cell.ReadOnly = false;
                }
            }
            else if (dgvResultsList.Columns[columnIndex].Name == "LogicalDelete" && rowIndex >= 0)
            {
                if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0)
                {
                    IndicatorCategoryDeleteRequestDto deleteRequest = new IndicatorCategoryDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstLogicalDeleteRequest.Add(deleteRequest);
                    dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }

            else if (dgvResultsList.Columns[columnIndex].Name == "PhysicalDelete" && rowIndex >= 0)
            {
                if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0)
                {
                    IndicatorCategoryDeleteRequestDto deleteRequest = new IndicatorCategoryDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstPhysicalDeleteRequest.Add(deleteRequest);

                    dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }
            else if (dgvResultsList.Columns[columnIndex].Name == "Recycle" && rowIndex >= 0)
            {
                if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0)
                {
                    IndicatorCategoryDeleteRequestDto deleteRequest = new IndicatorCategoryDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstRecycleRequest.Add(deleteRequest);

                    dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }
            else if (dgvResultsList.Columns[columnIndex].Name == "Claims" && rowIndex >= 0)
            {
                if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0)
                {
                    int tempId = int.Parse(row.Cells["Id"].Value.ToString());
                    // NormalForm frm = new NormalForm(indicatorService, lookUpValueService);
                }
            }
        }

        public bool CellBeginEdit(int rowIndex)
        {
            if ((dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value == null || bool.Parse(dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value.ToString()) == false) && (dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0))
            {
                return true;
            }
            return false;
        }

        private void CellValidated(int rowIndex, int columnIndex)
        {

        }

        public void RowPostPaint(int rowIndex)
        {
            dgvResultsList.Rows[rowIndex].Cells["RowNumber"].Value = (rowIndex + 1).ToString();

            if (dgvResultsList.Rows[rowIndex].Cells["Id"].Value == null)
            {
                foreach (DataGridViewCell cell in dgvResultsList.Rows[rowIndex].Cells)
                {
                    cell.ReadOnly = false;
                }
            }
            dgvResultsList.Rows[rowIndex].Cells["RowNumber"].ReadOnly = true;
        }

        private IndicatorCategoryAddRequestDto AddMaping(DataGridViewRow row)
        {
            try
            {
                IndicatorCategoryAddRequestDto addRequest = new IndicatorCategoryAddRequestDto();

                var entityFields = addRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (DataGridViewColumn column in dgvResultsList.Columns)
                {
                    var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Equals("<" + column.Name + ">k__BackingField"));
                    if (fieldInfo != null)
                    {
                        // fieldInfo.SetValue(addRequest, row.Cells[column.Name].Value);
                        fieldInfo.SetValue(addRequest, Convert.ChangeType(row.Cells[column.Name].Value, fieldInfo.FieldType));
                    }
                }
                addRequest = AfterAddMapping(addRequest);
                return addRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private IndicatorCategoryAddRequestDto AfterAddMapping(IndicatorCategoryAddRequestDto addRequest)
        {
            addRequest.FkIndicatorId = fkIndicatorId == 0 ? addRequest.FkIndicatorId : fkIndicatorId;
            addRequest.FkCategoryId = fkCategoryId == 0 ? addRequest.FkCategoryId : fkCategoryId;
            return addRequest;
        }

        private IndicatorCategoryEditRequestDto EditMaping(DataGridViewRow row)
        {
            try
            {
                IndicatorCategoryEditRequestDto editRequest = new IndicatorCategoryEditRequestDto();
                var entityFields = editRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (DataGridViewColumn column in dgvResultsList.Columns)
                {
                    var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Equals("<" + column.Name + ">k__BackingField"));
                    if (fieldInfo != null)
                    {
                        //fieldInfo.SetValue(editRequest, row.Cells[column.Name].Value);
                        fieldInfo.SetValue(editRequest, Convert.ChangeType(row.Cells[column.Name].Value, fieldInfo.FieldType));
                    }
                }
                editRequest = AfterEditMapping(editRequest);
                return editRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private IndicatorCategoryEditRequestDto AfterEditMapping(IndicatorCategoryEditRequestDto addRequest)
        {
            addRequest.FkIndicatorId = fkIndicatorId == 0 ? addRequest.FkIndicatorId : fkIndicatorId;
            addRequest.FkCategoryId = fkCategoryId == 0 ? addRequest.FkCategoryId : fkCategoryId;
            return addRequest;
        }

        private async void Upload()
        {
            try
            {
                await ShouldChangesBeSaved();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetFileName(openFileDialog.FileName).StartsWith("IndicatorCategory"))
                    {
                        DataTable dataTable = ((DataSet)ExcelManager.Read<DataSet>(openFileDialog.FileName)).Tables[0];
                        dgvResultsList.DataSource = null;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int index = dgvResultsList.Rows.Add();
                            foreach (DataColumn column in dataTable.Columns)
                            {
                                foreach (DataGridViewColumn item in dgvResultsList.Columns)
                                {
                                    if (item.HeaderText == column.ColumnName)
                                    {
                                        DataGridViewCell cell = dgvResultsList.Rows[index].Cells[item.Name];
                                        if (cell is DataGridViewComboBoxCell)
                                        {
                                            object dataSource = ((DataGridViewComboBoxCell)cell).DataSource;
                                            if (dataSource is LookUpValueShortInfoDto[])
                                            {
                                                var selectItem = ((LookUpValueShortInfoDto[])dataSource).FirstOrDefault(item => item.Display == row[column].ToString());
                                                if (selectItem != null)
                                                {
                                                    cell.Value = selectItem.Id;
                                                }
                                            }
                                            else if (dataSource is CategorySearchResponseDto[])
                                            {
                                                var selectItem = ((CategorySearchResponseDto[])dataSource).FirstOrDefault(item => item.Title == row[column].ToString());
                                                if (selectItem != null)
                                                {
                                                    cell.Value = selectItem.Id;
                                                }

                                                DataGridViewCell cellMaster = dgvResultsList.Rows[index].Cells["VrtParentCategory"];
                                                object dataSourceMaster = ((DataGridViewComboBoxCell)cellMaster).DataSource;
                                                var selectItemMaster = ((CategorySearchResponseDto[])dataSourceMaster).FirstOrDefault(item => item.Id == selectItem.FkParentInfo.Id);
                                                if (selectItemMaster != null)
                                                {
                                                    cellMaster.Value = selectItemMaster.Id;
                                                }
                                            }
                                            else if (dataSource is IndicatorSearchResponseDto[])
                                            {
                                                var selectItem = ((IndicatorSearchResponseDto[])dataSource).FirstOrDefault(item => item.Title == row[column].ToString());
                                                if (selectItem != null)
                                                {
                                                    cell.Value = selectItem.Id;
                                                }
                                            }
                                        }
                                        else if (cell is DataGridViewTextBoxCell)
                                        {
                                            ((DataGridViewTextBoxCell)cell).Value = row[column].ToString();
                                        }
                                        break;
                                    }
                                    //else if(item.Name == "VrtParentCategory" && column.ColumnName == "دسته بندی")
                                    //{
                                    //    DataGridViewCell cell = dgvResultsList.Rows[index].Cells[item.Name];
                                    //    object dataSource = ((DataGridViewComboBoxCell)cell).DataSource;
                                    //    var selectItem = ((CategorySearchResponseDto[])dataSource).FirstOrDefault(item => item.FkParentInfo.Title == row[column].ToString());
                                    //    if (selectItem != null)
                                    //    {
                                    //        cell.Value = selectItem.Id;
                                    //    }
                                    //}
                                }
                            }
                            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto()
                            {
                                filters = new List<GenericSearchFilterDto>(),
                            };
                            searchRequest.filters.Add(new GenericSearchFilterDto()
                            {
                                columnName = "FlgLogicalDelete",
                                value = false.ToString(),
                                LogicalOperator = LogicalOperator.Begin,
                                operation = FilterOperator.Equals,
                                type = PhraseType.Condition,
                            });
                            searchRequest.filters.Add(new GenericSearchFilterDto()
                            {
                                columnName = "FkCategoryId",
                                value = dgvResultsList.Rows[index].Cells["FkCategoryId"].Value.ToString(),
                                LogicalOperator = LogicalOperator.And,
                                operation = FilterOperator.Equals,
                                type = PhraseType.Condition,
                            });
                            searchRequest.filters.Add(new GenericSearchFilterDto()
                            {
                                columnName = "FkIndicatorId",
                                value = dgvResultsList.Rows[index].Cells["FkIndicatorId"].Value.ToString(),
                                LogicalOperator = LogicalOperator.And,
                                operation = FilterOperator.Equals,
                                type = PhraseType.Condition,
                            });
                            (bool isSuccess, lstSearchResponse) = await indicatorCategoryService.Search(searchRequest);
                            if (isSuccess && lstSearchResponse.Count() > 0)
                            {
                                dgvResultsList.Rows[index].Cells["Id"].Value = lstSearchResponse.FirstOrDefault().Id;
                                if (dgvResultsList.Rows[index].Cells["FkIndicatorId"].Value.ToString() != lstSearchResponse.FirstOrDefault().FkIndicatorInfo.Id.ToString() ||
                                    dgvResultsList.Rows[index].Cells["FkCategoryId"].Value.ToString() != lstSearchResponse.FirstOrDefault().FkCategoryInfo.Id.ToString() ||
                                    dgvResultsList.Rows[index].Cells["Description"].Value.ToString() != lstSearchResponse.FirstOrDefault().Description
                                    )
                                {
                                    dgvResultsList.Rows[index].Cells["FlgEdited"].Value = true;
                                }
                            }
                            else
                            {
                                dgvResultsList.Rows[index].Cells["Id"].Value = 0;
                            }
                            foreach (DataGridViewColumn item in dgvResultsList.Columns)
                            {
                                CellValidated(index, item.Index);
                            }
                            RowLeave(index);
                            lstSearchResponse = new List<IndicatorCategorySearchResponseDto>();
                        }
                    }
                    else
                    {
                        throw new Exception("نام فایل باید با SystemClaims آغاز گردد!");
                    }
                }
            }
            catch (Exception ex)
            {
                dgvResultsList.Rows.Clear();
                MessageBox.Show("عملیات بارگزاری موفقیت‌آمیز نبود: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Download()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = "IndicatorCategory-" + Helper.Convert.ConvertGregorianToShamsi(DateTime.Now, "RRRRMMDDHH24MISSMS");
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFileDialog.FileName = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf('.')) + "\\IndicatorCategory" + saveFileDialog.FileName.Substring(saveFileDialog.FileName.LastIndexOf('.'));
                    if (!Directory.Exists(Path.GetDirectoryName(saveFileDialog.FileName)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(saveFileDialog.FileName));
                    }
                    string fileName = saveFileDialog.FileName;
                    bool result = ExcelManager.Write(fileName, new List<DataGridView>() { dgvResultsList });
                    var filePath = Path.GetDirectoryName(fileName);
                    MessageBox.Show("عملیات بارگیری موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("عملیات بارگیری موفقیت‌آمیز نبود: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvResultsList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                if (!(e.Control is ComboBox))
                    return;

                ComboBox comboBox = e.Control as ComboBox;

                if (dgvResultsList.CurrentCell.ColumnIndex == dgvResultsList.Columns["VrtParentCategory"].Index)
                {
                    if (comboBox != null)
                    {
                        comboBox.DropDown -= new EventHandler(CategoryListRefresh);
                        comboBox.SelectedIndexChanged -= new EventHandler(VrtParentCategory_SelectedIndexChanged);
                        comboBox.SelectedIndexChanged += new EventHandler(VrtParentCategory_SelectedIndexChanged);
                    }
                }
                else if (dgvResultsList.CurrentCell.ColumnIndex == dgvResultsList.Columns["FkCategoryId"].Index)
                {
                    if (comboBox != null)
                    {
                        comboBox.SelectedIndexChanged -= new EventHandler(VrtParentCategory_SelectedIndexChanged);
                        comboBox.DropDown -= new EventHandler(CategoryListRefresh);
                        comboBox.DropDown += new EventHandler(CategoryListRefresh);
                    }
                }
                else
                {
                    comboBox.DropDown -= new EventHandler(CategoryListRefresh);
                    comboBox.SelectedIndexChanged -= new EventHandler(VrtParentCategory_SelectedIndexChanged);

                }
            }
            catch (Exception ex)
            {
                int i = 1;

            }
        }

        private void CategoryListRefresh(object sender, EventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null)
            {
                var currentRow = dgvResultsList.CurrentRow;
                if (currentRow != null)
                {
                    int? selectedMasterCategoryId = (int?)currentRow.Cells["VrtParentCategory"].Value;
                    if (selectedMasterCategoryId != null)
                    {
                        var filteredDetailCategories = columns.lstDetailCategory
                            .Where(c => c.FkParentInfo != null && c.FkParentInfo.Id == selectedMasterCategoryId)
                            .ToList();

                        comboBox.DataSource = filteredDetailCategories;
                        comboBox.DisplayMember = "Title";
                        comboBox.ValueMember = "Id";
                    }
                }
            }
        }

        private void VrtParentCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvResultsList.CurrentCell.ColumnIndex == dgvResultsList.Columns["VrtParentCategory"].Index)
                {
                    if (dgvResultsList.CurrentRow == null) return;

                    var selectedMasterCategoryId = (int)((ComboBox)sender).SelectedValue;
                    //var selectedMasterCategoryId = (int)((DataGridViewComboBoxCell)dgvResultsList.CurrentCell).Value;

                    var categoryCell = (DataGridViewComboBoxCell)dgvResultsList.CurrentRow.Cells["FkCategoryId"];

                    // فیلتر کردن دسته‌بندی‌ها بر اساس انتخاب دسته‌بندی اصلی  
                    var filteredDetailCategories = columns.lstDetailCategory
                        .Where(c => c.FkParentInfo != null && c.FkParentInfo.Id == selectedMasterCategoryId)
                        .ToList();

                    categoryCell.DataSource = filteredDetailCategories;
                    categoryCell.DisplayMember = "Title";
                    categoryCell.ValueMember = "Id";

                    categoryCell.Value = null; // ریست کردن انتخاب  
                }
            }
            catch (Exception ex)
            {
                // نمایش پیام خطا یا لاگ کردن  
                MessageBox.Show($"خطا در انتخاب دسته‌بندی: {ex.Message}");
            }
        }
    }
}
