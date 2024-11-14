using Generic.Base.Handler.Map;
using Generic.Service.DTO.Concrete;
using Generic.Service.Normal.Composition.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PMIS.DTO;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Services.Contract;
using System.ComponentModel;
using System.Reflection;

namespace PMIS.Forms
{
    public partial class BaseStandardForm<TEntityService, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto, TEntityColumnsDto>
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
        private BaseStandardFormElements formElements;
        private bool isLoaded = false;
        #endregion

        public BaseStandardForm(TEntityService _entityService, ILookUpValueService _lookUpValueService, BaseStandardFormElements _formElements)
        {
            entityService = _entityService;
            lookUpValueService = _lookUpValueService;
            formElements = _formElements;

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
                formElements.dgvFiltersList.AllowUserToAddRows = false;
                AddColumnsToGridView(formElements.dgvFiltersList, "FilterColumns");
                formElements.dgvFiltersList.Rows.Add();
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
                formElements.dgvResultsList.AutoGenerateColumns = false;
                formElements.dgvResultsList.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "ردیف",
                    Name = "RowNumber",
                    ReadOnly = true,
                    Visible = true,
                    Frozen = true,
                });
                AddColumnsToGridView(formElements.dgvResultsList, "ResultColumns");
                formElements.dgvResultsList.Columns.Add(new DataGridViewCheckBoxColumn()
                {
                    HeaderText = "حذف شده",
                    Name = "FlgLogicalDelete",
                    DataPropertyName = "FlgLogicalDelete",
                    ReadOnly = false,
                    Visible = false,
                    IndeterminateValue = false
                });
                formElements.dgvResultsList.Columns.Add(new DataGridViewCheckBoxColumn()
                {
                    HeaderText = "ویرایش شده",
                    Name = "FlgEdited",
                    ReadOnly = false,
                    Visible = false,
                    IndeterminateValue = false
                });
                formElements.dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "Edit",
                    Text = "ویرایش",
                    ReadOnly = false,
                    Visible = true,
                    UseColumnTextForButtonValue = true,
                });
                formElements.dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "LogicalDelete",
                    Text = "حذف موقت",
                    ReadOnly = false,
                    Visible = true,
                    UseColumnTextForButtonValue = true,
                });
                formElements.dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
                {
                    HeaderText = "",
                    Name = "Recycle",
                    Text = "بازیابی",
                    ReadOnly = false,
                    Visible = false,
                    UseColumnTextForButtonValue = true,

                });
                formElements.dgvResultsList.Columns.Add(new DataGridViewButtonColumn()
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
            foreach (DataGridViewColumn column in formElements.dgvFiltersList.Columns)
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

        public void RefreshVisuals()
        {
            try
            {
                formElements.dgvResultsList.Columns["Edit"].Visible = !formElements.chbRecycle.Checked;
                formElements.dgvResultsList.Columns["LogicalDelete"].Visible = !formElements.chbRecycle.Checked;
                formElements.dgvResultsList.Columns["Recycle"].Visible = formElements.chbRecycle.Checked;
                formElements.dgvResultsList.Columns["PhysicalDelete"].Visible = formElements.chbRecycle.Checked;
                formElements.dgvResultsList.AllowUserToAddRows = !formElements.chbRecycle.Checked;

                foreach (DataGridViewRow row in formElements.dgvResultsList.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                    row.Cells["FlgEdited"].Value = false;

                }
                if (formElements.dgvResultsList.Rows.Count > 0)
                {
                    formElements.dgvResultsList.CurrentCell = formElements.dgvResultsList.Rows[0].Cells[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SearchEntity()
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
                value = formElements.chbRecycle.Checked.ToString(),
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
            foreach (DataGridViewRow row in formElements.dgvFiltersList.Rows)
            {
                GenericSearchFilterDto tempFilter = new GenericSearchFilterDto()
                {
                    InternalFilters = new List<GenericSearchFilterDto>(),
                    LogicalOperator = row.Index == 0 ? LogicalOperator.Begin : LogicalOperator.Or,
                    type = PhraseType.Parentheses,
                };
                foreach (DataGridViewColumn column in formElements.dgvFiltersList.Columns)
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
                    formElements.dgvResultsList.DataSource = null;
                    MessageBox.Show("موردی یافت نشد!!!");
                }
                else
                {
                    formElements.dgvResultsList.DataSource = new BindingList<TEntitySearchResponseDto>(list.ToList());
                }
            }
            else
            {
                MessageBox.Show("عملیات موفقیت‌آمیز نبود!!!");
            }
            RefreshVisuals();
            isLoaded = true;
        }

        public async Task AddEntity()
        {
            try
            {
                lstAddRequest = new List<TEntityAddRequestDto>();

                foreach (DataGridViewRow row in formElements.dgvResultsList.Rows)
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

        public async Task EditEntity()
        {
            try
            {
                lstEditRequest = new List<TEntityEditRequestDto>();

                foreach (DataGridViewRow row in formElements.dgvResultsList.Rows)
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

        public async Task LogicalDeleteEntity()
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

        public async Task RecycleEntity()
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

        public async Task PhysicalDeleteEntity()
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

        public void RowEnter(int rowIndex)
        {
            if (isLoaded)
            {
                DataGridViewRow selectedRow = formElements.dgvResultsList.Rows[rowIndex];
                selectedRow.DefaultCellStyle.BackColor = Color.LightBlue;
                selectedRow.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        public void RowLeave(int rowIndex)
        {
            DataGridViewRow previousRow = formElements.dgvResultsList.Rows[rowIndex];
            if (formElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value != null && bool.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value.ToString()))
            {
                previousRow.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) == 0)
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
            if (formElements.dgvResultsList.Rows[rowIndex].IsNewRow)
                return;
            if (formElements.dgvResultsList.Columns[columnIndex].Name == "Edit" && rowIndex >= 0)
            {
                var row = formElements.dgvResultsList.Rows[rowIndex];
                foreach (DataGridViewCell cell in row.Cells)
                {
                    formElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value = true;
                    cell.ReadOnly = false;
                }
            }
            else if (formElements.dgvResultsList.Columns[columnIndex].Name == "LogicalDelete" && rowIndex >= 0)
            {
                var row = formElements.dgvResultsList.Rows[rowIndex];
                if (formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstLogicalDeleteRequest.Add(deleteRequest);
                    formElements.dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }

            else if (formElements.dgvResultsList.Columns[columnIndex].Name == "PhysicalDelete" && rowIndex >= 0)
            {
                var row = formElements.dgvResultsList.Rows[rowIndex];
                if (formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstPhysicalDeleteRequest.Add(deleteRequest);

                    formElements.dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }
            else if (formElements.dgvResultsList.Columns[columnIndex].Name == "Recycle" && rowIndex >= 0)
            {
                var row = formElements.dgvResultsList.Rows[rowIndex];
                if (formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstRecycleRequest.Add(deleteRequest);

                    formElements.dgvResultsList.Rows.RemoveAt(rowIndex);
                }
            }
        }

        public bool CellBeginEdit(int rowIndex)
        {
            if ((formElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value == null || bool.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value.ToString()) == false) && (formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value != null && int.Parse(formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value.ToString()) != 0))
            {
                return true;
            }
            return false;
        }

        public void RowPostPaint(int rowIndex)
        {
            formElements.dgvResultsList.Rows[rowIndex].Cells["RowNumber"].Value = (rowIndex + 1).ToString();

            if (formElements.dgvResultsList.Rows[rowIndex].Cells["Id"].Value == null)
            {
                foreach (DataGridViewCell cell in formElements.dgvResultsList.Rows[rowIndex].Cells)
                {
                    cell.ReadOnly = false;
                }
            }
            formElements.dgvResultsList.Rows[rowIndex].Cells["RowNumber"].ReadOnly = true;
        }

        public TEntityAddRequestDto AddMaping(DataGridViewRow row)
        {
            try
            {
                TEntityAddRequestDto addRequest = new TEntityAddRequestDto();
                //try { addRequest.Code = row.Cells["Code"].Value?.ToString(); } catch (Exception ex) { }
                //try { addRequest.Title = row.Cells["Title"].Value?.ToString(); } catch (Exception ex) { }
                //try { addRequest.FkLkpFormId = int.Parse(row.Cells["FkLkpFormId"].Value?.ToString()); } catch (Exception ex) { }
                //try { addRequest.FkLkpManualityId = int.Parse(row.Cells["FkLkpManualityId"].Value?.ToString()); } catch (Exception ex) { }
                //try { addRequest.FkLkpUnitId = int.Parse(row.Cells["FkLkpUnitId"].Value?.ToString()); } catch (Exception ex) { }
                //try { addRequest.FkLkpPeriodId = int.Parse(row.Cells["FkLkpPeriodId"].Value?.ToString()); } catch (Exception ex) { }
                //try { addRequest.FkLkpMeasureId = int.Parse(row.Cells["FkLkpMeasureId"].Value?.ToString()); } catch (Exception ex) { }
                //try { addRequest.FkLkpDesirabilityId = int.Parse(row.Cells["FkLkpDesirabilityId"].Value?.ToString()); } catch (Exception ex) { }
                //try { addRequest.Formula = row.Cells["Formula"].Value?.ToString(); } catch (Exception ex) { }
                //try { addRequest.Description = row.Cells["Description"].Value?.ToString(); } catch (Exception ex) { }
                return addRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public TEntityEditRequestDto EditMaping(DataGridViewRow row)
        {
            try
            {
                TEntityEditRequestDto editRequest = new TEntityEditRequestDto();
                //try { editRequest.Id = int.Parse(row.Cells["Id"].Value?.ToString()); } catch (Exception ex) { }
                //try { editRequest.Code = row.Cells["Code"].Value?.ToString(); } catch (Exception ex) { }
                //try { editRequest.Title = row.Cells["Title"].Value?.ToString(); } catch (Exception ex) { }
                //try { editRequest.FkLkpFormId = int.Parse(row.Cells["FkLkpFormId"].Value?.ToString()); } catch (Exception ex) { }
                //try { editRequest.FkLkpManualityId = int.Parse(row.Cells["FkLkpManualityId"].Value?.ToString()); } catch (Exception ex) { }
                //try { editRequest.FkLkpUnitId = int.Parse(row.Cells["FkLkpUnitId"].Value?.ToString()); } catch (Exception ex) { }
                //try { editRequest.FkLkpPeriodId = int.Parse(row.Cells["FkLkpPeriodId"].Value?.ToString()); } catch (Exception ex) { }
                //try { editRequest.FkLkpMeasureId = int.Parse(row.Cells["FkLkpMeasureId"].Value?.ToString()); } catch (Exception ex) { }
                //try { editRequest.FkLkpDesirabilityId = int.Parse(row.Cells["FkLkpDesirabilityId"].Value?.ToString()); } catch (Exception ex) { }
                //try { editRequest.Formula = row.Cells["Formula"].Value?.ToString(); } catch (Exception ex) { }
                //try { editRequest.Description = row.Cells["Description"].Value?.ToString(); } catch (Exception ex) { }
                return editRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    public class BaseStandardFormElements
    {
        public DataGridView dgvFiltersList;
        public DataGridView dgvResultsList;
        public CheckBox chbRecycle;
    }
}
