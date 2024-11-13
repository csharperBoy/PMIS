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
            panel3 = new Panel();
            chbRecycle = new CheckBox();
            cbLkpForm = new ComboBox();
            label3 = new Label();
            txtTitle = new TextBox();
            label2 = new Label();
            txtCode = new TextBox();
            label1 = new Label();
            btnSearch = new Button();
            btnAdd = new Button();
            panel2 = new Panel();
            dgvIndicatorList = new DataGridView();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIndicatorList).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnAdd);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(4);
            panel1.Size = new Size(1264, 44);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(chbRecycle);
            panel3.Controls.Add(cbLkpForm);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(txtTitle);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(txtCode);
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(182, 4);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(0, 8, 0, 0);
            panel3.Size = new Size(1078, 36);
            panel3.TabIndex = 2;
            // 
            // chbRecycle
            // 
            chbRecycle.AutoSize = true;
            chbRecycle.Dock = DockStyle.Right;
            chbRecycle.Location = new Point(587, 8);
            chbRecycle.Margin = new Padding(3, 2, 3, 2);
            chbRecycle.Name = "chbRecycle";
            chbRecycle.Size = new Size(60, 28);
            chbRecycle.TabIndex = 6;
            chbRecycle.Text = "بازیابی";
            chbRecycle.UseVisualStyleBackColor = true;
            // 
            // cbLkpForm
            // 
            cbLkpForm.Dock = DockStyle.Right;
            cbLkpForm.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLkpForm.FormattingEnabled = true;
            cbLkpForm.Location = new Point(647, 8);
            cbLkpForm.Margin = new Padding(3, 2, 3, 2);
            cbLkpForm.Name = "cbLkpForm";
            cbLkpForm.Size = new Size(133, 23);
            cbLkpForm.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Right;
            label3.Location = new Point(780, 8);
            label3.Name = "label3";
            label3.Size = new Size(24, 15);
            label3.TabIndex = 4;
            label3.Text = "فرم";
            // 
            // txtTitle
            // 
            txtTitle.Dock = DockStyle.Right;
            txtTitle.Location = new Point(804, 8);
            txtTitle.Margin = new Padding(3, 2, 3, 2);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(110, 23);
            txtTitle.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Right;
            label2.Location = new Point(914, 8);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 2;
            label2.Text = "عنوان";
            // 
            // txtCode
            // 
            txtCode.Dock = DockStyle.Right;
            txtCode.Location = new Point(949, 8);
            txtCode.Margin = new Padding(3, 2, 3, 2);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(110, 23);
            txtCode.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Right;
            label1.Location = new Point(1059, 8);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 0;
            label1.Text = "کد";
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Left;
            btnSearch.Location = new Point(107, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 36);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "جستجو";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnAdd
            // 
            btnAdd.Dock = DockStyle.Left;
            btnAdd.Location = new Point(4, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(103, 36);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "اعمال تغییرات";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnApply_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvIndicatorList);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 44);
            panel2.Name = "panel2";
            panel2.Size = new Size(1264, 685);
            panel2.TabIndex = 1;
            // 
            // dgvIndicatorList
            // 
            dgvIndicatorList.AllowUserToOrderColumns = true;
            dgvIndicatorList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvIndicatorList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIndicatorList.Dock = DockStyle.Fill;
            dgvIndicatorList.Location = new Point(0, 0);
            dgvIndicatorList.Name = "dgvIndicatorList";
            dgvIndicatorList.RowHeadersWidth = 51;
            dgvIndicatorList.Size = new Size(1264, 685);
            dgvIndicatorList.TabIndex = 0;
            dgvIndicatorList.CellBeginEdit += dgvIndicatorList_CellBeginEdit;
            dgvIndicatorList.CellContentClick += dgvIndicatorList_CellContentClick;
            dgvIndicatorList.DataError += dgvIndicatorList_DataError;
            dgvIndicatorList.RowEnter += dgvIndicatorList_RowEnter;
            dgvIndicatorList.RowLeave += dgvIndicatorList_RowLeave;
            dgvIndicatorList.RowPostPaint += dgvIndicatorList_RowPostPaint;
            dgvIndicatorList.RowsAdded += dgvIndicatorList_RowsAdded;
            // 
            // IndicatorIdCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 729);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "IndicatorIdCard";
            Text = "IndicatorIdCard";
            Load += IndicatorIdCard_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvIndicatorList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button btnAdd;
        private DataGridView dgvIndicatorList;
        private Panel panel3;
        private Label label1;
        private TextBox txtTitle;
        private Label label2;
        private TextBox txtCode;
        private ComboBox cbLkpForm;
        private Label label3;
        private Button btnSearch;
        private CheckBox chbRecycle;
    }
}