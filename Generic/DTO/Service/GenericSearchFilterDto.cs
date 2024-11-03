using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.DTO.Service
{
    public class GenericSearchFilterDto
    {
        public string columnName { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public List<GenericSearchFilterDto> InternalFilters { get; set; }
        public FilterOperator operation { get; set; }

        public LogicalOperator LogicalOperator { get; set; }

    }
    public enum LogicalOperator
    {
        And,
        Or,
        begin
    }
    public enum FilterOperator
    {
        Equals,
        NotEquals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
}
