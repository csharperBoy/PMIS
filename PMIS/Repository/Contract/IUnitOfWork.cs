using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Claim = PMIS.Models.Claim;
using User = PMIS.Models.User;

namespace PMIS.Repository.Contract
{
    public interface IUnitOfWork
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

    }
}
