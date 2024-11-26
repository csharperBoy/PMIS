using Microsoft.Extensions.DependencyInjection;
using PMIS.Bases;
using PMIS.Models;
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
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void labelUsername_Click(object sender, EventArgs e)
        {

        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string hashText = Hasher.HasherHMACSHA512.Hash(GlobalVariable.username + "+" + textBoxOldPassword.Text);
                using (var context = new PmisContext())
                {
                    context.Database.EnsureCreated();
                    User user = context.Users.Where(x => x.PasswordHash == hashText).First();
                    if (user.Id != 0)
                    {
                        if (textBoxNewPassword.Text == textBoxReNewPassword.Text)
                        {
                            string newPasswordHash = Hasher.HasherHMACSHA512.Hash(GlobalVariable.username + "+" + textBoxNewPassword.Text);
                            user.PasswordHash = newPasswordHash;
                            context.Users.Update(user);
                            context.SaveChanges();
                            context.Dispose();
                            MessageBox.Show("رمز عبور با موفقیت تغییر کرد");
                        }
                        else
                        {
                            MessageBox.Show("رمز عبور جدید را به درستی تکرار کنید");
                        }
                    }
                    else
                    {
                        MessageBox.Show("رمز عبور اشتباه است");
                    }
                };
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("رمز عبور اشتباه است");
              //  MessageBox.Show("Not OK Authentication : " + ex.Message);
            }
        }
    }
}
