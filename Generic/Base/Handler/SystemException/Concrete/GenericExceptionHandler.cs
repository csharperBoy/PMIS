using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemException.Contract;

namespace Generic.Base.Handler.SystemException.Concrete
{
    public class GenericExceptionHandler : AbstractGenericExceptionHandler
    {
        public override Task HandleException(Exception ex)
        {
            return base.HandleException(ex);
        }
    }
}
