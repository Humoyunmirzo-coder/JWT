using Microsoft.AspNetCore.Mvc.Filters;

namespace JWT_UI.FilterAtrebutte
{

	public class SampleAsyncActionFilter : IAsyncActionFilter , IActionFilter	
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{
			throw new NotImplementedException();
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			throw new NotImplementedException();
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
             Console.WriteLine(" OnActionExecutionAsync ActionExecutingContext Atrebute ");
            await next();
			Console.WriteLine(" OnActionExecutionAsync ActionExecutingContext Atrebute ");
			//throw new NotImplementedException();
		}
	}
}
