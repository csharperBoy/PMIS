using Generic.Base.Handler.SystemException.Contract;

namespace Generic.Base.Handler.SystemException.Abstract
{
    public abstract class AbstractGenericExceptionHandler : IGenericExceptionHandler
    {
        private Stack<Exception> exceptions = new Stack<Exception>();
        public async Task<object> AssignExceptionInfoToObject(object obj, Exception ex)
        {
            var errorMessageProperty = obj.GetType().GetProperty("ErrorMessage");
            if (errorMessageProperty != null)
            {
                errorMessageProperty.SetValue(obj, ex.Message);
            }
            return await Task.FromResult<object>(obj);
        }

        public virtual Task HandleException(Exception ex)
        {
            exceptions.Push(ex);
            return Task.CompletedTask;
        }

        public Stack<Exception> GetStack()
        {
            return exceptions;
        }

        public Exception PopException()
        {
            return exceptions.Pop();
        }

        public void PushException(Exception ex)
        {
            exceptions.Push(ex);
        }
    }
}