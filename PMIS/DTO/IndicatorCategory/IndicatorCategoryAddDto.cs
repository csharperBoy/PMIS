﻿using Generic.Service.DTO.Concrete;
using PMIS.DTO.ClaimUserOnSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorCategory
{
    public class IndicatorCategoryAddRequestDto : GenericAddRequestDto
    {
      //  public int Id { get; set; }

        public int FkIndicatorId { get; set; }

        public int FkCategoryId { get; set; }

        public string? Description { get; set; }

       // public string? SystemInfo { get; set; }

       // public bool FlgLogicalDelete { get; set; }

      //  public virtual Category FkCategory { get; set; } = null!;

       // public virtual Indicator FkIndicator { get; set; } = null!;
    }
    public class IndicatorCategoryAddResponseDto : GenericAddResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
          where TDestination : class
          where TSource : class
        {
            if (source is Models.IndicatorCategory sourceModel)
            {
                if (destination is IndicatorCategoryAddResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }

    }
}
