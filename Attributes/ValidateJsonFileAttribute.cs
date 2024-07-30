using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Method, Inherited = true)]
public class ValidateJsonFileAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("file", out var fileObj) && fileObj is IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                context.Result = new BadRequestObjectResult("File is empty or not provided.");
                return;
            }

            if (file.ContentType != "application/json")
            {
                context.Result = new BadRequestObjectResult("Invalid file type. Please provide a JSON file.");
                return;
            }
        }
        else
        {
            context.Result = new BadRequestObjectResult("File is missing in the request.");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
       
    }
}
