using Generic.Service.DTO.Abstract;

namespace Generic.Service.DTO.Concrete
{
    public class GenericDeleteRequestDto : AbstractGenericRequestDto
    {
        public override Task<bool> BeforeAction()
        {
            throw new NotImplementedException();
        }
    }
    public class GenericDeleteResponseDto : AbstractGenericResponseDto
    {
        public override Task<bool> AfterAction()
        {
            throw new NotImplementedException();
        }
    }
}
