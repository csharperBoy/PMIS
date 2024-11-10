using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMIS.Forms
{
    public partial class TabControlWithCloseTab : TabControl
    {
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            for (int i = 0; i < TabCount; i++)
            {
                var tabRect = GetTabRect(i);
                var closeButtonRect = new Rectangle(tabRect.Left, tabRect.Top, (tabRect.Right - tabRect.Left), (tabRect.Bottom - tabRect.Top));
                if (closeButtonRect.Contains(e.Location))
                {
                    // بستن تب  
                    TabPages.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
