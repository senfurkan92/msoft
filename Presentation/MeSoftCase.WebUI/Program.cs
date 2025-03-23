using AspNetCoreRateLimit;
using FluentValidation;
using FluentValidation.AspNetCore;
using MeSoftCase.Application.Services;
using MeSoftCase.Domain.Entities;
using MeSoftCase.Infrastructure.Persistance.Context;
using MeSoftCase.Infrastructure.Services;
using MeSoftCase.WebUI.Config;
using MeSoftCase.WebUI.Middlewares;
using MeSoftCase.WebUI.Models.AppUserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<SignUpModelValidator>();

builder.Configuration.AddJsonFile($"ipratelimiting.json", false, true);

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureCustomValidationModel();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"), config =>
    {
        config.MigrationsAssembly("MeSoftCase.Infrastructure");
    });
});

builder.Services.AddIdentity<AppUser, AppRole>(actions =>
{
    actions.Password.RequiredLength = 8;
    actions.Password.RequireDigit = true;
    actions.Password.RequireUppercase = true;
    actions.Password.RequireLowercase = true;
    actions.Password.RequireNonAlphanumeric = false;
    actions.Password.RequiredUniqueChars = 0;

    actions.User.RequireUniqueEmail = true;

    // Protect login from several requests (Block requests for a while)
    actions.Lockout.MaxFailedAccessAttempts = 5;
    actions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecurityKey"]!))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["MeSoftToken"];
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            context.Response.Cookies.Delete("MeSoftToken");
            context.HandleResponse();
            context.Response.Redirect("/Account");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            context.Response.Cookies.Delete("MeSoftToken");
            context.Response.Redirect("/Account");
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();

app.UseMiddleware<BlackListMiddleware>();
app.UseMiddleware<CustomExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ApiContextMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    areaName: "Api",
    name: "Api",
    pattern: "api/{controller=Home}/{action=Index}/{id?}");

app.Run();
