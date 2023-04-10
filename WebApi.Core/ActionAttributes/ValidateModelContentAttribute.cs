using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Core.Exceptions;

namespace WebApi.Core.ActionAttributes
{
    public class ValidateModelContentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            foreach (var argument in context.ActionArguments)
            {
                if (((argument.Value).GetType().Name).Contains("Dto"))
                {
                    if (argument.Value is null)
                    {
                        throw new BadRequestException("Invalid model, Please fill the required fields.");
                    }
                }
            }

        }

    }
}
