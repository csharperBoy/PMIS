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
            menuStripMain = new MenuStrip();
            ورودمقادیرToolStripMenuItem = new ToolStripMenuItem();
            ورودمقادیرToolStripMenuItem1 = new ToolStripMenuItem();
            براساسفرمToolStripMenuItem = new ToolStripMenuItem();
            براساستاریخToolStripMenuItem = new ToolStripMenuItem();
            براساسشاخصToolStripMenuItem = new ToolStripMenuItem();
            اطلاعاتکاربریToolStripMenuItem = new ToolStripMenuItem();
            تعریفکاربرانToolStripMenuItem = new ToolStripMenuItem();
            دسترسیهایکاربرانToolStripMenuItem = new ToolStripMenuItem();
            اطلاعاتپایهToolStripMenuItem = new ToolStripMenuItem();
            شناسنامهشاخصهاToolStripMenuItem = new ToolStripMenuItem();
            دستهبندیهایشاخصهاToolStripMenuItem = new ToolStripMenuItem();
            تخصیصدستهبندیبهشاخصToolStripMenuItem = new ToolStripMenuItem();
            تخصیصشاخصبهدستهبندیToolStripMenuItem = new ToolStripMenuItem();
            ادعاهایکاربرانرویشاخصهاToolStripMenuItem = new ToolStripMenuItem();
            تخصیصکاربربهشاخصToolStripMenuItem = new ToolStripMenuItem();
            تخصیصشاخصبهکاربرToolStripMenuItem = new ToolStripMenuItem();
            خروجToolStripMenuItem = new ToolStripMenuItem();
            flowLayoutPanelMain = new FlowLayoutPanel();
            tabControlMain = new TabControl();
            tabPageOnForm = new TabPage();
            tabPageOnDate = new TabPage();
            tabPageOnIndicator = new TabPage();
            tabPageUserDefine = new TabPage();
            tabPageUserAccess = new TabPage();
            tabPageIndicatorIdCard = new TabPage();
            tabPageIndicatorAssignToCategory = new TabPage();
            tabPageCategoryAssignToIndicator = new TabPage();
            tabPageIndicatorAssignToUser = new TabPage();
            tabPageUserAssignToIndicator = new TabPage();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { ورودمقادیرToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(800, 24);
            menuStripMain.TabIndex = 1;
            menuStripMain.Text = "menuStripMain";
            menuStripMain.TextDirection = ToolStripTextDirection.Vertical90;
            // 
            // ورودمقادیرToolStripMenuItem
            // 
            ورودمقادیرToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ورودمقادیرToolStripMenuItem1, اطلاعاتکاربریToolStripMenuItem, اطلاعاتپایهToolStripMenuItem, خروجToolStripMenuItem });
            ورودمقادیرToolStripMenuItem.Name = "ورودمقادیرToolStripMenuItem";
            ورودمقادیرToolStripMenuItem.Size = new Size(36, 20);
            ورودمقادیرToolStripMenuItem.Text = "منو";
            ورودمقادیرToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // ورودمقادیرToolStripMenuItem1
            // 
            ورودمقادیرToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { براساسفرمToolStripMenuItem, براساستاریخToolStripMenuItem, براساسشاخصToolStripMenuItem });
            ورودمقادیرToolStripMenuItem1.Name = "ورودمقادیرToolStripMenuItem1";
            ورودمقادیرToolStripMenuItem1.Size = new Size(180, 22);
            ورودمقادیرToolStripMenuItem1.Text = "ورود مقادیر";
            // 
            // براساسفرمToolStripMenuItem
            // 
            براساسفرمToolStripMenuItem.Name = "براساسفرمToolStripMenuItem";
            براساسفرمToolStripMenuItem.Size = new Size(180, 22);
            براساسفرمToolStripMenuItem.Text = "بر اساس فرم";
            براساسفرمToolStripMenuItem.Click += براساسفرمToolStripMenuItem_Click;
            // 
            // براساستاریخToolStripMenuItem
            // 
            براساستاریخToolStripMenuItem.Name = "براساستاریخToolStripMenuItem";
            براساستاریخToolStripMenuItem.Size = new Size(180, 22);
            براساستاریخToolStripMenuItem.Text = "بر اساس تاریخ";
            براساستاریخToolStripMenuItem.Click += براساستاریخToolStripMenuItem_Click;
            // 
            // براساسشاخصToolStripMenuItem
            // 
            براساسشاخصToolStripMenuItem.Name = "براساسشاخصToolStripMenuItem";
            براساسشاخصToolStripMenuItem.Size = new Size(180, 22);
            براساسشاخصToolStripMenuItem.Text = "بر اساس شاخص";
            براساسشاخصToolStripMenuItem.Click += براساسشاخصToolStripMenuItem_Click;
            // 
            // اطلاعاتکاربریToolStripMenuItem
            // 
            اطلاعاتکاربریToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { تعریفکاربرانToolStripMenuItem, دسترسیهایکاربرانToolStripMenuItem });
            اطلاعاتکاربریToolStripMenuItem.Name = "اطلاعاتکاربریToolStripMenuItem";
            اطلاعاتکاربریToolStripMenuItem.Size = new Size(180, 22);
            اطلاعاتکاربریToolStripMenuItem.Text = "اطلاعات کاربری";
            // 
            // تعریفکاربرانToolStripMenuItem
            // 
            تعریفکاربرانToolStripMenuItem.Name = "تعریفکاربرانToolStripMenuItem";
            تعریفکاربرانToolStripMenuItem.Size = new Size(180, 22);
            تعریفکاربرانToolStripMenuItem.Text = "تعریف کاربران";
            تعریفکاربرانToolStripMenuItem.Click += تعریفکاربرانToolStripMenuItem_Click;
            // 
            // دسترسیهایکاربرانToolStripMenuItem
            // 
            دسترسیهایکاربرانToolStripMenuItem.Name = "دسترسیهایکاربرانToolStripMenuItem";
            دسترسیهایکاربرانToolStripMenuItem.Size = new Size(180, 22);
            دسترسیهایکاربرانToolStripMenuItem.Text = "دسترسی‌های کاربران";
            دسترسیهایکاربرانToolStripMenuItem.Click += دسترسیهایکاربرانToolStripMenuItem_Click;
            // 
            // اطلاعاتپایهToolStripMenuItem
            // 
            اطلاعاتپایهToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { شناسنامهشاخصهاToolStripMenuItem, دستهبندیهایشاخصهاToolStripMenuItem, ادعاهایکاربرانرویشاخصهاToolStripMenuItem });
            اطلاعاتپایهToolStripMenuItem.Name = "اطلاعاتپایهToolStripMenuItem";
            اطلاعاتپایهToolStripMenuItem.Size = new Size(180, 22);
            اطلاعاتپایهToolStripMenuItem.Text = "اطلاعات پایه";
            // 
            // شناسنامهشاخصهاToolStripMenuItem
            // 
            شناسنامهشاخصهاToolStripMenuItem.Name = "شناسنامهشاخصهاToolStripMenuItem";
            شناسنامهشاخصهاToolStripMenuItem.Size = new Size(221, 22);
            شناسنامهشاخصهاToolStripMenuItem.Text = "شناسنامه شاخص‌ها";
            شناسنامهشاخصهاToolStripMenuItem.Click += شناسنامهشاخصهاToolStripMenuItem_Click;
            // 
            // دستهبندیهایشاخصهاToolStripMenuItem
            // 
            دستهبندیهایشاخصهاToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { تخصیصدستهبندیبهشاخصToolStripMenuItem, تخصیصشاخصبهدستهبندیToolStripMenuItem });
            دستهبندیهایشاخصهاToolStripMenuItem.Name = "دستهبندیهایشاخصهاToolStripMenuItem";
            دستهبندیهایشاخصهاToolStripMenuItem.Size = new Size(221, 22);
            دستهبندیهایشاخصهاToolStripMenuItem.Text = "دسته‌بندی‌های شاخص‌ها";
            // 
            // تخصیصدستهبندیبهشاخصToolStripMenuItem
            // 
            تخصیصدستهبندیبهشاخصToolStripMenuItem.Name = "تخصیصدستهبندیبهشاخصToolStripMenuItem";
            تخصیصدستهبندیبهشاخصToolStripMenuItem.Size = new Size(221, 22);
            تخصیصدستهبندیبهشاخصToolStripMenuItem.Text = "تخصیص دسته‌بندی به شاخص";
            تخصیصدستهبندیبهشاخصToolStripMenuItem.Click += تخصیصدستهبندیبهشاخصToolStripMenuItem_Click;
            // 
            // تخصیصشاخصبهدستهبندیToolStripMenuItem
            // 
            تخصیصشاخصبهدستهبندیToolStripMenuItem.Name = "تخصیصشاخصبهدستهبندیToolStripMenuItem";
            تخصیصشاخصبهدستهبندیToolStripMenuItem.Size = new Size(221, 22);
            تخصیصشاخصبهدستهبندیToolStripMenuItem.Text = "تخصیص شاخص به دسته‌بندی";
            تخصیصشاخصبهدستهبندیToolStripMenuItem.Click += تخصیصشاخصبهدستهبندیToolStripMenuItem_Click;
            // 
            // ادعاهایکاربرانرویشاخصهاToolStripMenuItem
            // 
            ادعاهایکاربرانرویشاخصهاToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { تخصیصکاربربهشاخصToolStripMenuItem, تخصیصشاخصبهکاربرToolStripMenuItem });
            ادعاهایکاربرانرویشاخصهاToolStripMenuItem.Name = "ادعاهایکاربرانرویشاخصهاToolStripMenuItem";
            ادعاهایکاربرانرویشاخصهاToolStripMenuItem.Size = new Size(221, 22);
            ادعاهایکاربرانرویشاخصهاToolStripMenuItem.Text = "ادعاهای کاربران روی شاخص‌ها";
            // 
            // تخصیصکاربربهشاخصToolStripMenuItem
            // 
            تخصیصکاربربهشاخصToolStripMenuItem.Name = "تخصیصکاربربهشاخصToolStripMenuItem";
            تخصیصکاربربهشاخصToolStripMenuItem.Size = new Size(195, 22);
            تخصیصکاربربهشاخصToolStripMenuItem.Text = "تخصیص کاربر به شاخص";
            تخصیصکاربربهشاخصToolStripMenuItem.Click += تخصیصکاربربهشاخصToolStripMenuItem_Click;
            // 
            // تخصیصشاخصبهکاربرToolStripMenuItem
            // 
            تخصیصشاخصبهکاربرToolStripMenuItem.Name = "تخصیصشاخصبهکاربرToolStripMenuItem";
            تخصیصشاخصبهکاربرToolStripMenuItem.Size = new Size(195, 22);
            تخصیصشاخصبهکاربرToolStripMenuItem.Text = "تخصیص شاخص به کاربر";
            تخصیصشاخصبهکاربرToolStripMenuItem.Click += تخصیصشاخصبهکاربرToolStripMenuItem_Click;
            // 
            // خروجToolStripMenuItem
            // 
            خروجToolStripMenuItem.Name = "خروجToolStripMenuItem";
            خروجToolStripMenuItem.Size = new Size(180, 22);
            خروجToolStripMenuItem.Text = "خروج";
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
            // tabPageOnForm
            // 
            tabPageOnForm.Location = new Point(4, 24);
            tabPageOnForm.Name = "tabPageOnForm";
            tabPageOnForm.Padding = new Padding(3);
            tabPageOnForm.Size = new Size(192, 0);
            tabPageOnForm.TabIndex = 0;
            tabPageOnForm.Text = "ورود مقادیر بر اساس فرم";
            tabPageOnForm.UseVisualStyleBackColor = true;
            // 
            // tabPageOnDate
            // 
            tabPageOnDate.Location = new Point(4, 24);
            tabPageOnDate.Name = "tabPageOnDate";
            tabPageOnDate.Padding = new Padding(3);
            tabPageOnDate.Size = new Size(192, 0);
            tabPageOnDate.TabIndex = 1;
            tabPageOnDate.Text = "ورود مقادیر بر اساس تاریخ";
            tabPageOnDate.UseVisualStyleBackColor = true;
            // 
            // tabPageOnIndicator
            // 
            tabPageOnIndicator.Location = new Point(4, 24);
            tabPageOnIndicator.Name = "tabPageOnIndicator";
            tabPageOnIndicator.Padding = new Padding(3);
            tabPageOnIndicator.Size = new Size(192, 0);
            tabPageOnIndicator.TabIndex = 1;
            tabPageOnIndicator.Text = "ورود مقادیر بر اساس شاخص";
            tabPageOnIndicator.UseVisualStyleBackColor = true;
            // 
            // tabPageUserDefine
            // 
            tabPageUserDefine.Location = new Point(4, 24);
            tabPageUserDefine.Name = "tabPageUserDefine";
            tabPageUserDefine.Padding = new Padding(3);
            tabPageUserDefine.Size = new Size(192, 0);
            tabPageUserDefine.TabIndex = 1;
            tabPageUserDefine.Text = "تعریف کاربران";
            tabPageUserDefine.UseVisualStyleBackColor = true;
            // 
            // tabPageUserAccess
            // 
            tabPageUserAccess.Location = new Point(4, 24);
            tabPageUserAccess.Name = "tabPageUserAccess";
            tabPageUserAccess.Padding = new Padding(3);
            tabPageUserAccess.Size = new Size(192, 0);
            tabPageUserAccess.TabIndex = 1;
            tabPageUserAccess.Text = "دسترسی‌های کاربران";
            tabPageUserAccess.UseVisualStyleBackColor = true;
            // 
            // tabPageIndicatorIdCard
            // 
            tabPageIndicatorIdCard.Location = new Point(4, 24);
            tabPageIndicatorIdCard.Name = "tabPageIndicatorIdCard";
            tabPageIndicatorIdCard.Padding = new Padding(3);
            tabPageIndicatorIdCard.Size = new Size(192, 0);
            tabPageIndicatorIdCard.TabIndex = 1;
            tabPageIndicatorIdCard.Text = "شناسنامه شاخص";
            tabPageIndicatorIdCard.UseVisualStyleBackColor = true;
            // 
            // tabPageIndicatorAssignToCategory
            // 
            tabPageIndicatorAssignToCategory.Location = new Point(4, 24);
            tabPageIndicatorAssignToCategory.Name = "tabPageIndicatorAssignToCategory";
            tabPageIndicatorAssignToCategory.Padding = new Padding(3);
            tabPageIndicatorAssignToCategory.Size = new Size(192, 0);
            tabPageIndicatorAssignToCategory.TabIndex = 1;
            tabPageIndicatorAssignToCategory.Text = "تخصیص شاخص به دسته‌بندی";
            tabPageIndicatorAssignToCategory.UseVisualStyleBackColor = true;
            // 
            // tabPageCategoryAssignToIndicator
            // 
            tabPageCategoryAssignToIndicator.Location = new Point(4, 24);
            tabPageCategoryAssignToIndicator.Name = "tabPageCategoryAssignToIndicator";
            tabPageCategoryAssignToIndicator.Padding = new Padding(3);
            tabPageCategoryAssignToIndicator.Size = new Size(192, 0);
            tabPageCategoryAssignToIndicator.TabIndex = 1;
            tabPageCategoryAssignToIndicator.Text = "تخصیص دسته‌بندی به شاخص";
            tabPageCategoryAssignToIndicator.UseVisualStyleBackColor = true;
            // 
            // tabPageIndicatorAssignToUser
            // 
            tabPageIndicatorAssignToUser.Location = new Point(4, 24);
            tabPageIndicatorAssignToUser.Name = "tabPageIndicatorAssignToUser";
            tabPageIndicatorAssignToUser.Padding = new Padding(3);
            tabPageIndicatorAssignToUser.Size = new Size(192, 0);
            tabPageIndicatorAssignToUser.TabIndex = 1;
            tabPageIndicatorAssignToUser.Text = "تخصیص شاخص به کاربران";
            tabPageIndicatorAssignToUser.UseVisualStyleBackColor = true;
            // 
            // tabPageUserAssignToIndicator
            // 
            tabPageUserAssignToIndicator.Location = new Point(4, 24);
            tabPageUserAssignToIndicator.Name = "tabPageUserAssignToIndicator";
            tabPageUserAssignToIndicator.Padding = new Padding(3);
            tabPageUserAssignToIndicator.Size = new Size(192, 0);
            tabPageUserAssignToIndicator.TabIndex = 1;
            tabPageUserAssignToIndicator.Text = "تخصیص کاربران به شاخص";
            tabPageUserAssignToIndicator.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControlMain);
            Controls.Add(flowLayoutPanelMain);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            Name = "MainForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "MainForm";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStripMain;
        private ToolStripMenuItem ورودمقادیرToolStripMenuItem;
        private ToolStripMenuItem ورودمقادیرToolStripMenuItem1;
        private ToolStripMenuItem براساسفرمToolStripMenuItem;
        private ToolStripMenuItem براساستاریخToolStripMenuItem;
        private ToolStripMenuItem براساسشاخصToolStripMenuItem;
        private ToolStripMenuItem اطلاعاتکاربریToolStripMenuItem;
        private ToolStripMenuItem تعریفکاربرانToolStripMenuItem;
        private ToolStripMenuItem دسترسیهایکاربرانToolStripMenuItem;
        private ToolStripMenuItem اطلاعاتپایهToolStripMenuItem;
        private ToolStripMenuItem شناسنامهشاخصهاToolStripMenuItem;
        private ToolStripMenuItem دستهبندیهایشاخصهاToolStripMenuItem;
        private ToolStripMenuItem تخصیصدستهبندیبهشاخصToolStripMenuItem;
        private ToolStripMenuItem تخصیصشاخصبهدستهبندیToolStripMenuItem;
        private ToolStripMenuItem ادعاهایکاربرانرویشاخصهاToolStripMenuItem;
        private ToolStripMenuItem تخصیصکاربربهشاخصToolStripMenuItem;
        private ToolStripMenuItem تخصیصشاخصبهکاربرToolStripMenuItem;
        private ToolStripMenuItem خروجToolStripMenuItem;
        private FlowLayoutPanel flowLayoutPanelMain;
        private TabControl tabControlMain;
        private TabPage tabPageOnForm;
        private TabPage tabPageOnDate;
        private TabPage tabPageOnIndicator;
        private TabPage tabPageUserDefine;
        private TabPage tabPageUserAccess;
        private TabPage tabPageIndicatorIdCard;
        private TabPage tabPageIndicatorAssignToCategory;
        private TabPage tabPageCategoryAssignToIndicator;
        private TabPage tabPageIndicatorAssignToUser;
        private TabPage tabPageUserAssignToIndicator;
    }
}