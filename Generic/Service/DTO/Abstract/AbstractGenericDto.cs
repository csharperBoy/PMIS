using Generic.Service.DTO.Contract;

namespace Generic.Service.DTO.Abstract
{
    public abstract class AbstractGenericRequestDto : IGenericRequestDto
    {
        public abstract Task<bool> BeforeAction();
    }
    public abstract class AbstractGenericResponseDto : IGenericResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public abstract Task<bool> AfterAction();
    }
}
