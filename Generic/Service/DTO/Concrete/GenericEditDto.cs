using Generic.Service.DTO.Abstract;

namespace Generic.Service.DTO.Concrete
{
    public class GenericEditRequestDto : AbstractGenericRequestDto
    {
        public override Task<bool> BeforeAction()
        {
            throw new NotImplementedException();
        }
    }
    public class GenericEditResponseDto : AbstractGenericResponseDto
    {
        public override Task<bool> AfterAction()
        {
            throw new NotImplementedException();
        }
    }
}
