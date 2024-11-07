﻿using Generic.Base.Handler.Map.Contract;

namespace Generic.Base.Handler.Map.Abstract
{
    public abstract class AbstractGenericMapHandler : IGenericMapHandler, IGenericMappingOperationOptions
    {

        //public static async Task<TDestination> StaticMap<TMapper, TSource, TDestination>(TMapper mapper, TSource source)
        //    where TSource : class
        //    where TDestination : class
        //{
        //    try
        //    {
        //        var subclassType = mapper.GetType();
        //        var instance = Activator.CreateInstance(subclassType) as IGenericMapHandler;

        //        if (instance == null)
        //        {
        //            throw new InvalidOperationException("Could not create instance of subclass.");
        //        }

        //        return await instance.Map<TSource, TDestination>(source);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public abstract Task<TDestination> Map<TSource, TDestination>(TSource source, Action<IGenericMappingOperationOptions> opts = null)
            where TSource : class, new()
            where TDestination : class, new();
        //{
        //   await BeforeMap(async (src, dest) =>
        //    {
        //        var mapMethodSrc = typeof(TSource).GetMethod("BeforeMap");
        //        if (mapMethodSrc != null)
        //        {
        //            var result = await (Task<TSource>)mapMethodSrc.Invoke(null, new object[] { src,dest });

        //        }
        //        var mapMethodDest = typeof(TDestination).GetMethod("BeforeMap");
        //        if (mapMethodDest != null)
        //        {
        //            var result = await (Task<TDestination>)mapMethodDest.Invoke(null, new object[] { src, dest });

        //        }
        //    });

        //   await AfterMap(async (src, dest) =>
        //    {
        //        var mapMethodSrc = typeof(TSource).GetMethod("AfterMap");
        //        if (mapMethodSrc != null)
        //        {
        //            var result = await (Task<TSource>)mapMethodSrc.Invoke(null, new object[] { src , dest });

        //        }
        //        var mapMethodDest = typeof(TDestination).GetMethod("AfterMap");
        //        if (mapMethodDest != null)
        //        {
        //            var result = await (Task<TDestination>)mapMethodDest.Invoke(null, new object[] { src, dest });

        //        }
        //    });
        //}
        public abstract Task BeforeMap(Action<object, object> beforeFunction);

        public abstract Task AfterMap(Action<object, object> afterFunction);


        //public delegate Task<TDestination> MappingHandler<TSource, TDestination>(TSource source, TDestination destination);
        //public event MappingHandler<object, object> MappingEvent;

        //public virtual async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //    where TSource : class
        //    where TDestination : class
        //{
        //    try
        //    {
        //        if (MappingEvent != null)
        //        {
        //            await MappingEvent.Invoke(source, destination);
        //        }

        //        return await Task.FromResult(destination);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
