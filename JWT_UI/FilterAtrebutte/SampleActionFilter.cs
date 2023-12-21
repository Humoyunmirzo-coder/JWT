using Microsoft.AspNetCore.Mvc.Filters;

namespace JWT_UI.FilterAtrebutte
{

	public class SampleActionFilter : IActionFilter, IAuthorizationFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{
			throw new NotImplementedException();
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			throw new NotImplementedException();
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			throw new NotImplementedException();
		}
	}
}
