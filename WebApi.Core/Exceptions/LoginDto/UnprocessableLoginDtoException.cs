namespace WebApi.Core.Exceptions.LoginDto
{
    public class UnprocessableLoginDtoException : UnprocessableEntityException
    {
        public UnprocessableLoginDtoException(string errors = null) 
            : base($"Entity can't be processed! " 
                  + errors != null ? "Details " + errors : null)
        {
        }
    }
}
