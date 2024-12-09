var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = int.MaxValue;
    options.EnableDetailedErrors = true;
});
builder.Services.AddSqlServer<ChatDbContext>(builder.Configuration.GetConnectionString("ChatDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<User, IdentityRole<int>>()
       .AddEntityFrameworkStores<ChatDbContext>()
       .AddUserManager<UserManager<User>>();
builder.Services.AddDefaultCors(builder.Configuration);
builder.Services.AddAndConfigureJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddCustomServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseMiddleware<GenericExceptionHandlerMiddleware>();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>(app.Configuration["SignalR:HubRoute"]!);

app.Run();
