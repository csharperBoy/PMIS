using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using PMIS.DTO.ClaimOnSystem;
using PMIS.DTO.User;
using PMIS.Services.Contract;
using System.Windows.Forms;

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
        private IClaimOnSystemService claimOnSystemService;
        private Serilog.ILogger logHandler;
        public MainForm(IIndicatorService _indicatorService, IClaimOnSystemService _claimOnSystemService, IIndicatorValueService _indicatorValueService, IUserService _userService, ILookUpService _lookUpService, ILookUpValueService _lookUpValueService, ILookUpDestinationService _lookUpDestinationService, IClaimUserOnIndicatorService _claimUserOnIndicatorService, AbstractGenericLogWithSerilogHandler _logHandler)
        {
            InitializeComponent();
            claimOnSystemService = _claimOnSystemService;
            indicatorService = _indicatorService;
            indicatorValueService = _indicatorValueService;
            userService = _userService;
            lookUpService = _lookUpService;
            lookUpValueService = _lookUpValueService;
            lookUpDestinationService = _lookUpDestinationService;
            claimUserOnIndicatorService = _claimUserOnIndicatorService;
            logHandler = _logHandler.CreateLogger();
            CustomInitialize();
        }
        private async void CustomInitialize()
        {
            await CheckSystemClaimsRequired();
        }
        private async Task CheckSystemClaimsRequired()
        {
            try
            {
                IEnumerable<ClaimOnSystemSearchResponseDto> claims = await claimOnSystemService.GetCurrentUserClaims();
                if (!claims.Any(c => c.FkLkpClaimOnSystemInfo.Value == "ChangePasswordForm"))
                {
                    UsersToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimOnSystemInfo.Value == "UserForm"))
                {
                    UsersToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimOnSystemInfo.Value == "IndicatorForm"))
                {
                    IndicatorsToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimOnSystemInfo.Value == "ClaimUserOnIndicatorForm"))
                {
                    ClaimUserOnIndicatorToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimOnSystemInfo.Value == "ClaimUserOnSystemForm"))
                {
                    ClaimUserOnIndicatorToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimOnSystemInfo.Value == "IndicatorValueForm"))
                {
                    IndicatorValueToolStripMenuItem.Visible = false;
                }
                else
                {
                    new IndicatorValueForm(indicatorValueService, indicatorService, claimOnSystemService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangePasswordForm(claimOnSystemService, userService);
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserForm(userService, claimOnSystemService, claimUserOnIndicatorService, indicatorService, lookUpValueService, tabControlMain);
        }

        private void IndicatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorForm(indicatorService, claimOnSystemService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
        }

        private void ClaimUserOnIndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClaimUserOnIndicatorForm(claimOnSystemService, claimUserOnIndicatorService, userService, indicatorService, lookUpValueService, 0, 0, tabControlMain);
        }

        private void ClaimOnSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClaimUserOnSystemForm(claimOnSystemService, userService, indicatorService, lookUpValueService, 0, 0, tabControlMain);
        }

        private void IndicatorValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorValueForm(indicatorValueService, indicatorService, claimOnSystemService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
