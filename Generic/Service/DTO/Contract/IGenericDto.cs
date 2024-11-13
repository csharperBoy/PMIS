namespace Generic.Service.DTO.Contract
{
    public interface IGenericDto
    {

    }
    public interface IGenericRequestDto : IGenericDto
    {
        Task<bool> BeforeAction();
    }
    public interface IGenericResponseDto : IGenericDto
    {
        Task<bool> AfterAction();
    }
}
