using Generic.Base;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using PMIS.Bases;
using PMIS.DTO.Indicator;
using PMIS.Models;
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
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
           // IndicatorAddRequestDto a = await GenericMapHandler.StaticMap<AutoMapHandler,Indicator, IndicatorAddRequestDto>(new AutoMapHandler(), new Indicator() { Code = "Ali"});
            new PMIS.Bases.Initializer().Initialize();
        }

        private void buttonEntry_Click(object sender, EventArgs e)
        {
            try
            {
                //string hashText = Hasher.HasherHMACSHA512.Hash(textBoxUsername.Text + "+" + textBoxPassword.Text);
                //using (var context = new PmisContext()) 
                //{
                //    context.Database.EnsureCreated();
                //    if (context.Users.Where(x => x.PasswordHash == hashText).First().Id != 0)
                //    {
                //        GlobalVariable.username = context.Users.Where(x => x.PasswordHash == hashText).First().UserName;

                //    }
                //    else
                //    {
                //        return ;
                //    }
                //    context.Dispose();
                //};
                GlobalVariable.username = "Admin";
                this.Hide();
                (new MainForm()).ShowDialog();
                this.Close();

            } 
            catch (Exception)
            {
                MessageBox.Show("Not OK Authentication");
            }
        }
    }
}
