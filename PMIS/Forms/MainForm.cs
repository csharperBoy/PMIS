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
        IIndicatorValueService indicatorValueService;
        ILookUpDestinationService lookUpDestinationService;
        IClaimUserOnIndicatorService claimUserOnIndicatorService;
        private Serilog.ILogger logHandler;
        public MainForm(IIndicatorService _indicatorService, IIndicatorValueService _indicatorValueService, IUserService _userService, ILookUpService _lookUpService, ILookUpValueService _lookUpValueService, ILookUpDestinationService _lookUpDestinationService, IClaimUserOnIndicatorService _claimUserOnIndicatorService, AbstractGenericLogWithSerilogHandler _logHandler)
        {

            InitializeComponent();
            indicatorService = _indicatorService;
            indicatorValueService = _indicatorValueService;
            userService = _userService;
            lookUpService = _lookUpService;
            lookUpValueService = _lookUpValueService;
            lookUpDestinationService = _lookUpDestinationService;
            claimUserOnIndicatorService = _claimUserOnIndicatorService;
            logHandler = _logHandler.CreateLogger();
        }

        private void براساسفرمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorValueDataEntryOnForm();
        }

        private void براساستاریخToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorValueDataEntryOnDate(indicatorValueService,indicatorService,claimUserOnIndicatorService,userService,lookUpValueService, tabControlMain);
        }

        private void براساسشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorValueDataEntryOnIndicator();
        }

        private void تعریفکاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserForm(userService, claimUserOnIndicatorService, indicatorService, lookUpValueService, tabControlMain);
        }

        private void دسترسیهایکاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void شناسنامهشاخصهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorForm(indicatorService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
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
    }
}
