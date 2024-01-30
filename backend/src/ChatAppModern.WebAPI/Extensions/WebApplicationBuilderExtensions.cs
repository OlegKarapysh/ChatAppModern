namespace ChatAppModern.WebAPI.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddControllers();
        builder.Services.AddSqlServer<ChatDbContext>(builder.Configuration.GetConnectionString("ChatDbModern"));
        builder.Services.AddIdentity<User, IdentityRole<Guid>>()
               .AddEntityFrameworkStores<ChatDbContext>()
               .AddUserManager<UserManager<User>>();
        builder.Services.AddDefaultCors(builder.Configuration);
        builder.Services.AddAndConfigureJwtAuthentication(builder.Configuration);
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCustomServices();
    }
}