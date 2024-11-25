
using Generic.Service.DTO.Concrete;
using Generic.Service.Normal.Composition.Contract;
using Microsoft.IdentityModel.Tokens;
using PMIS.DTO.IndicatorValue;
using PMIS.DTO.IndicatorValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User;
using PMIS.Forms.Generic;
using PMIS.Models;
using PMIS.Services;
using PMIS.Services.Contract;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using Generic.Helper;
using PMIS.DTO.Indicator;
using System.Collections.Generic;
using PMIS.DTO.LookUpValue;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Generic.Base.Handler.Map.Concrete;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map;
using AutoMapper;
using Castle.Components.DictionaryAdapter;
using PMIS.Bases;
using PMIS.DTO.ClaimUserOnIndicator;
using System.Linq;
using Microsoft.AspNetCore.Components.RenderTree;

namespace PMIS.Forms
{
    public partial class IndicatorValueForm : Form
    {

        #region Variables
        private List<IndicatorValueAddRequestDto> lstAddRequest;
        private List<IndicatorValueEditRequestDto> lstEditRequest;
        private List<IndicatorValueDeleteRequestDto> lstLogicalDeleteRequest;
        private List<IndicatorValueDeleteRequestDto> lstPhysicalDeleteRequest;
        private List<IndicatorValueDeleteRequestDto> lstRecycleRequest;
        private IEnumerable<IndicatorValueSearchResponseDto> lstSearchResponse;
        private List<DateTime> lstDates;
        private BindingSource resultBindingSource;
        private IndicatorValueColumnsDto columns;
        private ILookUpValueService lookUpValueService;
        private IUserService userService;
        private IIndicatorValueService indicatorValueService;
        private IIndicatorService indicatorService;
        private IClaimUserOnIndicatorService claimUserOnIndicatorService;
        private DateTime dateTimeFrom;
        private DateTime dateTimeTo;
        private IEnumerable<IndicatorSearchResponseDto> indicators;
        IEnumerable<ClaimUserOnIndicatorSearchResponseDto> userClaims;
        private bool isLoaded = false;
        private TabControl tabControl;
        #endregion

        public IndicatorValueForm(IIndicatorValueService _IndicatorValueService, IIndicatorService _indicatorService, IClaimUserOnIndicatorService _claimUserOnIndicatorService, IUserService _userService, ILookUpValueService _lookUpValueService, TabControl _tabControl)
        {
            InitializeComponent();
            indicatorValueService = _IndicatorValueService;
            indicatorService = _indicatorService;
            lookUpValueService = _lookUpValueService;
            claimUserOnIndicatorService = _claimUserOnIndicatorService;
            userService = _userService;
            CustomInitialize();
            tabControl = _tabControl;
            AddNewTabPage(tabControl, this);
        }

        private void AddNewTabPage(TabControl tabControl, Form form)
        {
            TabPage tabPage = new TabPage();
            tabPage.Location = new Point(4, 24);
            tabPage.Name = "tabPageIndicatorValueForm";
            tabPage.Padding = new Padding(3);
            tabPage.Size = new Size(192, 0);
            tabPage.TabIndex = 0;
            tabPage.Text = "ورود مقادیر";
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
            tabControl.SelectedTab = tabPage;
        }

        private void NormalForm_Load(object sender, EventArgs e)
        {

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchEntity();
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                await EdiIndicatorValue();
                // await LogicalDeleteEntity();
                //await PhysicalDeleteEntity();
                // await RecycleEntity();
                await AddEntity();
                RefreshVisuals();

                MessageBox.Show("تغییرات با موفقیت اعمال شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در اعمال تغییرات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvResultsList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(sender, e);

        }

        private void dgvResultsList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
           // RowLeave(sender, e);
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
            isLoaded = false;
        }

        private void dgvResultsList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }



