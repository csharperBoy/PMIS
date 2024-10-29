using Generic.Base.Handler.SystemException.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.SystemException.Abstract
{
    public abstract class AbstractGenericExceptionHandler : IGenericExceptionHandler
    {
        public async Task<object> AssignExceptionInfoToObject(object obj, Exception ex)
        {
            var errorMessageProperty = typeof(object).GetProperty("ErrorMessage");
            if (errorMessageProperty != null)
            {
                errorMessageProperty.SetValue(obj, ex.Message);
            }
            return await Task.FromResult<object>(obj);
        }
        
    }
}
