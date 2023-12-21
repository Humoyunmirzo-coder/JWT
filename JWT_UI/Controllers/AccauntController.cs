using Aplication.Services;
using Domain.Entity;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace JWT_UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccauntController : ControllerBase
    {
        private readonly IIdentityServise     _identityServise;
		private readonly ILogger <User> _logger;

		public AccauntController(IIdentityServise identityServise, ILogger<User> logger)
		{
			_identityServise = identityServise;
			_logger = logger;
		}

			


		[HttpPost]
        [AllowAnonymous]
    public async Task<Response<GetUserModel>> Register(User user)
            => await _identityServise.RegisterAsync(user);

        [HttpPost]
        [AllowAnonymous]    
         public Task<Response<Token>>Login (Credential credential)
            =>_identityServise.LoginAsync(credential);

        [HttpGet]
        public async Task<Response<bool>> Logout ()
            =>await _identityServise.LogoutAsync();

        [HttpDelete]
        public async Task<Response<bool>> Delete (int userId)
            =>await _identityServise.DeleteUserAsync(userId);

        [HttpGet]
        [Authorize(Roles ="Teacher")]
        public string[] GetAllStudents()
        {
            return new string[] { "Ali", "Zubayr" };
        }
        [HttpGet]
        public string GetHtmlContent()
        {
            string html = @"<!DOCTYPE html>
                                       <html>
                                      <head>
                                      <title>Page Title</title>
                                      </head>
                                      <body>
                                      <h1>This is a Heading</h1>
                                      <p>This is a paragraph.</p>
                                      </body>
                                      </html>";
            return html;
        }

    }
}
