using Application.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeleperformanceTask.Auth;

namespace TeleperformanceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService userService) : ControllerBase
    {
        [HttpPost]
        [Route("Auth")]
        public Task<RetuenAuth> AuthenticateUser(AuthenticationRequest request)
        {
            return userService.AuthenticateUser(request);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<bool> RegisterUser(RegisterationRequest request, CancellationToken cancellationToken)
        {
            return await userService.RegisterUser(request, cancellationToken);
        }
    }
}
