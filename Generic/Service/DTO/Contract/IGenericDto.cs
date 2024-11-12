namespace Generic.Service.DTO.Contract
{
    public interface IGenericRequestDto
    {
        Task<bool> BeforeAction();
    }
    public interface IGenericResponseDto
    {
        Task<bool> AfterAction();
    }
}
