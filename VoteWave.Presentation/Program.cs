using VoteWave.Application;
using VoteWave.Infrastructure;
using VoteWave.Infrastructure.Auth.Options;
using VoteWave.Infrastructure.Seeding;
using VoteWave.Presentation.Services.IServices;
using VoteWave.Presentation.Services;
using VoteWave.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddShared();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews();

// add JwtOptions configuring
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

// adding authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

//app.UseShared();
app.UseInfrastructureMiddlewares();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"),
    appBuilder =>
    {
        appBuilder.UseShared(); // for Exception Middleware
    });


await app.SeedAsync();
app.Run();
