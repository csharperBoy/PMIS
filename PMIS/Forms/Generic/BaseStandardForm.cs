using PMIS.DTO.LookUpDestination;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMIS.Services;
using Generic.Service.DTO.Abstract;
using Generic.Service.DTO.Concrete;
using Generic.Service.Normal.Composition;
using Microsoft.EntityFrameworkCore;
using PMIS.DTO.LookUpValue.Info;
using System.ComponentModel;
using System.Reflection;
using Generic.Service.Normal.Composition.Contract;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace PMIS.Forms.Generic
{
    public abstract class BaseStandardForm<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto, TEntityService>
        : Form
       where TContext : DbContext
     where TEntity : class, new()
     where TEntityAddRequestDto : GenericAddRequestDto, new()
     where TEntityAddResponseDto : GenericAddResponseDto, new()
     where TEntityEditRequestDto : GenericEditRequestDto, new()
     where TEntityEditResponseDto : GenericEditResponseDto, new()
     where TEntityDeleteRequestDto : GenericDeleteRequestDto, new()
     where TEntityDeleteResponseDto : GenericDeleteResponseDto, new()
     where TEntitySearchResponseDto : class, new()
     where TEntityService : IGenericNormalService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto>

    {
        #region Variables
        protected IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination;
        private List<TEntityAddRequestDto> lstAddRequest;
        private List<TEntityEditRequestDto> lstEditRequest;
        private List<TEntityDeleteRequestDto> lstLogicalDeleteRequest;
        private List<TEntityDeleteRequestDto> lstPhysicalDeleteRequest;
        private List<TEntityDeleteRequestDto> lstRecycleRequest;
        private TEntityService entityService;
        protected ILookUpValueService lookUpValueService;
        private bool isLoaded = false;
        DataGridView dgvFiltersList;
        DataGridView dgvResultsList;
        CheckBox chbRecycle;
        #endregion

        public BaseStandardForm(TEntityService _entityService, ILookUpValueService _lookUpValueService)
        {



            entityService = _entityService;
            lookUpValueService = _lookUpValueService;



            CustomInitialize();



        }
        protected abstract void CustomInitializeComponent();
        public abstract Task<(DataGridView, DataGridView, CheckBox)> SetDataControls();

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
                }
            }

        }


        public async void CustomInitialize()
        {
            CustomInitializeComponent();
            (dgvFiltersList, dgvResultsList, chbRecycle) = await SetDataControls();
            #region MyRegion1

            lstLogicalDeleteRequest = new List<TEntityDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<TEntityDeleteRequestDto>();
            lstRecycleRequest = new List<TEntityDeleteRequestDto>();
            lstLookUpDestination = await lookUpValueService.GetList("Indicator");
            dgvResultsList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ردیف",
                Name = "RowNumber",
                ReadOnly = true,
                Visible = true,
                Frozen = true,
            });
            #endregion
            await GenerateDgvFilterColumnsInitialize();
            await GenerateDgvResultColumnsInitialize();
            FiltersInitialize();
            #region MyRegion2

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
            #endregion
            SearchEntity();
        }
        protected abstract Task GenerateDgvResultColumnsInitialize();

        protected abstract Task GenerateDgvFilterColumnsInitialize();

        protected async Task SearchEntity()
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
                value = chbRecycle.Checked.ToString(),
                LogicalOperator = LogicalOperator.Begin,
                operation = FilterOperator.Equals,
                type = PhraseType.Condition,
            });
            GenericSearchFilterDto filter = new GenericSearchFilterDto()
            {
                InternalFilters = new List<GenericSearchFilterDto>(),
                LogicalOperator =  LogicalOperator.And ,
                type = PhraseType.Parentheses,
            };
            foreach (DataGridViewRow row in dgvFiltersList.Rows)
            {
                GenericSearchFilterDto tempFilter = new GenericSearchFilterDto()
                {
                    InternalFilters = new List<GenericSearchFilterDto>(),
                    LogicalOperator = row.Index == 0 ? LogicalOperator.Begin: LogicalOperator.Or,
                    type = PhraseType.Parentheses,
                };
                foreach (DataGridViewColumn column in dgvFiltersList.Columns)
                {
                    var cellValue = row.Cells[column.Name].Value == null ? "" : row.Cells[column.Name].Value.ToString();
                    if ((column is not DataGridViewComboBoxColumn  && !cellValue.IsNullOrEmpty()) || (column is DataGridViewComboBoxColumn && cellValue != "" && cellValue != "0"))
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
                    dgvResultsList.DataSource = null;
                    MessageBox.Show("موردی یافت نشد!!!");
                }
                else
                {
                    dgvResultsList.DataSource = new BindingList<TEntitySearchResponseDto>(list.ToList());
                }
            }
            else
            {
                MessageBox.Show("عملیات موفقیت‌آمیز نبود!!!");
            }
            RefreshVisuals();
            isLoaded = true;
        }

        protected void RefreshVisuals()
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
        protected async Task AddEntity()
        {
            try
            {
                lstAddRequest = new List<TEntityAddRequestDto>();

                foreach (DataGridViewRow row in dgvResultsList.Rows)
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
        protected async Task EditEntity()
        {
            try
            {
                lstEditRequest = new List<TEntityEditRequestDto>();

                foreach (DataGridViewRow row in dgvResultsList.Rows)
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

        protected abstract TEntityAddRequestDto AddMaping(DataGridViewRow row);


        protected abstract TEntityEditRequestDto EditMaping(DataGridViewRow row);
        protected async Task LogicalDeleteEntity()
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

        protected async Task PhysicalDeleteEntity()
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

        protected async Task RecycleEntity()
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
        protected void dgvResultsList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvResultsList.Rows[e.RowIndex].Cells["RowNumber"].Value = (e.RowIndex + 1).ToString();

            if (dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value == null)
            {
                foreach (DataGridViewCell cell in dgvResultsList.Rows[e.RowIndex].Cells)
                {
                    cell.ReadOnly = false;
                }
            }
            dgvResultsList.Rows[e.RowIndex].Cells["RowNumber"].ReadOnly = true;
        }

        protected void dgvResultsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (dgvResultsList.Rows[e.RowIndex].IsNewRow)
                return;
            if (dgvResultsList.Columns[e.ColumnIndex].Name == "Edit" && e.RowIndex >= 0)
            {
                var row = dgvResultsList.Rows[e.RowIndex];
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dgvResultsList.Rows[e.RowIndex].Cells["FlgEdited"].Value = true;
                    cell.ReadOnly = false;
                }
            }
            else if (dgvResultsList.Columns[e.ColumnIndex].Name == "LogicalDelete" && e.RowIndex >= 0)
            {
                var row = dgvResultsList.Rows[e.RowIndex];
                if (dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstLogicalDeleteRequest.Add(deleteRequest);
                    dgvResultsList.Rows.RemoveAt(e.RowIndex);
                }
            }

            else if (dgvResultsList.Columns[e.ColumnIndex].Name == "PhysicalDelete" && e.RowIndex >= 0)
            {
                var row = dgvResultsList.Rows[e.RowIndex];
                if (dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstPhysicalDeleteRequest.Add(deleteRequest);

                    dgvResultsList.Rows.RemoveAt(e.RowIndex);
                }
            }
            else if (dgvResultsList.Columns[e.ColumnIndex].Name == "Recycle" && e.RowIndex >= 0)
            {
                var row = dgvResultsList.Rows[e.RowIndex];
                if (dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0)
                {
                    TEntityDeleteRequestDto deleteRequest = new TEntityDeleteRequestDto();
                    var entityFields = deleteRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Equals("<" + "Id" + ">k__BackingField"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(deleteRequest, int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                    }
                    lstRecycleRequest.Add(deleteRequest);

                    dgvResultsList.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        protected void dgvResultsList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        protected void dgvResultsList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        protected void dgvResultsList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if ((dgvResultsList.Rows[e.RowIndex].Cells["FlgEdited"].Value == null || bool.Parse(dgvResultsList.Rows[e.RowIndex].Cells["FlgEdited"].Value.ToString()) == false) && (dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) != 0))
            {
                e.Cancel = true;
            }
        }

        protected void dgvResultsList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow previousRow = dgvResultsList.Rows[e.RowIndex];
            if (dgvResultsList.Rows[e.RowIndex].Cells["FlgEdited"].Value != null && bool.Parse(dgvResultsList.Rows[e.RowIndex].Cells["FlgEdited"].Value.ToString()))
            {
                previousRow.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) == 0)
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

        protected void dgvResultsList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoaded)
            {
                DataGridViewRow selectedRow = dgvResultsList.Rows[e.RowIndex];
                selectedRow.DefaultCellStyle.BackColor = Color.LightBlue;
                selectedRow.DefaultCellStyle.ForeColor = Color.White;
            }
        }

    }
}
