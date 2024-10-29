using AutoMapper;
using Generic.Base.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base
{
    public abstract class GenericMapper : IGenericMapper        
    {
        public async Task<TDestination> Map<TSource, TDestination>(TSource source)
           where TDestination : class
           where TSource : class
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
            var mapper = new Mapper(config);
            TDestination destination = mapper.Map<TDestination>(source);
            destination = await ExtraMap(destination, source);
            return destination;
        }
        protected virtual async Task<TDestination> ExtraMap<TDestination, TSource>(TDestination destination, TSource source)
            where TDestination : class
            where TSource : class
        {
            return destination;
        }
        //public async Task<TEntityDestination> Map(TEntitySource source)
        //{
        //    try
        //    {
        //        TEntityDestination destination = null ;
        //        var sourceFields = source.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        //        var destinationFields = destination.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        //        //var matchingFields = fields1.Select(f => f.Name).Intersect(fields2.Select(f => f.Name)).Count();

        //        //return matchingFields;

        //        return destination;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
