namespace ChatAppModern.WebAPI.Controllers;

[ApiController, Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register"), AllowAnonymous]
    [ProducesResponseType<UserAuthTokensDto>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserAuthTokensDto>> RegisterAsync([FromBody] RegistrationDto registerDto)
    {
        // TODO: replace with CreatedAtAction().
        return this.OkObjectResultOrProblem(await _authService.RegisterAsync(registerDto));
    }

    [HttpPost("login"), AllowAnonymous]
    [ProducesResponseType<UserAuthTokensDto>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserAuthTokensDto>> LoginAsync([FromBody] LoginDto loginDto)
    {
        return this.OkObjectResultOrProblem(await _authService.LoginAsync(loginDto));
    }

    [HttpPost("refresh"), AllowAnonymous]
    [ProducesResponseType<UserAuthTokensDto>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<UserAuthTokensDto>> RefreshTokensAsync([FromBody] AuthTokensDto tokens)
    {
        return this.OkObjectResultOrProblem(await _authService.RefreshTokenPairAsync(tokens));
    }
}