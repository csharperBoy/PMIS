using Generic.Service.DTO.Concrete;
using Generic.Service.Normal.Composition.Contract;
using Microsoft.IdentityModel.Tokens;
using PMIS.DTO;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Services.Contract;
using System.ComponentModel;
using System.Reflection;

namespace PMIS.Forms
{
    public abstract class AbstractBaseStandardForm
    {
        public abstract void RefreshVisuals();
        public abstract Task SearchEntity();
        public abstract Task AddEntity();
        public abstract Task EditEntity();
        public abstract Task LogicalDeleteEntity();
        public abstract Task RecycleEntity();
        public abstract Task PhysicalDeleteEntity();
        public abstract void RowEnter(int rowIndex);
        public abstract void RowLeave(int rowIndex);
        public abstract void CellContentClick(int rowIndex, int columnIndex);
        public abstract bool CellBeginEdit(int rowIndex);
        public abstract void RowPostPaint(int rowIndex);
    }
    public partial class BaseStandardForm<TEntityService, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto, TEntityColumnsDto> : AbstractBaseStandardForm
     where TEntityService : IGenericNormalService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto>
     where TEntity : class, new()
     where TEntityAddRequestDto : GenericAddRequestDto, new()
     where TEntityAddResponseDto : GenericAddResponseDto, new()
     where TEntityEditRequestDto : GenericEditRequestDto, new()
     where TEntityEditResponseDto : GenericEditResponseDto, new()
     where TEntityDeleteRequestDto : GenericDeleteRequestDto, new()
     where TEntityDeleteResponseDto : GenericDeleteResponseDto, new()
     where TEntitySearchResponseDto : GenericSearchResponseDto, new()
     where TEntityColumnsDto : GenericColumnsDto, new()
    {

        #region Variables
        private TEntityService entityService;
        private List<TEntityAddRequestDto> lstAddRequest;
        private List<TEntityEditRequestDto> lstEditRequest;
        private List<TEntityDeleteRequestDto> lstLogicalDeleteRequest;
        private List<TEntityDeleteRequestDto> lstPhysicalDeleteRequest;
        private List<TEntityDeleteRequestDto> lstRecycleRequest;
        private TEntityColumnsDto columns;
        private ILookUpValueService lookUpValueService;
        private NormalFormElements NormalFormElements;
        private bool isLoaded = false;
        #endregion

        public BaseStandardForm(TEntityService _entityService, ILookUpValueService _lookUpValueService, NormalFormElements _NormalFormElements)
        {
            entityService = _entityService;
            lookUpValueService = _lookUpValueService;
            NormalFormElements = _NormalFormElements;

            CustomInitialize();
        }

        private async void CustomInitialize()
        {
            // InitializeComponent();
            columns = new TEntityColumnsDto();
            await columns.Initialize(lookUpValueService);
            lstLogicalDeleteRequest = new List<TEntityDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<TEntityDeleteRequestDto>();
            lstRecycleRequest = new List<TEntityDeleteRequestDto>();
            GenerateDgvFilterColumnsInitialize();
            GenerateDgvResultColumnsInitialize();
            FiltersInitialize();
            SearchEntity();
        }

        private void GenerateDgvFilterColumnsInitialize()
        {
            try
            {
                NormalFormElements.dgvFiltersList.AllowUserToAddRows = false;
                AddColumnsToGridView(NormalFormElements.dgvFiltersList, "FilterColumns");
                NormalFormElements.dgvFiltersList.Rows.Add();
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
                NormalFormElements.dgvResultsList.AutoGenerateColumns = false;
                NormalFormElements.dgvResultsList.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "ردیف",
                    Name = "RowNumber",
                    ReadOnly = true,
                    Visible = true,
                    Frozen = true,
                });
                AddColumnsToGridView(NormalFormElements.dgvResultsList, "ResultColumns");
                NormalFormElements.dgvResultsList.Columns.Add(new DataGridViewCheckBoxColumn()
                {
                    HeaderText = "حذف شده",
                    Name = "FlgLogicalDelete",
                    DataPropertyName = "FlgLogicalDelete",
                    ReadOnly = false,
                    Visible = false,
                    IndeterminateValue = false
                });
                NormalFormElements.dgvResultsList.Columns.Add(new DataGridViewCheckBoxColumn()
                {
                    HeaderText = "ویرایش شده",
                    Name = "FlgEdited",
                    ReadOnly = false,
                    Visible = false,
                    IndeterminateValue = false
                });
                NormalFormElements.dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "Edit",
                    Text = "ویرایش",
                    ReadOnly = false,
                    Visible = true,
                    UseColumnTextForButtonValue = true,
                });
                NormalFormElements.dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "LogicalDelete",
                    Text = "حذف موقت",
                    ReadOnly = false,
                    Visible = true,
                    UseColumnTextForButtonValue = true,
                });
                NormalFormElements.dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "Recycle",
                    Text = "بازیابی",
                    ReadOnly = false,
                    Visible = false,
                    UseColumnTextForButtonValue = true,

                });
                NormalFormElements.dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
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
            foreach (DataGridViewColumn column in NormalFormElements.dgvFiltersList.Columns)
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
                }
            }
        }

        public override void RefreshVisuals()
        {
            try
            {
                NormalFormElements.dgvResultsList.Columns["Edit"].Visible = !NormalFormElements.chbRecycle.Checked;
                NormalFormElements.dgvResultsList.Columns["LogicalDelete"].Visible = !NormalFormElements.chbRecycle.Checked;
                NormalFormElements.dgvResultsList.Columns["Recycle"].Visible = NormalFormElements.chbRecycle.Checked;
                NormalFormElements.dgvResultsList.Columns["PhysicalDelete"].Visible = NormalFormElements.chbRecycle.Checked;
                NormalFormElements.dgvResultsList.AllowUserToAddRows = !NormalFormElements.chbRecycle.Checked;

                foreach (DataGridViewRow row in NormalFormElements.dgvResultsList.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                    row.Cells["FlgEdited"].Value = false;

                }
                if (NormalFormElements.dgvResultsList.Rows.Count > 0)
                {
                    NormalFormElements.dgvResultsList.CurrentCell = NormalFormElements.dgvResultsList.Rows[0].Cells[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override async Task SearchEntity()
        {
            isLoaded = false;
            lstAddRequest = new List<TEntityAddRequestDto>();
            lstEditRequest = new List<TEntityEditRequestDto>();
            lstLogicalDeleteRequest = new List<TEntityDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<TEntityDeleteRequestDto>();
            lstRecycleRequest = new List<TEntityDeleteRequestDto>();

            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto()
            {
                filters = new List<GenericSearchFilterDto>(),
            };
            searchRequest.filters.Add(new GenericSearchFilterDto()
            {
                columnName = "FlgLogicalDelete",
                value = NormalFormElements.chbRecycle.Checked.ToString(),
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
            foreach (DataGridViewRow row in NormalFormElements.dgvFiltersList.Rows)
            {
                GenericSearchFilterDto tempFilter = new GenericSearchFilterDto()
                {
                    InternalFilters = new List<GenericSearchFilterDto>(),
                    LogicalOperator = row.Index == 0 ? LogicalOperator.Begin : LogicalOperator.Or,
                    type = PhraseType.Parentheses,
                };
                foreach (DataGridViewColumn column in NormalFormElements.dgvFiltersList.Columns)
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


            (bool isSuccess, IEnumerable<TEntitySearchResponseDto> list) = await entityService.Search(searchRequest);

            if (isSuccess)
            {
                if (list.Count() == 0)
                {
                    NormalFormElements.dgvResultsList.DataSource = null;
                    MessageBox.Show("موردی یافت نشد!!!");
                }
                else
                {
                    NormalFormElements.dgvResultsList.DataSource = new BindingList<TEntitySearchResponseDto>(list.ToList());
                }
            }
            else
            {
                MessageBox.Show("عملیات موفقیت‌آمیز نبود!!!");
            }
            RefreshVisuals();
            isLoaded = true;
        }

        public override async Task AddEntity()
        {
            try
            {
                lstAddRequest = new List<TEntityAddRequestDto>();

                foreach (DataGridViewRow row in NormalFormElements.dgvResultsList.Rows)
                {
                    try
                    {
                        if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) == 0)
                        {
                            TEntityAddRequestDto addRequest = new TEntityAddRequestDto();

                            addRequest = AddMaping(row);
                            lstAddRequest.Add(addRequest);
                        }
                    }
                    catch (Exception) { }
                }

                //(bool isSuccess, IEnumerable<TEntityAddResponseDto> list) = await TEntityService.AddGroup(lstAddRequest);
                bool isSuccess = await entityService.AddRange(lstAddRequest);

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

        public override async Task EditEntity()
        {
            try
            {
                lstEditRequest = new List<TEntityEditRequestDto>();

                foreach (DataGridViewRow row in NormalFormElements.dgvResultsList.Rows)
                {
                    try
                    {
                        if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0 && bool.Parse((row.Cells["FlgEdited"].Value ?? false).ToString()) == true)
                        {
                            TEntityEditRequestDto editRequest = new TEntityEditRequestDto();

                            editRequest = EditMaping(row);
                            lstEditRequest.Add(editRequest);
                        }
                    }
                    catch (Exception) { }
                }

                //(bool isSuccess, IEnumerable<TEntityEditResponseDto> list) = await TEntityService.EditGroup(lstEditRequest);
                bool isSuccess = await entityService.EditRange(lstEditRequest);
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

        public override async Task LogicalDeleteEntity()
        {
            try
            {

                //(bool isSuccess, IEnumerable<TEntityDeleteResponseDto> list) = await TEntityService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await entityService.LogicalDeleteRange(lstLogicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstLogicalDeleteRequest = new List<TEntityDeleteRequestDto>();
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

        public override async Task RecycleEntity()
        {
            try
            {

                //(bool isSuccess, IEnumerable<TEntityDeleteResponseDto> list) = await TEntityService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await entityService.RecycleRange(lstRecycleRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstRecycleRequest = new List<TEntityDeleteRequestDto>();
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

        public override async Task PhysicalDeleteEntity()
        {
            try
            {

                //(bool isSuccess, IEnumerable<TEntityDeleteResponseDto> list) = await TEntityService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await entityService.PhysicalDeleteRange(lstPhysicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstPhysicalDeleteRequest = new List<TEntityDeleteRequestDto>();
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

        public override void RowEnter(int rowIndex)
        {
            if (isLoaded)
            {
                DataGridViewRow selectedRow = NormalFormElements.dgvResultsList.Rows[rowIndex];
                selectedRow.DefaultCellStyle.BackColor = Color.LightBlue;
                selectedRow.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        public override void RowLeave(int rowIndex)
        {
            DataGridViewRow previousRow = NormalFormElements.dgvResultsList.Rows[rowIndex];
            if (NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value != null && bool.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value.ToString()))
            {
                previousRow.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) == 0)
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

        public override void CellContentClick(int rowIndex, int columnIndex)
        {
            if (rowIndex == -1)
                return;
            if (NormalFormElements.dgvResultsList.Rows[rowIndex].IsNewRow)
                return;
            if (NormalFormElements.dgvResultsList.Columns[columnIndex].Name == "Edit" && rowIndex >= 0)
            {
                var row = NormalFormElements.dgvResultsList.Rows[rowIndex];
                foreach (DataGridViewCell cell in row.Cells)
                {
                    NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value = true;
                    cell.ReadOnly = false;
                }
            }
            else if (NormalFormElements.dgvResultsList.Columns[columnIndex].Name == "LogicalDelete" && rowIndex >= 0)
            {
                var row = NormalFormElements.dgvResultsList.Rows[rowIndex];
                if (NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstLogicalDeleteRequest.Add(deleteRequest);
                    NormalFormElements.dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }

            else if (NormalFormElements.dgvResultsList.Columns[columnIndex].Name == "PhysicalDelete" && rowIndex >= 0)
            {
                var row = NormalFormElements.dgvResultsList.Rows[rowIndex];
                if (NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstPhysicalDeleteRequest.Add(deleteRequest);

                    NormalFormElements.dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }
            else if (NormalFormElements.dgvResultsList.Columns[columnIndex].Name == "Recycle" && rowIndex >= 0)
            {
                var row = NormalFormElements.dgvResultsList.Rows[rowIndex];
                if (NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstRecycleRequest.Add(deleteRequest);

                    NormalFormElements.dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }
        }

        public override bool CellBeginEdit(int rowIndex)
        {
            if ((NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value == null || bool.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value.ToString()) == false) && (NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0))
            {
                return true;
            }
            return false;
        }

        public override void RowPostPaint(int rowIndex)
        {
            NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["RowNumber"].Value = (rowIndex + 1).ToString();

            if (NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value == null)
            {
                foreach (DataGridViewCell cell in NormalFormElements.dgvResultsList.Rows[rowIndex].Cells)
                {
                    cell.ReadOnly = false;
                }
            }
            NormalFormElements.dgvResultsList.Rows[rowIndex].Cells["RowNumber"].ReadOnly = true;
        }

        private TEntityAddRequestDto AddMaping(DataGridViewRow row)
        {
            try
            {
                TEntityAddRequestDto addRequest = new TEntityAddRequestDto();

                var entityFields = addRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (DataGridViewColumn column in NormalFormElements.dgvResultsList.Columns)
                {
                    var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Contains(column.Name));
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(addRequest, row.Cells[column.Name].Value);
                    }
                }
                return addRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private TEntityEditRequestDto EditMaping(DataGridViewRow row)
        {
            try
            {
                TEntityEditRequestDto editRequest = new TEntityEditRequestDto();
                var entityFields = editRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (DataGridViewColumn column in NormalFormElements.dgvResultsList.Columns)
                {
                    var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Contains(column.Name));
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(editRequest, row.Cells[column.Name].Value);
                    }
                }
                return editRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
