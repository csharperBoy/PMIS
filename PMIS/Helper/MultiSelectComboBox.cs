using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Helper
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Linq;

    public class MultiSelectComboBox : ComboBox
    {
        private CheckedListBox _checkedListBox;
        private bool _isDroppedDown;

        public MultiSelectComboBox()
        {
            _checkedListBox = new CheckedListBox();
            _checkedListBox.BorderStyle = BorderStyle.None;
            _checkedListBox.CheckOnClick = true;
            _checkedListBox.ItemCheck += CheckedListBox_ItemCheck;

            this.DropDownHeight = 1;
            this.DropDown += MultiSelectComboBox_DropDown;
            this.DropDownClosed += MultiSelectComboBox_DropDownClosed;
            this.LostFocus += MultiSelectComboBox_LostFocus;
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            if (_isDroppedDown) return;
            _isDroppedDown = true;

            _checkedListBox.Location = new Point(this.Left, this.Top + this.Height);
            _checkedListBox.Size = new Size(this.Width, 100);
            _checkedListBox.Items.Clear();
            foreach (var item in this.Items)
            {
                _checkedListBox.Items.Add(item, false);
            }

            this.Parent.Controls.Add(_checkedListBox);
            _checkedListBox.BringToFront();
            _checkedListBox.Focus();
        }

        private void MultiSelectComboBox_DropDown(object sender, EventArgs e)
        {
            _checkedListBox.Visible = true;
        }

        private void MultiSelectComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (_checkedListBox.CheckedItems.Count > 0)
            {
                this.Text = string.Join(", ", _checkedListBox.CheckedItems.Cast<string>());
            }
            else
            {
                this.Text = string.Empty;
            }

            _checkedListBox.Visible = false;
            _isDroppedDown = false;
        }

        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                _checkedListBox.Visible = true;
                this.Focus();
            });
        }

        private void MultiSelectComboBox_LostFocus(object sender, EventArgs e)
        {
            if (!_checkedListBox.Focused)
            {
                _checkedListBox.Visible = false;
                _isDroppedDown = false;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (!_checkedListBox.Focused)
            {
                _checkedListBox.Visible = false;
                _isDroppedDown = false;
            }
        }
    }

}
