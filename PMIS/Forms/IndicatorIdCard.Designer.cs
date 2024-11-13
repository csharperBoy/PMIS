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
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(5);
            panel1.Size = new Size(1025, 58);
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
            panel3.Location = new Point(209, 5);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(0, 10, 0, 0);
            panel3.Size = new Size(811, 48);
            panel3.TabIndex = 2;
            // 
            // chbRecycle
            // 
            chbRecycle.AutoSize = true;
            chbRecycle.Dock = DockStyle.Right;
            chbRecycle.Location = new Point(235, 10);
            chbRecycle.Name = "chbRecycle";
            chbRecycle.Size = new Size(74, 38);
            chbRecycle.TabIndex = 6;
            chbRecycle.Text = "بازیابی";
            chbRecycle.UseVisualStyleBackColor = true;
            // 
            // cbLkpForm
            // 
            cbLkpForm.Dock = DockStyle.Right;
            cbLkpForm.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLkpForm.FormattingEnabled = true;
            cbLkpForm.Location = new Point(309, 10);
            cbLkpForm.Name = "cbLkpForm";
            cbLkpForm.Size = new Size(151, 28);
            cbLkpForm.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Right;
            label3.Location = new Point(460, 10);
            label3.Name = "label3";
            label3.Size = new Size(31, 20);
            label3.TabIndex = 4;
            label3.Text = "فرم";
            // 
            // txtTitle
            // 
            txtTitle.Dock = DockStyle.Right;
            txtTitle.Location = new Point(491, 10);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(125, 27);
            txtTitle.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Right;
            label2.Location = new Point(616, 10);
            label2.Name = "label2";
            label2.Size = new Size(45, 20);
            label2.TabIndex = 2;
            label2.Text = "عنوان";
            // 
            // txtCode
            // 
            txtCode.Dock = DockStyle.Right;
            txtCode.Location = new Point(661, 10);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(125, 27);
            txtCode.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Right;
            label1.Location = new Point(786, 10);
            label1.Name = "label1";
            label1.Size = new Size(25, 20);
            label1.TabIndex = 0;
            label1.Text = "کد";
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Left;
            btnSearch.Location = new Point(123, 5);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(86, 48);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "جستجو";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnAdd
            // 
            btnAdd.Dock = DockStyle.Left;
            btnAdd.Location = new Point(5, 5);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(118, 48);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "اعمال تغییرات";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnApply_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvIndicatorList);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 58);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1025, 542);
            panel2.TabIndex = 1;
            // 
            // dgvIndicatorList
            // 
            dgvIndicatorList.AllowUserToOrderColumns = true;
            dgvIndicatorList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvIndicatorList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIndicatorList.Dock = DockStyle.Fill;
            dgvIndicatorList.Location = new Point(0, 0);
            dgvIndicatorList.Margin = new Padding(3, 4, 3, 4);
            dgvIndicatorList.Name = "dgvIndicatorList";
            dgvIndicatorList.RowHeadersWidth = 51;
            dgvIndicatorList.Size = new Size(1025, 542);
            dgvIndicatorList.TabIndex = 0;
            dgvIndicatorList.CellBeginEdit += dgvIndicatorList_CellBeginEdit;
            dgvIndicatorList.CellContentClick += dgvIndicatorList_CellContentClick;
            dgvIndicatorList.DataError += dgvIndicatorList_DataError;
            dgvIndicatorList.RowPostPaint += dgvIndicatorList_RowPostPaint;
            dgvIndicatorList.RowsAdded += dgvIndicatorList_RowsAdded;
            // 
            // IndicatorIdCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 600);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "IndicatorIdCard";
            Text = "IndicatorIdCard";
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