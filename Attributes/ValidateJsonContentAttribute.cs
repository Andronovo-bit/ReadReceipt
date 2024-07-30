using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace ReadReceipt.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class ValidateJsonContentAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var jsonContent = context.ActionArguments.TryGetValue("jsonContent", out var jsonContentObj) ? jsonContentObj : null;

            if (jsonContent != null && jsonContent is JsonElement jsonElement)
            {
                var stringContent = jsonElement.GetRawText();
                if (!IsValidJson(stringContent))
                {
                    context.Result = new BadRequestObjectResult("Invalid JSON content.");
                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("JSON content is missing in the request.");
                return;
            }

            await next();
        }

        private bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) return false;
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || // For object
                (strInput.StartsWith("[") && strInput.EndsWith("]")))   // For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Not used in this attribute
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Not used in this attribute
        }
    }

}
