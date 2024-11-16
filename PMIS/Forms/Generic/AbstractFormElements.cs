using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Forms.Generic
{
    public abstract class AbstractFormElements
    {
        public readonly DataGridView dgvFiltersList;
        public readonly DataGridView dgvResultsList;
        public readonly CheckBox chbRecycle;

        public AbstractFormElements(DataGridView _dgvFiltersList, DataGridView _dgvResultsList, CheckBox _chbRecycle)
        {
            dgvFiltersList = _dgvFiltersList;
            dgvResultsList = _dgvResultsList;
            chbRecycle = _chbRecycle;
        }
    }
}
