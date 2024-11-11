using Generic.Base.Handler.Map;
using Generic.Service.DTO;
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

namespace PMIS.Forms
{
    public partial class IndicatorIdCard : Form
    {
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
            IEnumerable<LookUpDestinationSearchResponseDto> lstLookUpDestination = await lookUpValueService.GetList("Indicator");
            GenericSearchRequestDto searchRequest = new GenericSearchRequestDto();
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
                Visible = false,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "کد",
                Name = "Code",
                DataPropertyName = "Code",
                Visible = true,
                Frozen = true
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "عنوان",
                Name = "Title",
                DataPropertyName = "Title",
                Visible = true,
                Frozen = true
            });
            dgvIndicatorList.Columns.Add(new DataGridViewComboBoxColumn()
            {
                HeaderText = "فرم مربوطه",
                Name = "FkLkpFormId",
                DataPropertyName = "FkLkpFormId",
                DisplayMember = "Display",
                ValueMember = "Id",
                DataSource = await lookUpValueService.GetList(lstLookUpDestination, "FkLkpFormID", "LkpForm"),
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
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "فرمول",
                Name = "Formula",
                DataPropertyName = "Formula",
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "توضیحات",
                Name = "Description",
                DataPropertyName = "Description",
                Visible = true,
            });
            dgvIndicatorList.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                HeaderText = "حذف",
                Name = "FlgLogicalDelete",
                DataPropertyName = "FlgLogicalDelete",
                Visible = true,
            });
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
                    dgvIndicatorList.DataSource = list;
                }
            }
            else
            {
                MessageBox.Show("عملیات موفقیت‌آمیز نبود!!!");
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            await AddIndicator();
        }

        private async Task AddIndicator()
        {
            try
            {
                List<IndicatorAddRequestDto> addRequest = new List<IndicatorAddRequestDto>();
                foreach (DataGridViewRow row in dgvIndicatorList.Rows)
                {
                    if (row.Cells["Id"].Value == null)
                    {
                        if (row.IsNewRow) continue;
                        addRequest.Add(new IndicatorAddRequestDto()
                        {
                            Code = row.Cells["Code"].Value?.ToString(),
                            Title = row.Cells["Title"].Value?.ToString(),
                            FkLkpFormId = int.Parse(row.Cells["FkLkpFormId"].Value?.ToString()),
                            FkLkpManualityId = int.Parse(row.Cells["FkLkpManualityId"].Value?.ToString()),
                            FkLkpUnitId = int.Parse(row.Cells["FkLkpUnitId"].Value?.ToString()),
                            FkLkpPeriodId = int.Parse(row.Cells["FkLkpPeriodId"].Value?.ToString()),
                            FkLkpMeasureId = int.Parse(row.Cells["FkLkpMeasureId"].Value?.ToString()),
                            FkLkpDesirabilityId = int.Parse(row.Cells["FkLkpDesirabilityId"].Value?.ToString()),
                            Formula = row.Cells["Formula"].Value?.ToString(),
                            Description = row.Cells["Description"].Value?.ToString()
                        });
                    }
                }
                (bool isSuccess, IEnumerable<IndicatorAddResponseDto> list) = await indicatorService.AddGroup(addRequest);

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

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            await EditIndicator();
        }

        private async Task EditIndicator()
        {
            try
            {
                List<IndicatorEditRequestDto> editRequest = new List<IndicatorEditRequestDto>();
                foreach (DataGridViewRow row in dgvIndicatorList.Rows)
                {
                    if (int.Parse(row.Cells["Id"].Value?.ToString()) != null)
                    {
                        editRequest.Add(new IndicatorEditRequestDto()
                        {
                            Id = int.Parse(row.Cells["Id"].Value?.ToString()),
                            Code = row.Cells["Code"].Value?.ToString(),
                            Title = row.Cells["Title"].Value?.ToString(),
                            FkLkpFormId = int.Parse(row.Cells["FkLkpFormId"].Value?.ToString()),
                            FkLkpManualityId = int.Parse(row.Cells["FkLkpManualityId"].Value?.ToString()),
                            FkLkpUnitId = int.Parse(row.Cells["FkLkpUnitId"].Value?.ToString()),
                            FkLkpPeriodId = int.Parse(row.Cells["FkLkpPeriodId"].Value?.ToString()),
                            FkLkpMeasureId = int.Parse(row.Cells["FkLkpMeasureId"].Value?.ToString()),
                            FkLkpDesirabilityId = int.Parse(row.Cells["FkLkpDesirabilityId"].Value?.ToString()),
                            Formula = row.Cells["Formula"].Value?.ToString(),
                            Description = row.Cells["Description"].Value?.ToString()
                        });
                    }
                }
                (bool isSuccess, IEnumerable<IndicatorEditResponseDto> list) = await indicatorService.EditGroup(editRequest);

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

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            await DeleteIndicator();
        }

        private async Task DeleteIndicator()
        {
            try
            {
                List<IndicatorDeleteRequestDto> deleteRequest = new List<IndicatorDeleteRequestDto>();
                List<IndicatorDeleteRequestDto> recycleRequest = new List<IndicatorDeleteRequestDto>();
                foreach (DataGridViewRow row in dgvIndicatorList.Rows)
                {
                    if (int.Parse(row.Cells["Id"].Value?.ToString()) != null)
                    {
                        if (bool.Parse(row.Cells["FlgLogicalDelete"].Value?.ToString()))
                        {
                            deleteRequest.Add(new IndicatorDeleteRequestDto()
                            {
                                Id = int.Parse(row.Cells["Id"].Value?.ToString())
                            });
                        }
                        else
                        {
                            recycleRequest.Add(new IndicatorDeleteRequestDto()
                            {
                                Id = int.Parse(row.Cells["Id"].Value?.ToString())
                            });
                        }
                    }
                }
                (bool isSuccess, IEnumerable<IndicatorDeleteResponseDto> list) = await indicatorService.LogicalDeleteGroup(deleteRequest);
                (bool isSuccess2, IEnumerable<IndicatorDeleteResponseDto> list2) = await indicatorService.RecycleGroup(recycleRequest);

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
        
        private void dgvIndicatorList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvIndicatorList.Rows[e.RowIndex].Cells["RowNumber"].Value = (e.RowIndex + 1).ToString();
        }
    }
}
