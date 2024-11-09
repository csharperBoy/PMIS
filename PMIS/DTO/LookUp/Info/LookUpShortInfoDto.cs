﻿using Generic.Base.Handler.Map;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUp.Info
{
    public class LookUpShortInfoDto
    {
        public async Task<LookUpShortInfoDto> extraMapFromBaseModel(PMIS.Models.LookUp baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.LookUpDestinationsInfo = await Task.WhenAll(baseModel.LookUpDestinations.Select(d => (new LookUpDestinationTinyInfoDto()).extraMapFromBaseModel(d)).ToList());
            this.LookUpValuesInfo = await Task.WhenAll(baseModel.LookUpValues.Select(v => (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(v)).ToList());
            return this;
        }
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        // public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        public virtual ICollection<LookUpDestinationTinyInfoDto> LookUpDestinationsInfo { get; set; } = new List<LookUpDestinationTinyInfoDto>();

        public virtual ICollection<LookUpValueTinyInfoDto> LookUpValuesInfo { get; set; } = new List<LookUpValueTinyInfoDto>();
    }
}
