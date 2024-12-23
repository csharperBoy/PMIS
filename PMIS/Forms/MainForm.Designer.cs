namespace PMIS.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStripMain = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            HelpToolStripMenuItem = new ToolStripMenuItem();
            ChangePasswordToolStripMenuItem = new ToolStripMenuItem();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            BaseInfoToolStripMenuItem = new ToolStripMenuItem();
            UsersToolStripMenuItem = new ToolStripMenuItem();
            IndicatorsToolStripMenuItem = new ToolStripMenuItem();
            ClaimUserOnIndicatorToolStripMenuItem = new ToolStripMenuItem();
            ClaimUserOnSystemToolStripMenuItem = new ToolStripMenuItem();
            IndicatorValueToolStripMenuItem = new ToolStripMenuItem();
            flowLayoutPanelMain = new FlowLayoutPanel();
            tabControlMain = new TabControlWithCloseTab();
            IndicatorCategoryToolStripMenuItem = new ToolStripMenuItem();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, BaseInfoToolStripMenuItem, IndicatorValueToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(7, 3, 0, 3);
            menuStripMain.Size = new Size(914, 30);
            menuStripMain.TabIndex = 1;
            menuStripMain.Text = "menuStripMain";
            menuStripMain.TextDirection = ToolStripTextDirection.Vertical90;
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { HelpToolStripMenuItem, ChangePasswordToolStripMenuItem, ExitToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(50, 24);
            FileToolStripMenuItem.Text = "فایل";
            FileToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // HelpToolStripMenuItem
            // 
            HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            HelpToolStripMenuItem.Size = new Size(172, 26);
            HelpToolStripMenuItem.Text = "راهنما";
            // 
            // ChangePasswordToolStripMenuItem
            // 
            ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem";
            ChangePasswordToolStripMenuItem.Size = new Size(172, 26);
            ChangePasswordToolStripMenuItem.Text = "تغییر گذرواژه";
            ChangePasswordToolStripMenuItem.Click += ChangePasswordToolStripMenuItem_Click;
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(172, 26);
            ExitToolStripMenuItem.Text = "خروج";
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // BaseInfoToolStripMenuItem
            // 
            BaseInfoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { UsersToolStripMenuItem, IndicatorsToolStripMenuItem, ClaimUserOnIndicatorToolStripMenuItem, ClaimUserOnSystemToolStripMenuItem, IndicatorCategoryToolStripMenuItem });
            BaseInfoToolStripMenuItem.Name = "BaseInfoToolStripMenuItem";
            BaseInfoToolStripMenuItem.Size = new Size(102, 24);
            BaseInfoToolStripMenuItem.Text = "اطلاعات پایه";
            BaseInfoToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // UsersToolStripMenuItem
            // 
            UsersToolStripMenuItem.Name = "UsersToolStripMenuItem";
            UsersToolStripMenuItem.Size = new Size(279, 26);
            UsersToolStripMenuItem.Text = "مدیریت کاربران";
            UsersToolStripMenuItem.Click += UsersToolStripMenuItem_Click;
            // 
            // IndicatorsToolStripMenuItem
            // 
            IndicatorsToolStripMenuItem.Name = "IndicatorsToolStripMenuItem";
            IndicatorsToolStripMenuItem.Size = new Size(279, 26);
            IndicatorsToolStripMenuItem.Text = "مدیریت شاخص‌ها";
            IndicatorsToolStripMenuItem.Click += IndicatorsToolStripMenuItem_Click;
            // 
            // ClaimUserOnIndicatorToolStripMenuItem
            // 
            ClaimUserOnIndicatorToolStripMenuItem.Name = "ClaimUserOnIndicatorToolStripMenuItem";
            ClaimUserOnIndicatorToolStripMenuItem.Size = new Size(279, 26);
            ClaimUserOnIndicatorToolStripMenuItem.Text = "مدیریت دسترسی به شاخص‌ها";
            ClaimUserOnIndicatorToolStripMenuItem.Click += ClaimUserOnIndicatorToolStripMenuItem_Click;
            // 
            // ClaimUserOnSystemToolStripMenuItem
            // 
            ClaimUserOnSystemToolStripMenuItem.Name = "ClaimUserOnSystemToolStripMenuItem";
            ClaimUserOnSystemToolStripMenuItem.Size = new Size(279, 26);
            ClaimUserOnSystemToolStripMenuItem.Text = "مدیریت دسترسی به فرم‌ها";
            ClaimUserOnSystemToolStripMenuItem.Click += ClaimUserOnSystemToolStripMenuItem_Click;
            // 
            // IndicatorValueToolStripMenuItem
            // 
            IndicatorValueToolStripMenuItem.Name = "IndicatorValueToolStripMenuItem";
            IndicatorValueToolStripMenuItem.Size = new Size(96, 24);
            IndicatorValueToolStripMenuItem.Text = "ورود مقادیر";
            IndicatorValueToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            IndicatorValueToolStripMenuItem.Click += IndicatorValueToolStripMenuItem_Click;
            // 
            // flowLayoutPanelMain
            // 
            flowLayoutPanelMain.Dock = DockStyle.Fill;
            flowLayoutPanelMain.Location = new Point(0, 30);
            flowLayoutPanelMain.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelMain.Name = "flowLayoutPanelMain";
            flowLayoutPanelMain.Size = new Size(914, 570);
            flowLayoutPanelMain.TabIndex = 2;
            // 
            // tabControlMain
            // 
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 30);
            tabControlMain.Margin = new Padding(3, 4, 3, 4);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.RightToLeftLayout = true;
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(914, 570);
            tabControlMain.TabIndex = 0;
            // 
            // IndicatorCategoryToolStripMenuItem
            // 
            IndicatorCategoryToolStripMenuItem.Name = "IndicatorCategoryToolStripMenuItem";
            IndicatorCategoryToolStripMenuItem.Size = new Size(279, 26);
            IndicatorCategoryToolStripMenuItem.Text = "مدیریت دسته بندی شاخص ها";
            IndicatorCategoryToolStripMenuItem.Click += IndicatorCategoryToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(tabControlMain);
            Controls.Add(flowLayoutPanelMain);
            Controls.Add(menuStripMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "داشبورد";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FlowLayoutPanel flowLayoutPanelMain;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem HelpToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem BaseInfoToolStripMenuItem;
        private ToolStripMenuItem UsersToolStripMenuItem;
        private ToolStripMenuItem IndicatorsToolStripMenuItem;
        private ToolStripMenuItem ClaimUserOnIndicatorToolStripMenuItem;
        private ToolStripMenuItem IndicatorValueToolStripMenuItem;
        private ToolStripMenuItem ChangePasswordToolStripMenuItem;
        private ToolStripMenuItem ClaimUserOnSystemToolStripMenuItem;
        private ToolStripMenuItem IndicatorCategoryToolStripMenuItem;
        private TabControlWithCloseTab tabControlMain;
    }
}