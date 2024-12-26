using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using PMIS.DTO.ClaimUserOnSystem;
using PMIS.DTO.User;
using PMIS.Services.Contract;
using System.Windows.Forms;

namespace PMIS.Forms
{
    public partial class MainForm : Form
    {
        IIndicatorService indicatorService;
        ICategoryService categoryService;
        IIndicatorCategoryService indicatorCategoryService;
        IUserService userService;
        ILookUpService lookUpService;
        ILookUpValueService lookUpValueService;
        IIndicatorValueService indicatorValueService;
        ILookUpDestinationService lookUpDestinationService;
        IClaimUserOnIndicatorService claimUserOnIndicatorService;
        private IClaimUserOnSystemService claimUserOnSystemService;
        private Serilog.ILogger logHandler;
        public MainForm(IIndicatorService _indicatorService, ICategoryService _categoryService, IIndicatorCategoryService _indicatorCategoryService, IClaimUserOnSystemService _claimUserOnSystemService, IIndicatorValueService _indicatorValueService, IUserService _userService, ILookUpService _lookUpService, ILookUpValueService _lookUpValueService, ILookUpDestinationService _lookUpDestinationService, IClaimUserOnIndicatorService _claimUserOnIndicatorService, AbstractGenericLogWithSerilogHandler _logHandler)
        {
            InitializeComponent();
            claimUserOnSystemService = _claimUserOnSystemService;
            indicatorService = _indicatorService;
            indicatorValueService = _indicatorValueService;
            userService = _userService;
            lookUpService = _lookUpService;
            lookUpValueService = _lookUpValueService;
            lookUpDestinationService = _lookUpDestinationService;
            claimUserOnIndicatorService = _claimUserOnIndicatorService;
            indicatorCategoryService = _indicatorCategoryService;
            categoryService = _categoryService;
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
                IEnumerable<ClaimUserOnSystemSearchResponseDto> claims = await claimUserOnSystemService.GetCurrentUserClaims();
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "ChangePasswordForm"))
                {
                    UsersToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "UserForm"))
                {
                    UsersToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "IndicatorForm"))
                {
                    IndicatorsToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "ClaimUserOnIndicatorForm"))
                {
                    ClaimUserOnIndicatorToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "ClaimUserOnSystemForm"))
                {
                    ClaimUserOnSystemToolStripMenuItem.Visible = false;
                }
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "IndicatorValueForm"))
                {
                    IndicatorValueToolStripMenuItem.Visible = false;
                }
                else
                {
                    new IndicatorValueForm(indicatorValueService, indicatorService, claimUserOnSystemService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangePasswordForm(claimUserOnSystemService, userService);
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserForm(userService, claimUserOnSystemService, claimUserOnIndicatorService, indicatorService, lookUpValueService, tabControlMain);
        }

        private void IndicatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorForm(indicatorService, claimUserOnSystemService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
        }

        private void ClaimUserOnIndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClaimUserOnIndicatorForm(claimUserOnSystemService, claimUserOnIndicatorService, userService, indicatorService, lookUpValueService, 0, 0, tabControlMain);
        }

        private void ClaimUserOnSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClaimUserOnSystemForm(claimUserOnSystemService, userService, indicatorService, lookUpValueService, 0, 0, tabControlMain);
        }

        private void IndicatorValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorValueForm(indicatorValueService, indicatorService, claimUserOnSystemService, claimUserOnIndicatorService, userService, lookUpValueService, tabControlMain);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IndicatorCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IndicatorCategoryForm(indicatorCategoryService,  claimUserOnSystemService, indicatorService, categoryService,0,0, tabControlMain);

        }
    }
}
