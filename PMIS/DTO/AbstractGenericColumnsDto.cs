using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO
{
    public abstract class AbstractGenericColumnsDto
    {
        public abstract Task Initialize(ILookUpValueService lookUpValueService);
    }
    public class GenericColumnsDto : AbstractGenericColumnsDto
    {
        public override Task Initialize(ILookUpValueService lookUpValueService)
        {
            throw new NotImplementedException();
        }
    }
}
