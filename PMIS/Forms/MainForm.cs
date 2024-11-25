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

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserForm(userService, claimUserOnIndicatorService, indicatorService, lookUpValueService, tabControlMain);
        }

        private void IndicatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorForm(indicatorService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
        }

        private void ClaimsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           new ClaimUserOnIndicatorForm(claimUserOnIndicatorService, userService, indicatorService, lookUpValueService, 0, 0, tabControlMain);
        }

        private void IndicatorValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorValueForm(indicatorValueService, indicatorService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
        }
    }
}
