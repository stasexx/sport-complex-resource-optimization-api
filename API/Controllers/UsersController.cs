using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models;

namespace SportComplexResourceOptimization.Api.Controllers;


public class UserController : BaseController 
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync([FromBody] UserDto register, CancellationToken cancellationToken)
    {
        await _userService.AddUserAsync(register, cancellationToken);
        return Ok();
    }
}