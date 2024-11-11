using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationEditRequestDto
    {
        public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string TableName { get; set; } = null!;

        public string ColumnName { get; set; } = null!;

        public string? Description { get; set; }
    }
    public class LookUpDestinationEditResponseDto
    {
        public string ErrorMessage { get; set; }
    }
}
