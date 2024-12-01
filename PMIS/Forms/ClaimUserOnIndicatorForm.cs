using Generic.Service.DTO.Concrete;
using Generic.Service.Normal.Composition.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PMIS.DTO.ClaimUserOnSystem;
using PMIS.DTO.ClaimUserOnIndicator;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User;
using PMIS.Forms.Generic;
using PMIS.Models;
using PMIS.Services;
using PMIS.Services.Contract;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Generic.Helper;
using WSM.WindowsServices.FileManager;

namespace PMIS.Forms
{
    public partial class ClaimUserOnIndicatorForm : Form
    {

        #region Variables
        private List<ClaimUserOnIndicatorAddRequestDto> lstAddRequest;
        private List<ClaimUserOnIndicatorEditRequestDto> lstEditRequest;
        private List<ClaimUserOnIndicatorDeleteRequestDto> lstLogicalDeleteRequest;
        private List<ClaimUserOnIndicatorDeleteRequestDto> lstPhysicalDeleteRequest;
        private List<ClaimUserOnIndicatorDeleteRequestDto> lstRecycleRequest;
        private IEnumerable<ClaimUserOnIndicatorSearchResponseDto> lstSearchResponse;
        private ClaimUserOnIndicatorColumnsDto columns;
        private ILookUpValueService lookUpValueService;
        private IClaimUserOnIndicatorService claimUserOnIndicatorService;
        private IUserService userService;
        private IIndicatorService indicatorService;
        private IClaimUserOnSystemService claimUserOnSystemService;
        private int fkUserId;
        private int fkIndicatorId;
        private bool isLoaded = false;
        private TabControl tabControl;
        #endregion

