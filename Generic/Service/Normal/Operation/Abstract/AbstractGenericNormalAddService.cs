﻿using AutoMapper;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Repository;
using Generic.Repository.Abstract;
using Generic.Repository.Contract;
using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Abstract
{
    public abstract class AbstractGenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        : IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
    {        
        private AbstractGenericRepository<TEntity,TContext> repository;
        private AbstractGenericMapHandler mapper;
        private AbstractGenericExceptionHandler exceptionHandler;
        protected AbstractGenericNormalAddService(
            AbstractGenericRepository<TEntity, TContext> _repository,
            AbstractGenericMapHandler _mapper,
            AbstractGenericExceptionHandler _exceptionHandler
            )
        {
            repository = _repository;
            mapper = _mapper;
            exceptionHandler = _exceptionHandler;
            //mapper.ExtraMap += ExtraMap<>();
        }
        //protected virtual async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //    where TSource : class
        //    where TDestination : class
        //{
        //    try
        //    {
        //        return await mapper.ExtraMap(source, destination);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public async Task<(bool, IEnumerable<TEntityAddResponseDto>)> AddGroup(IEnumerable<TEntityAddRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                bool resultIsSuccess = true;
                bool result = true;
                List<TEntityAddResponseDto> results = new List<TEntityAddResponseDto>();
                foreach (var req in requestInput)
                {
                    TEntity entity = null;
                    try
                    {
                        entity = await mapper.Map<TEntityAddRequestDto, TEntity>(req);

                        result = await repository.InsertAsync(entity);
                        await repository.SaveAsync();
                    }
                    catch (Exception ex)
                    {
                        TEntityAddResponseDto responseTemp = await mapper.Map<TEntity, TEntityAddResponseDto>(entity);


                        responseTemp = (TEntityAddResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
                        results.Add(responseTemp);
                    }

                    if (!result)
                        resultIsSuccess = false;


                    results.Add(await mapper.Map<TEntity, TEntityAddResponseDto>(entity));
                }
                await repository.CommitAsync();
                return (resultIsSuccess, results);
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> requestInput)
        {
            throw new NotImplementedException();
        }



    }
}