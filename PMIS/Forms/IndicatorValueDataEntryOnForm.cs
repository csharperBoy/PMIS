using PMIS.Helper;
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
    public partial class IndicatorValueDataEntryOnForm : Form
    {

        private MultiSelectComboBox multiSelectComboBox;
        public IndicatorValueDataEntryOnForm()
        {
            InitializeComponent();
            this.multiSelectComboBox = new MultiSelectComboBox(); this.multiSelectComboBox.Location = new Point(50, 50); this.multiSelectComboBox.Size = new Size(200, 21); this.multiSelectComboBox.Items.AddRange(new string[] { "Option 1", "Option 2", "Option 3" }); this.Controls.Add(this.multiSelectComboBox); this.ClientSize = new Size(300, 150); this.Text = "Multi-Select ComboBox Example";

        }

        private void IndicatorValueDataEntryOnForm_Load(object sender, EventArgs e)
        {

        }
    }
}
