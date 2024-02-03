namespace ChatAppModern.WebAPI.Controllers;

[ApiController, Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public AuthController(IAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("register"), AllowAnonymous]
    [ProducesResponseType<UserAuthTokensDto>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserAuthTokensDto>> RegisterAsync([FromBody] RegistrationDto registerDto)
    {
        var registrationResult = await _authService.RegisterAsync(registerDto);
        // TODO: create a method to extract common logic for matching success/error results.
        return registrationResult.IsFailed
            ? this.ProblemFromFailedResult(registrationResult)
            : Ok(registrationResult.Value); // TODO: replace with CreatedAtAction().
    }

    [HttpPost("login"), AllowAnonymous]
    [ProducesResponseType<UserAuthTokensDto>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserAuthTokensDto>> LoginAsync([FromBody] LoginDto loginDto)
    {
        var loginResult = await _authService.LoginAsync(loginDto);
        return loginResult.IsFailed
            ? this.ProblemFromFailedResult(loginResult)
            : Ok(loginResult.Value);
    }

    [HttpPost("refresh"), AllowAnonymous]
    [ProducesResponseType<UserAuthTokensDto>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<UserAuthTokensDto>> RefreshTokensAsync([FromBody] AuthTokensDto tokens)
    {
        var refreshResult = await _authService.RefreshTokenPairAsync(tokens);
        return refreshResult.IsFailed
            ? this.ProblemFromFailedResult(refreshResult)
            : Ok(refreshResult.Value);
    }
}