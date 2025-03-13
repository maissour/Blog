using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Data;
using Repository.Models;
using Services.Application;
using Services.Interfaces;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(connectionString, b => b.MigrationsAssembly("Repository"));
});

// Configure Identity with Cookie Authentication
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax; // Allow cookies to be sent in cross-origin requests from your frontend
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use HTTPS
    options.LoginPath = "/api/Auth/login"; // API endpoint for login
    options.LogoutPath = "/api/Auth/logout"; // API endpoint for logout
    options.AccessDeniedPath = "/api/Auth/forbidden"; // Endpoint when access is denied
    options.SlidingExpiration = true; // Refresh cookie expiration on each request
});

// Enable authentication and authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization();

builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<HomeRepository>();

var allowSpecificOrigins = "allowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Replace with your Vue.js frontend URL
                  .AllowCredentials() // Important for cookies!
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    using (var context = new ApplicationDbContext(service.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
    {
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "SuperAdmin", "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    ;
        if (context.Users.IsNullOrEmpty())
        {
            var userManager = service.GetRequiredService<UserManager<User>>();
            string firstName = "Ahcene"; // its just for development
            string email = "ahcen@gmail.com";
            string password = "Ahcen.123";
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new User
                {
                    FirstName = firstName,
                    UserName = email,
                    Email = email
                };
                try
                {
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "SuperAdmin");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating user: {ex.Message}");
                }
            }
        }
    }
};
app.UseHttpsRedirection();
//app.MapIdentityApi<User>();
app.UseRouting();
app.UseAuthentication();
app.UseCors(allowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
