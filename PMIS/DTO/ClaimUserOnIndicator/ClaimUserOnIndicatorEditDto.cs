﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimUserOnIndicator
{
    public class ClaimUserOnIndicatorEditRequestDto
    {
         public int Id { get; set; }

        public int FkLkpClaimUserOnIndicatorId { get; set; }

        public int FkUserId { get; set; }

        public int FkIndicatorId { get; set; }

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        //public virtual Indicator FkIndicator { get; set; } = null!;

        //public virtual LookUpValue FkLkpClaimUserOnIndicator { get; set; } = null!;

        //public virtual User FkUser { get; set; } = null!;
    }
    public class ClaimUserOnIndicatorEditResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}