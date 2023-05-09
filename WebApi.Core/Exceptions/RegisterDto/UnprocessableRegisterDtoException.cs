namespace WebApi.Core.Exceptions.RegisterDto
{
    public class UnprocessableRegisterDtoException : UnprocessableEntityException
    {
        public UnprocessableRegisterDtoException(string errors = null)
            : base($"Entity can't be processed! " + (errors != null ? "Details " + errors : null))
        {
        }
    }
}
