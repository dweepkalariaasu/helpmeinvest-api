using helpmeinvest.Models;
using helpmeinvest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace helpmeinvest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService service)
        {
            _loginService = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Credential cred)
        {
            IActionResult response = Unauthorized();
            var user = _loginService.AuthenticateUser(cred);

            if (user != null)
            {
                var tokenString = _loginService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult Create([FromBody] Credential cred)
        {
            IActionResult response = Unauthorized();
            var claims = HttpContext.User;

            if (claims != null)
            {
                var tokenString = _loginService.CreateCredentials(cred, claims);
                if (!string.IsNullOrEmpty(tokenString))
                {
                    response = Ok(new { token = tokenString });
                }
            }

            return response;
        }
    }
}
