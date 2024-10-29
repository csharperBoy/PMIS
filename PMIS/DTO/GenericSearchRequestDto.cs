using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO
{
    public class GenericSearchRequestDto
    {
        public int pageNumber { get; set; } = 0;
        public int recordNumber { get; set; } = 0;
    }
    public enum ConditionOperator
    {
        AND,
        OR,
    }
}
