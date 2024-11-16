using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using PMIS.Services.Contract;

namespace PMIS.Forms
{
    public partial class MainForm : Form
    {
        IIndicatorService indicatorService;
        IUserService userService;
        ILookUpService lookUpService;
        ILookUpValueService lookUpValueService;
        ILookUpDestinationService lookUpDestinationService;
        IClaimUserOnIndicatorService claimUserOnIndicatorService;
        private Serilog.ILogger logHandler;
        public MainForm(IIndicatorService _indicatorService, IUserService _userService, ILookUpService _lookUpService, ILookUpValueService _lookUpValueService, ILookUpDestinationService _lookUpDestinationService, IClaimUserOnIndicatorService _claimUserOnIndicatorService, AbstractGenericLogWithSerilogHandler _logHandler)
        {

            InitializeComponent();
            indicatorService = _indicatorService;
            userService = _userService;
            lookUpService = _lookUpService;
            lookUpValueService = _lookUpValueService;
            lookUpDestinationService = _lookUpDestinationService;
            claimUserOnIndicatorService = _claimUserOnIndicatorService;
            logHandler = _logHandler.CreateLogger();
        }

        private void براساسفرمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnForm, new IndicatorValueDataEntryOnForm());
        }

        private void براساستاریخToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddNewTabPage(tabPageOnDate, new IndicatorValueDataEntryOnDate());
        }

        private void براساسشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnDate, new IndicatorValueDataEntryOnIndicator());
        }

        private void تعریفکاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageUserForm, new UserForm(userService, claimUserOnIndicatorService, indicatorService, lookUpValueService));
        }

        private void دسترسیهایکاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void شناسنامهشاخصهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageIndicatorForm, new IndicatorForm(indicatorService, claimUserOnIndicatorService, userService, lookUpValueService));
        }

        private void تخصیصدستهبندیبهشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصشاخصبهدستهبندیToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصکاربربهشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصشاخصبهکاربرToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void AddNewTabPage(TabPage tabPage, Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            if (tabControlMain.TabPages.Contains(tabPage))
            {
                tabPage.Controls.Clear();
            }
            else
            {
                tabControlMain.Controls.Add(tabPage);
            }
            Panel panel = new Panel();
            panel.Controls.Add(form);
            panel.Dock = DockStyle.Fill;
            tabPage.Controls.Add(panel);
            form.Show();
        }
    }
}
