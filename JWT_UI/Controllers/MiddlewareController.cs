using Aplication.Services;
using Domain.Entity;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_UI.Controllers
{

	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class MiddlewareController : Controller
	{
		private readonly IIdentityServise _identityServise;
		private readonly ILogger<User> _logger;
		private readonly IdentityDbContext _dbContext				;
		public MiddlewareController(IIdentityServise identityServise, ILogger<User> logger, IdentityDbContext dbContext)
		{
			_identityServise = identityServise;
			_logger = logger;
			_dbContext = dbContext;
		}

		public IIdentityServise IdentityServise => _identityServise;

		[HttpGet]
		public IActionResult GetMiddleware()
		{
			try
			{
				_logger.LogInformation("Get Middle Detales ");
				var result = _dbContext.GetMiddle();
				if (result == null)
					throw new Exception(" Getting Error");
				return Ok(result);
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return BadRequest(" Ineter server error");

			}
		}

		[HttpPost]
		public async void  Intersepter()
		{
			await  _dbContext.SaveChangesAsync();
		}
	}

}
