using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Repository;
using Generic.Repository.Abstract;
using Generic.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using PMIS.Models;
using PMIS.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimUserOnIndicator = PMIS.Models.ClaimUserOnIndicator;
using User = PMIS.Models.User;

namespace PMIS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
       
        public  PmisContext context { get; set; } 
        public AbstractGenericRepository<ClaimUserOnIndicator, PmisContext> claimUserOnIndicatorRepository { get; set; }  
        public AbstractGenericRepository<Indicator, PmisContext> indicatorRepository { get; set; }                        
        public AbstractGenericRepository<IndicatorCategory, PmisContext> indicatorCategoryRepository { get; set; }        
        public AbstractGenericRepository<IndicatorValue, PmisContext> indicatorValueRepository { get; set; }              
        public AbstractGenericRepository<LookUp, PmisContext> lookUpRepository { get; set; }                              
        public AbstractGenericRepository<LookUpDestination, PmisContext> lookUpDestinationRepository { get; set; }        
        public AbstractGenericRepository<LookUpValue, PmisContext> lookUpValueRepository { get; set; }                    
        public AbstractGenericRepository<User, PmisContext> userRepository { get; set; } 
        public UnitOfWork(PmisContext _context,
            AbstractGenericRepository<ClaimUserOnIndicator, PmisContext> _claimUserOnIndicatorRepository,
            AbstractGenericRepository<Indicator, PmisContext> _indicatorRepository,
            AbstractGenericRepository<IndicatorCategory, PmisContext> _indicatorCategoryRepository,
            AbstractGenericRepository<IndicatorValue, PmisContext> _indicatorValueRepository,
                AbstractGenericRepository<LookUp, PmisContext> _lookUpRepository,
                AbstractGenericRepository<LookUpDestination, PmisContext> _lookUpDestinationRepository,
            AbstractGenericRepository<LookUpValue, PmisContext> _lookUpValueRepository,
                    AbstractGenericRepository<User, PmisContext> _userRepository
            )
        {
            this.context = _context;
            this.claimUserOnIndicatorRepository = _claimUserOnIndicatorRepository;
            this.indicatorRepository = _indicatorRepository;
            this.indicatorCategoryRepository = _indicatorCategoryRepository;
            this.indicatorValueRepository = _indicatorValueRepository;
            this.lookUpRepository = _lookUpRepository;
            this.lookUpDestinationRepository = _lookUpDestinationRepository;
            this.lookUpValueRepository = _lookUpValueRepository;
            this.userRepository = _userRepository;
        }
        //public UnitOfWork()
        //{
        //    this.context = new PmisContext();
        //    GenericExceptionHandler exe = new GenericExceptionHandler();
        //    GenericLogWithSerilogInFileHandler logh = new GenericLogWithSerilogInFileHandler(
        //            new Generic.Base.Handler.SystemLog.WithSerilog.DTO.GenericConfigureLogWithSerilogRequestDto() 
        //            { 
        //                inFileConfig = new Generic.Base.Handler.SystemLog.WithSerilog.DTO.GenericConfigureLogWithSerilogInFileRequestDto() 
        //                { filePath= "C:\\Users\\868\\source\\repos\\PMIS\\PMIS\\bin\\Debug\\net8.0-windows\\logs\\log20241108.txt" },
        //                logHandlerType = GenericLogWithSerilogHandlerFactory.LogHandlerType.File,
        //                minimumLevel = Serilog.Events.LogEventLevel.Information,
        //                rollingInterval = Serilog.RollingInterval.Hour,
        //            }
        //        );
        //    this.claimUserOnIndicatorRepository = new GenericSqlServerRepository<Models.ClaimUserOnIndicator, PmisContext>(this.context, exe, logh);
        //    this.indicatorRepository            = new GenericSqlServerRepository<Models.Indicator, PmisContext>(this.context, exe, logh);
        //    this.indicatorCategoryRepository    = new GenericSqlServerRepository<Models.IndicatorCategory, PmisContext>(this.context, exe, logh);
        //    this.indicatorValueRepository       = new GenericSqlServerRepository<Models.IndicatorValue, PmisContext>(this.context, exe, logh);
        //    this.lookUpRepository               = new GenericSqlServerRepository<Models.LookUp, PmisContext>(this.context, exe, logh);
        //    this.lookUpDestinationRepository    = new GenericSqlServerRepository<Models.LookUpDestination, PmisContext>(this.context, exe, logh);
        //    this.lookUpValueRepository          = new GenericSqlServerRepository<Models.LookUpValue, PmisContext>(this.context, exe, logh);
        //    this.userRepository                 = new GenericSqlServerRepository<Models.User, PmisContext>(this.context, exe, logh);
        //}
    }
}

