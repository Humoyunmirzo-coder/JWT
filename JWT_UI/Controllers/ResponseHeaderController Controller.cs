using JWT_UI.FilterAtrebutte;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_UI.Controllers
{

	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]

	[ResponseHeader("Filter-Header", "Filter Value")]
	
	public class ResponseHeaderController_Controller : Controller
	{
		[HttpGet]	
		public IActionResult Index() =>
	   Content("Examine the response headers using the F12 developer tools.");


		[HttpGet]
		[ResponseHeader("Another-Filter-Header", "Another Filter Value")]
		public IActionResult Multiple() =>
	   Content("Examine the response headers using the F12 developer tools.");

		[HttpGet]	
		public IActionResult Index1()
		{
			return View();
		}
	}
}
