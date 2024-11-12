using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpValue
{
    public class LookUpValueEditRequestDto : GenericEditRequestDto
    {
        public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string Value { get; set; } = null!;

        public string Display { get; set; } = null!;

        public int OrderNum { get; set; }

        public string? Description { get; set; }
    }
    public class LookUpValueEditResponseDto : GenericEditResponseDto
    {
        public int Id { get; set; }
    }
}
