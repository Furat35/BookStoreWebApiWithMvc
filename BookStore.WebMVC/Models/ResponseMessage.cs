using WebApi.Core.Models;

namespace BookStore.WebMVC.Models
{
    public class ResponseMessage<TContent>
    {
        #region Ctor
        public ResponseMessage(bool isSuccess, TContent content, ErrorDetails errorDetails)
        {
            IsSuccess = isSuccess;
            Content = content;
            ErrorDetails = errorDetails;
        }

        public ResponseMessage()
        {

        }
        #endregion

        public bool IsSuccess { get; set; }
        public TContent Content { get; set; }
        public ErrorDetails ErrorDetails { get; }
    }
}
