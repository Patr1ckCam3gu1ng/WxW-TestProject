namespace WuxiaWorld.BLL.ActionFilters {

    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class AdminOnlyActionFilter : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {

            try {

                // INFO:  The admin user is the only one who can write to the repo. All other
                // INFO:    users can only list the contents.

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