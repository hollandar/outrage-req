using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Require.Data;
using Require.Shared;
using RequireApi.Models.Responses;
using RequireApi.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var oidcOptions = new OidcOptions();
builder.Configuration.GetSection("Oidc").Bind(oidcOptions);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // base-address of your identityserver
                options.Authority = oidcOptions.Authority;
                options.Audience = oidcOptions.Audience;

                // if you are using API resources, you can specify the name here
                options.IncludeErrorDetails = true;
            });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .RequireClaim("scope", "require_services")
        .Build();
});

builder.Services.AddDbContext<RequireDbContext>(dbOptions =>
{
    dbOptions.UseNpgsql("", npgOptions =>
    {
        npgOptions.MigrationsAssembly("Require.Data");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/api/ping", () =>
{
    return Results.Ok(new ApiResult<PingResponse>(new PingResponse()));
}).RequireAuthorization();

app.MapHealthChecks("/api/health");

app.Run();
