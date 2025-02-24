﻿using AutoMapper;
using Generic.Base.Handler.Map.Contract;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.Indicator;
using PMIS.DTO.LookUpDestination.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationAddRequestDto : GenericAddRequestDto
    {
        public static async Task<TSource> BeforeMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is LookUpDestinationAddRequestDto sourceModel)
            {
                if (destination is Models.LookUpDestination destinationModel)
                {
                    sourceModel.Description = "(before map" + sourceModel.Description;
                }

            }
            return source;
        }
        public static async Task<TDestination> AfterMap<TSource,TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is LookUpDestinationAddRequestDto sourceModel)
            {
                if (destination is Models.LookUpDestination destinationModel)
                {
                    destinationModel.Description = sourceModel.Description + "after map)";
                }

            }
            return destination;
        }
        
        // public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string TableName { get; set; } = null!;

        public string ColumnName { get; set; } = null!;

        public string? Description { get; set; }

        public string? SystemInfo { get; set; }

        public bool? FlgLogicalDelete { get; set; }

      
    }
    public class LookUpDestinationAddResponseDto : GenericAddResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is Models.Indicator sourceModel)
            {
                if (destination is IndicatorAddResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }

        public int Id { get; set; }
    }
}
