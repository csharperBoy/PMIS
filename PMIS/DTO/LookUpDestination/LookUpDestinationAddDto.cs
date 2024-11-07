﻿using Generic.Base.Handler.Map.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationAddRequestDto
    {
        public static async Task<Models.LookUpDestination> AfterMap(LookUpDestinationAddRequestDto src, Models.LookUpDestination dest)
        {
            dest.Description = "test des";
            return dest;
        }
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
        public static async Task<LookUpDestinationAddResponseDto> AfterMap(Models.LookUpDestination src , LookUpDestinationAddResponseDto dest)
        {
            dest.ErrorMessage = "test des";          
            return dest;
        }

      
        public string ErrorMessage { get; set; }
    }
}