        private async void CustomInitialize()
        {
            // InitializeComponent();
            columns = new IndicatorValueColumnsDto();
            await columns.Initialize(lookUpValueService, indicatorService);
            lstLogicalDeleteRequest = new List<IndicatorValueDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<IndicatorValueDeleteRequestDto>();
            lstRecycleRequest = new List<IndicatorValueDeleteRequestDto>();
            userClaims = await GetClaims(GlobalVariable.userId);
            GenerateDgvFilterColumnsInitialize();
            GenerateDgvResultColumnsInitialize();
            FiltersInitialize();
            dgvResultsList.DataSource = resultBindingSource;
            //SearchEntity();
            btnSearch_Click(this, new EventArgs());
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
                }
            }
            PersianCalendar persianCalendar = new PersianCalendar();
            string year= persianCalendar.GetYear(persianCalendar.AddYears(DateTime.Now, 1)).ToString();
            
            dgvFiltersList.Rows[0].Cells["DateTimeFrom"].Value = Helper.Convert.ConvertGregorianToShamsi(DateTime.Now.AddDays(-14));
            dgvFiltersList.Rows[0].Cells["DateTimeTo"].Value =Helper.Convert.ConvertGregorianToShamsi( Helper.Convert.ConvertShamsiToGregorian( year + "/01/01").AddDays(-1) );
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

        public async Task SearchEntity()
        {
            isLoaded = false;
            lstAddRequest = new List<IndicatorValueAddRequestDto>();
            lstEditRequest = new List<IndicatorValueEditRequestDto>();
            lstLogicalDeleteRequest = new List<IndicatorValueDeleteRequestDto>();
            lstPhysicalDeleteRequest = new List<IndicatorValueDeleteRequestDto>();
            lstRecycleRequest = new List<IndicatorValueDeleteRequestDto>();
            lstSearchResponse = new List<IndicatorValueSearchResponseDto>();
            lstDates = new List<DateTime>();
            foreach (DataGridViewRow row in dgvFiltersList.Rows)
            {
                indicators = (IEnumerable<IndicatorSearchResponseDto>)((DataGridViewComboBoxColumn)dgvFiltersList.Columns["FkIndicatorId"]).DataSource;
                dateTimeFrom = row.Cells["DateTimeFrom"].Value != null ? Helper.Convert.ConvertShamsiToGregorian(row.Cells["DateTimeFrom"].Value.ToString()) : DateTime.Today.AddDays(-30);
                dateTimeTo = row.Cells["DateTimeTo"].Value != null ? Helper.Convert.ConvertShamsiToGregorian(row.Cells["DateTimeTo"].Value.ToString()) : DateTime.Today.AddDays(30);
                lstDates.AddRange(Helper.Convert.GetDatesBetween(dateTimeFrom, dateTimeTo));
                indicators = indicators.Where(i => i.Id != 0);
                if (row.Cells["FkIndicatorId"].Value != null && row.Cells["FkIndicatorId"].Value.ToString() != "0")
                {
                    indicators = indicators.Where(i => i.Id == int.Parse(row.Cells["FkIndicatorId"].Value.ToString()));
                }
            }
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
                        string columnName = column.Name;
                        FilterOperator filterOperator = FilterOperator.Contains;
                        if (column.Name == "DateTimeFrom")
                        {
                            cellValue = Helper.Convert.ConvertShamsiToGregorian(cellValue).ToString();

                            columnName = "DateTime";
                            filterOperator = FilterOperator.GreaterThanOrEqual;
                        }
                        else if (column.Name == "DateTimeTo")
                        {
                            cellValue = Helper.Convert.ConvertShamsiToGregorian(cellValue).ToString();
                            columnName = "DateTime";
                            filterOperator = FilterOperator.LessThanOrEqual;

                        }
                        else if (column is DataGridViewTextBoxColumn)
                        {
                            filterOperator = FilterOperator.Contains;
                        }
                        else
                        {
                            filterOperator = FilterOperator.Equals;
                        }
                        tempFilter.InternalFilters.Add(new GenericSearchFilterDto()
                        {
                            columnName = columnName,
                            value = cellValue,
                            LogicalOperator = column.Index == 0 ? LogicalOperator.Begin : LogicalOperator.And,
                            operation = filterOperator,
                            type = PhraseType.Condition,
                        });
                    }

                }
                filter.InternalFilters.Add(tempFilter);
            }
            searchRequest.filters.Add(filter);


            (bool isSuccess, lstSearchResponse) = await indicatorValueService.Search(searchRequest);
            lstSearchResponse = await GenerateRows2(lstSearchResponse);
            if (isSuccess)
            {
                if (lstSearchResponse.Count() == 0)
                {
                    dgvResultsList.DataSource = null;
                    MessageBox.Show("موردی یافت نشد!!!");
                }
                else
                {

                    // dgvResultsList.DataSource = new BindingList<IndicatorValueSearchResponseDto>(list.ToList());
                    var bindingList = new System.ComponentModel.BindingList<IndicatorValueSearchResponseDto>(lstSearchResponse.ToList());
                    resultBindingSource = new BindingSource(bindingList, null);
                    dgvResultsList.DataSource = resultBindingSource;
                }
            }
            else
            {
                MessageBox.Show("عملیات موفقیت‌آمیز نبود!!!");
            }
            RefreshVisuals();
            isLoaded = true;
        }
        #region Generate Row 1

        private async Task<IEnumerable<IndicatorValueSearchResponseDto>> GenerateRows(IEnumerable<IndicatorValueSearchResponseDto> _indicatorValueList)
        {
            try
            {
                List<IndicatorValueSearchResponseDto> result = _indicatorValueList.ToList();
                foreach (DataGridViewRow row in dgvFiltersList.Rows)
                {
                    IEnumerable<IndicatorSearchResponseDto> indicators = (IEnumerable<IndicatorSearchResponseDto>)((DataGridViewComboBoxColumn)dgvFiltersList.Columns["FkIndicatorId"]).DataSource;
                    DateTime dateTimeFrom = row.Cells["DateTimeFrom"].Value != null ? Helper.Convert.ConvertShamsiToGregorian(row.Cells["DateTimeFrom"].Value.ToString()) : DateTime.Today.AddDays(-30);
                    DateTime dateTimeTo = row.Cells["DateTimeTo"].Value != null ? Helper.Convert.ConvertShamsiToGregorian(row.Cells["DateTimeTo"].Value.ToString()) : DateTime.Today.AddDays(+30);
                    if (row.Cells["FkIndicatorId"].Value != null && row.Cells["FkIndicatorId"].Value != "0")
                    {
                        indicators = indicators.Where(i => i.Id == int.Parse(row.Cells["FkIndicatorId"].Value.ToString()));
                    }
                    for (DateTime date = dateTimeFrom; date <= dateTimeTo; date = date.AddDays(1))
                    {
                        foreach (IndicatorSearchResponseDto indicator in indicators)
                        {
                            IndicatorValueSearchResponseDto indicatorValue = new IndicatorValueSearchResponseDto()
                            {
                                FkIndicatorId = indicator.Id,
                                VrtLkpFormId = indicator.FkLkpFormId,
                                VrtLkpPeriodId = indicator.FkLkpPeriodId,
                                DateTime = date,
                            };
                            IEnumerable<IndicatorValueSearchResponseDto> blankIndicatorValues = await GenerateRowsForDate(indicatorValue, indicator);
                            blankIndicatorValues = await GenerateRowsForValueType(blankIndicatorValues, indicator);
                            blankIndicatorValues = blankIndicatorValues.Except(_indicatorValueList);
                            result.AddRange(blankIndicatorValues);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task<List<IndicatorValueSearchResponseDto>> GenerateRows2(IEnumerable<IndicatorValueSearchResponseDto> _indicatorValueList)
        {
            try
            {


                List<IndicatorValueSearchResponseDto> result = _indicatorValueList.ToList();


                //var blankDate = lstDates
                //                   .Where(date => !_indicatorValueList.Any(sr => sr.DateTime.Date == date.Date))
                //                   .ToList();
                //for (DateTime date = dateTimeFrom; date <= dateTimeTo; date = date.AddDays(1))
                //{
                var datesToRemove = new List<DateTime>();
                foreach (var date in lstDates)
                {
                    bool flgGenerated = false;
                    foreach (IndicatorSearchResponseDto indicator in indicators)
                    {

                        IndicatorValueSearchResponseDto indicatorValue = new IndicatorValueSearchResponseDto()
                        {
                            FkIndicatorId = indicator.Id,
                            VrtLkpFormId = indicator.FkLkpFormId,
                            VrtLkpPeriodId = indicator.FkLkpPeriodId,
                            DateTime = date,
                            shamsiDateTime = Helper.Convert.ConvertGregorianToShamsi(date)
                        };
                        List<IndicatorValueSearchResponseDto> blankIndicatorValues = (await GenerateRowsForDate(indicatorValue, indicator)).ToList();
                        blankIndicatorValues = (await GenerateRowsForValueType(blankIndicatorValues, indicator)).ToList();

                        blankIndicatorValues = blankIndicatorValues.Where(x => HasAccess("Add", x.FkLkpValueTypeInfo.Value, x.FkIndicatorId).Result == true).ToList();
                        blankIndicatorValues.RemoveAll(x => result.Any(r => r.DateTime == x.DateTime && r.FkIndicatorId == x.FkIndicatorId && r.FkLkpShiftId == x.FkLkpShiftId && r.FkLkpValueTypeId == x.FkLkpValueTypeId));




                        if (blankIndicatorValues.Count() > 0)
                        {
                            result.AddRange(blankIndicatorValues);
                            datesToRemove.Add(date);
                            flgGenerated = true;
                        }
                    }
                    if (flgGenerated)
                        break;
                }

                foreach (var dateToRemove in datesToRemove)
                {
                    lstDates.Remove(dateToRemove);
                }

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<IEnumerable<IndicatorValueSearchResponseDto>> GenerateRowsForValueType(IEnumerable<IndicatorValueSearchResponseDto> indicatorValues, IndicatorSearchResponseDto indicator)
        {
            try
            {
                List<IndicatorValueSearchResponseDto> result = new List<IndicatorValueSearchResponseDto>();
                IEnumerable<LookUpValueShortInfoDto> valueTypes = (IEnumerable<LookUpValueShortInfoDto>)((DataGridViewComboBoxColumn)dgvFiltersList.Columns["FkLkpValueTypeId"]).DataSource;
                AbstractGenericMapHandler mapper = GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto);
                IndicatorValueSearchResponseDto temp1;
                IndicatorValueSearchResponseDto temp2;
                IndicatorValueSearchResponseDto temp3;
                foreach (IndicatorValueSearchResponseDto indicatorValue in indicatorValues)
                {
                    switch (indicator.FkLkpManualityInfo.Value)
                    {
                        case "Nothing":
                            break;
                        case "Foresight":
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Foresight").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Foresight").Single();
                            temp1 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp1);
                            break;
                        case "Target":
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Target").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Target").Single();
                            temp1 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp1);
                            break;
                        case "Performance":
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Performance").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Performance").Single();
                            temp1 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp1);
                            break;
                        case "FT":
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Foresight").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Foresight").Single();
                            temp1 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp1);
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Target").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Target").Single();
                            temp2 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp2);
                            break;
                        case "FP":
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Foresight").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Foresight").Single();
                            temp1 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp1);
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Performance").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Performance").Single();
                            temp2 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp2);
                            break;
                        case "TP":
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Target").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Target").Single();
                            temp1 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp1);
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Performance").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Performance").Single();
                            temp2 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp2);
                            break;
                        case "FTP":
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Foresight").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Foresight").Single();
                            temp1 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp1);
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Target").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Target").Single();
                            temp2 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp2);
                            indicatorValue.FkLkpValueTypeId = valueTypes.Where(v => v.Value == "Performance").Single().Id;
                            indicatorValue.FkLkpValueTypeInfo = valueTypes.Where(v => v.Value == "Performance").Single();
                            temp3 = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp3);
                            break;
                        default:
                            break;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<IEnumerable<IndicatorValueSearchResponseDto>> GenerateRowsForDate(IndicatorValueSearchResponseDto indicatorValue, IndicatorSearchResponseDto indicator)
        {
            try
            {
                List<IndicatorValueSearchResponseDto> result = new List<IndicatorValueSearchResponseDto>();
                IEnumerable<LookUpValueShortInfoDto> shifts = (IEnumerable<LookUpValueShortInfoDto>)((DataGridViewComboBoxColumn)dgvFiltersList.Columns["FkLkpShiftId"]).DataSource;
                AbstractGenericMapHandler mapper = GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto);
                IndicatorValueSearchResponseDto temp;
                PersianCalendar persianCalendar = new PersianCalendar();
                switch (indicator.FkLkpPeriodInfo.Value)
                {
                    case "Annually":
                        if (persianCalendar.GetDayOfYear(indicatorValue.DateTime) == 1)
                        {
                            indicatorValue.FkLkpShiftId = shifts.Where(s => s.Value == "0").SingleOrDefault().Id;
                            temp = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp);
                            break;
                        }
                        break;
                    case "Biannual":
                        if (persianCalendar.GetDayOfYear(indicatorValue.DateTime) == 1 || persianCalendar.GetDayOfYear(indicatorValue.DateTime) == 187)
                        {
                            indicatorValue.FkLkpShiftId = shifts.Where(s => s.Value == "0").SingleOrDefault().Id;
                            temp = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp);
                            break;
                        }
                        break;
                    case "Seasonal":
                        if (persianCalendar.GetDayOfYear(indicatorValue.DateTime) == 1 || persianCalendar.GetDayOfYear(indicatorValue.DateTime) == 94 || persianCalendar.GetDayOfYear(indicatorValue.DateTime) == 187 || persianCalendar.GetDayOfYear(indicatorValue.DateTime) == 277)
                        {
                            indicatorValue.FkLkpShiftId = shifts.Where(s => s.Value == "0").SingleOrDefault().Id;
                            temp = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp);
                            break;
                        }
                        break;
                    case "Monthly":
                        if (persianCalendar.GetDayOfMonth(indicatorValue.DateTime) == 1)
                        {
                            indicatorValue.FkLkpShiftId = shifts.Where(s => s.Value == "0").SingleOrDefault().Id;
                            temp = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp);
                            break;
                        }
                        break;
                    case "Weekly":
                        if (persianCalendar.GetDayOfWeek(indicatorValue.DateTime) == DayOfWeek.Saturday)
                        {
                            indicatorValue.FkLkpShiftId = shifts.Where(s => s.Value == "0").SingleOrDefault().Id;
                            temp = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp);
                            break;
                        }
                        break;
                    case "Dayly":
                        indicatorValue.FkLkpShiftId = shifts.Where(s => s.Value == "0").SingleOrDefault().Id;
                        temp = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                        result.Add(temp);
                        break;
                    case "Shiftly":
                        foreach (LookUpValueShortInfoDto shift in shifts.Where(s => s.Id != 0 && s.Value != "0"))
                        {
                            indicatorValue.FkLkpShiftId = shift.Id;
                            temp = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp);
                        }
                        break;
                    case "Hourly":
                        for (int i = 0; i < 24; i++)
                        {
                            indicatorValue.DateTime.AddHours(i);
                            indicatorValue.FkLkpShiftId = shifts.Where(s => s.Value == "0").SingleOrDefault().Id;
                            temp = await mapper.Map<IndicatorValueSearchResponseDto, IndicatorValueSearchResponseDto>(indicatorValue);
                            result.Add(temp);
                        }
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        public async Task AddEntity()
        {
            try
            {
                lstAddRequest = new List<IndicatorValueAddRequestDto>();

                foreach (DataGridViewRow row in dgvResultsList.Rows)
                {
                    try
                    {
                        if (row.IsNewRow || row.Cells["Value"].Value == null)
                            continue;
                        var tempLst = ((LookUpValueShortInfoDto[])(((DataGridViewComboBoxCell)row.Cells["FkLkpValueTypeId"]).DataSource)).ToList();
                        string lkpValueType = tempLst.Where(l => l.Id == int.Parse(row.Cells["FkLkpValueTypeId"].Value.ToString())).SingleOrDefault().Value.ToString();
                        bool hasPermission = await HasAccess("Add", lkpValueType, int.Parse(row.Cells["FkIndicatorId"].Value.ToString()));
                        if (!hasPermission)
                        {
                            //MessageBox.Show("دسترسی برای ویرایش این شاخص را ندارید!!!");
                            continue;
                        }
                        if ((row.Cells["Id"].Value == null && row.Index + 1 < dgvResultsList.Rows.Count) || (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) == 0))
                        {
                            IndicatorValueAddRequestDto addRequest = new IndicatorValueAddRequestDto();

                            addRequest = AddMaping(row);
                            lstAddRequest.Add(addRequest);
                        }
                    }
                    catch (Exception) { }
                }

                //(bool isSuccess, IEnumerable<IndicatorValueAddResponseDto> list) = await indicatorValueService.AddGroup(lstAddRequest);
                bool isSuccess = await indicatorValueService.AddRange(lstAddRequest);

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

        public async Task EdiIndicatorValue()
        {
            try
            {
                lstEditRequest = new List<IndicatorValueEditRequestDto>();

                foreach (DataGridViewRow row in dgvResultsList.Rows)
                {
                    try
                    {
                        if (row.IsNewRow || row.Cells["Value"].Value == null)
                            continue;
                        var tempLst = ((LookUpValueShortInfoDto[])(((DataGridViewComboBoxCell)row.Cells["FkLkpValueTypeId"]).DataSource)).ToList();

                        string lkpValueType = tempLst.Where(l => l.Id == int.Parse(row.Cells["FkLkpValueTypeId"].Value.ToString())).SingleOrDefault().Value.ToString();
                        bool hasPermission = await HasAccess("Edit", lkpValueType, int.Parse(row.Cells["FkIndicatorId"].Value.ToString()));
                        if (!hasPermission)
                        {
                            //MessageBox.Show("دسترسی برای ویرایش این شاخص را ندارید!!!");
                            continue;
                        }
                        if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0 && bool.Parse((row.Cells["FlgEdited"].Value ?? false).ToString()) == true)
                        {
                            IndicatorValueEditRequestDto editRequest = new IndicatorValueEditRequestDto();

                            editRequest = EditMaping(row);
                            lstEditRequest.Add(editRequest);
                        }
                    }
                    catch (Exception) { }
                }

                //(bool isSuccess, IEnumerable<IndicatorValueEditResponseDto> list) = await indicatorValueService.EditGroup(lstEditRequest);
                bool isSuccess = await indicatorValueService.EditRange(lstEditRequest);
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
                //(bool isSuccess, IEnumerable<IndicatorValueDeleteResponseDto> list) = await indicatorValueService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorValueService.LogicalDeleteRange(lstLogicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstLogicalDeleteRequest = new List<IndicatorValueDeleteRequestDto>();
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

                //(bool isSuccess, IEnumerable<IndicatorValueDeleteResponseDto> list) = await indicatorValueService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorValueService.RecycleRange(lstRecycleRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstRecycleRequest = new List<IndicatorValueDeleteRequestDto>();
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

                //(bool isSuccess, IEnumerable<IndicatorValueDeleteResponseDto> list) = await indicatorValueService.LogicalDeleteGroup(lstDeleteRequest);
                bool isSuccess = await indicatorValueService.PhysicalDeleteRange(lstPhysicalDeleteRequest);
                if (isSuccess)
                {
                    // MessageBox.Show("عملیات موفقیت‌آمیز بود!!!");
                    lstPhysicalDeleteRequest = new List<IndicatorValueDeleteRequestDto>();
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

        public async void RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvResultsList.Rows[e.RowIndex].IsNewRow)
            {
                await Task.Run(async () =>
                {
                    // isLoaded = false;
                    //  dgvResultsList.RowEnter -= RowEnter;
                    lstSearchResponse = await GenerateRows2(lstSearchResponse);
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        var bindingList = new System.ComponentModel.BindingList<IndicatorValueSearchResponseDto>(lstSearchResponse.ToList());
                        resultBindingSource.DataSource = bindingList;
                        dgvResultsList.DataSource = resultBindingSource;
                    });
                    // dgvResultsList.RowEnter += RowEnter;



                });
            }
            else if (isLoaded)
            {

                DataGridViewRow selectedRow = dgvResultsList.Rows[e.RowIndex];
                selectedRow.DefaultCellStyle.BackColor = Color.LightBlue;
                selectedRow.DefaultCellStyle.ForeColor = Color.White;
            }

        }

        public void RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            isLoaded = true;
            DataGridViewRow previousRow = dgvResultsList.Rows[e.RowIndex];
            //Edit Style
            if (dgvResultsList.Rows[e.RowIndex].Cells["FlgEdited"].Value != null && bool.Parse(dgvResultsList.Rows[e.RowIndex].Cells["FlgEdited"].Value.ToString()))
            {
                previousRow.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;
            }
            // Add Style
            else if (dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value != null && int.Parse(dgvResultsList.Rows[e.RowIndex].Cells["Id"].Value.ToString()) == 0 && dgvResultsList.Rows[e.RowIndex].Cells["Value"].Value != null)
            {
                previousRow.DefaultCellStyle.BackColor = Color.Honeydew;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;

            }           
            //None Style
            else
            {
                previousRow.DefaultCellStyle.BackColor = Color.White;
                previousRow.DefaultCellStyle.ForeColor = Color.Black;
            }
           
        }

        public async void CellContentClick(int rowIndex, int columnIndex)
        {
            if (rowIndex == -1)
                return;
            if (dgvResultsList.Rows[rowIndex].IsNewRow)
                return;
            var row = dgvResultsList.Rows[rowIndex];
            if (dgvResultsList.Columns[columnIndex].Name == "Edit" && rowIndex >= 0 && row.Cells["Id"].Value.ToString() != "0")
            {
                string operationMode = row.Cells["Id"].Value.ToString() == "0" ? "Add" : "Edit";
                var tempLst = ((LookUpValueShortInfoDto[])(((DataGridViewComboBoxCell)row.Cells["FkLkpValueTypeId"]).DataSource)).ToList();
                string lkpValueType = tempLst.Where(l => l.Id == int.Parse(row.Cells["FkLkpValueTypeId"].Value.ToString())).SingleOrDefault().Value.ToString();
                bool hasPermission = await HasAccess(operationMode, lkpValueType, int.Parse(row.Cells["FkIndicatorId"].Value.ToString()));
                if (!hasPermission)
                {
                    MessageBox.Show("دسترسی برای ویرایش این شاخص را ندارید!!!");
                    return;
                }
                dgvResultsList.Rows[rowIndex].Cells["FlgEdited"].Value = true;
                dgvResultsList.Rows[rowIndex].Cells["Value"].ReadOnly = false;
            }
            else if (dgvResultsList.Columns[columnIndex].Name == "LogicalDelete" && rowIndex >= 0)
            {
                if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0)
                {
                    IndicatorValueDeleteRequestDto deleteRequest = new IndicatorValueDeleteRequestDto();
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
                    IndicatorValueDeleteRequestDto deleteRequest = new IndicatorValueDeleteRequestDto();
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
                    IndicatorValueDeleteRequestDto deleteRequest = new IndicatorValueDeleteRequestDto();
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
            //else if (dgvResultsList.Columns[columnIndex].Name == "Claims" && rowIndex >= 0)
            //{
            //    if (row.Cells["Id"].Value != null && int.Parse(row.Cells["Id"].Value.ToString()) != 0)
            //    {
            //        int tempId = int.Parse(row.Cells["Id"].Value.ToString());
            //        ClaimUserOnIndicatorValueForm frm = new ClaimUserOnIndicatorValueForm(claimUserOnIndicatorService, userService, IndicatorValueService, lookUpValueService, tempId);
            //        frm.Show();
            //    }
            //}
        }

        private async Task<IEnumerable<ClaimUserOnIndicatorSearchResponseDto>> GetClaims(int _userId)
        {
            try
            {

                (bool IsSuccess, IEnumerable<ClaimUserOnIndicatorSearchResponseDto> userClaims) = await claimUserOnIndicatorService.Search
                        (
                        new GenericSearchRequestDto()
                        {
                            filters = new List<GenericSearchFilterDto>()
                            {
                            new GenericSearchFilterDto()
                            {
                                columnName = "FkUserId",
                                value = _userId.ToString(),
                                LogicalOperator = LogicalOperator.Begin,
                                operation = FilterOperator.Equals,
                                type = PhraseType.Condition
                            }
                            }
                        }
                        );
                if (IsSuccess)
                {
                    return userClaims;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private Task<bool> HasAccess(string _operationMode, string _lkpValueType, int _indicatorId)
        {
            try
            {
                if (userClaims.Count() > 0)
                {
                    if (userClaims.Where(c => c.FkIndicatorId == _indicatorId && c.FkLkpClaimUserOnIndicatorInfo.Value == (_operationMode + _lkpValueType)).Count() > 0)
                        return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            catch (Exception ex)
            {

                throw;
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

        private IndicatorValueAddRequestDto AddMaping(DataGridViewRow row)
        {
            try
            {
                IndicatorValueAddRequestDto addRequest = new IndicatorValueAddRequestDto();

                var entityFields = addRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (DataGridViewColumn column in dgvResultsList.Columns)
                {
                    var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Equals("<" + column.Name + ">k__BackingField"));
                    if (fieldInfo != null)
                    {
                        //fieldInfo.SetValue(addRequest, row.Cells[column.Name].Value);
                        fieldInfo.SetValue(addRequest, Convert.ChangeType(row.Cells[column.Name].Value, fieldInfo.FieldType));
                    }
                }
                return addRequest;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private IndicatorValueEditRequestDto EditMaping(DataGridViewRow row)
        {
            try
            {
                IndicatorValueEditRequestDto editRequest = new IndicatorValueEditRequestDto();
                var entityFields = editRequest.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (DataGridViewColumn column in dgvResultsList.Columns)
                {
                    var fieldInfo = entityFields.FirstOrDefault(f => f.Name.Equals("<" + column.Name + ">k__BackingField"));
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

        private void dgvResultsList_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            RowValidated(sender,e);

        }

        private void dgvResultsList_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }
    }


}
