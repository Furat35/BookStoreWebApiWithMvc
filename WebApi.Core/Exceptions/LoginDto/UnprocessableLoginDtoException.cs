namespace WebApi.Core.Exceptions.LoginDto
{
    public class UnprocessableLoginDtoException : UnprocessableEntityException
    {
        public UnprocessableLoginDtoException(string errors) : base($"Entity can't be processed! Error: {errors}")
        {
        }
    }
}
