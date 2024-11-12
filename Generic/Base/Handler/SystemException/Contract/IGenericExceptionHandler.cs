namespace Generic.Base.Handler.SystemException.Contract
{
    internal interface IGenericExceptionHandler
    {
        Task<object> AssignExceptionInfoToObject(object responseTemp, Exception ex);

        Task HandleException(Exception ex);

        void PushException(Exception ex);

        Exception PopException();

        Stack<Exception> GetStack();
    }
}
