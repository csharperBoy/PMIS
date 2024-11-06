using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.DTO
{
    public class GenericSearchSortDto
    {
        public string columnName { get; set; } // نام ستون
        public SortDirection direction { get; set; } // جهت مرتب‌سازی

    }

    public enum SortDirection
    {
        Ascending,
        Descending

    }

}
