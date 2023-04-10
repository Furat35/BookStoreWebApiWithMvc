namespace WebApi.Core.Exceptions.RegisterDto
{
    public class UnprocessableRegisterDtoException : UnprocessableEntityException
    {
        public UnprocessableRegisterDtoException(string errors) : base($"Entity can't be processed! Error: {errors}")
        {
        }
    }
}
