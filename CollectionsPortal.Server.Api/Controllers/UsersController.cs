using CollectionsPortal.Server.BusinessLayer.Models.User;
using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using CollectionsPortal.Server.DataLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsPortal.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            await userService.RegisterAsync(registerUserDto);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var token = await userService.LoginAsync(loginUserDto);
            return Ok(new { message = "User logged in successfully", token });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("status/{status}")]
        public async Task<IActionResult> SetStatuses([FromBody] List<string> userIds, [FromRoute] int status)
        {
            await userService.SetStatusesAsync(userIds, (Status)status);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete()]
        public async Task<IActionResult> Delete([FromBody] List<string> userIds)
        {
            await userService.DeleteUsersAsync(userIds);
            return Ok();
        }
    }
}