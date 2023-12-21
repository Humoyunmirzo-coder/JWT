using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middleware
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _exception;
		private readonly Logger<ExceptionHandlerMiddleware> _logger;

		public ExceptionHandlerMiddleware(RequestDelegate exception, Logger<ExceptionHandlerMiddleware> logger)
		{
			_exception = exception;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _exception(context);
			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				
			}
		}
	}
}
