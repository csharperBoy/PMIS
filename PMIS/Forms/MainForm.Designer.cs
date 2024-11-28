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
            ClaimOnSystemToolStripMenuItem = new ToolStripMenuItem();
            IndicatorValueToolStripMenuItem = new ToolStripMenuItem();
            flowLayoutPanelMain = new FlowLayoutPanel();
            tabControlMain = new TabControlWithCloseTab();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, BaseInfoToolStripMenuItem, IndicatorValueToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(800, 24);
            menuStripMain.TabIndex = 1;
            menuStripMain.Text = "menuStripMain";
            menuStripMain.TextDirection = ToolStripTextDirection.Vertical90;
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { HelpToolStripMenuItem, ChangePasswordToolStripMenuItem, ExitToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(40, 20);
            FileToolStripMenuItem.Text = "فایل";
            FileToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // HelpToolStripMenuItem
            // 
            HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            HelpToolStripMenuItem.Size = new Size(136, 22);
            HelpToolStripMenuItem.Text = "راهنما";
            // 
            // ChangePasswordToolStripMenuItem
            // 
            ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem";
            ChangePasswordToolStripMenuItem.Size = new Size(136, 22);
            ChangePasswordToolStripMenuItem.Text = "تغییر گذرواژه";
            ChangePasswordToolStripMenuItem.Click += ChangePasswordToolStripMenuItem_Click;
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(136, 22);
            ExitToolStripMenuItem.Text = "خروج";
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // BaseInfoToolStripMenuItem
            // 
            BaseInfoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { UsersToolStripMenuItem, IndicatorsToolStripMenuItem, ClaimUserOnIndicatorToolStripMenuItem, ClaimOnSystemToolStripMenuItem });
            BaseInfoToolStripMenuItem.Name = "BaseInfoToolStripMenuItem";
            BaseInfoToolStripMenuItem.Size = new Size(80, 20);
            BaseInfoToolStripMenuItem.Text = "اطلاعات پایه";
            BaseInfoToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // UsersToolStripMenuItem
            // 
            UsersToolStripMenuItem.Name = "UsersToolStripMenuItem";
            UsersToolStripMenuItem.Size = new Size(219, 22);
            UsersToolStripMenuItem.Text = "مدیریت کاربران";
            UsersToolStripMenuItem.Click += UsersToolStripMenuItem_Click;
            // 
            // IndicatorsToolStripMenuItem
            // 
            IndicatorsToolStripMenuItem.Name = "IndicatorsToolStripMenuItem";
            IndicatorsToolStripMenuItem.Size = new Size(219, 22);
            IndicatorsToolStripMenuItem.Text = "مدیریت شاخص‌ها";
            IndicatorsToolStripMenuItem.Click += IndicatorsToolStripMenuItem_Click;
            // 
            // ClaimUserOnIndicatorToolStripMenuItem
            // 
            ClaimUserOnIndicatorToolStripMenuItem.Name = "ClaimUserOnIndicatorToolStripMenuItem";
            ClaimUserOnIndicatorToolStripMenuItem.Size = new Size(219, 22);
            ClaimUserOnIndicatorToolStripMenuItem.Text = "مدیریت دسترسی به شاخص‌ها";
            ClaimUserOnIndicatorToolStripMenuItem.Click += ClaimUserOnIndicatorToolStripMenuItem_Click;
            // 
            // ClaimOnSystemToolStripMenuItem
            // 
            ClaimOnSystemToolStripMenuItem.Name = "ClaimOnSystemToolStripMenuItem";
            ClaimOnSystemToolStripMenuItem.Size = new Size(219, 22);
            ClaimOnSystemToolStripMenuItem.Text = "مدیریت دسترسی به فرم‌ها";
            ClaimOnSystemToolStripMenuItem.Click += ClaimOnSystemToolStripMenuItem_Click;
            // 
            // IndicatorValueToolStripMenuItem
            // 
            IndicatorValueToolStripMenuItem.Name = "IndicatorValueToolStripMenuItem";
            IndicatorValueToolStripMenuItem.Size = new Size(75, 20);
            IndicatorValueToolStripMenuItem.Text = "ورود مقادیر";
            IndicatorValueToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            IndicatorValueToolStripMenuItem.Click += IndicatorValueToolStripMenuItem_Click;
            // 
            // flowLayoutPanelMain
            // 
            flowLayoutPanelMain.Dock = DockStyle.Fill;
            flowLayoutPanelMain.Location = new Point(0, 24);
            flowLayoutPanelMain.Name = "flowLayoutPanelMain";
            flowLayoutPanelMain.Size = new Size(800, 426);
            flowLayoutPanelMain.TabIndex = 2;
            // 
            // tabControlMain
            // 
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 24);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.RightToLeftLayout = true;
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(800, 426);
            tabControlMain.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControlMain);
            Controls.Add(flowLayoutPanelMain);
            Controls.Add(menuStripMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
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
        private TabControlWithCloseTab tabControlMain;
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
        private ToolStripMenuItem ClaimOnSystemToolStripMenuItem;
    }
}