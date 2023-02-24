using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace TravelPlaces.Filters
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            if (httpContext.Session.GetString("username") == null)
            {
                context.Result = new RedirectToActionResult("Login", "login", null);
            }
            base.OnActionExecuting(context);
        }
    }
}