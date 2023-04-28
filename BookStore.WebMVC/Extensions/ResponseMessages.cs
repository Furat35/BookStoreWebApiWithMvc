using BookStore.WebMVC.Models;
using Newtonsoft.Json;
using WebApi.Core.Models;

namespace BookStore.WebMVC.Extensions
{
    public static class ResponseMessages
    {
        public static ResponseMessage<T> SuccessResponse<T>(string readContent) where T : class
        {
            T content = null;
            if (readContent is not null)
                content = JsonConvert.DeserializeObject<T>(readContent);
            return new ResponseMessage<T>(true, content, null);
        }

        public static ResponseMessage<T> ErrorResponse<T>(string readContent) where T : class
        {
            ErrorDetails error = JsonConvert.DeserializeObject<ErrorDetails>(readContent);
            return new ResponseMessage<T>(false, null, error);
        }
    }
}
