using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IquiriumBE.Api.Modules.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("set-role")]
        public async Task<IActionResult> Post()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity!.Name;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Adicionar a role "User" ao usuário
            var roleExists = await _userManager.IsInRoleAsync(user, "User");
            if (!roleExists)
            {
                var result = await _userManager.AddToRoleAsync(user, "User");
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to assign role to user.");
                }
            }

            return Ok($"Role 'User' foi atribuída ao usuário {userName} (ID: {userId}).");
        }
    }
}
