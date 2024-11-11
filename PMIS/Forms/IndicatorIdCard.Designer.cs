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
            btnSearch = new Button();
            btnAdd = new Button();
            panel2 = new Panel();
            dgvIndicatorList = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIndicatorList).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnAdd);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 54);
            panel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Left;
            btnSearch.Location = new Point(115, 0);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(86, 54);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "جستجو";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnAdd
            // 
            btnAdd.Dock = DockStyle.Left;
            btnAdd.Location = new Point(0, 0);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(115, 54);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "اعمال تغییرات";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnApply_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvIndicatorList);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 54);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(914, 546);
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
            dgvIndicatorList.Size = new Size(914, 546);
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
            ClientSize = new Size(914, 600);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "IndicatorIdCard";
            Text = "IndicatorIdCard";
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
        private DataGridView dgvIndicatorList;
       

    }
}