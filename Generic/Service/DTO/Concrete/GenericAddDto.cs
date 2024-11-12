using Generic.Service.DTO.Abstract;

namespace Generic.Service.DTO.Concrete
{
    public class GenericAddRequestDto : AbstractGenericRequestDto
    {
        public override Task<bool> BeforeAction()
        {
            throw new NotImplementedException();
        }
    }
    public class GenericAddResponseDto : AbstractGenericResponseDto
    {
        public override Task<bool> AfterAction()
        {
            throw new NotImplementedException();
        }
    }
}
