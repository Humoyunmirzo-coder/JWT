using Microsoft.AspNetCore.Mvc.Filters;

namespace JWT_UI.FilterAtrebutte
{
	public class ResponseHeaderAttribute: Attribute , IAsyncResourceFilter
	{
		private readonly string _name;
		private readonly string _value;

		public ResponseHeaderAttribute(string name, string value) =>
			(_name, _value) = (name, value);

		public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
		{
			Console.WriteLine(" Exseption Filter atrebute");
            await next();
            Console.WriteLine(" Exseption Filter atrebute");
			//throw new NotImplementedException();
		}
	}
}
