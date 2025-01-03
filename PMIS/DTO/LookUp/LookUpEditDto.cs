﻿using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUp
{
    public class LookUpEditRequestDto : GenericEditRequestDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

    }
    public class LookUpEditResponseDto : GenericEditResponseDto
    {
        public int Id { get; set; }
    }
}
