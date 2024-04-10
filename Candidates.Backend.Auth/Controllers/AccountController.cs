using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Candidates.Backend.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet("/login")]
        public IActionResult Login() =>
            SignIn(new ClaimsPrincipal(
                new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                            new Claim("my_role_claim_extravaganza", "admin")
                        },
                        "cookie",
                        nameType: null,
                        roleType: "my_role_claim_extravaganza"
                    )
                ),
                authenticationScheme: "cookie");
    }
}
