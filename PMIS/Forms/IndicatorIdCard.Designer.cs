using System.Windows.Forms;

namespace PMIS.Forms
{
    partial class IndicatorIdCard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            btnSearch = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            dgvIndicatorList = new DataGridView();
            dgvtbId = new DataGridViewTextBoxColumn();
            dgvtbCode = new DataGridViewTextBoxColumn();
            dgvtbTitle = new DataGridViewTextBoxColumn();
            dgvcbLkpForm = new DataGridViewComboBoxColumn();
            dgvcbLkpManuality = new DataGridViewComboBoxColumn();
            dgvcbLkpUnit = new DataGridViewComboBoxColumn();
            dgvcbLkpPeriod = new DataGridViewComboBoxColumn();
            dgvcbLkpMeasure = new DataGridViewComboBoxColumn();
            dgvcbLkpDesirability = new DataGridViewComboBoxColumn();
            dgvtbFormula = new DataGridViewTextBoxColumn();
            dgvtbDescription = new DataGridViewTextBoxColumn();
            dgvchFlgLogicalDelete = new DataGridViewCheckBoxColumn();

            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIndicatorList).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnDelete);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 100);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvIndicatorList);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 100);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(914, 500);
            panel2.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Left;
            btnSearch.Location = new Point(0, 0);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(86, 100);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "جستجو";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnAdd
            // 
            btnAdd.Dock = DockStyle.Left;
            btnAdd.Location = new Point(75, 0);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 74);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "افزودن";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Dock = DockStyle.Left;
            btnEdit.Location = new Point(75, 0);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 74);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "ویرایش";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Dock = DockStyle.Left;
            btnDelete.Location = new Point(75, 0);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 74);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "حذف";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // dgvIndicatorList
            // 
            dgvIndicatorList.AutoGenerateColumns = false;
            dgvIndicatorList.AllowUserToAddRows = true;
            dgvIndicatorList.AllowUserToOrderColumns = true;
            dgvIndicatorList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIndicatorList.Dock = DockStyle.Fill;
            dgvIndicatorList.Location = new Point(0, 0);
            dgvIndicatorList.Margin = new Padding(3, 4, 3, 4);
            dgvIndicatorList.Name = "dgvIndicatorList";
            dgvIndicatorList.RowHeadersWidth = 51;
            dgvIndicatorList.Size = new Size(914, 500);
            dgvIndicatorList.TabIndex = 0;
            // 
            // dgvtbId 
            // 
            dgvtbId.HeaderText = "شناسه";
            dgvtbId.Name = "Id";
            dgvtbId.DataPropertyName = "Id";
            dgvtbId.Visible = false;
            dgvtbId.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvtbId);
            // 
            // dgvtbCode 
            // 
            dgvtbCode.HeaderText = "کد";
            dgvtbCode.Name = "Code";
            dgvtbCode.DataPropertyName = "Code";
            dgvtbCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvtbCode);
            //
            // dgvtbTitle
            // 
            dgvtbTitle.HeaderText = "عنوان";
            dgvtbTitle.Name = "Title";
            dgvtbTitle.DataPropertyName = "Title";
            dgvtbTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvtbTitle);
            // dgvcbLkpForm 
            // 
            dgvcbLkpForm.HeaderText = "فرم مربوطه";
            dgvcbLkpForm.Name = "FkLkpFormId";
            dgvcbLkpForm.DataPropertyName = "FkLkpFormId";
            dgvcbLkpForm.DisplayMember = "Display";
            dgvcbLkpForm.ValueMember = "Id";
            dgvcbLkpForm.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvcbLkpForm);
            // 
            // dgvcbLkpManuality 
            // 
            dgvcbLkpManuality.HeaderText = "دستی/اتوماتیک";
            dgvcbLkpManuality.Name = "FkLkpManualityId";
            dgvcbLkpManuality.DataPropertyName = "FkLkpManualityId";
            dgvcbLkpManuality.DisplayMember = "Display";
            dgvcbLkpManuality.ValueMember = "Id";
            dgvcbLkpManuality.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvcbLkpManuality);
            // 
            // dgvcbLkpUnit 
            // 
            dgvcbLkpUnit.HeaderText = "واحد عملیاتی";
            dgvcbLkpUnit.Name = "FkLkpUnitId";
            dgvcbLkpUnit.DataPropertyName = "FkLkpUnitId";
            dgvcbLkpUnit.DisplayMember = "Display";
            dgvcbLkpUnit.ValueMember = "Id";
            dgvcbLkpUnit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvcbLkpUnit);
            // 
            // dgvcbLkpPeriod 
            // 
            dgvcbLkpPeriod.HeaderText = "دوره زمانی";
            dgvcbLkpPeriod.Name = "FkLkpPeriodId";
            dgvcbLkpPeriod.DataPropertyName = "FkLkpPeriodId";
            dgvcbLkpPeriod.DisplayMember = "Display";
            dgvcbLkpPeriod.ValueMember = "Id";
            dgvcbLkpPeriod.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvcbLkpPeriod);
            // 
            // dgvcbLkpMeasure 
            // 
            dgvcbLkpMeasure.HeaderText = "واحد اندازه‌گیری";
            dgvcbLkpMeasure.Name = "FkLkpMeasureId";
            dgvcbLkpMeasure.DataPropertyName = "FkLkpMeasureId";
            dgvcbLkpMeasure.DisplayMember = "Display";
            dgvcbLkpMeasure.ValueMember = "Id";
            dgvcbLkpMeasure.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvcbLkpMeasure);
            // 
            // dgvcbLkpDesirability 
            // 
            dgvcbLkpDesirability.HeaderText = "مطلوبیت";
            dgvcbLkpDesirability.Name = "FkLkpDesirabilityId";
            dgvcbLkpDesirability.DataPropertyName = "FkLkpDesirabilityId";
            dgvcbLkpDesirability.DisplayMember = "Display";
            dgvcbLkpDesirability.ValueMember = "Id";
            dgvcbLkpDesirability.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvcbLkpDesirability);
            // 
            // dgvtbFormula 
            // 
            dgvtbFormula.HeaderText = "فرمول";
            dgvtbFormula.Name = "Formula";
            dgvtbFormula.DataPropertyName = "Formula";
            dgvtbFormula.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvtbFormula);// 
            //
            // dgvtbDescription
            // 
            dgvtbDescription.HeaderText = "توضیحات";
            dgvtbDescription.Name = "Description";
            dgvtbDescription.DataPropertyName = "Description";
            dgvtbDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvtbDescription);
            //
            // dgvchDelete
            // 
            dgvchFlgLogicalDelete.HeaderText = "حذف";
            dgvchFlgLogicalDelete.Name = "FlgLogicalDelete";
            dgvchFlgLogicalDelete.DataPropertyName = "FlgLogicalDelete";
            dgvchFlgLogicalDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvIndicatorList.Columns.Add(dgvchFlgLogicalDelete);
            // 
            // IndicatorIdCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "IndicatorIdCard";
            Text = "IndicatorIdCard";
            Load += IndicatorIdCard_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvIndicatorList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button btnSearch;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private DataGridView dgvIndicatorList;
        private DataGridViewTextBoxColumn dgvtbId;
        private DataGridViewTextBoxColumn dgvtbCode;
        private DataGridViewTextBoxColumn dgvtbTitle;
        private DataGridViewComboBoxColumn dgvcbLkpForm;
        private DataGridViewComboBoxColumn dgvcbLkpManuality;
        private DataGridViewComboBoxColumn dgvcbLkpUnit;
        private DataGridViewComboBoxColumn dgvcbLkpPeriod;
        private DataGridViewComboBoxColumn dgvcbLkpMeasure;
        private DataGridViewComboBoxColumn dgvcbLkpDesirability;
        private DataGridViewTextBoxColumn dgvtbFormula;
        private DataGridViewTextBoxColumn dgvtbDescription;
        private DataGridViewCheckBoxColumn dgvchFlgLogicalDelete;

    }
}