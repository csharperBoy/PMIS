using PMIS.Models;
using PMIS.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claim = PMIS.Models.Claim;
using User = PMIS.Models.User;

namespace PMIS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public PmisContext context { get; set; }
        public IGenericRepository<Claim> claimRepository { get; set; }
        public IGenericRepository<Indicator> indicatorRepository { get; set; }
        public IGenericRepository<IndicatorCategory> indicatorCategoryRepository { get; set; }
        public IGenericRepository<IndicatorValue> indicatorValueRepository { get; set; }
        public IGenericRepository<LookUp> lookUpRepository { get; set; }
        public IGenericRepository<LookUpDestination> lookUpDestinationRepository { get; set; }
        public IGenericRepository<LookUpValue> lookUpValueRepository { get; set; }
        public IGenericRepository<User> userRepository { get; set; }
        public UnitOfWork()
        {
            this.context = new PmisContext();
            this.claimRepository = new GenericRepository<Claim, PmisContext>(context);
            this.indicatorRepository = new GenericRepository<Indicator, PmisContext>(context);
            this.indicatorCategoryRepository = new GenericRepository<IndicatorCategory, PmisContext>(context);
            this.indicatorValueRepository = new GenericRepository<IndicatorValue, PmisContext>(context);
            this.lookUpRepository = new GenericRepository<LookUp, PmisContext>(context);
            this.lookUpDestinationRepository = new GenericRepository<LookUpDestination, PmisContext>(context);
            this.lookUpValueRepository = new GenericRepository<LookUpValue, PmisContext>(context);
            this.userRepository = new GenericRepository<User, PmisContext>(context);
        }
    }
}
