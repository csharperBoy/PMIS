using Generic.Service.DTO.Contract;

namespace Generic.Service.DTO.Abstract
{
    public abstract class AbstractGenericDto : IGenericDto
    {

    }
    public abstract class AbstractGenericRequestDto : AbstractGenericDto, IGenericRequestDto
    {
        public abstract Task<bool> BeforeAction();
    }
    public abstract class AbstractGenericResponseDto : AbstractGenericDto, IGenericResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public abstract Task<bool> AfterAction();
    }
}
