using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationAddRequestDto
    {
        // public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string TableName { get; set; } = null!;

        public string ColumnName { get; set; } = null!;

        public string? Description { get; set; }

        public string? SystemInfo { get; set; }

        public bool? FlgLogicalDelete { get; set; }

      
    }
    public class LookUpDestinationAddResponseDto
    {
        public static async Task<LookUpDestinationAddResponseDto> map(LookUpDestinationAddRequestDto y)
        {
            // منطق متد Map  
            await Task.Delay(1000); // شبیه‌سازی عملیات asynchronous  
            Console.WriteLine("Mapping executed.");
            return new LookUpDestinationAddResponseDto() { ErrorMessage = y.Description };
        }
        public string ErrorMessage { get; set; }
    }
}
