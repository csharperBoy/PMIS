using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Base.Handler.Log.Contract;
using Generic.Base.Handler.Map.Contract;
using Generic.Base.Handler.SystemException.Contract;

namespace Generic.Base.General.Contract
{
    internal interface IGeneric : IGenericMapHandler , IGenericLogHandler , IGenericExceptionHandler
    {
    }
}
