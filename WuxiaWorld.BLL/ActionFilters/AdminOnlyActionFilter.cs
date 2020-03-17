namespace WuxiaWorld.BLL.ActionFilters {

    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class AdminOnlyActionFilter : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {

            try {

                var userRole = context.HttpContext.User.FindFirst(ClaimTypes.Role).Value.ToLower();

                if (userRole.ToLowerInvariant() != "admin") {

                    context.Result = new UnauthorizedResult();
                }
            }
            catch {

                context.Result = new UnauthorizedResult();
            }
        }
    }

}