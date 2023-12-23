using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace JWT_UI.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class FilterAttrebute : Attribute, IAuthorizationFilter
    {
        private readonly string _value;
        private readonly string _key;

        public FilterAttrebute(string value, string key="permission")
        {
            _value = value;
            _key = key;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identities.IsNullOrEmpty())
            {
                context.Result =new UnauthorizedResult();
                return;

            }
            var permissionCailm = context.HttpContext.User.Claims
                .FirstOrDefault(c=>c.Type.Equals(_key,StringComparison.OrdinalIgnoreCase)&&
                                             c.Value.Equals(_value , StringComparison.OrdinalIgnoreCase));
            if (permissionCailm == null)
            {
                context.Result = new ForbidResult();
                return;
            }

        }
    }
}
