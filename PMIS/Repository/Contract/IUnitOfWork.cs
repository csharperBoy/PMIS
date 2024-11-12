using Generic.Repository;
using Generic.Repository.Abstract;
using Generic.Repository.Contract;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClaimUserOnIndicator = PMIS.Models.ClaimUserOnIndicator;
using User = PMIS.Models.User;

namespace PMIS.Repository.Contract
{
    public interface IUnitOfWork
    {
        public PmisContext context { get; set; }
        public AbstractGenericRepository<ClaimUserOnIndicator, PmisContext> claimUserOnIndicatorRepository { get; set; }
        public AbstractGenericRepository<Indicator, PmisContext> indicatorRepository { get; set; }
        public AbstractGenericRepository<IndicatorCategory, PmisContext> indicatorCategoryRepository { get; set; }
        public AbstractGenericRepository<IndicatorValue, PmisContext> indicatorValueRepository { get; set; }
        public AbstractGenericRepository<LookUp, PmisContext> lookUpRepository { get; set; }
        public AbstractGenericRepository<LookUpDestination, PmisContext> lookUpDestinationRepository { get; set; }
        public AbstractGenericRepository<LookUpValue, PmisContext> lookUpValueRepository { get; set; }
        public AbstractGenericRepository<User, PmisContext> userRepository { get; set; }


    }
}
