using Generic.Base.Handler.Map;
using Generic.Service.DTO.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.ApplicationServices;
using PMIS.Bases;
using PMIS.DTO.ClaimUserOnSystem;
using PMIS.DTO.IndicatorValue;
using PMIS.DTO.User;
using PMIS.Models;
using PMIS.Services;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMIS.Forms
{
    public partial class ChangePasswordForm : Form
    {

        #region Variables
        private IClaimOnSystemService claimOnSystemService;
        private IUserService userService;
        #endregion

        public ChangePasswordForm(IClaimOnSystemService _claimOnSystemService, IUserService _userService)
        {
            InitializeComponent();
            claimOnSystemService = _claimOnSystemService;
            userService = _userService;
            CustomInitialize();
        }

        private async void CustomInitialize()
        {
            if (!await CheckSystemClaimsRequired())
            {
                MessageBox.Show("باعرض پوزش شما دسترسی به این قسمت را ندارید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispose();
            }
            else
            {
                ShowDialog();
            }
        }

        private async Task<bool> CheckSystemClaimsRequired()
        {
            try
            {
                IEnumerable<ClaimUserOnSystemSearchResponseDto> claims = await claimOnSystemService.GetCurrentUserClaims();
                if (!claims.Any(c => c.FkLkpClaimUserOnSystemInfo.Value == "ChangePasswordForm"))
                {
                    Close();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string hashText = Hasher.HasherHMACSHA512.Hash(GlobalVariable.username + " + " + textBoxCurrentPassword.Text);

                (bool result, IEnumerable<UserSearchResponseDto> users) = await userService.Search(new GenericSearchRequestDto()
                {
                    filters = new List<GenericSearchFilterDto>()
                    {
                        new GenericSearchFilterDto()
                        {
                            columnName = "PasswordHash",
                            value = hashText,
                            LogicalOperator=LogicalOperator.Begin,
                            operation = FilterOperator.Equals,
                            type = PhraseType.Condition
                        }
                    }
                });

                if (result && users.Count() == 1)
                {
                    if (textBoxNewPassword.Text == textBoxRepeatNewPassword.Text)
                    {
                        string newPasswordHash = Hasher.HasherHMACSHA512.Hash(GlobalVariable.username + " + " + textBoxNewPassword.Text);
                        UserEditRequestDto userEditRequest = new UserEditRequestDto();
                        await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(users.First(), userEditRequest);
                        userEditRequest.PasswordHash = newPasswordHash;
                        bool isSuccess = await userService.EditRange(new List<UserEditRequestDto>() { userEditRequest });
                        if (isSuccess)
                        {
                            MessageBox.Show("عملیات موفقیت‌آمیز بود!!!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("عملیات تغییر گذرواژه موفقیت‌آمیز نبود!!!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("گذرواژه جدید را به درستی تکرار کنید!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("گذرواژه فعلی به درستی وارد نشده است!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("تغییر گذرواژه امکان‌پذیر نمی‌باشد!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
