namespace ChatAppModern.WebAPI.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        // app.UseAuthentication();
        // app.UseAuthorization();
        app.MapControllers();
    }
}