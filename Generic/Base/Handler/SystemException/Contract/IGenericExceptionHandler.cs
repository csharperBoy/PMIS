using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Generic.Base.Handler.SystemException.Contract
{
    internal interface IGenericExceptionHandler
    {
        Task<object> AssignExceptionInfoToObject(object responseTemp, Exception ex);
        //Task HandleException(Exception ex);

    }
}
