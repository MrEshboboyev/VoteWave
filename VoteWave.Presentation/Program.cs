using VoteWave.Application;
using VoteWave.Infrastructure;
using VoteWave.Infrastructure.Auth.Options;
using VoteWave.Infrastructure.Seeding;
using VoteWave.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddShared();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews();

// add JwtOptions configuring
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseShared();
app.UseInfrastructureMiddlewares();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

await app.SeedAsync();
app.Run();
