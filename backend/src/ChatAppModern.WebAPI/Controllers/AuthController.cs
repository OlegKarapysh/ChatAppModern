namespace ChatAppModern.WebAPI.Controllers;

[ApiController, Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly ChatDbContext _dbContext;

    public AuthController(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> Test()
    {
        _dbContext.Database.EnsureCreated();
        return Ok();
    }
}