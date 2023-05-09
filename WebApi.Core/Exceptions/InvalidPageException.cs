namespace WebApi.Core.Exceptions
{
    public class InvalidPageException : BadRequestException
    {
        public InvalidPageException() : base("Page does't exist")
        {
        }
    }
}
