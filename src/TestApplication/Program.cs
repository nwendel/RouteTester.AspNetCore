var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(x =>
{
    x.MapControllers();
});

app.Run();
