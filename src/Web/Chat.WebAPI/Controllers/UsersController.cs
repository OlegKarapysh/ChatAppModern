﻿namespace Chat.WebAPI.Controllers;

[ApiController, Authorize, Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public UsersController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IList<UserDto>>> GetAllUsersAsync()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
    {
        return Ok(await _userService.GetUserByIdAsync(_jwtService.GetIdClaim(User)));
    }

    [HttpGet("search")]
    public async Task<ActionResult<UsersPageDto>> SearchUsersPagedAsync(
        [FromQuery] PagedSearchDto searchData)
    {
        return Ok(await _userService.SearchUsersPagedAsync(searchData));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserInfoAsync(UserDto userData)
    {
        await _userService.UpdateUserAsync(userData, _jwtService.GetIdClaim(User));
        return Ok();
    }
}