using System.Windows.Forms;

namespace PMIS.Forms
{
    partial class ClaimUserOnIndicatorForm
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
            panelMenu = new Panel();
            btnDownload = new Button();
            btnUpload = new Button();
            btnApply = new Button();
            chbRecycle = new CheckBox();
            btnSearch = new Button();
            panelContent = new Panel();
            panelResults = new Panel();
            dgvResultsList = new DataGridView();
            panelFilters = new Panel();
            dgvFiltersList = new DataGridView();
            panelMenu.SuspendLayout();
            panelContent.SuspendLayout();
            panelResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResultsList).BeginInit();
            panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFiltersList).BeginInit();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.Controls.Add(btnDownload);
            panelMenu.Controls.Add(btnUpload);
            panelMenu.Controls.Add(btnApply);
            panelMenu.Dock = DockStyle.Top;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Margin = new Padding(4);
            panelMenu.Name = "panelMenu";
            panelMenu.Padding = new Padding(4);
            panelMenu.Size = new Size(1104, 33);
            panelMenu.TabIndex = 0;
            // 
            // btnDownload
            // 
            btnDownload.Dock = DockStyle.Right;
            btnDownload.Location = new Point(791, 4);
            btnDownload.Margin = new Padding(4);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(103, 25);
            btnDownload.TabIndex = 3;
            btnDownload.Text = "بارگیری";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // btnUpload
            // 
            btnUpload.Dock = DockStyle.Right;
            btnUpload.Location = new Point(894, 4);
            btnUpload.Margin = new Padding(4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(103, 25);
            btnUpload.TabIndex = 2;
            btnUpload.Text = "بارگزاری";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Visible = false;
            // 
            // btnApply
            // 
            btnApply.Dock = DockStyle.Right;
            btnApply.Location = new Point(997, 4);
            btnApply.Margin = new Padding(4);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(103, 25);
            btnApply.TabIndex = 1;
            btnApply.Text = "اعمال تغییرات";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // chbRecycle
            // 
            chbRecycle.AutoSize = true;
            chbRecycle.Dock = DockStyle.Left;
            chbRecycle.Location = new Point(0, 0);
            chbRecycle.Margin = new Padding(3, 2, 3, 2);
            chbRecycle.Name = "chbRecycle";
            chbRecycle.Size = new Size(60, 49);
            chbRecycle.TabIndex = 6;
            chbRecycle.Text = "بازیابی";
            chbRecycle.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Left;
            btnSearch.Location = new Point(60, 0);
            btnSearch.Margin = new Padding(4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 49);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "جستجو";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(panelResults);
            panelContent.Controls.Add(panelFilters);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 33);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1104, 508);
            panelContent.TabIndex = 1;
            // 
            // panelResults
            // 
            panelResults.Controls.Add(dgvResultsList);
            panelResults.Dock = DockStyle.Fill;
            panelResults.Location = new Point(0, 49);
            panelResults.Margin = new Padding(3, 2, 3, 2);
            panelResults.Name = "panelResults";
            panelResults.Size = new Size(1104, 459);
            panelResults.TabIndex = 2;
            // 
            // dgvResultsList
            // 
            dgvResultsList.AllowUserToOrderColumns = true;
            dgvResultsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvResultsList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultsList.Dock = DockStyle.Fill;
            dgvResultsList.Location = new Point(0, 0);
            dgvResultsList.Name = "dgvResultsList";
            dgvResultsList.RowHeadersWidth = 51;
            dgvResultsList.Size = new Size(1104, 459);
            dgvResultsList.TabIndex = 0;
            dgvResultsList.CellBeginEdit += dgvResultsList_CellBeginEdit;
            dgvResultsList.CellContentClick += dgvResultsList_CellContentClick;
            dgvResultsList.DataError += dgvResultsList_DataError;
            dgvResultsList.RowEnter += dgvResultsList_RowEnter;
            dgvResultsList.RowLeave += dgvResultsList_RowLeave;
            dgvResultsList.RowPostPaint += dgvResultsList_RowPostPaint;
            dgvResultsList.RowsAdded += dgvResultsList_RowsAdded;
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(dgvFiltersList);
            panelFilters.Controls.Add(btnSearch);
            panelFilters.Controls.Add(chbRecycle);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(0, 0);
            panelFilters.Margin = new Padding(3, 2, 3, 2);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1104, 49);
            panelFilters.TabIndex = 1;
            // 
            // dgvFiltersList
            // 
            dgvFiltersList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvFiltersList.BackgroundColor = SystemColors.Control;
            dgvFiltersList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFiltersList.Dock = DockStyle.Fill;
            dgvFiltersList.GridColor = SystemColors.Control;
            dgvFiltersList.Location = new Point(135, 0);
            dgvFiltersList.Margin = new Padding(3, 2, 3, 2);
            dgvFiltersList.Name = "dgvFiltersList";
            dgvFiltersList.RowHeadersVisible = false;
            dgvFiltersList.RowHeadersWidth = 50;
            dgvFiltersList.Size = new Size(969, 49);
            dgvFiltersList.TabIndex = 7;
            // 
            // ClaimUserOnIndicatorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1104, 541);
            Controls.Add(panelContent);
            Controls.Add(panelMenu);
            Name = "ClaimUserOnIndicatorForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "NormalForm";
            Load += NormalForm_Load;
            panelMenu.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            panelResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvResultsList).EndInit();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFiltersList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Panel panelContent;
        private Panel panelFilters;
        private Panel panelResults;
        private Button btnApply;
        private Button btnSearch;
        private CheckBox chbRecycle;
        private DataGridView dgvFiltersList;
        private DataGridView dgvResultsList;
        private Button btnDownload;
        private Button btnUpload;
    }
}