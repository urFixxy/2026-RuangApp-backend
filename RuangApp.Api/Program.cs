using Microsoft.EntityFrameworkCore;
using RuangApp.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RuangAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RuangAppContext") ?? throw new InvalidOperationException("Connection string 'RuangAppContext' not found.")));

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RuangAppContext>();
    context.Database.Migrate();
    Seeder.SeedData(context);
}

app.MapControllers();

app.Run();
