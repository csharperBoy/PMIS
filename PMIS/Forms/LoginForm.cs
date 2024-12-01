using Generic.Base;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Helper;
using Generic.Service.DTO.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.ApplicationServices;
using PMIS.Bases;
using PMIS.DTO.Indicator;
using PMIS.DTO.User;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMIS.Forms
{
    public partial class LoginForm : Form
    {

        #region Variables
        private IUserService userService;
        #endregion

        public LoginForm(IUserService _userService)
        {
            InitializeComponent();
            userService = _userService;
        }

        private async void buttonEntry_Click(object sender, EventArgs e)
        {
            await Entry();
        }

        private async void textBoxEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                await Entry();
            }
        }

        private async Task Entry()
        {
            try
            {
                string hashText = Hasher.HasherHMACSHA512.Hash(Helper.Convert.CapitalizeFirstLetter(textBoxUsername.Text.Trim()) + " + " + textBoxPassword.Text);

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
                    Hide();
                    GlobalVariable.username = users.First().UserName;
                    GlobalVariable.userId = users.First().Id;
                    var mainForm = Program.serviceProvider.GetRequiredService<MainForm>();
                    mainForm.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("کاربر با مشخصات وارد شده یافت نشد!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //using (var scope = Program.serviceProvider.CreateScope())
                //{
                //    var mainForm = Program.serviceProvider.GetRequiredService<MainForm>();
                //    mainForm.ShowDialog();
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در احراز هویت: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
