using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using Generic.Base.Handler.SystemException;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Service.DTO;
using Generic.Repository;
using Generic.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Models;
using PMIS.Repository;
using PMIS.Services;
using PMIS.Services.Contract;
using Serilog.Core;
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
    public partial class MainForm : Form
    {
        IIndicatorService indicatorService;
        ILookUpService lookUpService;
        ILookUpValueService lookUpValueService;
        ILookUpDestinationService lookUpDestinationService;
        private Serilog.ILogger logHandler;
        public MainForm(IIndicatorService _indicatorService, ILookUpService _lookUpService, ILookUpValueService _lookUpValueService, ILookUpDestinationService _lookUpDestinationService, AbstractGenericLogWithSerilogHandler _logHandler)
        {

            InitializeComponent();
            indicatorService = _indicatorService;
            lookUpService = _lookUpService;
            lookUpValueService = _lookUpValueService;
            lookUpDestinationService = _lookUpDestinationService;
            logHandler = _logHandler.CreateLogger();
        }

        private void براساسفرمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnForm, new IndicatorValueDataEntryOnForm());
        }

        private void براساستاریخToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnDate, new IndicatorValueDataEntryOnDate());
        }

        private void براساسشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnIndicator, new IndicatorValueDataEntryOnIndicator());
        }

        private void تعریفکاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnIndicator, new UserDataEntry());
        }

        private void دسترسیهایکاربرانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  AddNewTabPage(tabPageOnIndicator, new UserDataEntry());
        }

        private void شناسنامهشاخصهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageNormalForm, new NormalForm(indicatorService, lookUpValueService));
        }

        private void تخصیصدستهبندیبهشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصشاخصبهدستهبندیToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تخصیصکاربربهشاخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabPage(tabPageOnIndicator, new FormClaimUserOnIndicator());
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
