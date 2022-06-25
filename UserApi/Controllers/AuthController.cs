using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserApi.Service;

namespace UserApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
    {
        if (request is null)
            return BadRequest("Provide User Name and Password");

        var token = await _userService.VaildateUser(request.UserName, request.Password);

        if(token == null)
            return BadRequest("Invalid Username or Password");

        return Ok(token);
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var id = HttpContext.User.FindFirstValue("id");
        await Task.CompletedTask;
        return Ok(id);
    }
}

public class AuthRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}