        public ClaimUserOnIndicatorForm(IClaimUserOnSystemService _claimUserOnSystemService, IClaimUserOnIndicatorService _claimUserOnIndicatorService, IUserService _userService, IIndicatorService _indicatorService, ILookUpValueService _lookUpValueService, int _fkUserId, int _fkIndicatorId, TabControl _tabControl)
        {

            InitializeComponent();
            claimUserOnSystemService = _claimUserOnSystemService;
            claimUserOnIndicatorService = _claimUserOnIndicatorService;
            userService = _userService;
            indicatorService = _indicatorService;
            lookUpValueService = _lookUpValueService;
            fkUserId = _fkUserId;
            fkIndicatorId = _fkIndicatorId;
            tabControl = _tabControl;
            CustomInitialize();

        }
        private async void CustomInitialize()
        {
            int selectedIndex = tabControl.SelectedIndex;
            AddNewTabPage(tabControl, this);
            if (await CheckSystemClaimsRequired())
            {
                // InitializeComponent();
                columns = new ClaimUserOnIndicatorColumnsDto();
                await columns.Initialize(lookUpValueService, userService, indicatorService, fkUserId, fkIndicatorId);
                lstLogicalDeleteRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();
                lstPhysicalDeleteRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();
                lstRecycleRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();
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
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "ClaimUserOnIndicatorForm"))
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
            tabPage.Name = "tabPageClaimUserOnIndicatorForm";
            tabPage.Padding = new Padding(3);
            tabPage.Size = new Size(192, 0);
            tabPage.TabIndex = 0;
            tabPage.Text = "ادعاهای کاربران روی شاخص‌ها";
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
                    if (comboBoxColumn.DataSource is UserSearchResponseDto[] arrayUsr)
                    {
                        if (fkUserId != 0)
                        {
                            ((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkUserId"]).Value = ((UserSearchResponseDto)((DataGridViewComboBoxCell)dgvFiltersList.Rows[0].Cells["FkUserId"]).Items[0]).Id;
                            dgvFiltersList.Rows[0].Cells[column.Name].Value = ((UserSearchResponseDto)comboBoxColumn.Items[0]).Id;
                        }
                        else
                        {
                            List<UserSearchResponseDto> lstSourse = arrayUsr.ToList();
                            lstSourse.Insert(0, new UserSearchResponseDto() { Id = 0, UserName = "همه", });
                            comboBoxColumn.DataSource = lstSourse;
                            comboBoxColumn.DisplayMember = "UserName";
                            comboBoxColumn.ValueMember = "Id";
                            // comboBoxColumn.SelectedIndex = 0;
                            dgvFiltersList.Rows[0].Cells[column.Name].Value = 0;
                        }
                    }

                    //dgvFiltersList.Rows[0].Cells[column.Name].Value = 0;
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
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                    row.Cells["FlgEdited"].Value = false;

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
            if (lstAddRequest.Count != 0 ||
                lstEditRequest.Count != 0 ||
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
            lstAddRequest = new List<ClaimUserOnIndicatorAddRequestDto>();
            lstEditRequest = new List<ClaimUserOnIndicatorEditRequestDto>();
            lstLogicalDeleteRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();
            lstRecycleRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();

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


            (bool isSuccess, lstSearchResponse) = await claimUserOnIndicatorService.Search(searchRequest);
            dgvResultsList.DataSource = new BindingList<ClaimUserOnIndicatorSearchResponseDto>(lstSearchResponse.ToList());

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
                await EdiClaimUserOnIndicator();
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
                lstAddRequest = new List<ClaimUserOnIndicatorAddRequestDto>();

                foreach (DataGridViewRow row in dgvResultsList.Rows)
                {
                    try
                    {

                        if ((row.Cells["Id"].Value == null && row.Index + 1 < dgvResultsList.Rows.Count) || (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) == 0))
                        {
                            ClaimUserOnIndicatorAddRequestDto addRequest = new ClaimUserOnIndicatorAddRequestDto();

                            addRequest = AddMaping(row);
                            lstAddRequest.Add(addRequest);
                        }
                    }
                    catch (Exception) { }
                }

                //(bool isSuccess, IEnumerable<ClaimUserOnIndicatorAddResponseDto> list) = await claimUserOnIndicatorService.AddGroup(lstAddRequest);
                bool isSuccess = await claimUserOnIndicatorService.AddRange(lstAddRequest);

                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EdiClaimUserOnIndicator()
        {
            try
            {
                lstEditRequest = new List<ClaimUserOnIndicatorEditRequestDto>();

                foreach (DataGridViewRow row in dgvResultsList.Rows)
                {
                    try
                    {
                        if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0 && bool.Parse((row.Cells["FlgEdited"].Value ?? false).ToString()) == true)
                        {
                            ClaimUserOnIndicatorEditRequestDto editRequest = new ClaimUserOnIndicatorEditRequestDto();

                            editRequest = EditMaping(row);
                            lstEditRequest.Add(editRequest);
                        }
                    }
                    catch (Exception) { }
                }

                //(bool isSuccess, IEnumerable<ClaimUserOnIndicatorEditResponseDto> list) = await claimUserOnIndicatorService.EditGroup(lstEditRequest);
                bool isSuccess = await claimUserOnIndicatorService.EditRange(lstEditRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("عملیات ویرایش موفقیت آمیز نبود", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //(bool isSuccess, IEnumerable<ClaimUserOnIndicatorDeleteResponseDto> list) = await claimUserOnIndicatorService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await claimUserOnIndicatorService.LogicalDeleteRange(lstLogicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstLogicalDeleteRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();
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

                //(bool isSuccess, IEnumerable<ClaimUserOnIndicatorDeleteResponseDto> list) = await claimUserOnIndicatorService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await claimUserOnIndicatorService.RecycleRange(lstRecycleRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstRecycleRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();
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

                //(bool isSuccess, IEnumerable<ClaimUserOnIndicatorDeleteResponseDto> list) = await claimUserOnIndicatorService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await claimUserOnIndicatorService.PhysicalDeleteRange(lstPhysicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstPhysicalDeleteRequest = new List<ClaimUserOnIndicatorDeleteRequestDto>();
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
                    ClaimUserOnIndicatorDeleteRequestDto deleteRequest = new ClaimUserOnIndicatorDeleteRequestDto();
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
                    ClaimUserOnIndicatorDeleteRequestDto deleteRequest = new ClaimUserOnIndicatorDeleteRequestDto();
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
                    ClaimUserOnIndicatorDeleteRequestDto deleteRequest = new ClaimUserOnIndicatorDeleteRequestDto();
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

        private ClaimUserOnIndicatorAddRequestDto AddMaping(DataGridViewRow row)
        {
            try
            {
                ClaimUserOnIndicatorAddRequestDto addRequest = new ClaimUserOnIndicatorAddRequestDto();

                var entityFields = addRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (DataGridViewColumn column in dgvResultsList.Columns)
                {
                    var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Equals("<" + column.Name + ">k__BackingField"));
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(addRequest, row.Cells[column.Name].Value);
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

        private ClaimUserOnIndicatorAddRequestDto AfterAddMapping(ClaimUserOnIndicatorAddRequestDto addRequest)
        {
            addRequest.FkUserId = fkUserId == 0 ? addRequest.FkUserId : fkUserId;
            addRequest.FkIndicatorId = fkIndicatorId == 0 ? addRequest.FkIndicatorId : fkIndicatorId;
            return addRequest;
        }

        private ClaimUserOnIndicatorEditRequestDto EditMaping(DataGridViewRow row)
        {
            try
            {
                ClaimUserOnIndicatorEditRequestDto editRequest = new ClaimUserOnIndicatorEditRequestDto();
                var entityFields = editRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (DataGridViewColumn column in dgvResultsList.Columns)
                {
                    var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Equals("<" + column.Name + ">k__BackingField"));
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(editRequest, row.Cells[column.Name].Value);
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

        private ClaimUserOnIndicatorEditRequestDto AfterEditMapping(ClaimUserOnIndicatorEditRequestDto addRequest)
        {
            addRequest.FkUserId = fkUserId == 0 ? addRequest.FkUserId : fkUserId;
            addRequest.FkIndicatorId = fkIndicatorId == 0 ? addRequest.FkIndicatorId : fkIndicatorId;
            return addRequest;
        }

        private void Download()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = "IndicatorsClaims-" + Helper.Convert.ConvertGregorianToShamsi(DateTime.Now, "RRRRMMDDHH24MISSMS");
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFileDialog.FileName = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf('.')) + "\\IndicatorsClaims" + saveFileDialog.FileName.Substring(saveFileDialog.FileName.LastIndexOf('.'));
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
    }
}
